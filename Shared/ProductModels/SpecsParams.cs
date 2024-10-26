using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ProductModels
{
    public class SpecsParams
    {

        public string? sort { get; set; }
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize
        {
            get => pageSize;
            set => pageSize = value > 10 || value < 1 ? 10 : value;
        }


    }
}
