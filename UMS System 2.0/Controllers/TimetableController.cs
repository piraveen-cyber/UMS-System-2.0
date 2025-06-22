using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public static class TimetableController
    {
        public static void AddTimetable(int subjectId, string timeSlot, int roomId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO Timetables (SubjectID, TimeSlot, RoomID) VALUES (@subject, @slot, @room)", conn);
                cmd.Parameters.AddWithValue("@subject", subjectId);
                cmd.Parameters.AddWithValue("@slot", timeSlot);
                cmd.Parameters.AddWithValue("@room", roomId);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Timetable> GetAllTimetables()
        {
            var list = new List<Timetable>();
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Timetables", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Timetable
                    {
                        TimetableID = int.Parse(reader["TimetableID"].ToString()),
                        SubjectID = int.Parse(reader["SubjectID"].ToString()),
                        TimeSlot = reader["TimeSlot"].ToString(),
                        RoomID = int.Parse(reader["RoomID"].ToString())
                    });
                }
            }
            return list;
        }

        public static void UpdateTimetable(int id, int subjectId, string timeSlot, int roomId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE Timetables SET SubjectID = @subject, TimeSlot = @slot, RoomID = @room WHERE TimetableID = @id", conn);
                cmd.Parameters.AddWithValue("@subject", subjectId);
                cmd.Parameters.AddWithValue("@slot", timeSlot);
                cmd.Parameters.AddWithValue("@room", roomId);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteTimetable(int id)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM Timetables WHERE TimetableID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
