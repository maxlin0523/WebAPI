using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebApplication1.Controllers;
using WebApplication1.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Dapper;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public static SqlConnection conn = new SqlConnection(PlayerController.db_NBA);
        [TestMethod]
        public void TestMethod1()
        {
            var name = "A";
            var team = "ASSS_TEAM";
            var position = "C";
            var str =
            $@"
            UPDATE [dbo].[NBA]
               SET [Name] = '{name}'
                  ,[Team] = '{team}'
                  ,[Position] = '{position}'
               WHERE Name = '{name}'";

            var s = conn.Execute(str);



            Assert.AreEqual(1, 1);
        }
    }
}
