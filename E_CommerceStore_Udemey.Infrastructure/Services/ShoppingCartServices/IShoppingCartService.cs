using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.Helpers;
using E_CommerceStore_Udemey.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services.ShoppingCartServices
{
    public interface IShoppingCartService
    {
        //Task<ResponseDto> GetAll(Pagination pagination, Query query);

        //Task<List<CategoryViewModel>> GetCategoryName();
        //Task<int> Create(CreateCategoryDto dto);

        //Task<int> Update(UpdateCategoryDto dto);

        //Task<UpdateCategoryDto> Get(int Id);

        //Task<int> Delete(int Id);
        //Task<byte[]> ExportToExcel();



        Task<List<ShoppingCartViewModel>> GetAll();
        //Task<ShoppingCartViewModel> Get(int Id);


    }
}
