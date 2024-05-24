using FPro;
using Npgsql;
using System.Data;
using System.Collections.Concurrent;

namespace FMS_DB
{
	public class Geofences
	{

        DatabaseConnection dbconnection = new();


        public GVAR RetrieveGeofencesInformation()
        {

            GVAR GeofencesInformation = new GVAR();
            GeofencesInformation.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };
            GeofencesInformation.DicOfDT[Tags.Geofences.ToString()] = new DataTable();

            using (var connection = dbconnection.OpenConnection())
            {

                string query = $@"SELECT
                                GeofenceID,
                                GeofenceType,
                                AddedDate,
                                StrokeColor,
                                StrokeOpacity,
                                StrokeWeight,
                                FillColor,
                                FillOpacity
                                FROM
                                Geofences";

                if (connection.State == ConnectionState.Open)
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {

                            GeofencesInformation.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.GeofenceID.ToString());
                            GeofencesInformation.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.GeofenceType.ToString());
                            GeofencesInformation.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.AddedDate.ToString());
                            GeofencesInformation.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.StrokeColor.ToString());
                            GeofencesInformation.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.StrokeOpacity.ToString());
                            GeofencesInformation.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.StrokeWeight.ToString());
                            GeofencesInformation.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.FillColor.ToString());
                            GeofencesInformation.DicOfDT[Tags.Geofences.ToString()].Columns.Add(Tags.FillOpacity.ToString());

                            while (reader.Read())
                            {

                                DataRow row = GeofencesInformation.DicOfDT[Tags.Geofences.ToString()].NewRow();


                                row[Tags.GeofenceID.ToString()] = reader.GetInt64(0).ToString();
                                row[Tags.GeofenceType.ToString()] = reader.GetString(1);
                                row[Tags.AddedDate.ToString()] = reader.GetInt64(2).ToString();
                                row[Tags.StrokeColor.ToString()] = reader.GetString(3);
                                row[Tags.StrokeOpacity.ToString()] = reader.GetDouble(4).ToString();
                                row[Tags.StrokeWeight.ToString()] = reader.GetDouble(5).ToString();
                                row[Tags.FillColor.ToString()] = reader.GetString(6);
                                row[Tags.FillOpacity.ToString()] = reader.GetDouble(7).ToString();


                                GeofencesInformation.DicOfDT[Tags.Geofences.ToString()].Rows.Add(row);
                                GeofencesInformation.DicOfDT[Tags.Geofences.ToString()].AcceptChanges();


                            }


                        }
                    }

            }

            return GeofencesInformation;
        }

    }
}

