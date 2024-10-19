namespace Domain.Entities.OrderModule
{
	public class OrderItem : BaseEntity<Guid>
	{
        public ProductItem Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Product.Price * Quantity
	}
}