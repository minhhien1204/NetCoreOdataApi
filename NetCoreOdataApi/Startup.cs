using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using NetCoreOdataApi.Core;
using NetCoreOdataApi.Data;
using NetCoreOdataApi.Models;
using NetCoreOdataApi.Services;
using NetCoreOdataApi.Core.UnitOfWork;
using static NetCoreOdataApi.Services.CategoryService;
using NetCoreOdataApi.Core.Repositories;
using NetCoreOdataApi.Domain;

namespace NetCoreOdataApi
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
            //config cors
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAllOrigins",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                options.DefaultPolicyName = "AllowAllOrigins";
            });

            services.AddDbContext<IDataContext, DataContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false);
        
            services.AddOData();
            services.AddScoped<IDataContext, DataContext>();
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWorkAsync, UnitOfWork>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IParticipantService, ParticipantService>();
            services.AddTransient<IQuestionService, QuestionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseMvc(routeBuilder => 
            {
                //routeBuilder.EnableDependencyInjection();
                routeBuilder.Select().Filter().Count().Expand().OrderBy().MaxTop(null);
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
        }
        private IEdmModel GetEdmModel()
        {
            var edmBuilder = new ODataConventionModelBuilder();
            edmBuilder.EntitySet<Students>("Students");
            edmBuilder.EntitySet<CategoryViewModel>("Categories");
            //quiz project
            edmBuilder.EntitySet<ParticipantViewModel>("Participants");
            edmBuilder.EntitySet<QuestionViewModel>("Questions");
            return edmBuilder.GetEdmModel();
        }
    }
}
