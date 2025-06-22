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
    public static class RoomController
    {
        public static void AddRoom(string roomName, string roomType)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO Rooms (RoomName, RoomType) VALUES (@name, @type)", conn);
                cmd.Parameters.AddWithValue("@name", roomName);
                cmd.Parameters.AddWithValue("@type", roomType);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Room> GetAllRooms()
        {
            var list = new List<Room>();
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Rooms", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Room
                    {
                        RoomID = int.Parse(reader["RoomID"].ToString()),
                        RoomName = reader["RoomName"].ToString(),
                        RoomType = reader["RoomType"].ToString()
                    });
                }
            }
            return list;
        }

        public static void DeleteRoom(int roomId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM Rooms WHERE RoomID = @id", conn);
                cmd.Parameters.AddWithValue("@id", roomId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
