using Dapper;
using Microsoft.Data.SqlClient;
using ToDo.Api.Entities;

namespace ToDo.Api;

public static class Endpoints
{
    public static void MapToDoEndpoints(this WebApplication app)
    {
        app.MapGet("/todo", async (SqlConnection conn) =>
        {
            var items = await conn.QueryAsync<TodoItem>("SELECT * FROM TodoItems");
            return Results.Ok(items);
        });

        app.MapPost("/todo", async (SqlConnection conn, TodoItem item) =>
        {
            var id = await conn.ExecuteScalarAsync<int>(
                "INSERT INTO TodoItems (Title, IsCompleted) OUTPUT INSERTED.Id VALUES (@Title, @IsCompleted)", item);
            return Results.Created($"/todo/{id}", new { Id = id });
        });

        app.MapPut("/todo/{id}", async (SqlConnection conn, int id, TodoItem item) =>
        {
            var existingItem = await conn.QuerySingleOrDefaultAsync<TodoItem>("SELECT * FROM TodoItems WHERE Id = @Id", new { Id = id });
            if (existingItem == null)
                return Results.NotFound();

            existingItem.Title = item.Title;
            existingItem.IsCompleted = item.IsCompleted;

            await conn.ExecuteAsync("UPDATE TodoItems SET Title = @Title, IsCompleted = @IsCompleted WHERE Id = @Id", existingItem);
            return Results.NoContent();
        });

        app.MapDelete("/todo/{id}", async (SqlConnection conn, int id) =>
        {
            var existingItem = await conn.QuerySingleOrDefaultAsync<TodoItem>("SELECT * FROM TodoItems WHERE Id = @Id", new { Id = id });
            if (existingItem == null)
                return Results.NotFound();

            await conn.ExecuteAsync("DELETE FROM TodoItems WHERE Id = @Id", new { Id = id });
            return Results.NoContent();
        });
    }
}
