using ArtisanHubs.Data.Basic;
using ArtisanHubs.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtisanHubs.Data.Repositories.Products.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByArtistIdAsync(int artistId);
        Task<bool> ProductExistsByNameAsync(int artistId, string productName);
        Task<Product?> GetProductWithDetailsAsync(int productId);
    }
}
