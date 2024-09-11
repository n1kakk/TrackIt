using IdentityService.Interfaces;
using IdentityService.Models;
using IdentityService.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Controllers;

[ApiController]
[Route("api/identification")]
public class IdentityController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<IdentityUser> _signInManager;
    public IdentityController(UserManager<IdentityUser> userManager, ITokenService tokenService, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("register/client")]
    public async Task<IActionResult> RegisterClient([FromBody] RegisterClientModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = new IdentityUser { UserName = model.Username, Email = model.Email };
        var res = await _userManager.CreateAsync(user, model.Password);

        if (!res.Succeeded) return BadRequest(res.Errors);

        await _userManager.AddToRoleAsync(user, "Client");

        var token = await _tokenService.CreateToken(user);

        return Ok(new NewUserDto
        {
            Email = user.Email,
            Token = token
        });
    }


    [HttpPost("register/courier")]
    public async Task<IActionResult> RegisterCourier([FromBody] RegisterCourierModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = new IdentityUser { UserName = model.Username, Email = model.Email };
        var res = await _userManager.CreateAsync(user, model.Password);

        if (!res.Succeeded) return BadRequest(res.Errors);

        await _userManager.AddToRoleAsync(user, "Courier");

        var token = await _tokenService.CreateToken(user);

        return Ok(new NewUserDto
        {
            Email = user.Email,
            Token = token
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

        if (user == null) return Unauthorized("Login failed!");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized("Email not found and/or password incorrect");

        var token = await _tokenService.CreateToken(user);

        return Ok(new NewUserDto
        {
            Email = user.Email,
            Token = token
        });
    }

}