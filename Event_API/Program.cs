
using Event_API_BLL.Interfaces;
using Bll = Event_API_BLL.Services;
using Event_API_DAL.Repository;
using Dal = Event_API_DAL.Services;
using Event_API_Domain.Interfaces;
using Event_API_Domain.Models;
using Event_API_BLL.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Event_API.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Event_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            //Configuration Swagger Doc
            #region Swagger
            builder.Services.AddSwaggerGen(
            c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Event_Api",
                    Description = "Api fournissant des infos sur nos events",
                    Contact = new OpenApiContact
                    {
                        Name = "Event Creator",
                        Email = "eventCreator@eventCreator.be"
                    },
                    Version = "v1"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
            });
            #endregion


            builder.Services.AddScoped<ICrud<Event,Event,Event>, Dal.EventService>();
            builder.Services.AddScoped<ICrud<Event,EventComplete,EventComplete>, Bll.EventService>();

            builder.Services.AddScoped<IEventThemeRepo, Dal.EventThemeService>();
            builder.Services.AddScoped<IEventTheme, Bll.EventThemeService>();

            builder.Services.AddScoped<IThemesRepo, Dal.ThemesServices>();
            builder.Services.AddScoped<IThemes, Bll.ThemeService>();

            builder.Services.AddScoped<IPersonsRepo, Dal.PersonsServices>();
            builder.Services.AddScoped<IPersonsServices, Bll.PersonsServices>();

            builder.Services.AddScoped<IRolesRepo, Dal.RolesServices>();
            builder.Services.AddScoped<IRolesServices, Bll.RolesServices>();

            builder.Services.AddScoped<ICommentsRepo, Dal.CommentsService>();
            builder.Services.AddScoped<ICommentsServices, Bll.CommentsServices>();

            builder.Services.AddScoped<IExposantRepo_d, Dal.ExposantService_d>();
            builder.Services.AddScoped<IExposant_d, Bll.ExposantService_d>();

            builder.Services.AddScoped<TokenGenerator>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    builder.Configuration.GetSection("TokenInfo").GetSection("secretKey").Value)),
                ValidateLifetime = true,
                ValidAudience = "https://monclient.com",
                ValidIssuer = "https://monapi.com",
                ValidateAudience = false
            };
        }
    );

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("adminPolicy", policy => policy.RequireRole("admin"));
                options.AddPolicy("participantPolicy", policy => policy.RequireRole("participant"));
                options.AddPolicy("adminPolicy", policy => policy.RequireRole("admin"));
                options.AddPolicy("adminParticipantPolicy", policy => policy.RequireRole("admin", "participant"));
                options.AddPolicy("isConnectedPolicy", policy => policy.RequireAuthenticatedUser());
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.Run();
        }
    }
}
