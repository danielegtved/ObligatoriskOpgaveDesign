using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageRoom : IManageRoom
    {
        private string connectionString = @"Data Source=danielserver.database.windows.net;Initial Catalog=Daniel;User ID=Daniel;Password=Mineralvand1;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Room> GetAllRooms()
        {
            List<Room> roomList = new List<Room>();
            string queryString = "SELECT * FROM Room";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Room room = new Room();
                        room.Room_No = reader.GetInt32(0);
                        room.Hotel_No = reader.GetInt32(1);
                        room.Type = reader.GetString(2).First();
                        room.Price = reader.GetDouble(3);

                        roomList.Add(room);
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }

            return roomList;
        }

        public Room GetRoomFromID(int roomNr, int hotelNr)
        {
            string queryString = string.Format("SELECT * FROM Room WHERE Room_No = {0} AND Hotel_No = {1}", roomNr, hotelNr);
            Room room = new Room();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        room.Room_No = reader.GetInt32(0);
                        room.Hotel_No = reader.GetInt32(1);
                        room.Type = reader.GetString(2).First();
                        room.Price = reader.GetDouble(3);
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }

                return room;
            }
        }

        public bool CreateRoom(Room room)
        {
            string queryString = "INSERT INTO Room (Room_No, Hotel_No, Types, Price) VALUES (@RoomNumber, @HotelNumber, @Type, @Price)";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@RoomNumber", room.Room_No);
                command.Parameters.AddWithValue("@HotelNumber", room.Hotel_No);
                command.Parameters.AddWithValue("@Type", room.Type);
                command.Parameters.AddWithValue("@Price", room.Price);

                command.Connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public bool UpdateRoom(Room room, int roomNr, int hotelNr)
        {
            string queryString = "UPDATE Room SET Room_No = @RoomNumber, Hotel_No = @HotelNumber, Types = @Types, Price = @Price WHERE Room_No = @ParamNumber1 AND hotel_No = @ParamNumber2";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@ParamNumber1", roomNr);
                command.Parameters.AddWithValue("@ParamNumber2", hotelNr);
                command.Parameters.AddWithValue("@RoomNumber", room.Room_No);
                command.Parameters.AddWithValue("@HotelNumber", room.Hotel_No);
                command.Parameters.AddWithValue("@Types", room.Type);
                command.Parameters.AddWithValue("@Price", room.Price);

                command.Connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public void DeleteRoom(int roomNr, int hotelNr)
        {
            string queryString = string.Format("DELETE FROM Room WHERE Room_No = {0} AND Hotel_No = {1}", roomNr, hotelNr);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}