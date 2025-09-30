﻿using ArtisanHubs.Bussiness.Services.ArtistProfiles.Interfaces;
using ArtisanHubs.DTOs.DTO.Request.ArtistProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtisanHubs.API.Controllers
{
    [Route("api/artist-profiles")]
    [ApiController]
    public class ArtistProfilesController : ControllerBase
    {
        private readonly IArtistProfileService _artistProfileService;

        public ArtistProfilesController(IArtistProfileService artistProfileService)
        {
            _artistProfileService = artistProfileService;
        }
       
        private int GetCurrentAccountId()
        {
            // Xóa hoặc comment out toàn bộ khối #if DEBUG ... #endif
            // Chỉ giữ lại phần code đọc ID từ token

            var accountIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(accountIdString))
            {
                // Lỗi này xảy ra nếu token hợp lệ nhưng lại thiếu claim ID của người dùng,
                // cho thấy có vấn đề ở khâu tạo token.
                throw new InvalidOperationException("Account ID claim (NameIdentifier) not found in token.");
            }

            return int.Parse(accountIdString);
        }

        [Authorize(Roles = "Artist")]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var accountId = GetCurrentAccountId();
            var result = await _artistProfileService.GetMyProfileAsync(accountId);

            return StatusCode(result.StatusCode, result);
        }

        // POST: api/artist-profiles/me
        // Tạo profile cho chính nghệ nhân đang đăng nhập

        [Authorize(Roles = "Artist")]
        [HttpPost()]
        public async Task<IActionResult> CreateMyProfile([FromBody] ArtistProfileRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var accountId = GetCurrentAccountId();
            var result = await _artistProfileService.CreateMyProfileAsync(accountId, request);

            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/artist-profiles/me
        // Cập nhật profile cho chính nghệ nhân đang đăng nhập

        [Authorize(Roles = "Artist")]
        [HttpPut()]
        public async Task<IActionResult> UpdateMyProfile([FromBody] ArtistProfileRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var accountId = GetCurrentAccountId();
            var result = await _artistProfileService.UpdateMyProfileAsync(accountId, request);

            return StatusCode(result.StatusCode, result);
        }

        // === CÁC ENDPOINT CÔNG KHAI HOẶC DÀNH CHO ADMIN ===


        // Lấy danh sách tất cả profile (có thể cho Admin hoặc công khai)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var result = await _artistProfileService.GetAllProfilesAsync();
            return StatusCode(result.StatusCode, result);
        }

        // Xóa một profile (chức năng này thường dành cho Admin)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var result = await _artistProfileService.DeleteProfileAsync(id);
            return StatusCode(result.StatusCode, result);
        }

    }
}
