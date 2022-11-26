
using AuthServices.Contracts;
using DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.auth
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenHelper _tokenHelper;

        public AuthController(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }
        [HttpGet]
        public string GetToken()
        {
            var user = new UserDto
            {
                UserId= 1,
                FirstName= "FirstName"
            };
            return _tokenHelper.GenerateToken(user);
        }

        [HttpPost]
        public int? ValidateToken(string token)
        {
            return _tokenHelper.ValidateToken(token);
        }
    }
}
