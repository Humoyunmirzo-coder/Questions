﻿using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ConfigurationService
    {
        public static void AddIfrastuctureServices(this IServiceCollection services, IConfiguration configuration)
        {
         
            services.AddDbContext<QuestionDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection")));

        }
    }
}