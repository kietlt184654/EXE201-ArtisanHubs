using ArtisanHubs.Bussiness.Services.Products.Interfaces;
using ArtisanHubs.Data.Repositories.ArtistProfiles.Interfaces;
using ArtisanHubs.DTOs.DTO.Request.Products;
using Microsoft.AspNetCore.Mvc;

namespace ArtisanHubs.API.Controllers
{
    [Route("api/my-products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IArtistProfileRepository _artistProfileRepo;

        public ProductController(IProductService productService, IArtistProfileRepository artistProfileRepo)
        {
            _productService = productService;
            _artistProfileRepo = artistProfileRepo;
        }

        // Helper để lấy ArtistId
        private async Task<int?> GetCurrentArtistId()
        {
#if DEBUG
            // --- DÀNH CHO MÔI TRƯỜNG TEST ---
            // Giả lập nghệ nhân có ArtistId = 1 đang đăng nhập.
            // Đảm bảo trong DB có ArtistProfile với ArtistId = 1.
            return 1;
#else
            // --- DÀNH CHO MÔI TRƯỜNG PRODUCTION ---
            var accountIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(accountIdString) || !int.TryParse(accountIdString, out int accountId))
            {
                return null;
            }

            var artistProfile = await _artistProfileRepo.GetProfileByAccountIdAsync(accountId);
            return artistProfile?.ArtistId;
#endif
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var artistId = await GetCurrentArtistId();
            if (artistId == null)
            {
                // Trong môi trường test, lỗi này sẽ không xảy ra trừ khi bạn trả về null
                return Unauthorized("Artist profile not found.");
            }

            var result = await _productService.CreateProductAsync(artistId.Value, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetMyProductById(int productId)
        {
            var artistId = await GetCurrentArtistId();
            if (artistId == null) return Unauthorized("Artist profile not found.");
            var result = await _productService.GetMyProductByIdAsync(productId, artistId.Value);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyProducts()
        {
            // 1. Lấy ID của nghệ nhân đang đăng nhập
            var artistId = await GetCurrentArtistId();
            if (artistId == null)
            {
                return Unauthorized("Artist profile not found for this account.");
            }

            // 2. Gọi đến service để lấy danh sách sản phẩm
            var result = await _productService.GetMyProductsAsync(artistId.Value);

            // 3. Trả kết quả về cho client
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductRequest request)
        {
            var artistId = await GetCurrentArtistId();
            if (artistId == null) return Unauthorized("Artist profile not found.");
            var result = await _productService.UpdateProductAsync(productId, artistId.Value, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var artistId = await GetCurrentArtistId();
            if (artistId == null) return Unauthorized("Artist profile not found.");
            var result = await _productService.DeleteProductAsync(productId, artistId.Value);
            return StatusCode(result.StatusCode, result);
        }
    }
}
