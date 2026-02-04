using MySpace_DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

/* ===============================
   FILE UPLOAD LIMITS (ASP.NET CORE)
=============================== */

// Allow large multipart uploads
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 2L * 1024 * 1024 * 1024; // 2 GB
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});

// Kestrel limits
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 2L * 1024 * 1024 * 1024; // 2 GB
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
builder.Services.AddControllersWithViews();

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

// ⚠️ Keep Authorization AFTER routing
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=MySpace_Login}/{id?}");

app.Run();
