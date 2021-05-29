using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Repository.Enities;

namespace WebApplication1.Repository.Interfaces
{
    public interface INbaRepository
    {
        IEnumerable<Nba> GetAll();

        Nba GetByName(NbaPrimary param);

        int Put(Nba dto);

        int Post(Nba dto);

        int Delete(NbaPrimary dto);

        bool IsExist(string name);
    }
}
