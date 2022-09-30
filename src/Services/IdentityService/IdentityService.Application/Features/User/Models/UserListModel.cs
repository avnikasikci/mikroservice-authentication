using Core.Persistence.Paging;
using IdentityService.Application.Features.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Features.User.Models
{
    public class UserListModel : BasePageableModel
    {
        public IList<UserListDto> Items { get; set; }

        //
    }
}
