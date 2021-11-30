using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.ViewModels;
using E_CommerceStore_Udemey.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services.ProductService
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAll();
        Task<int> Create(CreateProductDto dto);
        Task<int> Update(UpdateProductDto dto);
        Task<UpdateProductDto> Get(int Id);
        Task<int> Delete(int id);

    }
}
