namespace Presentation
{
	public class AuthenticationController(IServiceManager serviceManager) : ApiController
	{
		[HttpPost("Login")]
		public async Task<ActionResult<UserResultDto>> Login(LoginResultDto model) =>
			await serviceManager.AuthenticationService().LoginAsync(model);
		[HttpPost("Register")]
		public async Task<ActionResult<UserResultDto>> Register(RegisterResultDto model) =>
			await serviceManager.AuthenticationService().RegisterAsync(model);
	}
}
