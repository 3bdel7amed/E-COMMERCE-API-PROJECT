using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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

			return mapper.Map<UserResultDto>(user) with { Token = await GenerateTokenAsync(user) };
		}

		public async Task<UserResultDto> RegisterAsync(RegisterResultDto registerModel)
		{
			var user = new User()
			{
				DespalyName = registerModel.DisplayName,
				Email = registerModel.Email,
				UserName = registerModel.DisplayName,
				PhoneNumber = registerModel.PhoneNumber,
			};
			var result = await userManager.CreateAsync(user, registerModel.Password);

			if (!result.Succeeded) throw new ValidationException
				(result.Errors.Select(e => e.Description).ToList());

			return mapper.Map<UserResultDto>(user) with { Token = await GenerateTokenAsync(user) };
		}

		private async Task<string> GenerateTokenAsync(User user)
		{

			var claims = new List<Claim>
			{
				new Claim (ClaimTypes.Name,user.UserName),
				new Claim (ClaimTypes.Email,user.Email),
			};
			var roles = await userManager.GetRolesAsync(user);
			foreach (var item in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, item));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ah_Ya_Shamandora_Sabry_Tal_Rody_We_Gawbeny_3a_Elsoaal"));

			var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				claims: claims,
				signingCredentials: signingCreds,
				audience: "",
				issuer: "",
				expires: DateTime.UtcNow.AddDays(30)
				);

			return new JwtSecurityTokenHandler().WriteToken(token);

		}
	}
}
