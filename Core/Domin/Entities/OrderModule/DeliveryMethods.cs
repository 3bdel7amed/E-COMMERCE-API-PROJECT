namespace Domain.Entities.OrderModule
{
	public class DeliveryMethods : BaseEntity<int>
	{
		public DeliveryMethods(string shortName, string description, string deliveryTime, decimal cost)
		{
			ShortName = shortName;
			Description = description;
			DeliveryTime = deliveryTime;
			Cost = cost;
		}
        public DeliveryMethods()
        {
            
        }

        public string ShortName { get; set; }
		public string Description { get; set; }
		public string DeliveryTime { get; set; }
		public decimal Cost { get; set; }
	}
}