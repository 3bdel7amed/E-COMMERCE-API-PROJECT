namespace Domain.Entities.OrderModule
{
	public class ProductItem
	{
        public ProductItem()
        {
            
        }
		public ProductItem(int id, string name, string pictureUrl)
		{
			Id = id;
			Name = name;
			PictureUrl = pictureUrl;
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string PictureUrl { get; set; }
	}
}