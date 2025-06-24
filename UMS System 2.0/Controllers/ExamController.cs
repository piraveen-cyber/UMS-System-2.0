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
    public static class ExamController
    {
        public static List<Exam> GetAllExams()
        {
            var exams = new List<Exam>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT ExamID, ExamName, SubjectID FROM Exams";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            exams.Add(new Exam
                            {
                                ExamID = Convert.ToInt32(reader["ExamID"]),
                                ExamName = reader["ExamName"].ToString(),
                                SubjectID = Convert.ToInt32(reader["SubjectID"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error loading exams:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return exams;
        }

        public static void AddExam(string examName, int subjectId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO Exams (ExamName, SubjectID) VALUES (@name, @subjectId)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", examName);
                        cmd.Parameters.AddWithValue("@subjectId", subjectId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("✅ Exam added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error adding exam:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void UpdateExam(int examId, string examName, int subjectId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Exams SET ExamName = @name, SubjectID = @subjectId WHERE ExamID = @id";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", examName);
                        cmd.Parameters.AddWithValue("@subjectId", subjectId);
                        cmd.Parameters.AddWithValue("@id", examId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("✅ Exam updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error updating exam:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DeleteExam(int examId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Exams WHERE ExamID = @id";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", examId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("✅ Exam deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error deleting exam:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
