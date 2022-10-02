using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Features.Product.Queries;
using ProductService.Application.Features.User.Models;

namespace ProductService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            GetListProductQuery getListUserQuery = new() { };
            ProductListModel result = await Mediator.Send(getListUserQuery);
            return Ok(result);
        }
    }
}
