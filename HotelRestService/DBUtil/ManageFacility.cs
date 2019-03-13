using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageFacility : IManageFacility
    {
        private string connectionString = @"Data Source=danielserver.database.windows.net;Initial Catalog=Daniel;User ID=Daniel;Password=Mineralvand1;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public List<Facility> GetAllFacilities()
        {
            List<Facility> FacilityList = new List<Facility>();
            string queryString = "SELECT * FROM Facility";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Facility facility = new Facility();
                        facility.Hotel_No = reader.GetInt32(0);
                        facility.Bar = reader.GetString(1).First();
                        facility.TableTennis = reader.GetString(2).First();
                        facility.PoolTable = reader.GetString(3).First();
                        facility.Restaurant = reader.GetString(4).First();
                        facility.SwimmingPool = reader.GetString(5).First();
                        FacilityList.Add(facility);
                    }
                }
                finally
                {
                    reader.Close();
                }

            }

            return FacilityList;
        }
        public Facility GetFacilityFromId(int Hotel_No)
        {
            string queryString = "SELECT * FROM Facility WHERE Hotel_No = " + Hotel_No;

            Facility facility = new Facility();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        facility.Hotel_No = reader.GetInt32(0);
                        facility.Bar = reader.GetString(1).First();
                        facility.TableTennis = reader.GetString(2).First();
                        facility.PoolTable = reader.GetString(3).First();
                        facility.Restaurant = reader.GetString(4).First();
                        facility.SwimmingPool = reader.GetString(5).First();
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return facility;
        }


        public bool CreateFacility(Facility facility)
        {
            string queryString = "INSERT INTO Facility (Hotel_No, Swimming_Pool, Bar, Table_Tennis, Pool_Table, Restaurant) VALUES (@Number, @SwimmingPool , @Bar, @TableTennis, @PoolTable, @Restaurant)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Number", facility.Hotel_No);
                command.Parameters.AddWithValue("@Bar", facility.Bar);
                command.Parameters.AddWithValue("@TableTennis", facility.TableTennis);
                command.Parameters.AddWithValue("@PoolTable", facility.PoolTable);
                command.Parameters.AddWithValue("@Restaurant", facility.Restaurant);
                command.Parameters.AddWithValue("@SwimmingPool", facility.SwimmingPool);

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

        public bool UpdateFacility(Facility facility, int Hotel_No)
        {
            string queryString = "UPDATE Facility SET Hotel_No = @Number, Swimming_Pool = @SwimmingPool, Bar = @Bar, Table_Tennis = @TableTennis, Pool_Table = @PoolTable, Restaurant = @Restaurant  WHERE Hotel_No = @ParaNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@ParaNumber", Hotel_No);
                command.Parameters.AddWithValue("@Number", facility.Hotel_No);
                command.Parameters.AddWithValue("@Bar", facility.Bar);
                command.Parameters.AddWithValue("@TableTennis", facility.TableTennis);
                command.Parameters.AddWithValue("@PoolTable", facility.PoolTable);
                command.Parameters.AddWithValue("@Restaurant", facility.Restaurant);
                command.Parameters.AddWithValue("@SwimmingPool", facility.SwimmingPool);


                command.Connection.Open();
                command.ExecuteNonQuery();
                try
                {
                    connection.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public void DeleteFacility(int Hotel_No)
        {
            string queryString = "DELETE FROM Facility WHERE Hotel_No =" + Hotel_No;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}