using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public static class SubjectController
    {
        public static void AddSubject(string subjectName, int courseId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO Subjects (SubjectName, CourseID) VALUES (@name, @course)", conn);
                cmd.Parameters.AddWithValue("@name", subjectName);
                cmd.Parameters.AddWithValue("@course", courseId);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Subject> GetAllSubjects()
        {
            var list = new List<Subject>();
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Subjects", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Subject
                    {
                        SubjectID = int.Parse(reader["SubjectID"].ToString()),
                        SubjectName = reader["SubjectName"].ToString(),
                        CourseID = int.Parse(reader["CourseID"].ToString())
                    });
                }
            }
            return list;
        }

        public static void UpdateSubject(int id, string newName, int courseId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE Subjects SET SubjectName = @name, CourseID = @course WHERE SubjectID = @id", conn);
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@course", courseId);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteSubject(int id)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM Subjects WHERE SubjectID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
