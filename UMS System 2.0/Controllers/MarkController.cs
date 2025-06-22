using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public static class MarkController
    {
        public static void AddMark(int studentId, int examId, int score)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO Marks (StudentID, ExamID, Score) VALUES (@student, @exam, @score)", conn);
                cmd.Parameters.AddWithValue("@student", studentId);
                cmd.Parameters.AddWithValue("@exam", examId);
                cmd.Parameters.AddWithValue("@score", score);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Mark> GetAllMarks()
        {
            var list = new List<Mark>();
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Marks", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Mark
                    {
                        MarkID = int.Parse(reader["MarkID"].ToString()),
                        StudentID = int.Parse(reader["StudentID"].ToString()),
                        ExamID = int.Parse(reader["ExamID"].ToString()),
                        Score = int.Parse(reader["Score"].ToString())
                    });
                }
            }
            return list;
        }

        public static void UpdateMark(int markId, int studentId, int examId, int score)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE Marks SET StudentID = @student, ExamID = @exam, Score = @score WHERE MarkID = @id", conn);
                cmd.Parameters.AddWithValue("@student", studentId);
                cmd.Parameters.AddWithValue("@exam", examId);
                cmd.Parameters.AddWithValue("@score", score);
                cmd.Parameters.AddWithValue("@id", markId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteMark(int markId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM Marks WHERE MarkID = @id", conn);
                cmd.Parameters.AddWithValue("@id", markId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
