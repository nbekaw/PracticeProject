using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.MongoDb;
using Domain.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly MongoDbContext _context;
    private readonly JwtSettings _jwt;

    public UserService(MongoDbContext context, IOptions<JwtSettings> jwt)
    {
        _context = context;
        _jwt = jwt.Value;
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var existing = await _context.Users.Find(x => x.Login == dto.Login).FirstOrDefaultAsync();
        if (existing != null) throw new Exception("User already exists");

        var user = new UserEntity
        {
            Login = dto.Login,
            PasswordHash = Hash(dto.Password)
        };

        await _context.Users.InsertOneAsync(user);
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.Find(x => x.Login == dto.Login).FirstOrDefaultAsync();
        if (user == null || user.PasswordHash != Hash(dto.Password))
            throw new Exception("Invalid credentials");

        return GenerateJwt(user);
    }

    private string Hash(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private string GenerateJwt(UserEntity user)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwt.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: new[] { new Claim(ClaimTypes.NameIdentifier, user.Id) },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
