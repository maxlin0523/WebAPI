using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Service.Misc
{
    public interface IResult
    {
        bool Success { get; set; }

        string Message { get; set; }
    }
}
