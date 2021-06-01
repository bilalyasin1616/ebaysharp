using Ebaysharp.Entities;

namespace Ebaysharp.Services.Inventory
{
    public class OfferService : RequestService
    {
        public OfferService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public OfferBase Get(string offerId)
        {
            CreateAuthorizedRequest($"{InventoryApiUrls.Offer}/{offerId}", RestSharp.Method.GET);
            return ExecuteRequest<OfferBase>();
        }

        public EbayList<Offer, EbayFilter> GetAll(EbayFilter ebayFilter, string sku)
        {
            CreateAuthorizedPagedRequest(ebayFilter, InventoryApiUrls.Offer, RestSharp.Method.GET);
            Request.AddQueryParameter("sku", sku);
            var response = ExecuteRequest<OffersResponse>();
            ebayFilter.NextPage = response.next;
            return new EbayList<Offer, EbayFilter>(ebayFilter, response.offers);
        }

        public OfferResponse Add(Offer offer)
        {
            CreateRequestWithTokenAndContentLanguage(InventoryApiUrls.Offer, RestSharp.Method.POST);
            Request.AddJsonBody(offer);
            return ExecuteRequest<OfferResponse>();
        }

        public void Update(OfferBase offer, string offerId)
        {
            CreateRequestWithTokenAndContentLanguage($"{InventoryApiUrls.Offer}/{offerId}", RestSharp.Method.PUT);
            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(offer);
            Request.AddParameter("application/json", jsonBody, RestSharp.ParameterType.RequestBody);
            ExecuteRequest();
        }

        public Responses AddBulk(BulkOffer bulkOffer)
        {
            CreateAuthorizedRequest(InventoryApiUrls.BulkCreateOffer, RestSharp.Method.POST);
            Request.AddHeader("content-type", "application/json");
            Request.AddJsonBody(bulkOffer);
            var response = RequestClient.Execute<Responses>(Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode==System.Net.HttpStatusCode.MultiStatus)
                return response.Data;
            else if(response.StatusCode==System.Net.HttpStatusCode.BadRequest)
            {
                if (response.Data != null)
                    response.Data.responses.ForEach(offerResponse =>
                    {
                        if (offerResponse.errors != null)
                            offerResponse.errors.ForEach(err =>
                            {
                                if(err.errorId==(int)EbayResponseCodes.Offer_Exits)
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

        public Responses PublishBulk(BulkOfferPublish bulkOfferPublish)
        {
            CreateAuthorizedRequest(InventoryApiUrls.BulkPublishOffer, RestSharp.Method.POST);
            Request.AddHeader("content-type", "application/json");
            Request.AddJsonBody(bulkOfferPublish);
            var response = RequestClient.Execute<Responses>(Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK ||
                response.StatusCode == System.Net.HttpStatusCode.MultiStatus)
                return response.Data;
            else
                throw new EbayException("Ebay Api didn't respond with Okay, see exception for more details", response);
        }

        public OfferPublishByInventoryGroupResponse PublishByInventoryItemGroup(string inventoryItemGroupKey)
        {
            CreateAuthorizedRequest($"{InventoryApiUrls.Offer}/publish_by_inventory_item_group", RestSharp.Method.POST);
            Request.AddHeader("content-type", "application/json");
            Request.AddJsonBody(new PublishOffersByInventoryGroup() { inventoryItemGroupKey = inventoryItemGroupKey, marketplaceId = UsEbayMarketPlaceId });
            var response = RequestClient.Execute<OfferPublishByInventoryGroupResponse>(Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response.Data != null)
                    response.Data.responseCode = (int)response.StatusCode;
                return response.Data;
            }
            else
            {
                //2nd try incase api throws bad request, no Sku are available for publish
                response = RequestClient.Execute<OfferPublishByInventoryGroupResponse>(Request);
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

        public void WithdrawByInventoryItemGroup(string inventoryItemGroupKey)
        {
            CreateAuthorizedRequestJson($"{InventoryApiUrls.Offer}/publish_by_inventory_item_group", RestSharp.Method.POST);
            Request.AddJsonBody(new PublishOffersByInventoryGroup() { inventoryItemGroupKey = inventoryItemGroupKey, marketplaceId = UsEbayMarketPlaceId });
            ExecuteRequest<WithdrawByInventoryGroupResponse>();
        }

        public void Delete(string offerId)
        {
            CreateAuthorizedRequest($"{InventoryApiUrls.Offer}/{offerId}", RestSharp.Method.DELETE);
            ExecuteRequest();
        }


    }
}
