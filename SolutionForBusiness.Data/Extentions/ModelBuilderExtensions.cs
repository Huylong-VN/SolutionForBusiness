using SolutionForBusiness.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace SolutionForBusiness.Data.Extentions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder model)
        {
            model.Entity<Category>().HasData(
           new Category()
           {
               Id = 1,
               Name = "Sản Phẩm bán chạy",
               Description = "Những sản phảm bán cháy nhất tháng"
           });
            model.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Name = "Sản Phẩm 1",
                    Price = 250000,
                    Description = "Đây là sẩn phẩm đầu tiên",
                    Image = null,
                    DateCreated = DateTime.Now,
                    CategoryId = 1
                }, new Product()
                {
                    Id = 2,
                    Name = "Sản Phẩm 2",
                    Price = 250000,
                    Description = "Đây là sẩn phẩm thứ hai",
                    Image = null,
                    DateCreated = DateTime.Now,
                    CategoryId = 1
                }, new Product()
                {
                    Id = 3,
                    Name = "Sản Phẩm 3",
                    Price = 250000,
                    Description = "Đây là sẩn phẩm thứ 3",
                    Image = null,
                    DateCreated = DateTime.Now,
                    CategoryId = 1
                }
                );
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var userId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            var hasher = new PasswordHasher<User>();
            model.Entity<User>().HasData(
                new User()
                {
                    Id = userId,
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    FirstName = "Huy",
                    LastName = "Nguyễn Anh",
                    Email = "Huynabhaf190133@fpt.edu.vn",
                    NormalizedEmail = "Huynabhaf190133@fpt.edu.vn",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Huylong37"),
                    SecurityStamp = string.Empty,
                    Dob = "05/10/2001"
                });
            model.Entity<Role>().HasData(
                new Role()
                {
                    Id = roleId,
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Administrator role"
                });
            model.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = userId
            });
            model.Entity<Order>().HasData(
                new Order()
                {
                    Id = 1,
                    DateCreated = DateTime.Now,
                    ShipName = "Nguyễn Văn 1",
                    ShipPhone = "0399056507",
                    UserId = userId,
                });
            model.Entity<OrderProduct>().HasData(
                new OrderProduct() { OrderId = 1, ProductId = 1 });
        }
    }
}