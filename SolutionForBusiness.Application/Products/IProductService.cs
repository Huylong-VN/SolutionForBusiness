using SolutionForBusiness.Application.Common;
using SolutionForBusiness.ViewModels.Common;
using SolutionForBusiness.ViewModels.Products;
using System.Threading.Tasks;

namespace SolutionForBusiness.Application.Products
{
    public interface IProductService
    {
        Task<PagedResult<ProductVM>> GetProductPaging(GetProductPagingRequest request);

        Task<ApiResult<bool>> Create(ProductCreateRequest request);

        Task<ApiResult<bool>> Update(ProductUpdateRequest request);

        Task<ApiResult<bool>> Delete(int Id);

        Task<ProductVM> GetById(int Id);
    }
}