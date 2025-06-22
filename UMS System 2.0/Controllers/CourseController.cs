using System;
using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public static class CourseController
    {
        public static void AddCourse(string courseName)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO Courses (CourseName) VALUES (@name)", conn);
                cmd.Parameters.AddWithValue("@name", courseName);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateCourse(int courseId, string newName)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE Courses SET CourseName = @name WHERE CourseID = @id", conn);
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@id", courseId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteCourse(int courseId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM Courses WHERE CourseID = @id", conn);
                cmd.Parameters.AddWithValue("@id", courseId);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Course> GetAllCourses()
        {
            var list = new List<Course>();
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Courses", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Course
                    {
                        CourseID = Convert.ToInt32(reader["CourseID"]),
                        CourseName = reader["CourseName"].ToString()
                    });
                }
            }
            return list;
        }

        internal static bool CourseExists(string name)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Courses WHERE LOWER(CourseName) = LOWER(@name)", conn);
                cmd.Parameters.AddWithValue("@name", name);
                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
