using ArtisanHubs.API.DTOs.Common;
using ArtisanHubs.DTOs.DTO.Reponse.Carts;
using ArtisanHubs.DTOs.DTO.Request.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtisanHubs.Bussiness.Services.Carts.Interfaces
{
    public interface ICartService
    {
        Task<ApiResponse<CartResponse>> AddToCartAsync(int accountId, AddToCartRequest request);
        Task<ApiResponse<CartResponse>> GetCartByAccountIdAsync(int accountId);
    }
}
