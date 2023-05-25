using Identity.Service.Queries.DTOs;
using Identity.Service.Queries.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Identity.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserQueryService _userQueryService;
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(
            IUserQueryService userQueryService, 
            ILogger<UserController> logger, 
            IMediator mediator)
        {
            _userQueryService = userQueryService;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<UserDto>> GetAll(int page = 1, int take = 10,  string? ids = null)
        {
            IEnumerable<int>? users = ids?.Split(',').Select(s => int.Parse(s));
            return await _userQueryService.GetPagedAsync(page, take, users);
        }

        [HttpGet("{id}")]
        public async Task<UserDto> Get(int id) => await _userQueryService.GetAsync(id);
    }
}
