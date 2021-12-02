using E_CommerceStore_Udemey.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAll();

    }
}
