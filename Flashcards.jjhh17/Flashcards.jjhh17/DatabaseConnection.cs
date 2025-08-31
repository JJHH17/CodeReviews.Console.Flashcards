using System.Data.SqlClient;
using System.Net.NetworkInformation;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.IO;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace Flashcards.jjhh17
{
    public class DatabaseConnection
    {

        private static readonly string server = ConfigurationManager.AppSettings["Server"];
        private static readonly string instance = ConfigurationManager.AppSettings["DatabaseName"];
        public static string connectionString = $@"Server=({server})\{instance};Integrated Security=true;";

        public static void StackTableCreation()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();

            var sql = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Stacks' and xtype='U')
                    CREATE TABLE Stacks (
                        StackName NVARCHAR(50) PRIMARY KEY,
                        Description NVARCHAR(100),
                    )";
            connection.Execute(sql);
            Console.WriteLine("Table 'Stacks' created.");
        }

        public static void AddStack(string name, string description)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO Stacks (StackName, Description) VALUES (@StackName, @Description)";
            connection.Execute(sql, new { StackName = name, Description = description });
            Console.WriteLine($"Stack '{name}' added to the database.");
        }
    }
}
