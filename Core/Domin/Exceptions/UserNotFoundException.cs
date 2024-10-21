
namespace Domain.Exceptions
{
	public class UserNotFoundException(string email)
		: NotFoundException($"User Of Email {email} Is Not Found!")
	{
	}
}
