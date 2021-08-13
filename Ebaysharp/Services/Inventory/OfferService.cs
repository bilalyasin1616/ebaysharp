using Ebaysharp.Entities;
using System.Threading.Tasks;

namespace Ebaysharp.Services.Inventory
{
    public class OfferService : RequestService
    {
        public OfferService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public virtual async Task<OfferResponse> AddAsync(Offer offer)
        {
            await CreateRequestWithTokenAndContentLanguageAsync(InventoryApiUrls.Offer, RestSharp.Method.POST);
            Request.AddJsonBody(offer);
            return await ExecuteRequestAsync<OfferResponse>();
        }

        public virtual async Task<Responses> AddBulkAsync(BulkOffer bulkOffer)
        {
            await CreateAuthorizedRequestAsync(InventoryApiUrls.BulkCreateOffer, RestSharp.Method.POST);
            Request.AddHeader("content-type", "application/json");
            Request.AddJsonBody(bulkOffer);
            var response = await RequestClient.ExecuteAsync<Responses>(Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.MultiStatus)
                return response.Data;
            //Ebay Api weirdly throws bad request for dew offers when bulk request is sent, but on retry it will add the failed ones successfully
            //But during the 2nd retry those offers which have been created will throw an error this piece of code will get their offer id and add it to response
            //This is a temporary fix
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                if (response.Data != null)
                    response.Data.responses.ForEach(offerResponse =>
                    {
                        if (offerResponse.errors != null)
                            offerResponse.errors.ForEach(err =>
                            {
                                if (err.errorId == (int)EbayResponseCodes.Offer_Exits)
                                {
                                    var parameter = err.parameters.Find(param => param.name == "offerId");
                                    if (parameter != null)
                                    {
                                        offerResponse.offerId = parameter.value;
                                        offerResponse.statusCode = (int)EbayResponseCodes.AlreadyExists;
                                    }
                                }
                            });
                    });
                return response.Data;
            }
            else
                throw new EbayException("Ebay Api didn't respond with Okay, see exception for more details", response);
        }

        public virtual async Task DeleteAsync(string offerId)
        {
            await CreateAuthorizedRequestAsync($"{InventoryApiUrls.Offer}/{offerId}", RestSharp.Method.DELETE);
            await ExecuteRequestAsync();
        }

        public virtual async Task<OfferBase> GetAsync(string offerId)
        {
            await CreateAuthorizedRequestAsync($"{InventoryApiUrls.Offer}/{offerId}", RestSharp.Method.GET);
            return await ExecuteRequestAsync<OfferBase>();
        }

        public virtual async Task<EbayList<Offer, EbayFilter>> GetAllAsync(EbayFilter ebayFilter, string sku)
        {
            await CreateAuthorizedPagedRequestAsync(ebayFilter, InventoryApiUrls.Offer, RestSharp.Method.GET);
            Request.AddQueryParameter("sku", sku);
            var response = await ExecuteRequestAsync<OffersResponse>();
            ebayFilter.NextPage = response.next;
            return new EbayList<Offer, EbayFilter>(ebayFilter, response.offers);
        }

        public virtual async Task<OfferPublishByInventoryGroupResponse> PublishByInventoryItemGroupAsync(string inventoryItemGroupKey)
        {
            await CreateAuthorizedRequestAsync($"{InventoryApiUrls.Offer}/publish_by_inventory_item_group", RestSharp.Method.POST);
            Request.AddHeader("content-type", "application/json");
            Request.AddJsonBody(new PublishOffersByInventoryGroup() { inventoryItemGroupKey = inventoryItemGroupKey, marketplaceId = UsEbayMarketPlaceId });
            var response = await RequestClient.ExecuteAsync<OfferPublishByInventoryGroupResponse>(Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response.Data != null)
                    response.Data.responseCode = (int)response.StatusCode;
                return response.Data;
            }
            else
            {
                //2nd try incase api throws bad request, no Sku are available for publish
                response = await RequestClient.ExecuteAsync<OfferPublishByInventoryGroupResponse>(Request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (response.Data != null)
                        response.Data.responseCode = (int)response.StatusCode;
                    return response.Data;
                }
                else
                    throw new EbayException("Ebay Api didn't respond with Okay, see exception for more details", response);
            }
        }

        public virtual async Task<Responses> PublishBulkAsync(BulkOfferPublish bulkOfferPublish)
        {
            await CreateAuthorizedRequestAsync(InventoryApiUrls.BulkPublishOffer, RestSharp.Method.POST);
            Request.AddHeader("content-type", "application/json");
            Request.AddJsonBody(bulkOfferPublish);
            var response = await RequestClient.ExecuteAsync<Responses>(Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK ||
                response.StatusCode == System.Net.HttpStatusCode.MultiStatus)
                return response.Data;
            else
                throw new EbayException("Ebay Api didn't respond with Okay, see exception for more details", response);
        }

        public virtual async Task UpdateAsync(OfferBase offer, string offerId)
        {
            await CreateRequestWithTokenAndContentLanguageAsync($"{InventoryApiUrls.Offer}/{offerId}", RestSharp.Method.PUT);
            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(offer);
            Request.AddParameter("application/json", jsonBody, RestSharp.ParameterType.RequestBody);
            await ExecuteRequestAsync();
        }

        public virtual async Task WithdrawByInventoryItemGroupAsync(string inventoryItemGroupKey)
        {
            await CreateAuthorizedRequestJsonAsync($"{InventoryApiUrls.Offer}/publish_by_inventory_item_group", RestSharp.Method.POST);
            Request.AddJsonBody(new PublishOffersByInventoryGroup() { inventoryItemGroupKey = inventoryItemGroupKey, marketplaceId = UsEbayMarketPlaceId });
            await ExecuteRequestAsync<WithdrawByInventoryGroupResponse>();
        }


    }
}
