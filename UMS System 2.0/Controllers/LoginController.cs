using System.Data.SQLite;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public static class LoginController
    {
        public static string CheckLogin(string username, string password)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                string query = "SELECT Role FROM Users WHERE Username = @username AND Password = @password";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    var result = cmd.ExecuteScalar();
                    return result?.ToString(); // Returns role or null
                }
            }
        }




        public static bool IsUserExists(string username)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0; // Returns true if user exists
                }
            }
        }
    }
}
