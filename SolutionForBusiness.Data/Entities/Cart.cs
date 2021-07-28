using System;

namespace SolutionForBusiness.Data.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime DateCreated { get; set; }
        public int ProductId { set; get; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}