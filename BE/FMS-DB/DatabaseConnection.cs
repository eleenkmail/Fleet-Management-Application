using Npgsql;

namespace FMS_DB;


public class DatabaseConnection
{
    private readonly string _connectionString;


    public DatabaseConnection()
    {
        _connectionString = "Host=localhost;Username=postgres;Password=1234567899as;Database=eleenkmail_fms";

    }
    public NpgsqlConnection OpenConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return connection;
    }


    public NpgsqlConnection Connection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        return connection;
    }

}

