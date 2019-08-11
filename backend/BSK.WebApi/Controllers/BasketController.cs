using System.Threading.Tasks;
using BSK.Application.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BSK.WebApi.Controllers
{
    [ApiController, Route("api/basket")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-product")]
        public async Task<AddProductToBasket.Result> AddProductToBasket([FromBody] AddProductToBasket.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("remove-product")]
        public async Task<RemoveProductFromBasket.Result> RemoveProductFromBasket([FromBody] RemoveProductFromBasket.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("get-basket")]
        public async Task<GetBasket.Result> GetBasket([FromBody] GetBasket.Command command)
        {
            return await _mediator.Send(command);
        }
    }
}