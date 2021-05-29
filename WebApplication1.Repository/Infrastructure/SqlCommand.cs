using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Repository.Infrastructure
{
    /// <summary>
    /// 類別反射出SQL語法 預設class name為table name
    /// </summary>
    public class SqlCommand
    {
        /// <summary>
        /// Insert語法組裝
        /// </summary>
        public static string Insert<T>() where T : class
        {
            var type = typeof(T);

            var props = type.GetProperties();

            // 遍歷陣列取得欄位、欄位值加入內存
            var columns = props.Select(prop =>
                prop.Name);

            var values = props.Select(prop =>
                $"@{prop.Name}");

            // 欄位
            var column = string.Join(", ", columns);

            // 欄位參數
            var value = string.Join(", ", values);

            var table = type.Name;

            // 組裝
            var sql = new StringBuilder();
            sql.AppendLine($"INSERT INTO dbo.{table}");
            sql.AppendLine($"( {column} )");
            sql.AppendLine($"VALUES");
            sql.AppendLine($"( {value} )");
            return sql.ToString();
        }

        /// <summary>
        /// Update語法組裝
        /// </summary>
        public static string Update<T>(object conditionalParam = null) where T : class
        {
            var type = typeof(T);

            // 遍歷陣列取得欄位並組裝加入內存
            var columns = type.GetProperties()
                .Select(prop => $"{prop.Name} = @{prop.Name}").ToList();

            // 條件式內存
            var condition = new List<string>();

            // 檢查有無條件式參數
            if (conditionalParam != null)
            {
                // 遍歷陣列取得條件式並組裝加入條件式內存
                condition = conditionalParam.GetType()
                    .GetProperties()
                    .Select(prop => $"{prop.Name} = @{prop.Name}").ToList();

                // 不需要再把條件式欄位也update，故從更新欄位內存中排除
                columns = columns.Except(condition).ToList();

                // 條件式前面需加入WHERE
                condition[0] = $"WHERE {condition[0]} ";
            }

            // 欄位
            var column = string.Join(", ", columns);

            // 條件式
            var where = string.Join("AND ", condition);

            var table = type.Name;

            // 組裝
            var sql = new StringBuilder();
            sql.AppendLine($"UPDATE dbo.{table}");
            sql.AppendLine($"SET");
            sql.AppendLine($"{column}");
            sql.AppendLine($"{where}");
            return sql.ToString();
        }

        /// <summary>
        /// Select語法組裝
        /// </summary>
        public static string Select<T>(object conditionalParam = null) where T : class
        {
            var type = typeof(T);

            // 遍歷陣列取得欄位並加入內存
            var columns = type.GetProperties()
                .Select(prop => prop.Name);

            // 條件式內存
            var condition = new List<string>();

            // 檢查有無條件式參數
            if (conditionalParam != null)
            {
                // 遍歷陣列取得條件式並組裝加入條件式內存
                condition = conditionalParam.GetType()
                    .GetProperties()
                    .Select(prop => $"{prop.Name} = @{prop.Name}").ToList();

                // 條件式前面需加入WHERE
                condition[0] = $"WHERE {condition[0]} ";
            }

            // 欄位
            var column = string.Join(", ", columns);

            // 條件式
            var where = string.Join("AND ", condition);

            var table = type.Name;

            // 組裝
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine($"{column}");
            sql.AppendLine($"FROM dbo.{table}");
            sql.AppendLine($"{where}");
            return sql.ToString();
        }

        /// <summary>
        /// Delete語法組裝
        /// </summary>
        public static string Delete<T>(object conditionalParam = null) where T : class
        {
            var type = typeof(T);
            // 條件式內存
            var condition = new List<string>();

            // 表示有條件式參數
            if (conditionalParam != null)
            {
                // 遍歷陣列取得條件式並組裝加入條件式內存
                condition = conditionalParam.GetType()
                    .GetProperties()
                    .Select(prop => $"{prop.Name} = @{prop.Name}").ToList();

                // 條件式前面需加入WHERE
                condition[0] = "WHERE " + condition[0];
            }

            // 條件式
            var where = string.Join("AND ", condition);

            var table = type.Name;

            // 組裝
            var sql = new StringBuilder();
            sql.AppendLine($"DELETE FROM dbo.{table}");
            sql.AppendLine($"{where}");
            return sql.ToString();
        }
    }
}
