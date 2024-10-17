﻿using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public class AuthenticationService(UserManager<User> userManager, IMapper mapper) : IAuthenticationService
	{
		public async Task<UserResultDto> LoginAsync(LoginResultDto loginModel)
		{
			var user = await userManager.FindByEmailAsync(loginModel.Email);
			if (user is null) throw new UnAuthorizedException($"This Email ({loginModel.Email}) Isn't Exist");
			var result = await userManager.CheckPasswordAsync(user, loginModel.Password);
			if (!result) throw new UnAuthorizedException();


			return mapper.Map<UserResultDto>(user) with { Token = "token" };
		}

		public async Task<UserResultDto> RegisterAsync(RegisterResultDto registerModel)
		{
			var user = await userManager.CreateAsync
				(mapper.Map<User>(registerModel), registerModel.Password);

			if (!user.Succeeded) throw new ValidationException
				(user.Errors.Select(e => e.Description).ToList());

			return mapper.Map<UserResultDto>(user) with { Token = "token" };
		}
	}
}
