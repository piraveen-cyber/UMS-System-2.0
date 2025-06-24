using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UMS_System_2._0.Controllers
{
    public static class StudentController
    {
        public static void AddStudent(string name, int courseId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    var cmd = new SQLiteCommand("INSERT INTO Students (Name, CourseID) VALUES (@name, @course)", conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@course", courseId);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("✅ Student added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error adding student:\n" + ex.Message, "Add Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<Student> GetAllStudents()
        {
            var list = new List<Student>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error loading students:\n" + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;
        }

        public static void UpdateStudent(int id, string name, int courseId)
        {
            try
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
                MessageBox.Show("✅ Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error updating student:\n" + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DeleteStudent(int id)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    var cmd = new SQLiteCommand("DELETE FROM Students WHERE StudentID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("✅ Student deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error deleting student:\n" + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
