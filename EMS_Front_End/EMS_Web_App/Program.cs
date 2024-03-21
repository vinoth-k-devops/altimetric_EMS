using System.IdentityModel.Tokens.Jwt;
using EMS_Common.Handler;
using EMS_Common.Variables;
using EMS_Web_App.Models;
using EMS_Web_App.StaticFunc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient(typeof(IAPIHandler<>), typeof(APIHandler<>));
builder.Services.AddTransient<IAuthHandler, AuthHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IAuthorizationHandler, AuthorizeToken>();

builder.Services.AddSession();
//builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.Configuration["APISettings:BaseURL"]!.ToString()) });

//await builder.Build().RunAsync();

#region Configure Authetication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.Events = new JwtBearerEvents
        {            
            OnTokenValidated = async context =>
            {
                await CustomTokenValidator.onTokenValidated(context, builder.Configuration["APISettings:BaseURL"]!.ToString());

                if (context.Principal == null)
                    context.Fail("No user");
            }
        };
    });
#endregion

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Permission", policyBuilder =>
    {
        policyBuilder.Requirements.Add(new TokenAuthorizationRequirement());
    });
});

#region Configure APISettings

var _apiSettings = builder.Configuration.GetSection("APISettings");
builder.Services.Configure<APISettings>(_apiSettings);

#endregion

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
//else
//{
//    app.UseMiddleware<GlobalExceptionFilter>();
//}

app.UseSession();
app.Use(async (context, next) =>
{
    var token = context.Session.GetString(Constant.__TOKEN__);
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + token);
    }
    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();