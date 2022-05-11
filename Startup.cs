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
using BackEnd.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BackEnd
{
    public class Startup
    {
        public static string ContentRootPath {get; set;}
        String MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new ByteArraySerializer(BsonType.String));

            MongoDbSettings MongoDBsettings =  Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

            // Thêm Identity

            services.AddIdentity<ApplicationUser, ApplicationRole>().
                    AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
                        MongoDBsettings.ConnectionString, "Item_Product");


            services.AddSingleton<IMongoClient>(servicesProvider => {
                return new MongoClient(MongoDBsettings.ConnectionString);
            });
            services.AddCors();
            // MongoDb
            services.AddSingleton<IItemsRepository, MongodbItemRepositories>(); // khai triển qua Interface
            services.AddSingleton<IProductRepository, MongoDbProductRepository>();
            services.AddSingleton<IImgProduct, MongoDbImgProduct>();
            services.AddSingleton<ICategoryProduct, MongodbCategoryRepository>();

            services.AddScoped<JwtService>();

            services.AddControllers(option =>{
                option.SuppressAsyncSuffixInActionNames = false; //Phương thức xóa bỏ hậu tố bất đồng bộ (Async)
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
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

            app.UseCors(buider => {
                buider.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseAuthentication();

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
