using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using WebAPI.Configuration;
using WebAPI.Middlewares;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        .WriteTo.File("Logs/logBootstrap-.txt", rollingInterval: RollingInterval.Day)
                        .CreateBootstrapLogger();

            try
            {
                Log.Information("Starting up the application...");

                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog((ctx, services, config) =>
                config.ReadFrom.Configuration(ctx.Configuration)
                      .ReadFrom.Services(services));

                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll",
                        policy => policy.AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader());
                });

                builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                                    .AddEnvironmentVariables();

                if (builder.Environment.IsDevelopment())
                {
                    builder.Configuration.AddUserSecrets<Program>(optional: true);
                }

                builder.Services.AddOptions<JwtOptions>()
                        .Bind(builder.Configuration.GetSection("Authentication:Jwt"))
                        .Validate(o => !string.IsNullOrWhiteSpace(o.Secret), "JWT Secret is required.")
                        .ValidateOnStart();

                var jwt = builder.Configuration.GetSection("Authentication:Jwt").Get<JwtOptions>()!;
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret));

                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = !string.IsNullOrWhiteSpace(jwt.Issuer),
                        ValidateAudience = !string.IsNullOrWhiteSpace(jwt.Audience),
                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

                builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

                builder.Services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                }).AddMvc().AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

                builder.Services.AddInfrastructureServicesAsync(builder.Configuration);

                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    var securityScheme = new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "Enter JWT Bearer token **_only_**",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };

                    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { securityScheme, new string[] { } }
                    });

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    if (File.Exists(xmlPath))
                    {
                        c.IncludeXmlComments(xmlPath);
                    }
                });

                builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

                var app = builder.Build();

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            $"IGCSELearningHubApp API {description.GroupName.ToUpperInvariant()}");
                    }
                });

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                }

                app.UseHttpsRedirection();

                app.UseMiddleware<CorrelationIdMiddleware>();

                app.UseMiddleware<RequestResponseLoggingMiddleware>();

                app.UseMiddleware<GlobalExceptionMiddleware>();

                app.UseCors("AllowAll");

                app.UseAuthentication();

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
