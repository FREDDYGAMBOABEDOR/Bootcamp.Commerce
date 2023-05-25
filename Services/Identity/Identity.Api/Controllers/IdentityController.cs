using Identity.Domain;
using Identity.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("v1/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMediator _mediator;

        public IdentityController(
            ILogger<IdentityController> logger, 
            SignInManager<ApplicationUser> signInManager, 
            IMediator mediator)
        {
            _logger = logger;
            _signInManager = signInManager;
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserCreateCommand _command)
        {
            if(ModelState.IsValid)
            {
                var result = await _mediator.Send(_command);

                if (!result.Succeeded) {
                    return BadRequest(result.Errors);
                }

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("authentication")]
        public async Task<IActionResult> Authentication(UserLoginCommand _command)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(_command);

                if (!result.Succeeded)
                {
                    return BadRequest("Access Denied..");
                }

                return Ok(result);
            }
            return BadRequest();
        }
    }
}
