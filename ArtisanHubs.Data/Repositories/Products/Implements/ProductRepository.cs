﻿using ArtisanHubs.Data.Basic;
using ArtisanHubs.Data.Entities;
using ArtisanHubs.Data.Repositories.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtisanHubs.Data.Repositories.Products.Implements
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ArtisanHubsDbContext _context;

        public ProductRepository(ArtisanHubsDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsByArtistIdAsync(int artistId)
        {
            return await _context.Products
                                  .Include(p => p.Category)
                                  .Where(p => p.ArtistId == artistId)
                                  .ToListAsync();
        }
        public async Task<bool> ProductExistsByNameAsync(int artistId, string productName)
        {
            // Chuyển cả hai về chữ thường để so sánh không phân biệt hoa/thường
            var normalizedProductName = productName.ToLower();

            return await _context.Products
                .AnyAsync(p => p.ArtistId == artistId && p.Name.ToLower() == normalizedProductName);
        }

        public async Task<Product?> GetProductWithDetailsAsync(int productId)
        {
            return await _context.Products
                                 .Include(p => p.Category) // <-- THÊM DÒNG NÀY
                                 .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
           return await _context.Products
                                 .Include(p => p.Category)
                                 .Where(p => p.CategoryId == categoryId)
                                 .ToListAsync();
        }
    }
}
