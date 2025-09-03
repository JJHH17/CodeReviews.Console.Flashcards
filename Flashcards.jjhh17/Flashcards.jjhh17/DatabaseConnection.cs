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
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "INSERT INTO Stacks (StackName, Description) VALUES (@StackName, @Description)";
                connection.Execute(sql, new { StackName = name, Description = description });
                Console.WriteLine($"Stack '{name}' added to the database.");
            }
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
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Flashcards' and xtype='U')
                    CREATE TABLE Flashcards (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Front NVARCHAR(100),
                        Back NVARCHAR(100),
                        StackName NVARCHAR(50),
                        FOREIGN KEY (StackName) REFERENCES Stacks(StackName)
                    )";
                connection.Execute(sql);
            }
        }

        public static void AddFlashcard(string front, string back, string stackName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "INSERT INTO Flashcards (Front, Back, StackName) VALUES (@Front, @Back, @StackName)";
                connection.Execute(sql, new { Front = front, Back = back, StackName = stackName });
            }
        }

        public static List<Flashcards> ReturnFlashcards(string stackName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Flashcards WHERE StackName = @stackName";
                var cards = connection.Query<Flashcards>(sql, new { stackName }).ToList();
                return cards;
            }
        }

        public static bool FlashcardExists(string front)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "SELECT COUNT(1) FROM Flashcards WHERE Front = @Front";
                int count = connection.ExecuteScalar<int>(sql, new { Front = front });
                return count > 0;
            }
        }

        public static void DeleteFlashcard(string front)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "DELETE FROM Flashcards WHERE Front = @Front";
                connection.Execute(sql, new { Front = front });
            }
        }

        public static void DeleteStack(string stackName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Flashcard deletion
                using (var deleteFlashcards = new SqlCommand(
                    "DELETE FROM Flashcards WHERE Stackname = @StackName", connection))
                {
                    deleteFlashcards.Parameters.AddWithValue("StackName", stackName);
                    deleteFlashcards.ExecuteNonQuery();
                }

                // Stacks deletion
                using (var deleteStacks = new SqlCommand(
                    "DELETE FROM Stacks WHERE StackName = @StackName", connection))
                {
                    deleteStacks.Parameters.AddWithValue("@StackName", stackName);
                    deleteStacks.ExecuteNonQuery();
                }
            }
        }

        public static void StudyAreaTableCreation()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'StudyArea' and xtype='U')
                    CREATE TABLE StudyArea (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        Date NVARCHAR(100),
                        Score INT,
                        StackName NVARCHAR(50),
                        FOREIGN KEY (StackName) REFERENCES Stacks(StackName)
                        )";
                connection.Execute(sql);
            }
        }

        public static void AddStudySession(string date, int score, string stackName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "INSERT INTO StudyArea (Date, Score, StackName) VALUES (@Date, @Score, @StackName)";
                connection.Execute(sql, new { Date = date, score = score, StackName = stackName });
            }
        }
    }
}
