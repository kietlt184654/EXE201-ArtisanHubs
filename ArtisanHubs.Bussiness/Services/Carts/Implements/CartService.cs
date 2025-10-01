using ArtisanHubs.API.DTOs.Common;
using ArtisanHubs.Bussiness.Services.Carts.Interfaces;
using ArtisanHubs.Data.Entities;
using ArtisanHubs.Data.Repositories.Carts.Interfaces;
using ArtisanHubs.Data.Repositories.Products.Interfaces;
using ArtisanHubs.DTOs.DTO.Reponse.Carts;
using ArtisanHubs.DTOs.DTO.Request.Carts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtisanHubs.Bussiness.Services.Carts.Implements
{
    public class CartService : ICartService
    {

        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public CartService(ICartRepository cartRepository, IMapper mapper, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        // Thêm sản phẩm vào giỏ hàng
        public async Task<ApiResponse<CartResponse?>> AddToCartAsync(int accountId, AddToCartRequest request)
        {
            try
            {

                var product = await _productRepository.GetByIdAsync(request.ProductId);
                if (product == null)
                {
                    return ApiResponse<CartResponse?>.FailResponse("Product not found.", 404);
                }

                var cart = await _cartRepository.GetCartByAccountIdAsync(accountId);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        AccountId = accountId,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _cartRepository.CreateCartAsync(cart);
                }

                var existingItem = cart.CartItems.FirstOrDefault(item => item.ProductId == request.ProductId);

                if (existingItem != null)
                {
                    existingItem.Quantity += request.Quantity;
                }
                else
                {

                    var newItem = new CartItem
                    {
                        CartId = cart.Id,
                        ProductId = request.ProductId,
                        Quantity = request.Quantity,
                        AddedAt = DateTime.UtcNow
                    };
                    cart.CartItems.Add(newItem);
                }

                cart.UpdatedAt = DateTime.UtcNow;

                await _cartRepository.UpdateCartAsync(cart);

                // Map kết quả sang DTO để trả về
                var response = _mapper.Map<CartResponse>(cart);
                return ApiResponse<CartResponse?>.SuccessResponse(response, "Product added to cart successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<CartResponse?>.FailResponse($"Error adding product to cart: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<CartResponse?>> GetCartByAccountIdAsync(int accountId)
        {
            try
            {
                // Lấy cart từ repo
                var cart = await _cartRepository.GetCartByAccountIdAsync(accountId);

                if (cart == null)
                {
                    return ApiResponse<CartResponse?>.FailResponse("Cart not found.", 404);
                }

                // Map bằng AutoMapper
                var cartResponse = _mapper.Map<CartResponse>(cart);

                // Tính tổng tiền nếu cần
                cartResponse.TotalPrice = cartResponse.Items.Sum(i => i.Price * i.Quantity);

                return ApiResponse<CartResponse?>.SuccessResponse(cartResponse);
            }
            catch (Exception ex)
            {
                return ApiResponse<CartResponse?>.FailResponse($"Error retrieving cart: {ex.Message}", 500);
            }
        }

    }
}
