using System.Data;
using System.Data.Common;
using Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Application.FunctionalTests;

public class SqliteTestDatabase : ITestDatabase
{
    private readonly SqliteConnection _connection;

    public SqliteTestDatabase()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
    }

    public async Task InitialiseAsync()
    {
        if (_connection.State != ConnectionState.Open)
        {
            await _connection.OpenAsync();
        }

        var options = new DbContextOptionsBuilder<TvMazeApiDbContext>()
            .UseSqlite(_connection)
            .Options;

        var context = new TvMazeApiDbContext(options);

        await context.Database.EnsureCreatedAsync();
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public string GetConnectionString()
    {
        return _connection.ConnectionString;
    }

    public async Task ResetAsync()
    {
        // For SQLite in-memory databases, we need to manually clear all tables
        // while keeping the connection open to preserve the schema
        var options = new DbContextOptionsBuilder<TvMazeApiDbContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new TvMazeApiDbContext(options);

        // Delete all records from all tables
        // Order matters for foreign key constraints
        context.TvMazeShows.RemoveRange(context.TvMazeShows);

        await context.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}
