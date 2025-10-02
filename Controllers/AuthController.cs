
using MedTox_WebAPI.Data;
using MedTox_WebAPI.DTOs;
using MedTox_WebAPI.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  


namespace MedTox_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email already exists");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);
            if (user == null)
                return Unauthorized("Invalid email or password");

            return Ok(new { message = "Login successful", user = new { user.Id, user.Name, user.Email } });
        }

        // ✅ New GET API for signup details
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            // Return all users, hiding passwords for safety
            var users = await _context.Users
                .Select(u => new { u.Id, u.Name, u.Email }) // Do NOT return Password
                .ToListAsync();

            return Ok(users);
        }
    }
}

