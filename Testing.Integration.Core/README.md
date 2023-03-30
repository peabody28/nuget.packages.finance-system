## AbstractWebApplicationFactory.AddDbContextService method example (SQLite)
```
protected override void AddDbConnectionService(IServiceCollection services)
{
    services.AddSingleton<DbConnection>(container =>
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        return connection;
    });
}
```

## AbstractWebApplicationFactory.AddDbConnectionService method example (SQLite)
```
protected override void AddDbContextService(IServiceCollection services)
{
    services.AddDbContext<TContext>((container, options) =>
    {
        var connection = container.GetRequiredService<DbConnection>();
        options.UseSqlite(connection);
    });
}
```