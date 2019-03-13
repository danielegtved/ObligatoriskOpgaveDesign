using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageBooking : IManageBooking
    {
        private string connectionString = @"Data Source=danielserver.database.windows.net;Initial Catalog=Daniel;User ID=Daniel;Password=Mineralvand1;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<Booking> GetAllBookings()
        {
            List<Booking> bookingList = new List<Booking>();
            string queryString = "SELECT * FROM Booking";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking();
                        booking.BookingID = reader.GetInt32(0);
                        booking.Hotel_No = reader.GetInt32(1);
                        booking.Guest_No = reader.GetInt32(2);
                        booking.Date_From = reader.GetDateTime(3);
                        booking.Date_To = reader.GetDateTime(4);
                        booking.Room_No = reader.GetInt32(5);

                        bookingList.Add(booking);
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }

            return bookingList;
        }

        public Booking GetBookingFromID(int bookingId)
        {
            string queryString = string.Format("SELECT * FROM Booking WHERE Booking_Id = {0}", bookingId);
            Booking booking = new Booking();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        booking.BookingID = reader.GetInt32(0);
                        booking.Hotel_No = reader.GetInt32(1);
                        booking.Guest_No = reader.GetInt32(2);
                        booking.Date_From = reader.GetDateTime(3);
                        booking.Date_To = reader.GetDateTime(4);
                        booking.Room_No = reader.GetInt32(5);
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }

                return booking;
            }
        }

        public bool CreateBooking(Booking booking)
        {
            string queryString = "INSERT INTO Booking (Hotel_No, Guest_No, Date_From, Date_To, Room_No) VALUES (@HotelNumber, @GuestNumber, @DateFrom, @DateTo, @RoomNumber)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@BookingId", booking.BookingID);
                command.Parameters.AddWithValue("@HotelNumber", booking.Hotel_No);
                command.Parameters.AddWithValue("@GuestNumber", booking.Guest_No);
                command.Parameters.AddWithValue("@DateFrom", booking.Date_From);
                command.Parameters.AddWithValue("@DateTo", booking.Date_To);
                command.Parameters.AddWithValue("@RoomNumber", booking.Room_No);

                command.Connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateBooking(Booking booking, int bookingId)
        {
            string queryString = "UPDATE Booking SET Hotel_No = @HotelNumber, Guest_No = @GuestNumber, Date_From = @DateFrom, Date_To = @DateTo, Room_No = @RoomNumber WHERE Booking_id = @BookingId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@BookingId", bookingId);
                command.Parameters.AddWithValue("@HotelNumber", booking.Hotel_No);
                command.Parameters.AddWithValue("@GuestNumber", booking.Guest_No);
                command.Parameters.AddWithValue("@DateFrom", booking.Date_From);
                command.Parameters.AddWithValue("@DateTo", booking.Date_To);
                command.Parameters.AddWithValue("@RoomNumber", booking.Room_No);

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

        public void DeleteBooking(int bookingId)
        {
            string queryString = string.Format("DELETE FROM Booking WHERE Booking_id = {0}", bookingId);

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