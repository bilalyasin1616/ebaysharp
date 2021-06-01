using Ebaysharp.Entities;

namespace Ebaysharp.Services.Taxonomy
{
    public class CategoryService : RequestService
    {
        public CategoryService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }
        public CategoryTree GetCategoryTreeRoot()
        {
            CreateAuthorizedRequest($"{TaxonomyApiUrls.getDefaultCategoryTreeIdUrl}?marketplace_id={UsEbayMarketPlaceId}", RestSharp.Method.GET);
            return ExecuteRequest<CategoryTree>();
        }

        public CategoryTree GetCategoryTreeNode(CategoryTree categoryTree)
        {
            CreateAuthorizedRequest($"{TaxonomyApiUrls.CategoryTreeUrl}/{categoryTree.categoryTreeId}", RestSharp.Method.GET);
            return ExecuteRequest<CategoryTree>();
        }

        public ItemAspectsForCategory GetItemAspectsForCategory(string categoryTreeId, string categoryId)
        {
            CreateAuthorizedRequest($"{TaxonomyApiUrls.CategoryTreeUrl}/{categoryTreeId}{TaxonomyApiUrls.GetItemAspectsForCategory}?category_id={categoryId}", RestSharp.Method.GET);
            return ExecuteRequest<ItemAspectsForCategory>();
        }
    }
}
