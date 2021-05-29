using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication1.Controllers.Models.Parameters;
using WebApplication1.Controllers.Models.ViewModels;
using WebApplication1.Controllers.Parameters;
using WebApplication1.Repository.Enities;
using WebApplication1.Service.Dtos;

namespace WebApplication1.Controllers.Infrastructure.AutoMapperProfile
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            CreateMap<NbaDto, NbaViewModel>();
            CreateMap<NbaParameter, NbaDto>();
            CreateMap<NbaPrimaryParameter, NbaPrimaryDto>();
        }
    }
}