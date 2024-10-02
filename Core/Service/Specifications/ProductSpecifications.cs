using Domain.Contracts.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
	public class ProductSpecifications : Specifications<Product>
	{
		public ProductSpecifications(int id) : base(p => p.Id == id)
		{
			AddInclude(p => p.ProductBrand);
			AddInclude(p => p.ProductType);
		}
		public ProductSpecifications() : base(null)
		{
			AddInclude(p => p.ProductBrand);
			AddInclude(p => p.ProductType);
		}
	}
}
