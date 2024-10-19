using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class User : IdentityUser
	{

        public string DespalyName{ get; set; }
        public Address Address { get; set; }
    }
}
