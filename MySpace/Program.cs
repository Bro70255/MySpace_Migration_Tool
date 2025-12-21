using MySpace_DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ⬅ Register EF Core DbContext
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MAFIT"));
});

// ⬅ Register 2-Layer DI services
builder.Services.AddScoped<Data_Layer>();

// ⬅ REQUIRED for DeepSeek API
builder.Services.AddHttpClient();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// PIPELINE
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=MySapce_Login}/{id?}")
    .WithStaticAssets();

app.Run();
