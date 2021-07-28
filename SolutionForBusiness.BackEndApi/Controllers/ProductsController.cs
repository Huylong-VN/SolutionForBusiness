using Microsoft.AspNetCore.Mvc;
using SolutionForBusiness.Application.Products;
using SolutionForBusiness.ViewModels.Products;
using System.Threading.Tasks;

namespace SolutionForBusiness.BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetProductPagingRequest()
            {
                Keyword = keyword,
                categoryId = categoryId,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var product = await _productService.GetProductPaging(request);
            return Ok(product);
        }
    }
}