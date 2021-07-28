using System;
using System.Collections.Generic;

namespace SolutionForBusiness.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime DateCreated { get; set; }
        public List<Cart> Carts { set; get; }
        public Category Category { set; get; }
        public int CategoryId { get; set; }
        public List<OrderProduct> OrderProducts { set; get; }
    }
}