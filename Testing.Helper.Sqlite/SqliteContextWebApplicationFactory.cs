﻿using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using Testing.Integration.Core;

namespace Testing.Helper.Sqlite
{
    /// <summary>
    /// Web application factory extension for SQLite database
    /// </summary>
    /// <typeparam name="TProgram"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class SqliteContextWebApplicationFactory<TProgram, TContext> : AbstractWebApplicationFactory<TProgram, TContext>
    {
        protected override void AddDbConnectionService(IServiceCollection services)
        {
            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                return connection;
            });
        }

        protected override void AddDbContextService(IServiceCollection services)
        {
            services.AddDbContext<TContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });
        }
    }
}
