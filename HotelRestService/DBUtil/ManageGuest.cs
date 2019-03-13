using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageGuest : IManageGuest
    {
        private string connectionString = @"Data Source=danielserver.database.windows.net;Initial Catalog=Daniel;User ID=Daniel;Password=Mineralvand1;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Guest> GetAllGuest()
        {
            List<Guest> AllGuests = new List<Guest>();

            string queryString = "SELECT * FROM Guest";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Guest guest = new Guest();
                        guest.Guest_No = reader.GetInt32(0); 
                        guest.Name = reader.GetString(1); 
                        guest.Address = reader.GetString(2);
                        AllGuests.Add(guest);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return AllGuests;
        }

        public Guest GetGuestFromId(int guestNr)
        {
            string queryString = "SELECT * FROM Guest WHERE Guest_No = " + guestNr;

            Guest guest = new Guest();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        guest.Guest_No = reader.GetInt32(0);
                        guest.Name = reader.GetString(1);
                        guest.Address = reader.GetString(2);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return guest;
        }

        public bool CreateGuest(Guest guest)
        {
            string queryString = "INSERT INTO Guest (Guest_No, Name, Address) VALUES ( @Number , @Name, @Address)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Number", guest.Guest_No);
                command.Parameters.AddWithValue("@Name", guest.Name);
                command.Parameters.AddWithValue("@Address", guest.Address);

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

        public bool UpdateGuest(Guest guest, int guestNr)
        {
            string queryString = "UPDATE Guest SET Guest_No = @Number, Name = @Name, Address = @Address WHERE Guest_No = @ParaNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@ParaNumber", guestNr);
                command.Parameters.AddWithValue("@Number", guest.Guest_No);
                command.Parameters.AddWithValue("@Name", guest.Name);
                command.Parameters.AddWithValue("@Address", guest.Address);

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

        public void DeleteGuest(int guestNr)
        {
            string queryString = "DELETE FROM Guest WHERE Guest_No = @ParaNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                
                command.Parameters.AddWithValue("@ParaNumber", guestNr);

                command.Connection.Open();
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