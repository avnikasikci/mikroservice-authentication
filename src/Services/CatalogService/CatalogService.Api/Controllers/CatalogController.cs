using CatalogService.Application.Features.Catalog.Models;
using CatalogService.Application.Features.Catalog.Queries;
using Microsoft.AspNetCore.Mvc;


namespace CatalogService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            GetListCatalogQuery getListUserQuery = new() { };
            CatalogListModel result = await Mediator.Send(getListUserQuery);
            return Ok(result);
        }
    }
}
