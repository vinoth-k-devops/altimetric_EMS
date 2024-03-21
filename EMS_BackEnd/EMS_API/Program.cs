using System.Collections.Generic;
using System.Text;
using EMS_Domain.EMS;
using EMS_Domain.Model;
using EMS_Service.Interfaces;
using EMS_Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

#region Configure SwaggerGen

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

#endregion

#region Configure JWTSettings

var _jwtsetting = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JWTSettings>(_jwtsetting);

#endregion

builder.Services.AddDbContext<EMSContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLITEConnection")));

builder.Services.AddTransient(typeof(IMasterOperation<>), typeof(MasterOperation<>));
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<ITransaction, Transaction>();

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:ValidAudience"],
        ValidIssuer = builder.Configuration["JwtSettings:ValidIssuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!))
    };
});

#endregion

#region CORS Enable

const string CorsPolicy = "HandleCORS";

builder.Services.AddCors(options =>
{
    var origins = new List<string>();
    builder.Configuration.Bind("Cors:Origins", origins);

    options.AddPolicy(name: CorsPolicy, builder =>
    {
        builder.WithOrigins(origins.ToArray())
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

# endregion

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

app.UseCors("HandleCORS");

app.MapControllers();

app.Run();

