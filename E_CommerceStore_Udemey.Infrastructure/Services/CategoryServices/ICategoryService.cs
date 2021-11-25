using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll();
        Task<int> Create(CreateCategoryDto dto);
        Task<int> Update(UpdateCategoryDto dto);
        Task<UpdateCategoryDto> Get(int Id);
        Task<int> Delete(UpdateCategoryDto dto);
    }
}
