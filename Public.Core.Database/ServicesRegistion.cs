using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Public.Entities.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public.Core.Database
{
    public static class ServicesRegistion
    {
        public static void AddDatabaseService(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<UserDbContext>(opntions => opntions.UseSqlServer(configuration.GetConnectionString("TestDb")));
        }
    }
}
