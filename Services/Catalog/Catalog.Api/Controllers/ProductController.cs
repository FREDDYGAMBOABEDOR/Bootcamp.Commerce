using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.Queries.DTOs;
using Catalog.Service.Queries.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Catalog.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductQueryService _productQueryService;
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;

        public ProductController(IProductQueryService productQueryService, ILogger<ProductController> logger, IMediator mediator)
        {
            _productQueryService = productQueryService;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<ProductDto>> GetPaged(int page, int take, string? ids)
        {
            IEnumerable<int> products = null;
            if (!string.IsNullOrEmpty(ids))
                products = ids.Split(',').Select(x => Convert.ToInt32(x));

            return await _productQueryService.GetPagedAsync(page, take, products);
        }

        [HttpGet("{id}")]
        public async Task<ProductDto> Get(int id) => await _productQueryService.GetAsync(id);

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
    }
}
