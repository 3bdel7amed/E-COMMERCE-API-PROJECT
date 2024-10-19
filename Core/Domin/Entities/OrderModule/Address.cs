namespace Domain.Entities.OrderModule
{
	public class Address
	{
		public Address(string firstName, string lastName, string city, string street, string country, string phone)
		{
			FirstName = firstName;
			LastName = lastName;
			City = city;
			Street = street;
			Country = country;
			Phone = phone;
		}
		public Address()
		{
		}
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
	}
}