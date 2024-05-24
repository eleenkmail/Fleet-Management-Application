using FPro;
using Npgsql;
using System.Numerics;
using System.Data;
using System.Collections.Concurrent;


namespace FMS_DB
{
    public class VehiclesInformations
    {
        DatabaseConnection dbconnection = new();

        public void AddVehicleInformation(GVAR Gvar)
        {

            using (var connection = dbconnection.OpenConnection())
            {
                string query = $"INSERT INTO VehiclesInformations (VehicleID, DriverID, VehicleMake, VehicleModel, PurchaseDate) VALUES (@VehicleID, @DriverID, @VehicleMake, @VehicleModel, @PurchaseDate)";
                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("VehicleID", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleID.ToString()]));
                        command.Parameters.AddWithValue("DriverID", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.DriverID.ToString()]));
                        command.Parameters.AddWithValue("VehicleMake", Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleMake.ToString()]);
                        command.Parameters.AddWithValue("VehicleModel", Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleModel.ToString()]);
                        command.Parameters.AddWithValue("PurchaseDate", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.PurchaseDate.ToString()]));

                        int rowsAffected = command.ExecuteNonQuery();

                    }
            }
        }

        public void UpdateVehicleInformation(GVAR Gvar)
        {

            using (var connection = dbconnection.OpenConnection())
            {
                string query = $"UPDATE VehiclesInformations SET DriverID = @DriverID, VehicleMake = @VehicleMake, VehicleModel = @VehicleModel, PurchaseDate = @PurchaseDate WHERE VehicleID = @VehicleID";
                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("VehicleID", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleID.ToString()]));
                        command.Parameters.AddWithValue("DriverID", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.DriverID.ToString()]));
                        command.Parameters.AddWithValue("VehicleMake", Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleMake.ToString()]);
                        command.Parameters.AddWithValue("VehicleModel", Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleModel.ToString()]);
                        command.Parameters.AddWithValue("PurchaseDate", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.PurchaseDate.ToString()]));

                        int rowsAffected = command.ExecuteNonQuery();

                    }
            }
        }


        public void DeleteVehicleInformation(GVAR Gvar)
        {

            using (var connection = dbconnection.OpenConnection())
            {
                string query = $"DELETE FROM VehiclesInformations WHERE VehicleID = @VehicleID";
                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("VehicleID", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleID.ToString()]));

                        int rowsAffected = command.ExecuteNonQuery();
                    }
            }
        }



        public GVAR RrtrieveDetailedVehicleInformation(GVAR Gvar)
        {

            GVAR VehicleInfo = new GVAR();
            VehicleInfo.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };
            using (var connection = dbconnection.OpenConnection())
            {

                string query = $@" SELECT 
                                V.VehicleNumber,
                                V.VehicleType,
                                D.DriverName,
                                D.PhoneNumber,
                                CONCAT(R.Latitude, ',', R.Longitude) AS LastPosition,
                                VI.VehicleMake,
                                VI.VehicleModel,
                                R.Epoch AS LastGPSTime,
                                R.VehicleSpeed AS LastGPSSpeed,
                                R.Address AS LastAddress
                            FROM 
                                Vehicles V
                            JOIN 
                                VehiclesInformations VI ON V.VehicleID = VI.VehicleID
                            JOIN 
                                Driver D ON VI.DriverID = D.DriverID
                            JOIN 
                                (SELECT 
                                    VehicleID,
                                    Latitude,
                                    Longitude,
                                    Epoch,
                                    VehicleSpeed,
                                    Address
                                FROM 
                                    RouteHistory
                                WHERE
                                    VehicleID = @VehicleID
                                    ORDER BY 
                                    Epoch DESC
                                LIMIT 1) R ON V.VehicleID = R.VehicleID";


                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("VehicleID", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleID.ToString()]));


                        using (var reader = command.ExecuteReader())
                        {

                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()] = new DataTable();

                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.VehicleNumber.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.VehicleType.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.DriverName.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.PhoneNumber.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.LastPosition.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.VehicleMake.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.VehicleModel.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.LastGPSTime.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.LastGPSSpeed.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.LastAddress.ToString());



                            while (reader.Read())
                            {

                                DataRow row = VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].NewRow();


                                row[Tags.VehicleNumber.ToString()] = reader.GetInt64(0).ToString();
                                row[Tags.VehicleType.ToString()] = reader.GetString(1);
                                row[Tags.DriverName.ToString()] = reader.GetString(2);
                                row[Tags.PhoneNumber.ToString()] = reader.GetInt64(3).ToString();
                                row[Tags.LastPosition.ToString()] = reader.GetString(4);
                                row[Tags.VehicleMake.ToString()] = reader.GetString(5);
                                row[Tags.VehicleModel.ToString()] = reader.GetString(6).ToString();
                                row[Tags.LastGPSTime.ToString()] = reader.GetInt64(7).ToString();
                                row[Tags.LastGPSSpeed.ToString()] = reader.GetString(8);
                                row[Tags.LastAddress.ToString()] = reader.GetString(9);

                                VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Rows.Add(row);
                                VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].AcceptChanges();


                            }


                        }
                    }

            }

            return VehicleInfo;
        }






        public GVAR RrtrieveVehicleInformation()
        {

            GVAR VehicleInfo = new GVAR();
            VehicleInfo.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };
            using (var connection = dbconnection.OpenConnection())
            {

                string query = $@" SELECT
                                V.VehicleID,
                                V.VehicleNumber,
                                V.VehicleType,
                                D.DriverID,  
                                D.DriverName,
                                D.PhoneNumber,
                                VI.VehicleMake,
                                VI.VehicleModel,
                                VI.PurchaseDate
                            FROM 
                                Vehicles V
                            JOIN 
                                VehiclesInformations VI ON V.VehicleID = VI.VehicleID
                            JOIN 
                                Driver D ON VI.DriverID = D.DriverID";



                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                      

                        using (var reader = command.ExecuteReader())
                        {

                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()] = new DataTable();

                           
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.VehicleID.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.VehicleNumber.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.VehicleType.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.DriverID.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.DriverName.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.PhoneNumber.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.VehicleMake.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.VehicleModel.ToString());
                            VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Columns.Add(Tags.PurchaseDate.ToString());
                          


                            while (reader.Read())
                            {

                                DataRow row = VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].NewRow();

                                row[Tags.VehicleID.ToString()] = reader.GetInt64(0).ToString();
                                row[Tags.VehicleNumber.ToString()] = reader.GetInt64(1).ToString();
                                row[Tags.VehicleType.ToString()] = reader.GetString(2);
                                row[Tags.DriverID.ToString()] = reader.GetInt64(3).ToString();
                                row[Tags.DriverName.ToString()] = reader.GetString(4);
                                row[Tags.PhoneNumber.ToString()] = reader.GetInt64(5).ToString();  
                                row[Tags.VehicleMake.ToString()] = reader.GetString(6);
                                row[Tags.VehicleModel.ToString()] = reader.GetString(7);
                                row[Tags.PurchaseDate.ToString()] = reader.GetInt64(8).ToString();
                    
                               

                                VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].Rows.Add(row);
                                VehicleInfo.DicOfDT[Tags.VehicleInformation.ToString()].AcceptChanges();


                            }


                        }
                    }

            }

            return VehicleInfo;
        }



    }


}
