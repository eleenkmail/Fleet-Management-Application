using Fleck;
using Npgsql;
using FMS_DB;

namespace FMA_API.Controllers
{

    public class WebSocketService
    {

        private readonly List<IWebSocketConnection> _allSockets = new List<IWebSocketConnection>();
        DatabaseConnection db = new DatabaseConnection();

        public WebSocketService()
        {
            var server = new WebSocketServer("ws://0.0.0.0:8181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {

                    _allSockets.Add(socket);

                };

                socket.OnClose = () =>
                {
                    _allSockets.Remove(socket);
                };

                Task.Run(() => ListenForDatabaseUpdates());
            });

        }

        private async Task ListenForDatabaseUpdates()
        {
            using var connection = db.Connection();

            await connection.OpenAsync();
            connection.Notification += async (sender, args) =>
            {

                foreach (var socket in _allSockets)
                {
                    if (socket.IsAvailable)
                    {
                        await socket.Send("new routehistory added");
                    }
                }
            };


            using (var command = new NpgsqlCommand($"LISTEN trigger_log_changes;", connection))
            {

                await command.ExecuteNonQueryAsync();

            }


            while (true)
            {
                connection.Wait();
            }
        }

    }
}




