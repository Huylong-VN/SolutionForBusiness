using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolutionForBusiness.Application.Products;
using SolutionForBusiness.Application.Users;
using SolutionForBusiness.ViewModels.Products;
using System;
using System.Threading.Tasks;

namespace SolutionForBusiness.BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public readonly IUserService _userService;

        public ProductsController(IProductService productService, IUserService userService)
        {
            _userService = userService;
            _productService = productService;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 6)
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

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            var usercheck = await _userService.getRoleUser(request.userId);
            if (usercheck.IsSuccessed)
            {
                var result = await _productService.Create(request);
                if (result.IsSuccessed) return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid userId, int Id)
        {
            var usercheck = await _userService.getRoleUser(userId);
            if (usercheck.IsSuccessed)
            {
                var result = await _productService.Delete(Id);
                if (result.IsSuccessed) return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            var usercheck = await _userService.getRoleUser(request.userId);
            if (usercheck.IsSuccessed)
            {
                var result = await _productService.Update(request);
                if (result.IsSuccessed) return Ok(result);
            }
            return BadRequest();
        }
    }
}