using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Repository.Enities;
using WebApplication1.Repository.Helpers;
using WebApplication1.Repository.Infrastructure;
using WebApplication1.Repository.Interfaces;

namespace WebApplication1.Repository.Implements
{
    public class NbaRepository : INbaRepository
    {
        private IDatabaseHelper _databaseHelper;

        public NbaRepository(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;       
        }

        public IEnumerable<Nba> GetAll()
        {
            using (var conn = _databaseHelper.GetConnection())
            {
                var sql = SqlCommand.Select<Nba>();
                var result = conn.Query<Nba>(sql);
                return result;
            }
        }

        public Nba GetByName(NbaPrimary param)
        {
            using (var conn = _databaseHelper.GetConnection())
            {
                var sql = SqlCommand.Select<Nba>(param);
                var result = conn.QueryFirst<Nba>(sql, param);
                return result;
            }
        }

        public int Put(Nba param)
        {
            using (var conn = _databaseHelper.GetConnection())
            {
                var sql = SqlCommand.Update<Nba>(new { param.Name });
                var result = conn.Execute(sql, param);
                return result;
            }
        }

        public int Post(Nba param)
        {
            using (var conn = _databaseHelper.GetConnection())
            {
                var sql = SqlCommand.Insert<Nba>();
                var result = conn.Execute(sql,param);
                return result;
            }
        }

        public int Delete(NbaPrimary param)
        {
            using (var conn = _databaseHelper.GetConnection())
            {
                var sql = SqlCommand.Delete<Nba>(param);
                var result = conn.Execute(sql, param);
                return result;
            }
        }

        public bool IsExist(string name)
        {
            using (var conn = _databaseHelper.GetConnection())
            {
                var param = new { Name = name };
                var sql = "SELECT COUNT(Name) FROM [dbo].[Nba] WHERE Name = @Name";
                var result = conn.QueryFirstOrDefault<int>(sql, param);
                return result > 0;
            }
        }
    }
}
