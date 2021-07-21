# Ebaysharp .Net Library for Ebay API

Goal of this library is to make life easy for developers who are looking to use Ebay's API for development. This library contains models and services that will wrap the communication logic to make your life easy.

**Note:** Ebay API tends to be quite unstable, lot of services, difficult to implement a flow as a 3rd party integrator and very questionable design decisions for Ebay's team. I have tried my best to cover the commerce part here. There is huge room for improvement and still a number of uncovered routes. Suggestions or contributions are always welcome.

## Contributors

If anyone would like to contribute to this package, email me at [bilalyasin1616@gmail.com](mailto:bilalyasin1616@gmail.com).
I would love to get some help so we can increase the API coverage and make it great package.

# Installation

Ebaysharp is [available on NuGet](https://www.nuget.org/packages/Ebaysharp/). Use the package manager
console in Visual Studio to install it:

```pwsh
Install-Package Ebaysharp
```

If you're using .NET Core, you can use the `dotnet` command from your favorite shell:

```sh
dotnet add package Ebaysharp
```

If you're using Paket with an F# project, use this command:

```sh
paket add Ebaysharp --project /path/to/project.fsproj
```

# Topics

## Environments

By default the environment is set to Production but you can change it for testing to sandbox
**Import note:** It is recommended to use production environment even for testing, Ebay's sandbox is absolutely crap, very unstable and useless. Pretty much most of the calls will fail without any reason of its sandbox and there are a lot of issues created on Ebay forums regarding that.

### Possible Environments

```cs
public enum Environments
{
    Sandbox, Production
}
```

### Setting environment

You need to setup the environment once in your project before calling the services

```cs
EnvironmentManager.Environment = Environments.Sandbox;
```

## Creating ClientToken object

ClientToken object is used to initialize all services, so make sure all the information in this are correct.
Otherwise call to the API will not work.

```cs
var clientToken = new ClientToken()
{
    clientId = "Your clientId",
    clientSecret = "Your clientSecret",
    devId = "Your devId",
    oauthCredentials = "Your clientId:clientSecret base64 encoded",
    ruName = "Your ruName from developer portal",
    scopes = "Space separated scopes from Ebay API"
};
```
**oauthCredentials** is clientId:clientSecret base64 encoded string. Will refactored and remove this requirement soon as it can be done internally.
**ruName** is unique identifier for your redirect url, that can be found in your developer portal
**scopes** string of scopes separated by a space each. [more on oauth scopes](https://developer.ebay.com/api-docs/static/oauth-scopes.html)

## Getting access tokens

### Generating redirect url

```cs
var state="Your application/user state encoded url safe";
var redirectUrl = new OauthService(clientToken).GetRedirectUrl(state);
```

Redirect user to this url for getting access to their account with specified scopes

### Exchange temporary token for access tokens

```cs
var authService = new OauthService(clientToken);
var token = await authService.GetAccessTokenAsync(code);
```

You can store these access tokens in your database and later use them to access the resources specified in the scopes

## Refreshing the access token

Access tokens provided by Ebay Api will expire after the time specified in **AccessToken.expires_in** property, but it can be refreshed by using the long term refresh token provided in **AccessToken.refresh_token** property.
Ebaysharp implements the token refresh internally given that the refresh_token is valid. After performing certain operation(s) you might expect that the token is refreshed by the respective service and would like to update it in your database. You can use the following code snippet.

```cs
var accessToken = GetAccessToken(); //assuming this function returns AccessToken object with valid tokens
var service = new Service(clientToken, accessToken);
//after performing operation(s)
if(service.IsAccessTokenRefreshed(accessToken.date_last_updated))
    service.AccessToken; //you can use this access token object to update tokens in your store
```

## Sample for getting a list of all objects from a resource while managing pagination

```cs
var limit = 10; //getting 10 records at a time
var orderService = new OrderService(clientToken, accessToken);
EbayList<Order, EbayFilter> orders = null;
do
{
    var filter = orders != null ? orders.GetNextPageFilter() : new EbayFilter(limit);
    orders = await orderService.GetAllAsync(filter);
} while (orders.HasNextPage());
```

You can set the limit to how many records you want in each call.
**Note:** I know this is not the best way to do this, but at the time it served me the purpose. Will try to refactor the code soon to make it better and intuitive

# Resources

-   [Fulfillment Policy Service](#fulfillment-policy-service)
-   [Payment Policy Service](#payment-policy-service)
-   [Return Policy Service](#return-policy-service)
-   [Privileges Service](#privilages-service)
-   [Identity Service](#identity-service)
-   [Order Service](#order-service)
-   [Inventory Item Group Service](#inventory-item-group-service)
-   [Inventory Item Service](#inventory-item-service)
-   [Location Service](#location-service)
-   [Catalog Service](#category-service)
-   [Ebay Meta Service](#Ebay-meta-service)
-   [Offer Service](#offer-service)
-   [Meta Service](#meta-service)

## <a name="fulfillment-policy-service"></a>Fulfillment Policy Service

### Get all fulfillment policies

```cs
var limit = 10; //getting 10 records at a time
var fulfillmentService = new FulfillmentPolicyService(clientToken, accessToken);
EbayList<Order, EbayFilter> fulfillmentPolicies = null;
do
{
    var filter = fulfillmentPolicies != null ? fulfillmentPolicies.GetNextPageFilter() : new EbayFilter(limit);
    fulfillmentPolicies = await fulfillmentPolicies.GetAllAsync(filter);
} while (fulfillmentPolicies.HasNextPage());
```

### Add fulfillment policy

```cs
var fulfillmentPolicy = new FulfillmentPolicy();
var fulfillmentService = new FulfillmentPolicyService(clientToken, accessToken);
fulfillmentPolicy = await fulfillmentService.AddAsync(fulfillmentPolicy);
```

### Update fulfillment policy

```cs
var fulfillmentPolicy = new FulfillmentPolicy();
var fulfillmentService = new FulfillmentPolicyService(clientToken, accessToken);
fulfillmentPolicy = await fulfillmentService.UpdateAsync(fulfillmentPolicy);
```

### Delete fulfillment policy

```cs
var fulfillmentPolicyId = 123456;
var fulfillmentService = new FulfillmentPolicyService(clientToken, accessToken);
await fulfillmentService.DeleteAsync(fulfillmentPolicyId);
```

## <a name="payment-policy-service"></a>Payment Policy Service

### Get all payment policies

```cs
var limit = 10; //getting 10 records at a time
var paymentService = new PaymentPolicyService(clientToken, accessToken);
EbayList<Order, EbayFilter> paymentPolicies = null;
do
{
    var filter = paymentPolicies != null ? paymentPolicies.GetNextPageFilter() : new EbayFilter(limit);
    paymentPolicies = await paymentPolicies.GetAllAsync(filter);
} while (paymentPolicies.HasNextPage());
```

### Add payment policy

```cs
var paymentPolicy = new PaymentPolicy();
var paymentService = new PaymentPolicyService(clientToken, accessToken);
paymentPolicy = await paymentService.AddAsync(paymentPolicy);
```

### Update payment policy

```cs
var paymentPolicy = new PaymentPolicy();
var paymentService = new PaymentPolicyService(clientToken, accessToken);
paymentPolicy = await paymentService.UpdateAsync(paymentPolicy);
```

### Delete payment policy

```cs
var paymentPolicyId = 123456;
var paymentService = new PaymentPolicyService(clientToken, accessToken);
await paymentService.DeleteAsync(paymentPolicyId);
```

## <a name="return-policy-service"></a>Return Policy Service

### Get all return policies

```cs
var limit = 10; //getting 10 records at a time
var returnService = new returnPolicyService(clientToken, accessToken);
EbayList<Order, EbayFilter> returnPolicies = null;
do
{
    var filter = returnPolicies != null ? returnPolicies.GetNextPageFilter() : new EbayFilter(limit);
    returnPolicies = await returnPolicies.GetAllAsync(filter);
} while (returnPolicies.HasNextPage());
```

### Add return policy

```cs
var returnPolicy = new returnPolicy();
var returnService = new ReturnPolicyService(clientToken, accessToken);
returnPolicy = await returnService.AddAsync(returnPolicy);
```

### Update return policy

```cs
var returnPolicy = new returnPolicy();
var returnService = new ReturnPolicyService(clientToken, accessToken);
returnPolicy = await returnService.UpdateAsync(returnPolicy);
```

### Delete return policy

```cs
var returnPolicyId = 123456;
var returnService = new ReturnPolicyService(clientToken, accessToken);
await returnService.DeleteAsync(returnPolicyId);
```

## <a name="privileges-service"></a>Privileges Service

### Get user privileges

```cs
var privilegesService = new PrivilegesService(clientToken, accessToken);
var privileges = await privilegesService.GetPrivilegeAsync();
```

## <a name="identity-service"></a>Identity Service

### Get user

```cs
var identityService = new IdentityService(clientToken, accessToken);
var user = await identityService.GetUserAsync();
```

## <a name="order-service"></a>Order Service

### Get all orders

```cs
var limit = 10; //getting 10 records at a time
var orderService = new OrderService(clientToken, accessToken);
EbayList<Order, EbayFilter> orders = null;
do
{
    var filter = orders != null ? orders.GetNextPageFilter() : new EbayFilter(limit);
    orders = await orderService.GetAllAsync(filter);
} while (orders.HasNextPage());
```

### Create order fulfillment

```cs
var orderId = "12121";
var shippingFulfillment = new ShippingFulfillment();
var orderService = new OrderService(clientToken, accessToken);
var fulfillmentId = await orderService.CreateFulfillmentAsync(shippingFulfillment, orderId);
```

## <a name="inventory-item-group-service"></a>Inventory Item Group Service

### Create or replace inventory item group

```cs
var inventoryItemGroupKey = "test-group";
var inventoryItemGroup = new InventoryItemGroup();
var inventoryItemGroupService = new InventoryItemGroupService(clientToken, accessToken);
await inventoryItemGroupService.CreateOrReplaceInventoryItemGroupAsync(inventoryItemGroup, inventoryItemGroupKey);
```

### Get inventory item group

```cs
var inventoryItemGroupKey = "test-group";
var inventoryItemGroupService = new InventoryItemGroupService(clientToken, accessToken);
var inventoryItemGroup = await inventoryItemGroupService.GetInventoryItemsGroupAsync(inventoryItemGroupKey);
```

### Delete inventory item group

```cs
var inventoryItemGroupKey = "test-group";
var inventoryItemGroupService = new InventoryItemGroupService(clientToken, accessToken);
await inventoryItemGroupService.DeleteGroupInventoryItemsAsync(inventoryItemGroupKey);
```

## <a name="inventory-item-service"></a>Inventory Item Service

### Create or replace inventory item

```cs
var sku = "test-sku";
var inventoryItem = new InventoryItem();
var inventoryItemService = new InventoryItemService(clientToken, accessToken);
await inventoryItemService.CreateOrReplaceAsync(inventoryItem, sku);
```

### Bulk create or replace inventory items

```cs
var bulkInventoryItem = new BulkInventoryItem();
var inventoryItemService = new InventoryItemService(clientToken, accessToken);
var bulkInventoryItemResponses = await inventoryItemService.BulkCreateOrReplaceAsync(bulkInventoryItem);
```

### Get inventory item

```cs
var sku = "test-sku";
var inventoryItemService = new InventoryItemService(clientToken, accessToken);
var inventoryItem = await inventoryItemService.GetAsync(sku);
```

### Get all inventory items

```cs
var limit = 10; //getting 10 records at a time
var inventoryItemService = new InventoryItemService(clientToken, accessToken);
EbayList<InventoryItem, EbayFilter> inventoryItems = null;
do
{
    var filter = inventoryItems != null ? inventoryItems.GetNextPageFilter() : new EbayFilter(limit);
    inventoryItems = await inventoryItemService.GetAllAsync(filter);
} while (inventoryItems.HasNextPage());
```

### Delete inventory item

```cs
var sku = "test-sku";
var inventoryItemService = new InventoryItemService(clientToken, accessToken);
await inventoryItemService.DeleteAsync(sku);
```

## <a name="location-service"></a>Location Service

### Add location

```cs
var merchantLocationKey = "unique-location-identifier";
var inventoryLocation = new InventoryLocation();
var locationService = new LocationService(clientToken, accessToken);
await locationService.AddAsync(merchantLocationKey, inventoryLocation);
```

### Delete location

```cs
var merchantLocationKey = "unique-location-identifier";
var locationService = new LocationService(clientToken, accessToken);
await locationService.DeleteAsync(merchantLocationKey);
```

### Get all locations

```cs
var limit = 10; //getting 10 records at a time
var locationService = new LocationService(clientToken, accessToken);
EbayList<InventoryLocation, EbayFilter> locations = null;
do
{
    var filter = locations != null ? locations.GetNextPageFilter() : new EbayFilter(limit);
    locations = await locationService.GetAllAsync(filter);
} while (locations.HasNextPage());
```

### Get location

```cs
var merchantLocationKey = "unique-location-identifier";
var locationService = new LocationService(clientToken, accessToken);
var inventoryLocation = await locationService.GetAsync(merchantLocationKey);
```

## <a name="offer-service"></a>Offer Service

### Add offer

```cs
var offer = new Offer();
var offerService = new OfferService(clientToken, accessToken);
var offerResponse = await offerService.AddAsync(offer);
```

### Add offer bulk

```cs
var bulkOffers = new BulkOffer();
var offerService = new OfferService(clientToken, accessToken);
var offersResponse = await offerService.AddBulkAsync(bulkOffers);
```

### Delete offer

```cs
var offerId = "offer id";
var offerService = new OfferService(clientToken, accessToken);
await offerService.DeleteAsync(offerId);
```

### Get offer

```cs
var offerId = "offer id";
var offerService = new OfferService(clientToken, accessToken);
var offer = await offerService.GetAsync(offerId);
```

### Get all offer

```cs
var limit = 10; //getting 10 records at a time
var offerService = new OfferService(clientToken, accessToken);
EbayList<InventoryLocation, EbayFilter> offers = null;
do
{
    var filter = offers != null ? offers.GetNextPageFilter() : new EbayFilter(limit);
    offers = await offerService.GetAllAsync(filter);
} while (offers.HasNextPage());
```

### Publish inventory items group

```cs
var inventoryItemGroupKey = "inventory item group key";
var offerService = new OfferService(clientToken, accessToken);
var offerPublishByInventoryGroupResponse = await offerService.PublishByInventoryItemGroupAsync(inventoryItemGroupKey);
```

### Publish bulk offers

```cs
var bulkOfferPublish = new BulkOfferPublish();
var offerService = new OfferService(clientToken, accessToken);
var bulkPublishResponse = await offerService.PublishBulkAsync(bulkOfferPublish);
```

### Update offer

```cs
var offer = new Offer();
var offerService = new OfferService(clientToken, accessToken);
await offerService.UpdateAsync(offer);
```

### Withdraw offers by inventory item group

```cs
var inventoryItemGroupKey = "inventory item group key";
var offerService = new OfferService(clientToken, accessToken);
await offerService.WithdrawByInventoryItemGroupAsync(inventoryItemGroupKey);
```

## <a name="meta-service"></a>Meta Service

```cs
var detailName = "detail name";
var ebayMetaService = new EbayMetaService(clientToken, accessToken);
var geteBayDetailsResponse = await ebayMetaService.GetEbayDetailsAsync(detailName);
```