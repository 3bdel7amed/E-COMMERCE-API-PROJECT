namespace Domain.Entities.OrderModule
{
	public class OrderItem : BaseEntity<Guid>
	{
		public OrderItem(ProductItem product, int quantity, decimal price)
		{
			Product = product;
			Quantity = quantity;
			Price = price;
		}
        public OrderItem()
        {
            
        }

        public ProductItem Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Product.Price * Quantity
	}
}