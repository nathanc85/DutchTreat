using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext ctx;
        private readonly IHostingEnvironment hosting;

        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting)
        {
            this.ctx = ctx;
            this.hosting = hosting;
        }

        public void Seed()
        {
            ctx.Database.EnsureCreated();

            if (!ctx.Products.Any())
            {
                // Create sample data.
                var filePath = Path.Combine(hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

                ctx.Products.AddRange(products);

                var order = ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if (order != null)
                {
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };

                }

                ctx.SaveChanges();
            }
        }
    }
}
