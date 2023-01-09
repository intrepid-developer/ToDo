var builder = DistributedApplication.CreateBuilder(args);

// Adds a SQL Server database
var db = builder.AddSqlServer("db")
    .WithDataVolume() // Save the data to a separate data volume in Docker
    .WithLifetime(ContainerLifetime.Persistent); // Keep it running in Docker

// Add Database with basic setup script
var database = db.AddDatabase("todo-db")
    .WithCreationScript("""
                        CREATE DATABASE [todo-db];
                        USE [todo-db];
                        CREATE TABLE [TodoItems] (
                            [Id] INT PRIMARY KEY IDENTITY(1,1),
                            [Title] NVARCHAR(255) NOT NULL,
                            [IsCompleted] BIT NOT NULL DEFAULT 0,
                            [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE()
                        );
                        """);

// Adds our API
var apiService = builder.AddProject<Projects.ToDo_Api>("api")
    .WithExternalHttpEndpoints()
    .WithReference(database).WaitFor(database)
    .WithHttpsHealthCheck("/health");

// Adds our Web Frontend
builder.AddProject<Projects.ToDo_Web>("web")
    .WithExternalHttpEndpoints()
    .WithHttpsHealthCheck("/health")
    .WithReference(apiService).WaitFor(apiService);

builder.Build().Run();