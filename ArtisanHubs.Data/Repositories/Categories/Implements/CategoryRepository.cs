
using ArtisanHubs.Data.Basic;
using ArtisanHubs.Data.Entities;
using ArtisanHubs.Data.Repositories.Categories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArtisanHubs.Data.Repositories.Categories.Implements
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ArtisanHubsDbContext _context;
        public CategoryRepository(ArtisanHubsDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetByConditionAsync(Expression<Func<Category, bool>> condition)
        {
            return await _context.Categories.Where(condition).ToListAsync();
        }

        public async Task<bool> ExistAsync(Expression<Func<Category, bool>> condition)
        {
            return await _context.Categories.AnyAsync(condition);
        }
    }
}
