using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Product.API.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Infrastructure
{
    public class ProductDBSeeder
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var testDb = serviceScope.ServiceProvider.GetService<ProductContext>();
                if (await testDb.Database.EnsureCreatedAsync())
                {
                    if (!await testDb.Products.AnyAsync())
                    {
                        await InsertUsersSampleData(testDb);
                    }
                }
            }
        }

        public async Task InsertUsersSampleData(ProductContext db)
        {
            var products = GetProducts();
            db.Products.AddRange(products);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                throw;
            }

        }

        private List<Products> GetProducts()
        {
            var products = new List<Products>();

            Products product1 = new Products
            {
                 code = "P1",
                 Name="Product1",
                 price = 10m
            };

            products.Add(product1);

            Products product2 = new Products
            {
                code = "P2",
                Name = "Product2",
                price = 100m
            };

            products.Add(product2);
            return products;
        }
    }
}
