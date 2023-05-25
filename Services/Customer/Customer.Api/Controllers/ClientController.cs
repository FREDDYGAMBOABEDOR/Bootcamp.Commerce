using Customer.Service.EventHandlers.Commands;
using Customer.Service.Queries.DTOs;
using Customer.Service.Queries.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Customer.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("v1/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientQueryService _clientQueryService;
        private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;

        public ClientController(IClientQueryService clientQueryService, ILogger<ClientController> logger, IMediator mediator)
        {
            _clientQueryService = clientQueryService;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<ClientDto>> GetPaged(int page, int take, string? ids)
        {
            IEnumerable<int> products = null;
            if (!string.IsNullOrEmpty(ids))
                products = ids.Split(',').Select(x => Convert.ToInt32(x));

            return await _clientQueryService.GetPagedAsync(page, take, products);
        }

        [HttpGet("{id}")]
        public async Task<ClientDto> Get(int id) => await _clientQueryService.GetAsync(id);

        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
    }
}
