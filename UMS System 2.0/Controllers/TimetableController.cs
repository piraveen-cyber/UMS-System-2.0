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
    public static class TimetableController
    {
        public static void AddTimetable(int subjectId, string timeSlot, int roomId)
        {
            try
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
                MessageBox.Show("✅ Timetable added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error adding timetable:\n" + ex.Message, "Add Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<Timetable> GetAllTimetables()
        {
            var list = new List<Timetable>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    var cmd = new SQLiteCommand("SELECT * FROM Timetables", conn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Timetable
                        {
                            TimetableID = Convert.ToInt32(reader["TimetableID"]),
                            SubjectID = Convert.ToInt32(reader["SubjectID"]),
                            TimeSlot = reader["TimeSlot"].ToString(),
                            RoomID = Convert.ToInt32(reader["RoomID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error loading timetables:\n" + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;
        }

        public static void UpdateTimetable(int id, int subjectId, string timeSlot, int roomId)
        {
            try
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
                MessageBox.Show("✅ Timetable updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error updating timetable:\n" + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DeleteTimetable(int id)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    var cmd = new SQLiteCommand("DELETE FROM Timetables WHERE TimetableID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("✅ Timetable deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error deleting timetable:\n" + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
