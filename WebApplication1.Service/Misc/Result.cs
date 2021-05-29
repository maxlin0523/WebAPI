using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Service.Misc
{
    public class Result : IResult
    {
        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
