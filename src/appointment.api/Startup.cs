#region namespaces 

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Amazon.DynamoDBv2;
using Amazon.Lambda.Core;
using storytiling.core.Interfaces;
using storytiling.core.Profiles;
using storytiling.core.Services;
using storytiling.infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using storytiling.api.Helpers.CustomExceptions;

#endregion namespaces 

namespace storytiling.api
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            LambdaLogger.Log("Start ConfigureServices...");
            services.AddControllers();
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IWelcomeMessageRepository, WelcomeMessageRepository>();
            services.AddSingleton<IWelcomeMessageService, WelcomeMessageService>();
            services.AddSingleton<IContributorInviteRepository, ContributorInviteRepository>();
            services.AddSingleton<IContributorInviteService, ContributorInviteService>();
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddAutoMapper(typeof(StorytilingProfile));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerGeneratorOptions.Servers = new List<OpenApiServer> { new OpenApiServer { Url = "/" } };
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Storytiling API",
                    Description = "Storytiling API",
                    TermsOfService = new Uri("https://example.com/terms")
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Storytiling V1");
            });
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.ConfigureCustomExceptionMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
