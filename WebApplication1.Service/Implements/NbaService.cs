using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Repository.Enities;
using WebApplication1.Repository.Interfaces;
using WebApplication1.Service.Dtos;
using WebApplication1.Service.Interfaces;
using WebApplication1.Service.Misc;

namespace WebApplication1.Service.Implements
{
    public class NbaService : INbaService
    {
        private readonly INbaRepository _nbaRepository;
        private readonly IMapper _mapper;

        public NbaService(INbaRepository nbaRepository, IMapper mapper)
        {
            _nbaRepository = nbaRepository;
            _mapper = mapper;
        }

        public IEnumerable<NbaDto> GetAll()
        {
            var result = _nbaRepository.GetAll();

            return _mapper.Map<IEnumerable<NbaDto>>(result);      
        }

        public NbaDto GetByName(NbaPrimaryDto param)
        {
            var nbaPrimary = _mapper.Map<NbaPrimary>(param);

            var result = _nbaRepository.GetByName(nbaPrimary);

            return _mapper.Map<NbaDto>(result);        
        }

        public IResult Put(NbaDto dto)
        {
            var result = new Result(false);

            if (!_nbaRepository.IsExist(dto.Name))
            {
                result.Message = "此球員不存在";
                return result;
            }

            var nba = _mapper.Map<Nba>(dto);

            var flag = _nbaRepository.Put(nba);
            if (flag.Equals(1))
            {
                result.Success = true;
                result.Message = "更新成功";
                return result;
            }

            result.Message = "更新失敗";
            return result;
        
        }

        public IResult Post(NbaDto dto)
        {
            var result = new Result(false);

            if (_nbaRepository.IsExist(dto.Name))
            {
                result.Message = "此球員已存在";
                return result;
            }

            var nba = _mapper.Map<Nba>(dto);

            var flag = _nbaRepository.Post(nba);
            if (flag.Equals(1))
            {
                result.Success = true;
                result.Message = "新增成功";
                return result;
            }

            result.Message = "新增失敗";
            return result;
        }

        public IResult Delete(NbaPrimaryDto dto)
        {
            var result = new Result(false);

            if (!_nbaRepository.IsExist(dto.Name))
            {
                result.Message = "此球員不存在";
                return result;
            }

            var nbaPrimary = _mapper.Map<NbaPrimary>(dto);

            var flag = _nbaRepository.Delete(nbaPrimary);

            if (flag.Equals(1))
            {
                result.Success = true;
                result.Message = "刪除成功";
                return result;
            }

            result.Message = "刪除失敗";
            return result;
        }
    }
}
