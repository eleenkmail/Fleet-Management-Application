using System;
using System.Data;
using System.Data.Common;
using Npgsql;
using FPro;
using System.Numerics;
using System.Collections.Concurrent;

namespace FMS_DB
{
	public class Vehicles
	{

  
        DatabaseConnection dbconnection = new();


        public void AddVehicle(GVAR Gvar)
        {

            using (var connection = dbconnection.OpenConnection())
            {
                string query = $"INSERT INTO Vehicles (VehicleNumber, VehicleType) VALUES (@VehicleNumber, @VehicleType)";
                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                       
                        
                        command.Parameters.AddWithValue("VehicleNumber", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleNumber.ToString()]));
                        command.Parameters.AddWithValue("VehicleType", Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleType.ToString()]);

                        int rowsAffected = command.ExecuteNonQuery();

                    
                    }
               
            }
        }

        public void UpdateVehicle(GVAR Gvar)
        {

            using (var connection = dbconnection.OpenConnection())
            {
                string query = $"UPDATE Vehicles SET VehicleNumber = @VehicleNumber , VehicleType = @VehicleType WHERE VehicleID = @VehicleID";
                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("VehicleNumber", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleNumber.ToString()]));
                        command.Parameters.AddWithValue("VehicleType", Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleType.ToString()]);
                        command.Parameters.AddWithValue("VehicleID", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleID.ToString()]));

                        int rowsAffected = command.ExecuteNonQuery();

                   
                    }

              
            }
        }


        public void DeleteVehicle(GVAR Gvar)
        {
        
            using (var connection = dbconnection.OpenConnection())
            {
                string query = $"DELETE FROM Vehicles WHERE VehicleID = @VehicleID";
                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("VehicleID", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleID.ToString()]));

                        int rowsAffected = command.ExecuteNonQuery();

                    
                    }
            }

        }


        public GVAR RetriveAllVehicle()
        {
            GVAR Vehicles = new GVAR();
            Vehicles.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };
            Vehicles.DicOfDT[Tags.Vehicles.ToString()] = new DataTable();

            using (var connection = dbconnection.OpenConnection())
            {

                Vehicles.DicOfDT[Tags.Vehicles.ToString()] = new DataTable();
                Vehicles.DicOfDT[Tags.Vehicles.ToString()].Columns.Add(Tags.VehicleID.ToString());
                Vehicles.DicOfDT[Tags.Vehicles.ToString()].Columns.Add(Tags.VehicleNumber.ToString());
                Vehicles.DicOfDT[Tags.Vehicles.ToString()].Columns.Add(Tags.VehicleType.ToString());

                string query = $"SELECT VehicleID, VehicleNumber, VehicleType FROM Vehicles";

                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                DataRow row = Vehicles.DicOfDT[Tags.Vehicles.ToString()].NewRow();


                                row[Tags.VehicleID.ToString()] = reader.GetInt64(0).ToString();
                                row[Tags.VehicleNumber.ToString()] = reader.GetInt64(1).ToString();
                                row[Tags.VehicleType.ToString()] = reader.GetString(2);


                                Vehicles.DicOfDT[Tags.Vehicles.ToString()].Rows.Add(row);
                                Vehicles.DicOfDT[Tags.Vehicles.ToString()].AcceptChanges();


                            }


                        }
                  }

            }
            return Vehicles;
        }


    }
}

