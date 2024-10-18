using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Shared.UserModels;

namespace Presentation
{
    [ApiController]
	[Route("api/[controller]")]
	public class AuthenticationController(IServiceManager serviceManager) : ControllerBase
	{
		[HttpPost("Login")]
		public async Task<ActionResult<UserResultDto>> Login(LoginResultDto model) =>
			await serviceManager.AuthenticationService().LoginAsync(model);
		[HttpPost("Register")]
		public async Task<ActionResult<UserResultDto>> Register(RegisterResultDto model) =>
			await serviceManager.AuthenticationService().RegisterAsync(model);
	}
}
