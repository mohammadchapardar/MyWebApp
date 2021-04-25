using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWebApp.ConstantsClass;
using MyWebApp.Dto;
using MyWebApp.Models;
using MyWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Extentions
{
    public static class HostExtentions
    {
        public static IHost Migragion(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var AppDbcontext = scope.ServiceProvider.GetService<AppDbContext>();
                AppDbcontext.Database.Migrate();
                if (!AppDbcontext.Products.Any())
                {
                    Random randomNumber = new Random();
                    for (int i = 1; i < 40; i++)
                    {
                        if (i < 5)
                        {
                            AppDbcontext.Products.Add(new Products
                            {
                                Id = Guid.NewGuid(),
                                Name = "نام محصول" + " " + i,
                                Category = "کالای دیجیتال",
                                Description = "توضیحات محصول" + " " + i,
                                Price =randomNumber.Next(100,1_000_000)
                            });
                        }
                        else if (i < 10)
                        {
                            AppDbcontext.Products.Add(new Products
                            {
                                Id = Guid.NewGuid(),
                                Name = "نام محصول" + " " + i,
                                Category = "مد و پوشاک",
                                Description = "توضیحات محصول" + " " + i,
                                Price = randomNumber.Next(100, 1_000_000)
                            });
                        }
                        else if (i < 15)
                        {
                            AppDbcontext.Products.Add(new Products
                            {
                                Id = Guid.NewGuid(),
                                Name = "نام محصول" + " " + i,
                                Category = "زیبایی و سلامت",
                                Description = "توضیحات محصول" + " " + i,
                                Price = randomNumber.Next(100, 1_000_000)
                            });
                        }
                        else if (i<25)
                        {
                            AppDbcontext.Products.Add(new Products
                            {
                                Id = Guid.NewGuid(),
                                Name = "نام محصول" + " " + i,
                                Category = "خانه و آشپزخانه",
                                Description = "توضیحات محصول" + " " + i,
                                Price = randomNumber.Next(100, 1_000_000)
                            });
                        }
                        else
                        {
                            AppDbcontext.Products.Add(new Products
                            {
                                Id = Guid.NewGuid(),
                                Name = "نام محصول" + " " + i,
                                Category = "خوردنی و اشامیدنی",
                                Description = "توضیحات محصول" + " " + i,
                                Price = randomNumber.Next(100, 1_000_000)
                            });
                        }
                    }
                    AppDbcontext.SaveChanges();
                }

                var AuthDbContext = scope.ServiceProvider.GetService<AuthDbContext>();
                AuthDbContext.Database.Migrate();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<AppRole>>();
                if (!roleManager.Roles.Any())
                {
                    roleManager.CreateAsync(new AppRole(AuthConstants.AdminRole)).Wait();
                    roleManager.CreateAsync(new AppRole(AuthConstants.UserRole)).Wait();
                }

                var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
                if (!userManager.Users.Any())
                {
                    var user = new AppUser()
                    {
                        Id = Guid.NewGuid(),
                        UserName = "Admin",
                        PhoneNumber = "09123456879",
                        Email = "abc@de.fgh"
                    };
                    userManager.CreateAsync(user,"P@ssw0rd1122").Wait();
                    userManager.AddToRoleAsync(user, AuthConstants.AdminRole).Wait();
                }
            }
            return host;
        }
    }
}
