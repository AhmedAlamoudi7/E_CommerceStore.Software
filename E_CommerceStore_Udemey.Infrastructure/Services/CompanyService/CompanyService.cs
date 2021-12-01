using AutoMapper;
using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.Exceptions;
using E_CommerceStore_Udemey.Core.Helpers;
using E_CommerceStore_Udemey.Core.ViewModels;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.DATA.Models;
using FourEstate.Infrastructure.AutoMapper;
using FourEstate.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services.CompanyServices
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _Db;
        private readonly IMapper _mapper;

        public CompanyService(ApplicationDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }

       
        public async Task<List<CompanyViewModel>> GetAll()
        {
            var company = await _Db.Companys.Select(x=>new CompanyViewModel() { 
            Id= x.Id,
            Name =x.Name,
            City =x.City,
            PhoneNumber = x.PhoneNumber,
            PostalCode =x.PostalCode,
            State = x.State,
            StreetAddress = x.StreetAddress
            }).ToListAsync();
            //var mapper = _mapper.Map<List<CategoryViewModel>>(category);

            return company;
        }
        public async Task<List<CompanyViewModel>> GetCompanyName()
        {
            var company = await _Db.Companys.ToListAsync();
            return _mapper.Map<List<CompanyViewModel>>(company);
        }


        public async Task<int> Create(CreateCompanyDto dto)
        {
            var mapper = _mapper.Map<CreateCompanyDto, Company>(dto);
            await _Db.Companys.AddAsync(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id;
        }



        public async Task<int> Update(UpdateCompanyDto dto)
        {
            var company = await _Db.Companys.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var mapper = _mapper.Map<UpdateCompanyDto, Company>(dto, company);
            _Db.Companys.Update(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id;
        }

        public async Task<UpdateCompanyDto> Get(int Id)
        {
            var company = await _Db.Companys.SingleOrDefaultAsync(x => x.Id == Id);
            //if (category == null)
            //{
            //    throw new EntityNotFoundException();
            //}
            return _mapper.Map<UpdateCompanyDto>(company);
        }

        public async Task<int> Delete(UpdateCompanyDto dto)
        {
            var company = await _Db.Companys.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var mapper = _mapper.Map<UpdateCompanyDto, Company>(dto, company);
            _Db.Companys.Remove(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id;
        }

    }
}