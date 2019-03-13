using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageHotel : IManageHotel
    {
        private string connectionString = @"Data Source=danielserver.database.windows.net;Initial Catalog=Daniel;User ID=Daniel;Password=Mineralvand1;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Hotel> GetAllHotel()
        {
            List<Hotel> allHotel = new List<Hotel>();

            string queryString = "SELECT * FROM Hotel";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Hotel hotel = new Hotel();
                        hotel.Hotel_No = reader.GetInt32(0);
                        hotel.Name = reader.GetString(1);
                        hotel.Address = reader.GetString(2);

                        allHotel.Add(hotel);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return allHotel;
        }

        public Hotel GetHotelFromId(int hoteltNo)
        {
            string queryString = "SELECT * FROM Hotel WHERE Hotel_No = " + hoteltNo;

            Hotel hotel = new Hotel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        hotel.Hotel_No = reader.GetInt32(0);
                        hotel.Name = reader.GetString(1);
                        hotel.Address = reader.GetString(2);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return hotel;
        }

        public bool CreateHotel(Hotel hotel)
        {
            string queryString = "INSERT INTO Hotel (Hotel_No, Name, Address) VALUES ( @Number , @Name, @Address)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Number", hotel.Hotel_No);
                command.Parameters.AddWithValue("@Name", hotel.Name);
                command.Parameters.AddWithValue("@Address", hotel.Address);

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

        public bool UpdateHotel(Hotel hotel, int hotelNo)
        {
            string queryString = "UPDATE Hotel SET Hotel_No = @Number, Name = @Name, Address = @Address WHERE Hotel_No = @ParaNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@ParaNumber", hotelNo);
                command.Parameters.AddWithValue("@Number", hotel.Hotel_No);
                command.Parameters.AddWithValue("@Name", hotel.Name);
                command.Parameters.AddWithValue("@Address", hotel.Address);

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

        public Hotel DeleteHotel(int hotelNo)
        {
            string queryString = "DELETE FROM Hotel WHERE Hotel_No = @ParaNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@ParaNumber", hotelNo);

                command.Connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception e)
                {
                }
                return new Hotel();
            }
        }
    }
}