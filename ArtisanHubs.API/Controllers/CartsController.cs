using ArtisanHubs.Bussiness.Services.ArtistProfiles.Interfaces;
using ArtisanHubs.Bussiness.Services.Carts.Interfaces;
using ArtisanHubs.DTOs.DTO.Request.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtisanHubs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IArtistProfileService _artistProfileService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        protected int GetCurrentAccountId()
        {
            var accountIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(accountIdString))
            {
                // Lỗi này xảy ra nếu token hợp lệ nhưng lại thiếu claim ID,
                // cho thấy có vấn đề ở khâu tạo token.
                throw new InvalidOperationException("Account ID claim (NameIdentifier) not found in token.");
            }

            return int.Parse(accountIdString);
        }
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMyCart()
        {
            // Gọi hàm được kế thừa, code gọn gàng hơn rất nhiều
            var accountId = GetCurrentAccountId();
            var result = await _cartService.GetCartByAccountIdAsync(accountId);
            // Return trực tiếp kết quả từ service
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            // Gọi hàm được kế thừa
            var accountId = GetCurrentAccountId();
            var result = await _cartService.AddToCartAsync(accountId, request);
            // Return trực tiếp kết quả từ service
            return StatusCode(result.StatusCode, result);
        }

    }
}
