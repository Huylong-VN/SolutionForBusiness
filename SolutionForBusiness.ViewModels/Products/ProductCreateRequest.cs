using Microsoft.AspNetCore.Http;
using System;

namespace SolutionForBusiness.ViewModels.Products
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}