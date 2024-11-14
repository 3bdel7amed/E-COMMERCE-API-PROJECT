namespace Service.Abstraction
{
    public interface IAuthenticationService
	{
		public Task<UserResultDto> LoginAsync(LoginResultDto loginModel);
		public Task<UserResultDto> RegisterAsync(RegisterResultDto registerModel);
		public Task<UserResultDto> GetCurrentUserAsync(string email);
		public Task<bool> CheckEmailAsync(string email);
		public Task<AddressResultDto> GetAddressAsync(string email);
		public Task<AddressResultDto> UpdateAddressAsync(string email,AddressResultDto address);
	}
}
