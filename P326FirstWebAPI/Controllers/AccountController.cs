using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P326FirstWebAPI.DAL;
using P326FirstWebAPI.Dtos.User;
using P326FirstWebAPI.Models;

namespace P326FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    { 
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {

            var result = await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
             result = await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
             result = await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
            return StatusCode(201);

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = await _userManager.FindByNameAsync(registerDto.UserName);
            if (user != null) return BadRequest(409);
            user = new AppUser()
            {
                FullName=registerDto.FullName,
                UserName=registerDto.UserName };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            result = await _userManager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded) return BadRequest(result.Errors);

            return StatusCode(201);
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginDto loginDto)
        {
            return Ok(new {token=""});
        }
    }
}
