using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Service.Dtos;
using WebApplication1.Service.Misc;

namespace WebApplication1.Service.Interfaces
{
    public interface INbaService
    {
        IEnumerable<NbaDto> GetAll();

        NbaDto GetByName(NbaPrimaryDto param);

        IResult Put(NbaDto dto);

        IResult Post(NbaDto dto);

        IResult Delete(NbaPrimaryDto dto);
    }
}
