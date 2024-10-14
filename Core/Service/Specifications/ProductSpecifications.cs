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
		// Return By Id (Search)
		public ProductSpecifications(int id) : base(p => p.Id == id)
		{
			AddInclude(p => p.ProductBrand);
			AddInclude(p => p.ProductType);
		}
		public ProductSpecifications(string? sort, int? brandId, int? typeId, int pageSize, int pageIndex,string?search)
			// Handling Filtering
			: base(p =>
			(!typeId.HasValue || p.TypeId == typeId) &&
			(!brandId.HasValue || p.BrandId == brandId)&&
			(string.IsNullOrWhiteSpace(search)||p.Name.ToUpper().Contains(search.ToUpper().Trim())))
		{
			// Adding Includes
			AddInclude(p => p.ProductBrand);
			AddInclude(p => p.ProductType);

			// Apply Pagination
			if (pageSize > 10 || pageSize < 1) pageSize = 10;
			ApplyPagination(pageSize, pageIndex);


			// Handling Sorting
			if (!string.IsNullOrWhiteSpace(sort))
			{
				switch (sort.ToUpper().Trim())
				{
					case "NAMEDESC":
						SetOrderByDesc(p => p.Name);
						break;
					case "PRICEASC":
						SetOrderBy(p => p.Price);
						break;
					case "PRICEDESC":
						SetOrderByDesc(p => p.Price);
						break;
					default:
						SetOrderBy(p => p.Name);
						break;
				}

			}
		}
	}
}
