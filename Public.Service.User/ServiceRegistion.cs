using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Public.Service.User.Models;
using Public.Service.User.Services;
using Public.Service.User.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public.Service.User
{
    public static class ServiceRegistion
    {
        public static void AddUserService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddTransient<IValidator<ApiUserModel>, UserValidation>();
        }
    }
}
