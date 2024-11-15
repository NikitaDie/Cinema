using Microsoft.AspNetCore.Mvc;
using MusicStreaming.Core.DTOs;
using MusicStreaming.Core.Interfaces;
using MusicStreaming.Core.Models;
using MusicStreaming.Core.Services;

namespace MusicStreaming.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtTokenService _jwtTokenService;

    public UserController(IUserService userService, JwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
    }

    // GET: api/User/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
            return NotFound("User not found.");

        return Ok(user);
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        if (await _userService.UserExists(createUserDto.Email))
            return BadRequest("A user with this email already exists.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

        var user = await _userService.CreateUser(new User
        {
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            PasswordHash = hashedPassword
        });

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    // POST: api/User/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userService.GetUserByEmail(loginDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            return Unauthorized("Invalid email or password.");

        var token = _jwtTokenService.GenerateJwtToken(user.Id, user.Username);
        
        return Ok(new { Token = token });
    }

    // PUT: api/User/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto updateUserDto)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
            return NotFound("User not found.");

        user.Username = updateUserDto.Username ?? user.Username;
        user.Email = updateUserDto.Email ?? user.Email;

        await _userService.UpdateUser(user);

        return NoContent();
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
            return NotFound("User not found.");

        await _userService.DeleteUser(id);

        return NoContent();
    }
}