﻿using ArtisanHubs.Bussiness.Services.Products.Interfaces;
using ArtisanHubs.Data.Repositories.ArtistProfiles.Interfaces;
using ArtisanHubs.DTOs.DTO.Request.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtisanHubs.API.Controllers
{
    [Authorize(Roles = "Artist")]
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

        // ✅ HÀM HELPER MỚI: Chỉ lấy AccountId từ token
        private int GetCurrentAccountId()
        {
            var accountIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(accountIdString))
            {
                throw new InvalidOperationException("Claim 'NameIdentifier' (Account ID) not found in token.");
            }
            if (!int.TryParse(accountIdString, out var accountId))
            {
                throw new InvalidOperationException("Claim 'NameIdentifier' in token is not a valid integer.");
            }
            return accountId;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            // Bước 1: Lấy AccountId từ token
            var accountId = GetCurrentAccountId();

            // Bước 2: Dùng AccountId để lấy thông tin ArtistProfile
            var artistProfile = await _artistProfileRepo.GetProfileByAccountIdAsync(accountId);
            if (artistProfile == null)
            {
                return Unauthorized("Artist profile not found for this account.");
            }

            // Bước 3: Gọi service với ArtistId
            var result = await _productService.CreateProductAsync(artistProfile.ArtistId, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetMyProductById(int productId)
        {
            var accountId = GetCurrentAccountId();
            var artistProfile = await _artistProfileRepo.GetProfileByAccountIdAsync(accountId);
            if (artistProfile == null) return Unauthorized("Artist profile not found for this account.");

            var result = await _productService.GetMyProductByIdAsync(productId, artistProfile.ArtistId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyProducts()
        {
            var accountId = GetCurrentAccountId();
            var artistProfile = await _artistProfileRepo.GetProfileByAccountIdAsync(accountId);
            if (artistProfile == null)
            {
                return Unauthorized("Artist profile not found for this account.");
            }

            var result = await _productService.GetMyProductsAsync(artistProfile.ArtistId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductRequest request)
        {
            var accountId = GetCurrentAccountId();
            var artistProfile = await _artistProfileRepo.GetProfileByAccountIdAsync(accountId);
            if (artistProfile == null) return Unauthorized("Artist profile not found for this account.");

            var result = await _productService.UpdateProductAsync(productId, artistProfile.ArtistId, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var accountId = GetCurrentAccountId();
            var artistProfile = await _artistProfileRepo.GetProfileByAccountIdAsync(accountId);
            if (artistProfile == null) return Unauthorized("Artist profile not found for this account.");

            var result = await _productService.DeleteProductAsync(productId, artistProfile.ArtistId);
            return StatusCode(result.StatusCode, result);
        }
    }
}