using Core.Application.Requests;
using Core.Application.Utilities;
using Core.Security.Dtos;
using IdentityService.Application.Features.Auth.Command;
using IdentityService.Application.Features.Auth.Dto;
using IdentityService.Application.Features.User.Models;
using IdentityService.Application.Features.User.Queries.GetListUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };
            UserListModel result = await Mediator.Send(getListUserQuery);
            return Ok(result);
        }
    }
}
