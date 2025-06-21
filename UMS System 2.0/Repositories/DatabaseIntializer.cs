using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UMS_System_2._0.Models;

namespace UMS_System_2._0.Repositories
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase()
        {
            string dbFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");
            string dbPath = Path.Combine(dbFolderPath, "unicomtic.db");

            // Ensure folder exists
            if (!Directory.Exists(dbFolderPath))
            {
                Directory.CreateDirectory(dbFolderPath);
                Console.WriteLine("📁 Database folder created.");
            }

            // If DB does not exist, create and run SQL
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                Console.WriteLine("✅ Database created at: " + dbPath);

                string sqlPath = Path.Combine(dbFolderPath, "SQL", "CreateTables.sql");

                if (File.Exists(sqlPath))
                {
                    string createTableQuery = File.ReadAllText(sqlPath);

                    using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                    {
                        connection.Open();
                        using (var cmd = new SQLiteCommand(createTableQuery, connection))
                        {
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("✅ Tables created from SQL file.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("❌ SQL file not found at: " + sqlPath);
                }
            }
            else
            {
                Console.WriteLine("✔️ Database already exists at: " + dbPath);
            }
        }
    } 
}
