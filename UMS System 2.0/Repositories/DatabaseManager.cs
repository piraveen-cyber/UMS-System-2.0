using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS_System_2._0.Models;

namespace UMS_System_2._0.Repositories
{
    public sealed class DatabaseManager
    {
        private static readonly Lazy<DatabaseManager> _instance = new Lazy<DatabaseManager>(() => new DatabaseManager());

        public static DatabaseManager Instance => _instance.Value;

        private SQLiteConnection _connection;

        private DatabaseManager()
        {
            string dbPath = "unicomtic.db";
            if (!File.Exists(dbPath))
                SQLiteConnection.CreateFile(dbPath);

            _connection = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            _connection.Open();

            // Fire and forget (create tables)
            _ = CreateTablesAsync();
        }

        private async Task CreateTablesAsync()
        {
            string usersTable = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NOT NULL,
                        Password TEXT NOT NULL,
                        Role TEXT NOT NULL
                    );";

            using (var cmd = new SQLiteCommand(usersTable, _connection))
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";

            using (var cmd = new SQLiteCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new User
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Role = reader["Role"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}
