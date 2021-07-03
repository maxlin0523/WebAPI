using System.Collections.Generic;
using System.Web.Http;
using WebApplication1.Controllers.Parameters;
using WebApplication1.Controllers.Models.ViewModels;
using WebApplication1.Repository.Helpers;
using WebApplication1.Controllers.Models.Parameters;
using WebApplication1.Service.Interfaces;
using AutoMapper;
using WebApplication1.Service.Dtos;
using WebApplication1.Service.Misc;

namespace WebApplication1.Controllers
{
    [RoutePrefix("Nba")]
    public class NbaController : ApiController
    {
        private readonly INbaService _nbaService;

        private readonly IMapper _mapper;

        public NbaController(INbaService nbaService, IMapper mapper)
        {
            _nbaService = nbaService;
            _mapper = mapper;
        }

        [Route("All")]
        [HttpGet]
        public IEnumerable<NbaViewModel> GetAll()
        {
            var result = _nbaService.GetAll();
            return _mapper.Map<IEnumerable<NbaViewModel>>(result);
        }

        [Route()]
        [HttpGet]
        public NbaViewModel GetByName([FromUri] NbaPrimaryParameter param)
        {
            var primaryDto = _mapper.Map<NbaPrimaryDto>(param);
            var result = _nbaService.GetByName(primaryDto);
            return _mapper.Map<NbaViewModel>(result);
        }

        [Route()]
        [HttpPost]
        public IResult Post([FromBody] NbaParameter param)
        {
            var dto = _mapper.Map<NbaDto>(param);
            var result = _nbaService.Post(dto);
            return result;
        }

        [Route()]
        [HttpPut]
        public IResult Put([FromBody] NbaParameter param)
        {
            var dto = _mapper.Map<NbaDto>(param);
            var result = _nbaService.Put(dto);
            return result;
        }

        [Route()]
        [HttpDelete]
        public IResult Delete([FromUri] NbaPrimaryParameter param)
        {
            var primaryDto = _mapper.Map<NbaPrimaryDto>(param);
            var result = _nbaService.Delete(primaryDto);
            return result;
        }
    }
}
