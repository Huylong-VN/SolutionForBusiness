using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SolutionForBusiness.Application.Common;
using SolutionForBusiness.Data.EF;
using SolutionForBusiness.Data.Entities;
using SolutionForBusiness.ViewModels.Common;
using SolutionForBusiness.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SolutionForBusiness.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly SolutionDbContext _context;
        private readonly IStorageService _storageService;

        public ProductService(SolutionDbContext context, IStorageService storageService)
        {
            _storageService = storageService;
            _context = context;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ApiResult<bool>> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Image = await SaveFile(request.Image),
                CategoryId = request.CategoryId,
                DateCreated = DateTime.Now
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Delete(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            if (product == null) return new ApiErrorResult<bool>("Không tìm thấy sản phẩm");
            if (_storageService.GetFileUrl(product.Image) == null) await _storageService.DeleteFileAsync(product.Image);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<PagedResult<ProductVM>> GetProductPaging(GetProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        select new { p, c };
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.Name.Contains(request.Keyword));
            }
            if (request.categoryId != null && request.categoryId != 0)
            {
                query = query.Where(x => x.c.Id == request.categoryId);
            }

            //Total row
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).OrderBy(x => x.p.Id).Select(x => new ProductVM()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                Description = x.p.Description,
                Price = x.p.Price,
                DateCreated = x.p.DateCreated,
                Image = x.p.Image,
                CategoryName = x.c.Name
            }).ToListAsync();
            var pagedResult = new PagedResult<ProductVM>()
            {
                Items = data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ToTalRecords = totalRow
            };
            return pagedResult;
        }

        public async Task<ApiResult<bool>> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null) new ApiErrorResult<bool>("Không tìm tháy sản phẩm");
            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            product.CategoryId = request.CategoryId;
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ProductVM> GetById(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            var productVM = new ProductVM()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                DateCreated = product.DateCreated,
                Image = product.Image,
                CategoryName = product.Category.Name
            };
            return productVM;
        }
    }
}