using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BasketModels
{
    public record CustomerBasketResultDto
    {
        public CustomerBasketResultDto(string basketId)
        {
            BasketId = basketId;
        }

        public string BasketId { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }
    }
}
