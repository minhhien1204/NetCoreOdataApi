using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using NetCoreOdataApi.Core;
using NetCoreOdataApi.Data;
using NetCoreOdataApi.Models;
using NetCoreOdataApi.Services;
using NetCoreOdataApi.Core.UnitOfWork;
using NetCoreOdataApi.Core.Repositories;
using NetCoreOdataApi.Domain;
using NetCoreOdataApi.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;

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

            services.AddScoped<IDataContext, DataContext>();
            services.AddDbContext<DataContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<UserManager<ApplicationUser>>();

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
            })
               .AddEntityFrameworkStores<DataContext>()
               .AddDefaultTokenProviders();
            //JWT Authentication
            var key = Encoding.UTF8.GetBytes(Configuration["ApllicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>{
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false);

            services.AddOData();
            //services.AddScoped<IDataContext, DataContext>();
          
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

            app.UseAuthentication();

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
            edmBuilder.EntityType<QuestionViewModel>()
            .Collection
            .Function("GetAllAnswers")
            .ReturnsCollectionFromEntitySet<QuestionViewModel>("Questions");
            return edmBuilder.GetEdmModel();
        }
    }
}
