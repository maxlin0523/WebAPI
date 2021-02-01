using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DataCenter
    {       
        private static DataCenter _instance = new DataCenter();

        private DataCenter() 
        {
        }

        public static DataCenter GetInstance()
        {
            return _instance;
        }      
    }
}