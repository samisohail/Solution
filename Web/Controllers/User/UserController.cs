using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Queries.User;
using Web.Setup;

namespace Web.Controllers.User
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator= mediator;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var response = (await _mediator.Send(new GetAllUsersQuery())).BuildResponse();
            return response;
        }

    }
}
