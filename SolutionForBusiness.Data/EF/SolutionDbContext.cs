using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolutionForBusiness.Data.Configurations;
using SolutionForBusiness.Data.Entities;
using SolutionForBusiness.Data.Extentions;
using System;

namespace SolutionForBusiness.Data.EF
{
    public class SolutionDbContext : IdentityDbContext<User, Role, Guid>
    {
        public SolutionDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Configure using fluent Api
            builder.ApplyConfiguration(new UserClaimConfiguration());
            builder.ApplyConfiguration(new UserLoginConfiguration());
            builder.ApplyConfiguration(new UserTokenConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new RoleClaimConfiguration());
            builder.ApplyConfiguration(new CartConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new SlideConfiguration());
            builder.ApplyConfiguration(new OrderProductConfiguration());

            //Seeding data
            builder.Seed();
        }

        //DbSet
        public DbSet<Cart> Carts { set; get; }

        public DbSet<OrderProduct> OrderProducts { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<Slide> Slides { set; get; }
    }
}