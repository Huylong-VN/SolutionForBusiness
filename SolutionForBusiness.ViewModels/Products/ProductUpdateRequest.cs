using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionForBusiness.ViewModels.Products
{
    public class ProductUpdateRequest
    {
        public Guid userId { set; get; }
        public int ProductId { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
        public int CategoryId { set; get; }
    }
}