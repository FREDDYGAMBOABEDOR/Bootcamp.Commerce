using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Service.EventHandlers.Commands;
using Order.Service.Queries.DTOs;
using Order.Service.Queries.Interfaces;
using Service.Common.Collection;

namespace Order.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("v1/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderQueryService _orderQueryService;
        private readonly IMediator _mediator;

        public OrderController(IOrderQueryService orderQueryService, IMediator mediator)
        {
            _orderQueryService = orderQueryService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<OrderDto>> GetPaged(int page = 1, int take = 10) => await _orderQueryService.GetPagedAsync(page, take);

        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id) => await _orderQueryService.GetAsync(id);

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
    }
}
