using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNNTC.DAL;
using TestNNTC.DAL.Entities;

namespace TestNNTC
{
    public static class DataSeeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<TestNNTCDbContext>();
            context.Database.EnsureCreated();
            AddCatalogue(context);
        }

        private static void AddCatalogue(TestNNTCDbContext context)
        {
            var CatalogueItem = context.CatalogueList.FirstOrDefault();
            if (CatalogueItem != null) return;

            context.CatalogueList.Add(new CatalogueDataEntity
            {
                CategoryName = "Техника",
                CatalogueProducts = new List<CatalogueDataProduct>
                {
                    new CatalogueDataProduct { ProductName = "Телефон", Cost = 15000, Description = "Отличный телефон" },
                    new CatalogueDataProduct { ProductName = "Компьютер", Cost = 50000, Description = "Компьютер" },
                    new CatalogueDataProduct { ProductName = "Планшет", Cost = 20000, Description = "Планшет" },

                }
            });
            context.CatalogueList.Add(new CatalogueDataEntity
            {
                CategoryName = "Одежда",
                CatalogueProducts = new List<CatalogueDataProduct>
                {
                    new CatalogueDataProduct { ProductName = "Кофта", Cost = 1000, Description = "КофтаКофта" },
                    new CatalogueDataProduct { ProductName = "Майка", Cost = 500, Description = "МайкаМайка" },
                    new CatalogueDataProduct { ProductName = "Носки", Cost = 100, Description = "НоскиНоски" },

                }
            });
            context.CatalogueList.Add(new CatalogueDataEntity
            {
                CategoryName = "Продукты",
                CatalogueProducts = new List<CatalogueDataProduct>
                {
                    new CatalogueDataProduct { ProductName = "Яблоко", Cost = 100, Description = "Яблоко красное" },
                    new CatalogueDataProduct { ProductName = "Молоко", Cost = 70, Description = "Молоко 15%" },
                    new CatalogueDataProduct { ProductName = "Булочка", Cost = 30, Description = "Булочка с маком" },

                }
            });
            context.CatalogueList.Add(new CatalogueDataEntity
            {
                CategoryName = "Автомобили",
                CatalogueProducts = new List<CatalogueDataProduct>
                {
                    new CatalogueDataProduct { ProductName = "BMW", Cost = 1500000, Description = "BMW x5" },
                    new CatalogueDataProduct { ProductName = "Mercedec", Cost = 50000000, Description = "Merced" },
                    new CatalogueDataProduct { ProductName = "Supra", Cost = 20000000, Description = "SupraSupra" },

                }
            });

            context.SaveChanges();
        }
    }
}
