using FPro;
using Npgsql;
using System.Data;
using System.Collections.Concurrent;
namespace FMS_DB
{
	public class PolygonGeofence
	{
        DatabaseConnection dbconnection = new();


        public GVAR RetrievePolygonGeofencesCoordinates()
        {

            GVAR PolygonGeofences = new GVAR();
            PolygonGeofences.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };
            PolygonGeofences.DicOfDT[Tags.Geofences.ToString()] = new DataTable();

            using (var connection = dbconnection.OpenConnection())
            {

                string query = $@"SELECT
                                GeofenceID,
                                Latitude,
                                Longitude
                                FROM
                                PolygonGeofence";

                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {

                            
                            PolygonGeofences.DicOfDT[Tags.Geofences.ToString()] = new DataTable();

                            PolygonGeofences.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.GeofenceID.ToString());
                            PolygonGeofences.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.Latitude.ToString());
                            PolygonGeofences.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.Longitude.ToString());


                            while (reader.Read())
                            {

                                DataRow row = PolygonGeofences.DicOfDT[Tags.Geofences.ToString()].NewRow();


                                row[Tags.GeofenceID.ToString()] = reader.GetInt64(0).ToString();
                                row[Tags.Latitude.ToString()] = reader.GetDouble(1).ToString();
                                row[Tags.Longitude.ToString()] = reader.GetDouble(2).ToString();
                               


                                PolygonGeofences.DicOfDT[Tags.Geofences.ToString()].Rows.Add(row);
                                PolygonGeofences.DicOfDT[Tags.Geofences.ToString()].AcceptChanges();


                            }


                        }
                    }

            }

            return PolygonGeofences;
        }
    }
}




