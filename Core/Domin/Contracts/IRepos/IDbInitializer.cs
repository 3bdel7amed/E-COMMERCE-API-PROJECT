using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.IRepos
{
	public interface IDbInitializer
	{
		public Task InitializerAsync();
	}
}
