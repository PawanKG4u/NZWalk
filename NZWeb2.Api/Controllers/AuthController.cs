using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWeb2.Api.BAL;
using NZWeb2.Api.Models.Domain;
using NZWeb2.Api.Models.DTO;

namespace NZWeb2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Step - 7
    public class AuthController : ControllerBase
    {
        private readonly IUserRespository _IUserRespository;
        private readonly ITokenHandler _ITokenHandler;
        public AuthController(IUserRespository UserRespository, ITokenHandler iTokenHandler)
        {
            _IUserRespository = UserRespository;
            _ITokenHandler = iTokenHandler;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequestDTO paramLoginRequestDTO) 
        {
            var _ObjUser = await _IUserRespository.AuthenticationAsync(paramLoginRequestDTO.Username, paramLoginRequestDTO.Password);
            if (_ObjUser != null)
            {
                //Step - 10

                //Generate a JWT Token
                var token = await _ITokenHandler.CreateTokenAsync(_ObjUser);
                return Ok(token);
            }

            return BadRequest("Username and Password Invaild");
        }
    }
}
