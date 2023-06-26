using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto userRegDto)
        {
            var response = await _authRepo.RegisterAsync(
                    new User(){Username = userRegDto.Username},
                    userRegDto.Password);

            if (!response.Success)
                return BadRequest(response);
            
            return Ok(response);
            
        }
    }
}