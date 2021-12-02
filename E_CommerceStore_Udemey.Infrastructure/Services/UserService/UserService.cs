using AutoMapper;
using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.Exceptions;
using E_CommerceStore_Udemey.Core.ViewModels;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.DATA.Models;
using E_CommerceStore_Udemey.Infrastructure.Services;
using E_CommerceStore_Udemey.Infrastructure.Services.FileSerice;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;


        public UserService(ApplicationDbContext db,UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;

        }

        // mrthods to retub All Ptoparites in Base Controller to View In Cpanel
        public async Task<List<UserViewModel>> GetAll()
        {

            var users = await _userManager.Users.Include(x => x.Company).Select(x => new UserViewModel()
            {
                FullName = x.FullName,
                DOB = x.DOB.ToString(),

                CompanyVm = new CompanyViewModel()
                {
                    Id = x.Company.Id,
                    Name = x.Company.Name,
                    City = x.Company.City,
                    State = x.Company.State,
                    PhoneNumber = x.PhoneNumber,
                    PostalCode = x.Company.PostalCode,
                    StreetAddress = x.Company.StreetAddress
                },
                PhoneNumber = x.PhoneNumber


            }).ToListAsync();
            if (users == null)
            {
                throw new EntityNotFoundException();
            }
  /*          return _mapper.Map<List<UserViewModel>>(users)*/;
            return users;
        }


    }
}