using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services.CoverTypeServices
{
    public interface ICoverTypeService
    {
        Task<List<CoverTypeViewModel>> GetAll();
        Task<UpdateCoverTypeDto> Get(int Id);
        Task<int> Create(CreateCoverTypeDto dto);
        Task<int> Update(UpdateCoverTypeDto dto);
        Task<int> Delete(UpdateCoverTypeDto dto);
        Task<List<CoverTypeViewModel>> GetCoverTypeName();
    }
}
