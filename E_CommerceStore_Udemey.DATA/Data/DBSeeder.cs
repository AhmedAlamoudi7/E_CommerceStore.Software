using E_CommerceStore_Udemey.DATA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace E_CommerceStore_Udemey.DATA.Data
{
    public static class DBSeeder
    {
        public static IHost SeedDb(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                try
                {

                    //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CMS.Data.Models.User>>();
                    //userManager.SeedUser().Wait();

                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    context.SeedCategory().Wait();
                    context.SeedCoverType().Wait();
                    context.SeedProduct().Wait();
  

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return webHost;
        }

        //public static async Task SeedUser(this UserManager<CMS.Data.Models.User> userManager)
        //{
        //    if (await userManager.Users.AnyAsync())
        //    {
        //        return;
        //    }

        //    var user = new CMS.Data.Models.User
        //    {
        //        Email = "CMS@portal.com",
        //        FirstName = "CMS",
        //        LastName = "User",
        //        UserName = "CMS@portal.com",
        //        PhoneNumber = "0595555555",
        //        PhoneNumberConfirmed = true,
        //        EmailConfirmed = true
        //    };

        //    await userManager.CreateAsync(user, "CMSAdmin2020$$");

        //}

        public static async Task SeedCategory(this ApplicationDbContext context)
        {

            if (await context.Categories.AnyAsync())
            {
                return;
            }

            var category = new Category();
            category.Name = "A1";
            category.DisplayOrder = 12;

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();


        }
        public static async Task SeedCoverType(this ApplicationDbContext context)
        {

            if (await context.CoverTypes.AnyAsync())
            {
                return;
            }

            var coverType = new CoverType();
            coverType.CoverName = "ct1";

            await context.CoverTypes.AddAsync(coverType);
            await context.SaveChangesAsync();


        }
        public static async Task SeedProduct(this ApplicationDbContext context)
        {

            if (await context.Products.AnyAsync())
            {
                return;
            }

            var product = new Product();
            product.Title = "p1";
            product.Author = "Ahmed";
             product.CategoryId = 1;
             product.CoverTypeId = 1;
              product.Description = "p1 Description";
              product.ImageUrl = "54bc6d6b570d4e329adfd840ffe5734a.jpg";
             product.ISBN = "Saeed";
             product.ListPrice = 23;
             product.Price = 123;
             product.Price100 = 100;
            product.Price50 = 50;
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();


        }

        public static async Task SeedShoppingCart(this ApplicationDbContext context)
        {

            if (await context.ShoppingCarts.AnyAsync())
            {
                return;
            }

            var shoppingCart = new ShoppingCart();
            shoppingCart.Count = 14;
            shoppingCart.ProductId = 1;

            await context.ShoppingCarts.AddAsync(shoppingCart);
            await context.SaveChangesAsync();


        }
    }
}
