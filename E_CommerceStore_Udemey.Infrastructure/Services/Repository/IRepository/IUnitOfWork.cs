﻿using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category {  get; }
        ICoverTypeRepository CoverType {  get; }
        IProductRepository Product { get; }
        ICompanyRepository Company {  get; }
        IShoppingCartRepository ShoppingCart {  get; }
        IApplicationUserRepository ApplicationUser {  get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }

        void Save();
    }
}
