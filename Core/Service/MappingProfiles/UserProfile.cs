using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserResultDto>()
			// بسبب خطا املائى 
			.ForMember(d => d.DisplayName, s => s.MapFrom(u => u.DespalyName))
			.ForMember(d=>d.Email ,s=>s.MapFrom(u => u.Email))
			// Assuming you'll set the token separately
			.ForMember(d => d.Token, s => s.Ignore())
			.ReverseMap();

		}
	}
}
