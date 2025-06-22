using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public static class ExamController
    {
        public static void AddExam(string examName, int subjectId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO Exams (ExamName, SubjectID) VALUES (@name, @subject)", conn);
                cmd.Parameters.AddWithValue("@name", examName);
                cmd.Parameters.AddWithValue("@subject", subjectId);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Exam> GetAllExams()
        {
            var list = new List<Exam>();
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Exams", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Exam
                    {
                        ExamID = int.Parse(reader["ExamID"].ToString()),
                        ExamName = reader["ExamName"].ToString(),
                        SubjectID = int.Parse(reader["SubjectID"].ToString())
                    });
                }
            }
            return list;
        }

        public static void UpdateExam(int examId, string newName, int subjectId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE Exams SET ExamName = @name, SubjectID = @subject WHERE ExamID = @id", conn);
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@subject", subjectId);
                cmd.Parameters.AddWithValue("@id", examId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteExam(int examId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM Exams WHERE ExamID = @id", conn);
                cmd.Parameters.AddWithValue("@id", examId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
