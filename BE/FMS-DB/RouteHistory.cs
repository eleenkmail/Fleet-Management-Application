using FPro;
using Npgsql;
using System.Numerics;
using System.Collections.Concurrent;
using System.Data;

namespace FMS_DB
{
    public class RouteHistory
    {
        DatabaseConnection dbconnection = new();

        public GVAR AddRouteHistory(GVAR Gvar)
        {
            GVAR RH = new GVAR();
            RH.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            using (var connection = dbconnection.OpenConnection())
            {
                string query = $@"INSERT INTO RouteHistory
                                  (VehicleID, VehicleDirection, VehicleSpeed, Address, Status, Epoch, Latitude, Longitude)
                                  VALUES (@VehicleID, @VehicleDirection, @VehicleSpeed, @Address, @Status, @Epoch, @Latitude, @Longitude)";
                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("VehicleID", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleID.ToString()]));
                        command.Parameters.AddWithValue("VehicleDirection", int.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleDirection.ToString()]));
                        command.Parameters.AddWithValue("Status", Gvar.DicOfDic[Tags.Tags.ToString()][Tags.Status.ToString()]);
                        command.Parameters.AddWithValue("Address", Gvar.DicOfDic[Tags.Tags.ToString()][Tags.Address.ToString()]);
                        command.Parameters.AddWithValue("VehicleSpeed", Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleSpeed.ToString()]);
                        command.Parameters.AddWithValue("Epoch", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.Epoch.ToString()]));
                        command.Parameters.AddWithValue("Latitude", Double.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.Latitude.ToString()]));
                        command.Parameters.AddWithValue("Longitude", Double.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.Longitude.ToString()]));

                        command.ExecuteNonQuery();

                    }
                return RH;
            }
        }


        public GVAR RetrieveAllWithLastRouteHistory()
        {
            
            GVAR VehiclesWithLastRouteHistory = new GVAR();
           
            VehiclesWithLastRouteHistory.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            using (var connection = dbconnection.OpenConnection())
            {
                
                string query = $@" SELECT
                                    VehicleID,
                                    VehicleNumber,
                                    VehicleType,
                                    LastDirection AS LastDirection,
                                    LastStatus AS LastStatus,
                                    LastAddress AS LastAddress,
                                    LastLatitude AS LastLatitude,
                                    LastLongitude AS LastLongitude
                                    FROM
                                    (
                                        SELECT
                                            V.VehicleID,
                                            V.VehicleNumber,
                                            V.VehicleType,
                                            RH.VehicleDirection AS LastDirection,
                                            RH.Status AS LastStatus,
                                            RH.Address AS LastAddress,
                                            RH.Latitude AS LastLatitude,
                                            RH.Longitude AS LastLongitude,
                                            ROW_NUMBER() OVER (PARTITION BY V.VehicleID ORDER BY RH.Epoch DESC) AS RowNum
                                        FROM
                                            Vehicles V
                                        JOIN
                                            RouteHistory RH ON V.VehicleID = RH.VehicleID
                                    ) AS LatestEpochs
                                    WHERE
                                    RowNum = 1";

                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        
                        using (var reader = command.ExecuteReader())
                        {
                           
                            VehiclesWithLastRouteHistory.DicOfDT[Tags.Vehicles.ToString()] = new DataTable();


                            VehiclesWithLastRouteHistory.DicOfDT[Tags.Vehicles.ToString()].Columns.Add(Tags.VehicleID.ToString());
                            VehiclesWithLastRouteHistory.DicOfDT[Tags.Vehicles.ToString()].Columns.Add(Tags.VehicleNumber.ToString());
                            VehiclesWithLastRouteHistory.DicOfDT[Tags.Vehicles.ToString()].Columns.Add(Tags.VehicleType.ToString());
                            VehiclesWithLastRouteHistory.DicOfDT[Tags.Vehicles.ToString()].Columns.Add(Tags.LastDirection.ToString());
                            VehiclesWithLastRouteHistory.DicOfDT[Tags.Vehicles.ToString()].Columns.Add(Tags.LastStatus.ToString());
                            VehiclesWithLastRouteHistory.DicOfDT[Tags.Vehicles.ToString()].Columns.Add(Tags.LastAddress.ToString());
                            VehiclesWithLastRouteHistory.DicOfDT[Tags.Vehicles.ToString()].Columns.Add(Tags.LastLatitude.ToString());
                            VehiclesWithLastRouteHistory.DicOfDT[Tags.Vehicles.ToString()].Columns.Add(Tags.LastLongitude.ToString());

                            while (reader.Read())
                            {

                                DataRow row = VehiclesWithLastRouteHistory.DicOfDT[Tags.Vehicles.ToString()].NewRow();


                                row[Tags.VehicleID.ToString()] = reader.GetInt64(0).ToString();
                                row[Tags.VehicleNumber.ToString()] = reader.GetInt64(1).ToString();
                                row[Tags.VehicleType.ToString()] = reader.GetString(2);
                                row[Tags.LastDirection.ToString()] = reader.GetInt32(3).ToString();
                                row[Tags.LastStatus.ToString()] = reader.GetString(4);
                                row[Tags.LastAddress.ToString()] = reader.GetString(5);
                                row[Tags.LastLatitude.ToString()] = reader.GetDouble(6).ToString();
                                row[Tags.LastLongitude.ToString()] = reader.GetDouble(7).ToString();


                                VehiclesWithLastRouteHistory.DicOfDT[Tags.Vehicles.ToString()].Rows.Add(row);
                                VehiclesWithLastRouteHistory.DicOfDT[Tags.Vehicles.ToString()].AcceptChanges();

                                
                            }


                        }
                       
                    }
                

            }

            return VehiclesWithLastRouteHistory;
        }




        public GVAR RrtrieveRouteHistoryWithEpochRange(GVAR Gvar)
        {

            GVAR VehicleHistory = new GVAR();
            VehicleHistory.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            using (var connection = dbconnection.OpenConnection())
            {
                
                string query = $@" SELECT
                                    V.VehicleID,
                                    V.VehicleNumber,
                                    RH.VehicleDirection,
                                    RH.Status,
                                    RH.Address,
                                    RH.Latitude,
                                    RH.Longitude,
                                    RH.VehicleSpeed AS GPSSpeed,
                                    RH.Epoch AS GPSTime,
                                    RH.RouteHistoryID
                                    FROM Vehicles V
                                    JOIN RouteHistory RH ON V.VehicleID = RH.VehicleID
                                    WHERE V.VehicleID = @VehicleID
                                    AND
                                    RH.Epoch BETWEEN @FirstEpoch AND @LastEpoch";

                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("VehicleID", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.VehicleID.ToString()]));
                        command.Parameters.AddWithValue("FirstEpoch", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.FirstEpoch.ToString()]));
                        command.Parameters.AddWithValue("LastEpoch", BigInteger.Parse(Gvar.DicOfDic[Tags.Tags.ToString()][Tags.LastEpoch.ToString()]));



                        using (var reader = command.ExecuteReader())
                        {

                            VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()] = new DataTable();

                           
                            VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].Columns.Add(Tags.VehicleID.ToString());
                            VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].Columns.Add(Tags.VehicleNumber.ToString());
                            VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].Columns.Add(Tags.VehicleDirection.ToString());
                            VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].Columns.Add(Tags.Status.ToString());
                            VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].Columns.Add(Tags.Address.ToString());
                            VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].Columns.Add(Tags.Latitude.ToString());
                            VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].Columns.Add(Tags.Longitude.ToString());
                            VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].Columns.Add(Tags.GPSSpeed.ToString());
                            VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].Columns.Add(Tags.GPSTime.ToString());
                            VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].Columns.Add(Tags.RouteHistoryID.ToString());


                            while (reader.Read())
                            {

                                DataRow row = VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].NewRow();


                                row[Tags.VehicleID.ToString()] = reader.GetInt64(0).ToString();
                                row[Tags.VehicleNumber.ToString()] = reader.GetInt64(1).ToString();
                                row[Tags.VehicleDirection.ToString()] = reader.GetInt32(2).ToString();
                                row[Tags.Status.ToString()] = reader.GetString(3);
                                row[Tags.Address.ToString()] = reader.GetString(4);
                                row[Tags.Latitude.ToString()] = reader.GetDouble(5).ToString();
                                row[Tags.Longitude.ToString()] = reader.GetDouble(6).ToString();
                                row[Tags.GPSSpeed.ToString()] = reader.GetString(7);
                                row[Tags.GPSTime.ToString()] = reader.GetInt64(8).ToString();
                                row[Tags.RouteHistoryID.ToString()] = reader.GetInt64(9).ToString();

                                VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].Rows.Add(row);
                                VehicleHistory.DicOfDT[Tags.RouteHistory.ToString()].AcceptChanges();


                            }


                        }
                    }

            }

            return VehicleHistory;
        }




    }

}

                
   