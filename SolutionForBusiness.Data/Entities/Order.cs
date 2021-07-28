using System;
using System.Collections.Generic;

namespace SolutionForBusiness.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string ShipName { get; set; }

        public string ShipPhone { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
        public List<OrderProduct> OrderProducts { set; get; }
    }
}