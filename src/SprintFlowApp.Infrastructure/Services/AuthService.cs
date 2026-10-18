using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SprintFlowApp.Application.DTOs.Auth;
using SprintFlowApp.Application.Interfaces;
using SprintFlowApp.Domain.Entities;
using SprintFlowApp.Infrastructure.Data;

namespace SprintFlowApp.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public AuthService(AppDbContext db, IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var email = dto.Email.Trim().ToLowerInvariant();

        if (await _db.Users.AnyAsync(u => u.Email.ToLower() == email))
            throw new InvalidOperationException("An account with this email already exists.");

        var role = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "Member");

        if (role is null)
        {
            role = new Role { Name = "Member" };
            _db.Roles.Add(role);
            await _db.SaveChangesAsync();
        }

        var user = new User
        {
            Name = dto.Name.Trim(),
            Email = email,
            RoleId = role.Id
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return CreateTokenResponse(user, role.Name);
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var email = dto.Email.Trim().ToLowerInvariant();

        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email);

        if (user is null)
            return null;

        var result = _passwordHasher.VerifyHashedPassword(
            user, user.PasswordHash, dto.Password);

        if (result == PasswordVerificationResult.Failed)
            return null;

        return CreateTokenResponse(user, user.Role?.Name ?? "Member");
    }

    private AuthResponseDto CreateTokenResponse(User user, string role)
    {
        var jwt = _configuration.GetSection("Jwt");
        var key = jwt["Key"]
            ?? throw new InvalidOperationException("JWT signing key is not configured.");

        var issuer = jwt["Issuer"]
            ?? throw new InvalidOperationException("JWT issuer is not configured.");

        var audience = jwt["Audience"]
            ?? throw new InvalidOperationException("JWT audience is not configured.");

        var expires = DateTime.UtcNow.AddMinutes(60);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, role)
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        return new AuthResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAtUtc = expires,
            Name = user.Name,
            Email = user.Email,
            Role = role
        };
    }
}
