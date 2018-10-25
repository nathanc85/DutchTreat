﻿using System;
using System.Collections.Generic;
using System.Linq;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;

        public DutchRepository(DutchContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Products
                       .OrderBy(o => o.Title)
                       .ToList();
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
    }
}
