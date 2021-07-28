using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SolutionForBusiness.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dob { set; get; }
        public List<Cart> Carts { set; get; }
        public List<Order> Orders { set; get; }
    }
}