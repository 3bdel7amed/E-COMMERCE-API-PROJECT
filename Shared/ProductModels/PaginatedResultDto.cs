﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ProductModels
{
    public record PaginatedResultDto<TData>(int pageSize, int pageIndex, int totalCount, IEnumerable<TData> Data);

}
