using Ebaysharp.Entities;
using System.Threading.Tasks;

namespace Ebaysharp.Services.Taxonomy
{
    public class CategoryService : RequestService
    {
        public CategoryService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }
        public virtual async Task<CategoryTree> GetCategoryTreeRootAsync()
        {
            await CreateAuthorizedRequestAsync($"{TaxonomyApiUrls.getDefaultCategoryTreeIdUrl}?marketplace_id={UsEbayMarketPlaceId}", RestSharp.Method.GET);
            return await ExecuteRequestAsync<CategoryTree>();
        }

        public virtual async Task<CategoryTree> GetCategoryTreeNodeAsync(CategoryTree categoryTree)
        {
            await CreateAuthorizedRequestAsync($"{TaxonomyApiUrls.CategoryTreeUrl}/{categoryTree.categoryTreeId}", RestSharp.Method.GET);
            return await ExecuteRequestAsync<CategoryTree>();
        }

        public virtual async Task<ItemAspectsForCategory> GetItemAspectsForCategoryAsync(string categoryTreeId, string categoryId)
        {
            await CreateAuthorizedRequestAsync($"{TaxonomyApiUrls.CategoryTreeUrl}/{categoryTreeId}{TaxonomyApiUrls.GetItemAspectsForCategory}?category_id={categoryId}", RestSharp.Method.GET);
            return await ExecuteRequestAsync<ItemAspectsForCategory>();
        }
    }
}
