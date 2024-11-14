using Domain.Contracts.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
	internal class PaginatedResultSpecifications : Specifications<Product>
	{
        public PaginatedResultSpecifications(string? sort, int? brandId, int? typeId, int pageSize, int pageIndex, string? search)
			// Handling Filtering
			: base(p =>
			(!typeId.HasValue || p.TypeId == typeId) &&
			(!brandId.HasValue || p.BrandId == brandId) &&
			(string.IsNullOrWhiteSpace(search) || p.Name.ToUpper().Contains(search.ToUpper().Trim())))
        {
        }
    }
}
