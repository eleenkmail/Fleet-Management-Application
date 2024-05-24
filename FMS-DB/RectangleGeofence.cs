using System;
using FPro;
using Npgsql;
using System.Data;
using System.Collections.Concurrent;

namespace FMS_DB
{
	public class RectangleGeofence
	{
        DatabaseConnection dbconnection = new();


        public GVAR RetrieveRectangleGeofencesCoordinates()
        {

            GVAR RectangleGeofences = new GVAR();
            RectangleGeofences.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };
            RectangleGeofences.DicOfDT[Tags.Geofences.ToString()] = new DataTable();

            using (var connection = dbconnection.OpenConnection())
            {

                string query = $@"SELECT
                                GeofenceID,
                                North,
                                East,
                                West,
                                South
                                FROM
                                RectangleGeofence";

                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {


                            RectangleGeofences.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.GeofenceID.ToString());
                            RectangleGeofences.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.North.ToString());
                            RectangleGeofences.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.East.ToString());
                            RectangleGeofences.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.West.ToString());
                            RectangleGeofences.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.South.ToString());

                            while (reader.Read())
                            {

                                DataRow row = RectangleGeofences.DicOfDT[Tags.Geofences.ToString()].NewRow();


                                row[Tags.GeofenceID.ToString()] = reader.GetInt64(0).ToString();
                                row[Tags.North.ToString()] = reader.GetDouble(1).ToString();
                                row[Tags.East.ToString()] = reader.GetDouble(2).ToString();
                                row[Tags.West.ToString()] = reader.GetDouble(3).ToString();
                                row[Tags.South.ToString()] = reader.GetDouble(4).ToString();


                                RectangleGeofences.DicOfDT[Tags.Geofences.ToString()].Rows.Add(row);
                                RectangleGeofences.DicOfDT[Tags.Geofences.ToString()].AcceptChanges();


                            }


                        }
                    }

            }

            return RectangleGeofences;
        }
    }
}

