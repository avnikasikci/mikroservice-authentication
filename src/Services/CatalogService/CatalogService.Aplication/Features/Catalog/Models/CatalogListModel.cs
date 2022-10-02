

using CatalogService.Application.Features.User.Dto;
using Core.Persistence.Paging;

namespace CatalogService.Application.Features.Catalog.Models
{
    public class CatalogListModel : BasePageableModel
    {
        public IList<CatalogListDto> Items { get; set; }

        //
    }
}
