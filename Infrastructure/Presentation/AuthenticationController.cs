using System.Security.Claims;

namespace Presentation
{
	public class AuthenticationController(IServiceManager serviceManager) : ApiController
	{
		[HttpPost("Login")]
		public async Task<ActionResult<UserResultDto>> Login(LoginResultDto model)
			=> await serviceManager.AuthenticationService().LoginAsync(model);
		[HttpPost("Register")]
		public async Task<ActionResult<UserResultDto>> Register(RegisterResultDto model)
			=> await serviceManager.AuthenticationService().RegisterAsync(model);
		[HttpGet("CheckUser")]
		public async Task<ActionResult<bool>> CheckUser(string email)
			=> Ok(await serviceManager.AuthenticationService()
				.CheckEmailAsync(email));
		[HttpGet("GetUser")]
		[Authorize]
		public async Task<ActionResult<UserResultDto>> GetUser()
			=> Ok(await serviceManager.AuthenticationService()
				.GetCurrentUserAsync(User.FindFirstValue(ClaimTypes.Email)!));
		[HttpGet("GetAddress")]
		[Authorize]
		public async Task<ActionResult<UserResultDto>> GetAddress()
			=> Ok(await serviceManager.AuthenticationService()
				.GetAddressAsync(User.FindFirstValue(ClaimTypes.Email)!));
		[HttpPut("UpdateAddress")]
		[Authorize]
		public async Task<ActionResult<UserResultDto>> UpdateAddress(AddressResultDto address)
			=> Ok(await serviceManager.AuthenticationService()
				.UpdateAddressAsync(User.FindFirstValue(ClaimTypes.Email)!,address));


	}
}
