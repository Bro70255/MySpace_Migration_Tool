using MySpace_DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

/* ===============================
   FILE UPLOAD LIMITS (2GB)
=============================== */
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 2L * 1024 * 1024 * 1024; // 2GB
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 2L * 1024 * 1024 * 1024; // 2GB
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);
});

/* ===============================
   DATABASE
=============================== */
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MAFIT")
    );
});

/* ===============================
   SERVICES
=============================== */
builder.Services.AddScoped<Data_Layer>();
builder.Services.AddHttpClient();

/* ===============================
   MVC + API SUPPORT
=============================== */
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

var app = builder.Build();

/* ===============================
   MIDDLEWARE
=============================== */

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

/* ===============================
   🔒 IP RESTRICTION (FLAG BASED)
=============================== */

var ipRestrictionEnabled =
    builder.Configuration.GetValue<int>("IpRestriction:Enabled") == 1;

var allowedIps =
    builder.Configuration.GetSection("IpRestriction:AllowedIps")
    .Get<string[]>() ?? Array.Empty<string>();

if (ipRestrictionEnabled)
{
    app.Use(async (context, next) =>
    {
        var remoteIpAddress = context.Connection.RemoteIpAddress;

        // Always allow localhost
        if (remoteIpAddress != null && IPAddress.IsLoopback(remoteIpAddress))
        {
            await next();
            return;
        }

        var remoteIp = remoteIpAddress?.ToString();

        if (string.IsNullOrEmpty(remoteIp) ||
            !allowedIps.Contains(remoteIp))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Access denied");
            return;
        }

        await next();
    });
}

/* ===============================
   AUTHORIZATION
=============================== */
app.UseAuthorization();

/* ===============================
   ROUTING
=============================== */

// API Controllers
app.MapControllers();

// MVC Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=MySpace_Login}/{id?}");

app.Run();
