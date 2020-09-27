using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Conctrate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using Newtonsoft.Json;
using Sys;

namespace API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
 
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
              

            });

            //  .AddJsonOptions(options =>
            // {

            ////     options.JsonSerializerOptions.WriteIndented = true;

            //     //options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //     //options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
            //     //options.JsonSerializerOptions.IgnoreNullValues = true;
            //     //options.JsonSerializerOptions.MaxDepth = 0;


            // }


            // );
            var key = Encoding.ASCII.GetBytes("Is*K|SNH.~!k'wwVgPi'pNTY-[},^N<xTOpxmSE+M4JUb]5)dVifRif|KovVuwA");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
     {
         x.RequireHttpsMetadata = false;   //Gelen isteklerin sadece HTTPS yani SSL sertifikasý olanlarý kabul etmesi(varsayýlan true)
         x.SaveToken = true;    //Eðer token onaylanmýþ ise sunucu tarafýnda kayýt edilir. 
         x.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,//Token 3.kýsým(imza) kontrolü
             IssuerSigningKey = new SymmetricSecurityKey(key),  //Neyle kontrol etmesi gerektigi
             ValidateIssuer = false,//Bu iki ayar ise "aud" ve "iss" claimlerini kontrol edelim mi diye soruyor
             ValidateAudience = false
         };
     });

            services.AddDbContext<MyDbContext>();
            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IUserService, UserService>();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:4200"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            //{
            //    Formatting = Formatting.None,
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                 
            //};

            // app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

            SeedData.BuildDataBase(app);
        }
    }
}
