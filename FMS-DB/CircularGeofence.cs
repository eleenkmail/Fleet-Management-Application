using FPro;
using Npgsql;
using System.Data;
using System.Collections.Concurrent;

namespace FMS_DB
{

    
    public class CircularGeofence
	{
        DatabaseConnection dbconnection = new();


        public GVAR RetrieveCircularGeofencesCoordinates()
        {

            GVAR CircularGeofences = new GVAR();
            CircularGeofences.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };
            CircularGeofences.DicOfDT[Tags.Geofences.ToString()] = new DataTable();

            using (var connection = dbconnection.OpenConnection())
            {

                string query = $@"SELECT
                                GeofenceID,
                                Radius,
                                Latitude,
                                Longitude
                                FROM
                                CircleGeofence";
                               
                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {



                            CircularGeofences.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.GeofenceID.ToString());
                            CircularGeofences.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.Radius.ToString());
                            CircularGeofences.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.Latitude.ToString());
                            CircularGeofences.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.Longitude.ToString());
                           
                            while (reader.Read())
                            {

                                DataRow row = CircularGeofences.DicOfDT[Tags.Geofences.ToString()].NewRow();


                                row[Tags.GeofenceID.ToString()] = reader.GetInt64(0).ToString();
                                row[Tags.Radius.ToString()] = reader.GetInt64(1).ToString();
                                row[Tags.Latitude.ToString()] = reader.GetDouble(2).ToString();
                                row[Tags.Longitude.ToString()] = reader.GetDouble(3).ToString();


                                CircularGeofences.DicOfDT[Tags.Geofences.ToString()].Rows.Add(row);
                                CircularGeofences.DicOfDT[Tags.Geofences.ToString()].AcceptChanges();


                            }


                        }
                    }

            }

            return CircularGeofences;
        }

    }
}


