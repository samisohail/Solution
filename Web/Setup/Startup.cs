using Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using ReadStack;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthServices.Contracts;
using AuthServices.Implementation;
using DataAccess.Contract;
using DataAccess.Implementation;
using AutoMapper;
using Queries;
using System.Xml;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using DataStore.DbEntities;

namespace Web.Setup
{
    public class Startup
    {
        public const string AllowedSpecificOrigins = "AllowedSpecificOrigins";
        private string[] _allowedCorsHosts;

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // ConfigureSerilog();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _allowedCorsHosts = Configuration.GetSection("Cors:Allow").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddPolicy(AllowedSpecificOrigins,
                    builder =>
                    {
                        builder
                            .WithOrigins(_allowedCorsHosts)
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed(_ => true)
                            .AllowAnyMethod();
                    });
            });

            // register MediatR
            services.AddMediatR(typeof(ReadSample).Assembly, typeof(CommandSample).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));

            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });

            ////// https://github.com/serilog/serilog-extensions-logging
            //services.AddLogging(loggingBuilder =>
            //    loggingBuilder.AddSerilog(dispose: true));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddDbContext<SolutionDbContext>(options =>
            {
                var connectionString = Configuration.GetValue<string>("ConnectionStrings:SolutionDB");
                var timeout = Configuration.GetValue<int>("DatabaseTimeoutInSeconds");
                options.UseSqlServer(connectionString, contextOptionsBuilder =>  contextOptionsBuilder.CommandTimeout(timeout));
            });

            // services registration
            services.AddScoped<ITokenHelper, JwtTokenHelper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            
            services.AddAutoMapper(typeof(QueriesAutomapper).Assembly, typeof(CommandsAutomapper).Assembly);
            services.AddHttpContextAccessor();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();
            // app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public void ConfigureSerilog()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
