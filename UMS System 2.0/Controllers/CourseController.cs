using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UMS_System_2._0.Controllers
{
    public static class CourseController
    {
        public static void AddCourse(string courseName)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    var cmd = new SQLiteCommand("INSERT INTO Courses (CourseName) VALUES (@name)", conn);
                    cmd.Parameters.AddWithValue("@name", courseName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding course: " + ex.Message);
            }
        }

        public static void UpdateCourse(int courseId, string newName)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Error updating course: " + ex.Message);
            }
        }

        public static void DeleteCourse(int courseId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    var cmd = new SQLiteCommand("DELETE FROM Courses WHERE CourseID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", courseId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting course: " + ex.Message);
            }
        }

        public static List<Course> GetAllCourses()
        {
            var list = new List<Course>();
            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching courses: " + ex.Message);
            }
            return list;
        }

        internal static bool CourseExists(string name)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Error checking if course exists: " + ex.Message);
                return false;
            }
        }
    }
}
