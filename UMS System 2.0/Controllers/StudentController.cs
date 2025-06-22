using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public static class StudentController
    {
        public static void AddStudent(string name, int courseId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO Students (Name, CourseID) VALUES (@name, @course)", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@course", courseId);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Student> GetAllStudents()
        {
            var list = new List<Student>();
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Students", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Student
                    {
                        StudentID = int.Parse(reader["StudentID"].ToString()),
                        Name = reader["Name"].ToString(),
                        CourseID = int.Parse(reader["CourseID"].ToString())
                    });
                }
            }
            return list;
        }

        public static void UpdateStudent(int id, string name, int courseId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE Students SET Name = @name, CourseID = @course WHERE StudentID = @id", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@course", courseId);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteStudent(int id)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM Students WHERE StudentID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
