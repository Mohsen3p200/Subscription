using Application.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation.AspNetCore;
using API.Configurations.ExceptionHandling;
using Infrastructure.Persistance.EFCore;
using Microsoft.EntityFrameworkCore;
using Domain.IRepositories;
using Infrastructure.Persistance.Repositories.Repository;
using AutoMapper;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(c => c.SwaggerDoc(name: "v1", new OpenApiInfo()
            {
                Title = "Web Service APIs",
                Version = "v1"
            }));

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")));
 
            services.AddMediatR(typeof(Assembly));

            services.AddHttpClient();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddControllers()
            .AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<Assembly>();
            });

            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ///No need to use it anymore due to using the middleware
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //Using our global handling exception in exceptionMiddleware class
            app.ConfigureExceptionHandler(env);

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "APIs V1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
