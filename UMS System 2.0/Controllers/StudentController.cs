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
        public static class StudentController
        {
            public static void AddStudent(string name, int courseId)
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    EnableForeignKeys(conn);

                    var cmd = new SQLiteCommand("INSERT INTO Students (Name, CourseID) VALUES (@name, @course)", conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@course", courseId);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        // Optional: log or display this message
                        throw new Exception("Error adding student: " + ex.Message);
                    }
                }
            }

            public static List<Student> GetAllStudents()
            {
                var list = new List<Student>();
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    EnableForeignKeys(conn);

                    var cmd = new SQLiteCommand("SELECT * FROM Students", conn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Student
                        {
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            Name = reader["Name"].ToString(),
                            CourseID = Convert.ToInt32(reader["CourseID"])
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
                    EnableForeignKeys(conn);

                    var cmd = new SQLiteCommand("UPDATE Students SET Name = @name, CourseID = @course WHERE StudentID = @id", conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@course", courseId);
                    cmd.Parameters.AddWithValue("@id", id);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        throw new Exception("Error updating student: " + ex.Message);
                    }
                }
            }

            public static void DeleteStudent(int id)
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    EnableForeignKeys(conn);

                    var cmd = new SQLiteCommand("DELETE FROM Students WHERE StudentID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        throw new Exception("Error deleting student: " + ex.Message);
                    }
                }
            }

            private static void EnableForeignKeys(SQLiteConnection conn)
            {
                var fkCmd = new SQLiteCommand("PRAGMA foreign_keys = ON;", conn);
                fkCmd.ExecuteNonQuery();
            }
        }
    }
