using Identity.API.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Infrastructure
{
    public class IdentityDbSeeder
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var testDb = serviceScope.ServiceProvider.GetService<IdentityDbContext>();
                if (await testDb.Database.EnsureCreatedAsync())
                {
                    if (!await testDb.Users.AnyAsync())
                    {
                        await InsertUsersSampleData(testDb);
                    }
                }
            }
        }

        public async Task InsertUsersSampleData(IdentityDbContext db)
        {
            var users = GetUsers();
            db.Users.AddRange(users);

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

        private List<Users> GetUsers()
        {
            var users = new List<Users>();

            Users adminUser = new Users
            {
                UserName = "admin",
                Password = "admin"
            };

            users.Add(adminUser);

            Users guestUser = new Users
            {
                UserName = "guest",
                Password = "guest"
            };

            users.Add(guestUser);
            return users;
        }
    }
}
