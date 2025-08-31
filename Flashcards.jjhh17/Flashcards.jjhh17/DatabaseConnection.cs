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
        }

        public static void AddStack(string name, string description)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO Stacks (StackName, Description) VALUES (@StackName, @Description)";
            connection.Execute(sql, new { StackName = name, Description = description });
            Console.WriteLine($"Stack '{name}' added to the database.");
        }

        public static List<Stacks> ReturnAllStacks()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Stacks";
                var stacks = connection.Query<Stacks>(sql).ToList();
                return stacks;
            }
        }

        public static bool StackExists(string name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "SELECT COUNT(1) FROM Stacks WHERE StackName = @StackName";
                int count = connection.ExecuteScalar<int>(sql, new { StackName = name });
                return count > 0;
            }
        }

        public static void FlashcardTableCreation()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();

            var sql = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Flashcards' and xtype='U')
                    CREATE TABLE Flashcards (
                        Id INT PRIMARY KEY,
                        Front NVARCHAR(100),
                        Back NVARCHAR(100),
                        StackName NVARCHAR(50),
                        FOREIGN KEY (StackName) REFERENCES Stacks(StackName)
                    )";
            connection.Execute(sql);
        }

        public static void AddFlashcard(long id, string front, string back, string stackName)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO Flashcards (Id, Front, Back, StackName) VALUES (@id, @front, @back, @stackName)";
            connection.Execute(sql, new { Id = id, Front = front, Back = back, StackName = stackName });
            Console.WriteLine($"Flashcard added to stack '{stackName}'.");
        }
    }
}
