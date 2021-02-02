using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class Paramter
    {
        /// <summary>
        /// 主KEY 索引
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否使用Dapper
        /// </summary>
        public bool IsDP { get; set; }

        /// <summary>
        /// Json資料
        /// </summary>
        public string Json { get; set; }
    }
}