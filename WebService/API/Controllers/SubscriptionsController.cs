using Application.Handlers.Subscriptions.Commands;
using Application.Handlers.Subscriptions.Models;
using Application.Handlers.Subscriptions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionForUserModel model)
        {
            var command = new CreateSubscriptionForUserCommand()
            {
                CreateSubscription = model
            };

            var result = await _mediator.Send(command);

            return CreatedAtAction("GetSubscription", new { id = result.Id }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscription([FromRoute] int id)
        {
            var command = new DeleteSubscriptionCommand(id);

            await _mediator.Send(command);

            return new OkResult();
         }

        [HttpGet("{id}",Name= "GetSubscription")]
        public async Task<IActionResult> GetSubscription([FromRoute] int id)
        {
            var query = new GetSubscriptionByIdQuery(id);

            var result = await _mediator.Send(query);

            return new OkObjectResult(result);
        }


    }
}
