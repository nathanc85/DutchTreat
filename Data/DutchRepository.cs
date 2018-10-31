using System;
using System.Collections.Generic;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");

                return _ctx.Products
                           .OrderBy(o => o.Title)
                           .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error GetAllProducts(): {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                       .Where(w => w.Title == category)
                       .OrderBy(o => o.Title)
                       .ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                       .Include(i => i.Items)
                       .ThenInclude(t => t.Product)
                       .ToList();
            }
            else
            {
                return _ctx.Orders
                       .ToList();
            }
        }

        public Order GetOrderById(int id)
        {
            return _ctx.Orders
                       .Include(i => i.Items)
                       .ThenInclude(t => t.Product)
                       .Where(w => w.Id == id)
                       .FirstOrDefault();
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }
    }
}
