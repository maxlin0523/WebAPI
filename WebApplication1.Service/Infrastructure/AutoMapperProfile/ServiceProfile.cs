using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication1.Repository.Enities;
using WebApplication1.Service.Dtos;

namespace WebApplication1.Service.Infrastructure.AutoMapperProfile
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<NbaPrimaryDto, NbaPrimary>();
            CreateMap<Nba, NbaDto>().ReverseMap();
        }
    }
}
