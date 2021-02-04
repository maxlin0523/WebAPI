using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RoutePrefix("player")]
    public class PlayerController : ApiController
    {
        private readonly SqlConnection _dapper;
        private readonly maxlin0523Entities _entities;
        public PlayerController()
        {
            _dapper = DataCenter.Instance.DapperNBA;
            _entities = DataCenter.Instance.EntitiesNBA;
        }

        // /player/get/true
        [Route("get/{IsDP}")]
        [HttpGet]
        public IEnumerable<NBA> Get([FromUri] Paramter param)
        {
            IEnumerable<NBA> result = null;

            var dpStr = "SELECT * FROM [dbo].[NBA]";

            if (param.IsDP)
            {
                result = _dapper.Query<NBA>(dpStr);
            }
            else
            {
                result = _entities.NBA.ToList();
            }
            return result;
        }

        [Route("get/{IsDP}/{Name}")]
        [HttpGet]
        public IEnumerable<NBA> GetByName([FromUri] Paramter param)
        {
            IEnumerable<NBA> result = null;
            var exception = string.Empty;

            try
            {
                if (param.IsDP)
                {
                    var dpStr = $"SELECT * FROM [dbo].[NBA] WHERE Name = @Name";
                    result = _dapper.Query<NBA>(dpStr, new  { param.Name});
                }
                else
                {
                    result = _entities.NBA.Where(c => c.Name == param.Name);
                }
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
            }
            return result;
        }

        // https://localhost:44355/player/post/{"name": "A", "position": "SG", "team": "A_TEAM"}/true
        // {"name": "A", "position": "SG", "team": "A_TEAM"}
        [Route("post/{IsDP}/{Json}")]
        [HttpGet]
        public string Post([FromUri] Paramter param)
        {
            var result = 1;
            var exception = string.Empty;

            try
            {
                var jsonObject = JsonConvert.DeserializeObject<NBA>(param.Json);
                if (param.IsDP)
                {
                    var dpStr = @"
                     INSERT INTO [dbo].[NBA]
                                ([Name]
                                ,[Team]
                                ,[Position])
                          VALUES
                                (@Name
                                ,@Team
                                ,@Position)";
                    result = _dapper.Execute(dpStr, jsonObject);
                }
                else
                {
                    _entities.NBA.Add(jsonObject);
                    _entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                result = 0;
            }

            return result == 1 ? "SUCCESS" : $"FAIL\n{exception}";
        }

        [Route("put/{IsDP}/{Json}")]
        [HttpGet]
        public string Put([FromUri] Paramter param)
        {
            var result = 1;
            var exception = string.Empty;

            try
            {
                var jsonObject = JsonConvert.DeserializeObject<NBA>(param.Json);
                if (param.IsDP)
                {
                    var datas = new { Name = jsonObject.Name, Team = jsonObject.Team, Position = jsonObject.Position };
                    var dpStr =@"
                     UPDATE [dbo].[NBA]
                        SET [Name] = @Name
                           ,[Team] = @Team
                           ,[Position] = @Position
                     WHERE Name = @Name";
                    result = _dapper.Execute(dpStr, datas);
                }
                else
                {
                    var data = _entities.NBA.Find(jsonObject.Name);
                    data.Name = jsonObject.Name;
                    data.Position = jsonObject.Position;
                    data.Team = jsonObject.Team;
                    _entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                result = 0;
            }

            return result == 1 ? "SUCCESS" : $"FAIL\n{exception}";
        }

        [Route("delete/{IsDP}/{Name}")]
        [HttpGet]
        public string Delete([FromUri] Paramter param)
        {
            var result = 1;
            var exception = string.Empty;

            try
            {
                if (param.IsDP)
                {
                    var dpStr = $"DELETE FROM [dbo].[NBA] WHERE Name = @Name";
                    result = _dapper.Execute(dpStr, new { Name = param.Name });
                }
                else
                {
                    var data = _entities.NBA.Single(c => c.Name == param.Name);
                    _entities.NBA.Remove(data);
                    _entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                result = 0;
            }

            return result == 1 ? "SUCCESS" : $"FAIL\n{exception}";
        }
    }
}
