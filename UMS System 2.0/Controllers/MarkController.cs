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
    public static class MarkController
    {
        public static List<Mark> GetAllMarks()
        {
            var marks = new List<Mark>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT MarkID, StudentID, ExamID, Score FROM Marks";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            marks.Add(new Mark
                            {
                                MarkID = Convert.ToInt32(reader["MarkID"]),
                                StudentID = Convert.ToInt32(reader["StudentID"]),
                                ExamID = Convert.ToInt32(reader["ExamID"]),
                                Score = (int)Convert.ToDouble(reader["Score"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error loading marks:\n" + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return marks;
        }

        public static void AddMark(int studentId, int examId, double score)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO Marks (StudentID, ExamID, Score) VALUES (@studentId, @examId, @score)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        cmd.Parameters.AddWithValue("@examId", examId);
                        cmd.Parameters.AddWithValue("@score", score);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("✅ Mark added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error adding mark:\n" + ex.Message, "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void UpdateMark(int markId, int studentId, int examId, double score)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Marks SET StudentID = @studentId, ExamID = @examId, Score = @score WHERE MarkID = @id";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        cmd.Parameters.AddWithValue("@examId", examId);
                        cmd.Parameters.AddWithValue("@score", score);
                        cmd.Parameters.AddWithValue("@id", markId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("✅ Mark updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error updating mark:\n" + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DeleteMark(int markId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Marks WHERE MarkID = @id";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", markId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("✅ Mark deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error deleting mark:\n" + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
