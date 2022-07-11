using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infreastructure_Layer.Data.MongoDb;
using Domain_Layer.Entities;
using Domain_Layer.Entities.Product;
using Infreastructure_Layer.Settings;
using Infreastructure_Layer.Data.Identity;
using Domain_Layer.Helpers;
using Domain_Layer.Services;
using Domain_Layer.Entities.Order;

namespace BackEnd
{
    public class Startup
    {
        public static string ContentRootPath {get; set;}
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Thêm Identity
            services.AddIdentityMongoDb(Configuration);

            services.AddCors();
            // MongoDb

            services.AddMongoDb();
            services.AddMongoRepostory<ImgAndVideoProduct>("ImgProduct");
            services.AddMongoRepostory<Category>("Categories");
            services.AddMongoRepostory<ProductReviews>("Products_Reviews");
            services.AddMongoRepostory<Product>("Product");
            services.AddMongoRepostory<ImgProductReview>("ImgAndVideoReviewsProduct");
            
            services.AddMongoRepostory<Order>("Order");

            services.AddScoped<JwtService>();

            services.AddControllers(option =>{
                option.SuppressAsyncSuffixInActionNames = false; //Phương thức xóa bỏ hậu tố bất đồng bộ (Async)
            });
            //Cài đặt OptionIdentity
            services.IdentityServices();
            
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP NET API", Version = "v1" });
            });
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
            });
        }
    }
}
