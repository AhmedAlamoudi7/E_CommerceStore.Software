using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.Helpers;
using E_CommerceStore_Udemey.Core.ViewModels;
using E_CommerceStore_Udemey.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services.CompanyServices
{
    public interface ICompanyService
    {
        Task<List<CompanyViewModel>> GetAll();
        Task<int> Create(CreateCompanyDto dto);
        Task<int> Update(UpdateCompanyDto dto);
        Task<UpdateCompanyDto> Get(int Id);
        Task<int> Delete(UpdateCompanyDto dto);
        Task<List<CompanyViewModel>> GetCompanyName();


    }
}
