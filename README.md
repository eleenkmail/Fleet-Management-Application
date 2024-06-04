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
- Notifications for database changes

## Technology Stack
- **Backend:** ASP.NET Core
- **Frontend:** Angular
- **WebSockets:** Fleck
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core
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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fleck;
using Newtonsoft.Json;
using Npgsql;

public class WebSocketService
{
    private readonly List<IWebSocketConnection> _allSockets = new List<IWebSocketConnection>();

    public WebSocketService()
    {
        var server = new WebSocketServer("ws://0.0.0.0:8181");
        server.Start(socket =>
        {
            socket.OnOpen = () =>
            {
                Console.WriteLine("Connection Opened!");
                _allSockets.Add(socket);
            };

            socket.OnClose = () =>
            {
                Console.WriteLine("Connection Closed!");
                _allSockets.Remove(socket);
            };
        });

        Task.Run(() => ListenForDatabaseUpdates());
    }

    private Gvar FetchLatestDataFromDatabase()
    {
        var connectionString = "Host=localhost;Username=postgres;Password=1234567899as;Database=eleenkmail_fms"; // Replace with your actual connection string

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("SELECT id, name, value FROM RouteHistory ORDER BY id DESC LIMIT 1", connection);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Gvar
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Value = reader.GetString(2)
            };
        }
        else
        {
            return null; // No data found
        }
    }

    private async Task ListenForDatabaseUpdates()
    {
        using var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=1234567899as;Database=eleenkmail_fms"); // Replace with your actual connection string

        await connection.OpenAsync();
        Console.WriteLine("Database connection opened and waiting for notifications.");

        connection.Notification += async (sender, args) =>
        {
            Console.WriteLine("Notification received: " + args.Payload);
            var updatedData = FetchLatestDataFromDatabase();
            if (updatedData != null)
            {
                var json = JsonConvert.SerializeObject(updatedData);
                foreach (var socket in _allSockets)
                {
                    if (socket.IsAvailable)
                    {
                        await socket.Send(json);
                        Console.WriteLine("Sent data to client: " + json);
                    }
                }
            }
        };

        using (var command = new NpgsqlCommand("LISTEN trigger_log_changes;", connection))
        {
            await command.ExecuteNonQueryAsync();
            Console.WriteLine("Subscribed to database notifications on channel 'trigger_log_changes'.");
        }

        while (true)
        {
            connection.Wait(); // Wait for PostgreSQL notifications
        }
    }
}

// Gvar class definition
public class Gvar
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}
