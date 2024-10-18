using Shared.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
    public interface IAuthenticationService
	{
		public Task<UserResultDto> LoginAsync(LoginResultDto loginModel);
		public Task<UserResultDto> RegisterAsync(RegisterResultDto registerModel);
	}
}
