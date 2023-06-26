using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> RegisterAsync(User user, string password);
        Task<ServiceResponse<string>> LoginAsync(string username, string password);
        Task<bool> UserExistsAsync(String username);

    }
}