using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Repositories;
using MongoDB.Driver;
using BackEnd.Settings;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BackEnd.Helpers;

namespace BackEnd
{
    public class Startup
    {
        String MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserDbContext>(options => {
                string ConllString = Configuration.GetConnectionString(nameof(UserDbContext));
                options.UseSqlServer(ConllString);
            });
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            MongoDbSettings MongoDBsettings =  Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

            services.AddSingleton<IMongoClient>(servicesProvider => {
                return new MongoClient(MongoDBsettings.ConnectionString);
            });
            services.AddCors(options => 
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy  =>
                        {
                            policy.WithOrigins("http://localhost:3000");
                        });
            }
            );
            // MongoDb
            services.AddSingleton<IItemsRepository, MongodbItemRepositories>(); // khai triển qua Interface
            services.AddSingleton<IProductRepository, MongoDbProductRepository>();

            // SQL thì phải dùng Scoped 
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<JwtService>();

            services.AddControllers(option =>{
                option.SuppressAsyncSuffixInActionNames = false; //Phương thức xóa bỏ hậu tố bất đồng bộ (Async)
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP_NET_API", Version = "v1" });
            });
            services.AddHealthChecks()
                .AddMongoDb(
                    MongoDBsettings.ConnectionString, 
                    name: "mongodb",
                    timeout: TimeSpan.FromSeconds(3),
                    tags: new[] {"ready"}
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP_NET_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck/ready",new HealthCheckOptions{
                    Predicate = (check) => check.Tags.Contains("ready"),
                    ResponseWriter = async(context, report) =>
                    {
                        string result = JsonSerializer.Serialize(
                            new{
                                status = report.Status.ToString(),
                                checks = report.Entries.Select(entry => new {
                                    name = entry.Key,
                                    status = entry.Value.Status.ToString(),
                                    exception = entry.Value.Exception != null ? entry.Value.Exception.Message :"none",
                                    duration = entry.Value.Duration.ToString()
                                })
                            }
                        );
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
                endpoints.MapHealthChecks("/healthcheck/live",new HealthCheckOptions{
                    Predicate = (_) => false
                });
            });
        }
    }
}
