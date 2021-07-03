using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Repository.Infrastructure
{
    /// <summary>
    /// 根據反射類別，映射出SQL語法。
    /// ※資料表名稱預設為類別名
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

            // 取得欄位加入內存
            var columns = props.Select(prop =>
                prop.Name);

            var values = props.Select(prop =>
                $"@{prop.Name}");

            // 欄位
            var columnString = string.Join(",\r\n", columns);

            // 欄位參數
            var valueString = string.Join(",\r\n", values);

            var table = type.Name;

            // 組裝
            var sql = new StringBuilder();
            sql.AppendLine($"INSERT INTO dbo.{table}");
            sql.AppendLine($"(");
            sql.AppendLine(columnString);
            sql.AppendLine($")");
            sql.AppendLine($"VALUES");
            sql.AppendLine($"(");
            sql.AppendLine(valueString);
            sql.AppendLine($")");
            return sql.ToString();
        }

        /// <summary>
        /// Update語法組裝
        /// </summary>
        public static string Update<T>(object param = null)
            where T : class
        {
            var type = typeof(T);

            // 取得欄位並組裝加入內存
            var columns = type.GetProperties()
                .Select(prop => $"{prop.Name} = @{prop.Name}")
                .ToList();

            // 條件式內存
            var conditions = new List<string>();

            // 檢查有無條件式
            if (param != null)
            {
                // 取得條件式並組裝加入條件式內存
                conditions = param.GetType()
                    .GetProperties()
                    .Select(prop => $"{prop.Name} = @{prop.Name}")
                    .ToList();

                // 條件式參數不需更新，故排除
                columns = columns.Except(conditions).ToList();

                // 條件式前面加上WHERE
                conditions[0] = $"WHERE {conditions[0]}";
            }

            // 欄位
            var columnString = string.Join(",\r\n", columns);

            // 條件式
            var conditionString = string.Join("\r\nAND ", conditions);

            var table = type.Name;

            // 組裝
            var sql = new StringBuilder();
            sql.AppendLine($"UPDATE dbo.{table}");
            sql.AppendLine($"SET");
            sql.AppendLine(columnString);
            sql.AppendLine(conditionString);
            return sql.ToString();
        }

        /// <summary>
        /// Select語法組裝
        /// </summary>
        public static string Select<T>(object param = null)
            where T : class
        {
            var type = typeof(T);

            // 取得欄位並加入內存
            var columns = type.GetProperties()
                .Select(prop => prop.Name);

            // 條件式內存
            var conditions = new List<string>();

            // 檢查有無條件式參數
            if (param != null)
            {
                // 取得條件式並組裝加入條件式內存
                conditions = param.GetType()
                    .GetProperties()
                    .Select(prop => $"{prop.Name} = @{prop.Name}")
                    .ToList();

                // 條件式前加上WHERE
                conditions[0] = $"WHERE {conditions[0]}";
            }

            var columnString = string.Join(",\r\n", columns);

            var conditionString = string.Join("\r\nAND ", conditions);

            var table = type.Name;

            // 組裝
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine(columnString);
            sql.AppendLine($"FROM dbo.{table}");
            sql.AppendLine(conditionString);
            return sql.ToString();
        }

        /// <summary>
        /// Delete語法組裝
        /// </summary>
        public static string Delete<T>(object param = null)
            where T : class
        {
            var type = typeof(T);

            // 條件式內存
            var conditions = new List<string>();

            // 判斷有無條件式參數
            if (param != null)
            {
                // 取得條件式並組裝加入條件式內存
                conditions = param.GetType()
                    .GetProperties()
                    .Select(prop => $"{prop.Name} = @{prop.Name}")
                    .ToList();

                // 條件式前加上WHERE
                conditions[0] = "WHERE " + conditions[0];
            }

            var conditionString = string.Join("\r\nAND ", conditions);

            var table = type.Name;

            // 組裝
            var sql = new StringBuilder();
            sql.AppendLine($"DELETE FROM dbo.{table}");
            sql.AppendLine(conditionString);
            return sql.ToString();
        }
    }
}
