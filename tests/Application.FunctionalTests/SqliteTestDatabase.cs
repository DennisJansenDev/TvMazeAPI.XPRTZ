using System.Data;
using System.Data.Common;
using Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Respawn;

namespace Application.FunctionalTests;

public class SqliteTestDatabase : ITestDatabase
{
    private readonly SqliteConnection _connection;
    private Respawn.Respawner _respawner = null!;

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

        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Sqlite
        });
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
        await _respawner.ResetAsync(_connection);
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}
