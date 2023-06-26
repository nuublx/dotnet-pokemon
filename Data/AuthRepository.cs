using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _Context;
        public AuthRepository(DataContext context)
        {
            _Context = context;            
        }
        public async Task<ServiceResponse<string>> LoginAsync(string username, string password)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            
            if (user is null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                // user doesn't exist or wrong password
                return new ServiceResponse<string>(){
                    Data = "",
                    Message = "Username or password is incorrect!",
                    Success = false
                };
            // user exist and correct password
            return new ServiceResponse<string>(){
                Data = "YOUR TOKEN",
                Message = "Logged in Successfully",
                Success = true
            }; 
        }

        public async Task<ServiceResponse<int>> RegisterAsync(User user, string password)
        {
            try {

                if (await UserExistsAsync(user.Username))
                    throw new Exception("Username already Exists !");

                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _Context.Users.Add(user);

                await _Context.SaveChangesAsync();
                
            }
            catch (Exception ex){
                return new ServiceResponse<int>(){
                    Message = ex.Message,
                    Success = false
                    };
            }

           return new ServiceResponse<int>(){
            Data = user.Id,
            Message = $"User Registered Successfully with ID '{user.Id}'",
            Success = true
           };

        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _Context.Users.AnyAsync(u => u.Username.ToLower().Equals(username.ToLower()));
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return ComputedHash.SequenceEqual(passwordHash);
        }
    }
}