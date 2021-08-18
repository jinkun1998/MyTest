using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Public.Core.Swagger
{
    public static class ServicesRegistion
    {
        public static void AddSwaggerService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "MyTest",
                    Version = "v1"
                });
            });
        }

        public static void UserSwaggerService(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                }); 
            }
        }
    }
}
