using Core.Persistence.Paging;
using ProductService.Application.Features.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Features.User.Models
{
    public class ProductListModel : BasePageableModel
    {
        public IList<ProductListDto> Items { get; set; }

        //
    }
}
