using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SportStore.Models.Data
{
    public class SeedData
    {
        public static void EnsurePopulated(IHost host)
        {
            var serviceScope = host.Services.CreateScope();

            try
            {
                var context = serviceScope.ServiceProvider.GetService<StoreDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Products.Any())
                {
                    var products = new Product[]
                    {
                        new ()
                        {
                            Name = "Kayak",
                            Description = "A boat for one person",
                            Category = "Watersports",
                            Price = 275
                        },
                        new ()
                        {
                            Name = "Lifejacket",
                            Description = "Protective and fashionable",
                            Category = "Watersports",
                            Price = 48.95m
                        },
                        new ()
                        {
                            Name = "Soccer Ball",
                            Description = "FIFA-approved size and weight",
                            Category = "Soccer",
                            Price = 19.50m
                        },
                        new ()
                        {
                            Name = "Corner Flags",
                            Description = "Give your playing field a professional touch",
                            Category = "Soccer",
                            Price = 34.95m
                        },
                        new ()
                        {
                            Name = "Stadium",
                            Description = "Flat-packed 35,000-seat stadium",
                            Category = "Soccer",
                            Price = 79500
                        },
                        new ()
                        {
                            Name = "Thinking Cap",
                            Description = "Improve brain efficiency by 75%",
                            Category = "Chess",
                            Price = 16
                        },
                        new ()
                        {
                            Name = "Unsteady Chair",
                            Description = "Secretly give your opponent a disadvantage",
                            Category = "Chess",
                            Price = 29.95m
                        },
                        new ()
                        {
                            Name = "Human Chess Board",
                            Description = "A fun game for the family",
                            Category = "Chess",
                            Price = 75
                        },
                        new()
                        {
                            Name = "Bling-Bling King",
                            Description = "Gold-plated, diamond-studded King",
                            Category = "Chess",
                            Price = 1200
                        }
                    };

                    context.Products.AddRange(products);
                    context.SaveChanges();
                }

            }
            catch (Exception)
            {
                var logger = serviceScope.ServiceProvider.GetService<ILogger<SeedData>>();
                logger.LogError("An exception occurred during the data filling process");
            }
        }
    }
}
