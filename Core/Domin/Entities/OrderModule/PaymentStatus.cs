using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Domain.Entities.OrderModule
{
	public enum PaymentStatus
	{
		[EnumMember(Value ="Pending")]
		Pending = 0,
		[EnumMember(Value = "Received")]
		Received = 1,
		[EnumMember(Value = "Failed")]
		Failed= 2,
	}
}