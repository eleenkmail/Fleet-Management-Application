# Fleet Management Application

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [WebSocket Integration](#websocket-integration)
- [Database Notifications](#database-notifications)
- [Contributing](#contributing)
- [License](#license)

## Introduction
The Fleet Management Application is a comprehensive system designed to manage and monitor vehicle fleets efficiently. The application provides real-time data updates via WebSockets and is built using ASP.NET Core for the backend and Angular for the frontend.

## Features
- Real-time updates using WebSockets
- CRUD operations for fleet management
- PostgreSQL database integration
- RESTful API
- Swagger API documentation
- Postman API documentation
- Notifications for database changes

## Technology Stack
- **Backend:** ASP.NET Core
- **Frontend:** Angular
- **WebSockets:** Fleck
- **Database:** PostgreSQL
- **Serialization:** Newtonsoft.Json

## Prerequisites
- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Node.js](https://nodejs.org/) and npm
- [PostgreSQL](https://www.postgresql.org/)

## Installation

### Backend

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/Fleet-Management-Application.git
    cd Fleet-Management-Application
    ```

2. Set up the database:
    - Create a PostgreSQL database.
    - Update the connection string in `appsettings.json`.

3. Run the backend project:
    ```sh
    cd FMA-API
    dotnet restore
    dotnet run
    ```

### Frontend

1. Navigate to the Angular project directory:
    ```sh
    cd FMA-Client
    ```

2. Install dependencies:
    ```sh
    npm install
    ```

3. Run the Angular development server:
    ```sh
    ng serve
    ```

## Usage
1. Open your browser and navigate to `http://localhost:4200` to access the Angular frontend.
2. Use the Swagger UI available at `http://localhost:5000/swagger` to explore the API endpoints.

## WebSocket Integration
The application uses WebSockets to provide real-time updates. The WebSocket server is set up using the Fleck library and listens for database changes.

### WebSocket Service
The WebSocket service is implemented in a separate file to handle connections and broadcast messages:

**WebSocketService.cs**:
```csharp
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
