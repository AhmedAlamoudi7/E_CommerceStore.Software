using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.Helpers;
using E_CommerceStore_Udemey.Core.ViewModels;
using E_CommerceStore_Udemey.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services.ShoppingCartServices
{
    public interface IShoppingCartService
    {
        Task<List<ShoppingCartViewModel>> GetAll();
        Task<int> Create(CreateShoppingCartDto dto);
        Task<int> Update(UpdateShoppingCartDto dto);
        Task<UpdateShoppingCartDto> Get(int Id);
        Task<int> Delete(int id);

        int IncrementCount(ShoppingCart dto, int count);
        Task<int> DecrementCount(CreateShoppingCartDto dto, int count);





    }
}
