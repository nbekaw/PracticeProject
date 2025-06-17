using Application.Dtos;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IUserService
{
    Task RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
}
