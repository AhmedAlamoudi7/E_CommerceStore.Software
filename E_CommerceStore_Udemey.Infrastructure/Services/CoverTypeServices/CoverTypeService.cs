using AutoMapper;
using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.ViewModels;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.DATA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services.CoverTypeServices
{
    public class CoverTypeService :ICoverTypeService
    {

        private readonly ApplicationDbContext _Db;
        private readonly IMapper _mapper;

        public CoverTypeService(ApplicationDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }


        public async Task<List<CoverTypeViewModel>> GetAll()
        {
            var cover = await _Db.CoverTypes.ToListAsync();
            var mapper = _mapper.Map<List<CoverTypeViewModel>> (cover);
            return mapper;
        }


        public async Task<UpdateCoverTypeDto> Get(int Id) 
        {
            var cover = await _Db.CoverTypes.SingleOrDefaultAsync(x => x.Id == Id);
            var mapper = _mapper.Map<UpdateCoverTypeDto>(cover);
            return mapper;
        }

        public async Task<int> Create(CreateCoverTypeDto dto)
        {
            var mapper = _mapper.Map<CoverType>(dto);
           await _Db.CoverTypes.AddAsync(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id;
        }


        public async Task<int> Update(UpdateCoverTypeDto dto)
        {
            var coverType = await _Db.CoverTypes.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var mapper = _mapper.Map<UpdateCoverTypeDto,CoverType>(dto,coverType);
             _Db.CoverTypes.Update(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id;
        }


        public async Task<int> Delete(UpdateCoverTypeDto dto)
        {
            var coverType = await _Db.CoverTypes.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var mapper = _mapper.Map<UpdateCoverTypeDto, CoverType>(dto, coverType);
             _Db.CoverTypes.Remove(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id;
        }


    }
}
