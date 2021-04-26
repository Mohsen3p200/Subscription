using Application.Handlers.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubscription([FromRoute] int id)
        {
            var query = new GetAllUserSubscriptionQuery(id);

            var result = await _mediator.Send(query);

            return new OkObjectResult(result);
        }
    }
}
