using ArtisanHubs.API.DTOs.Common;
using ArtisanHubs.Bussiness.Services.Accounts.Interfaces;
using ArtisanHubs.DTOs.DTO.Request.Accounts;
using ArtisanHubs.DTOs.DTOs.Reponse;
using ArtisanHubs.DTOs.DTOs.Request.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace ArtisanHubs.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly PhotoService _photoService;
        public AccountController(IAccountService accountService, PhotoService photoService)
        {
            _accountService = accountService;
            _photoService = photoService;
        }

        /// <summary>
        /// Lấy danh sách tất cả tài khoản
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _accountService.GetAllAccountAsync();
            return StatusCode(result.StatusCode, result); ;
        }

        /// <summary>
        /// Lấy 1 tài khoản theo id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _accountService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Tạo mới tài khoản
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AccountRequest request)
        {
            // Upload ảnh nếu có
            string? avatarUrl = null;
            if (request.AvatarFile != null)
            {
                avatarUrl = await _photoService.UploadImageAsync(request.AvatarFile);
                request.AvatarFile = null;
            }

            var result = await _accountService.CreateAsync(request, avatarUrl);
            return StatusCode(result.StatusCode, result);
        }


        /// <summary>
        /// Cập nhật tài khoản
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AccountRequest request)
        {
            var result = await _accountService.UpdateAsync(id, request);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _accountService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.LoginAsync(request);
            return StatusCode(result.StatusCode, result);
        }
    }
}
