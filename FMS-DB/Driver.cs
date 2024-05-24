using FPro;
using Npgsql;
using System.Data;
using System.Numerics;
using System.Collections.Concurrent;

namespace FMS_DB
{
    public class Driver
    {

        DatabaseConnection dbconnection = new();

        public void AddDriver(GVAR Gvar)
        {

            using (var connection = dbconnection.OpenConnection())
            {
                string query = $"INSERT INTO Driver (DriverName, PhoneNumber) VALUES (@DriverName, @PhoneNumber)";
                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("DriverName", Gvar.DicOfDic[Tags.Tags.ToString()][Tags.DriverName.ToString()]);
                        command.Parameters.AddWithValue("PhoneNumber", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.PhoneNumber.ToString()]));

                        int rowsAffected = command.ExecuteNonQuery();

                    }
            }
        }

        public void UpdateDriver(GVAR Gvar)
        {

            using (var connection = dbconnection.OpenConnection())
            {
                string query = $"UPDATE Driver SET DriverName = @DriverName, PhoneNumber = @PhoneNumber WHERE DriverID = @DriverID";
                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("DriverID", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.DriverID.ToString()]));
                        command.Parameters.AddWithValue("DriverName", Gvar.DicOfDic[Tags.Tags.ToString()][Tags.DriverName.ToString()]);
                        command.Parameters.AddWithValue("PhoneNumber", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.PhoneNumber.ToString()]));

                        int rowsAffected = command.ExecuteNonQuery();

                    }
            }
        }

        public void DeleteDriver(GVAR Gvar)
        {

            using (var connection = dbconnection.OpenConnection())
            {
                string query = $"DELETE FROM Driver WHERE DriverID = @DriverID";
                ;
                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("DriverID", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.DriverID.ToString()]));

                        int rowsAffected = command.ExecuteNonQuery();

                    }
            }
        }


        public GVAR RetriveAllDrivers()
        {
            GVAR AllDriver = new GVAR();
            AllDriver.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            using (var connection = dbconnection.OpenConnection())
            {
                string query = $"SELECT DriverID, DriverName, PhoneNumber FROM Driver";
                ;
                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {

                        using (var reader = command.ExecuteReader())
                        {

                            AllDriver.DicOfDT[Tags.Drivers.ToString()] = new DataTable();


                            AllDriver.DicOfDT[Tags.Drivers.ToString()].Columns.Add(Tags.DriverID.ToString());
                            AllDriver.DicOfDT[Tags.Drivers.ToString()].Columns.Add(Tags.DriverName.ToString());
                            AllDriver.DicOfDT[Tags.Drivers.ToString()].Columns.Add(Tags.PhoneNumber.ToString());




                            while (reader.Read())
                            {

                                DataRow row = AllDriver.DicOfDT[Tags.Drivers.ToString()].NewRow();


                                row[Tags.DriverID.ToString()] = reader.GetInt64(0).ToString();
                                row[Tags.DriverName.ToString()] = reader.GetString(1);
                                row[Tags.PhoneNumber.ToString()] = reader.GetInt64(2).ToString();

                                AllDriver.DicOfDT[Tags.Drivers.ToString()].Rows.Add(row);
                                AllDriver.DicOfDT[Tags.Drivers.ToString()].AcceptChanges();


                            }


                        }
                    }

            }

            return AllDriver;


        }
    }

} 

  


