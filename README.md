# Ebaysharp .Net Library for Ebay API

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

## Creating ClientToken object

ClientToken object is used to initilize all services, so make sure all the information in this are correct.
Otherwise call to the API will not work.

```cs
var clientToken = new ClientToken()
{
    clientId = "Your clientId",
    clientSecret = "Your clientSecret",
    devId = "Your devId",
    oauthCredentials = "Your clientId:clientSecret base64 encoded",
    ruName = "Your ruName from developer portal",
    scopes = "Space seperated scopes from Ebay API"
};
```
**oauthCredentials** is clientId:clientSecret base64 encoded string. Will refactore and remove this requirement soon as it can be done internally.
**ruName** is unique identifier for your redirect url, that can be found in your developer portal
**scopes** string of scopes seperated by a space each. [more on oauth scopes](https://developer.ebay.com/api-docs/static/oauth-scopes.html)

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
**Note** I know this is not the best way to do this, but at the time it served me the purpose. Will try to refactor the code soon to make it better and intuative

# Resources

-   [Fulfillment Policy Service](#fulfillment-policy-service)
-   [Payment Policy Service](#payment-policy-service)
-   [Return Policy Service](#return-policy-service)
-   [Identity Service](#identity-service)
-   [Order Service](#order-service)
-   [Inventory Item Group Service](#inventory-item-group-service)
-   [Inventory Item Service](#inventory-item-service)
-   [Listing Service](#listing-service)
-   [Location Service](#location-service)
-   [Catalog Service](#category-service)
-   [Ebay Meta Service](#Ebay-meta-service)
-   [Offer Service](#offer-service)

## <a name="fulfillment-policy-service"></a>Fulfillment Policy Service

### Get all fulfillment policies

```cs
var limit = 10; //getting 10 records at a time
var fulfilmentService = new FulfillmentPolicyService(clientToken, accessToken);
EbayList<Order, EbayFilter> fulfilmentPolicies = null;
do
{
    var filter = fulfilmentPolicies != null ? fulfilmentPolicies.GetNextPageFilter() : new EbayFilter(limit);
    fulfilmentPolicies = await fulfilmentPolicies.GetAllAsync(filter);
} while (fulfilmentPolicies.HasNextPage());
```

### Add fulfilment policy

```cs
var fulfilmentPolicy = new FulfillmentPolicy();
var fulfilmentService = new FulfillmentPolicyService(clientToken, accessToken);
fulfilmentPolicy = await fulfilmentService.AddAsync(fulfilmentPolicy);
```

### Update fulfilment policy

```cs
var fulfilmentPolicy = new FulfillmentPolicy();
var fulfilmentService = new FulfillmentPolicyService(clientToken, accessToken);
fulfilmentPolicy = await fulfilmentService.UpdateAsync(fulfilmentPolicy);
```

### Delete fulfilment policy

```cs
var fulfilmentPolicyId = 123456;
var fulfilmentService = new FulfillmentPolicyService(clientToken, accessToken);
await fulfilmentService.DeleteAsync(fulfilmentPolicyId);
```