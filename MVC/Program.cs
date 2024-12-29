using BLL.DAL;
using BLL.Models;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// AppSettings
var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings));
appSettingsSection.Bind(new AppSettings());

// IoC Container
var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<Db>(options => 
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 30)) // Ensure this matches your MySQL version
    )
);
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IService<Student,StudentModel>, StudentService>();
builder.Services.AddScoped<IService<Teacher,TeacherModel>, TeacherService>();
builder.Services.AddScoped<IService<Major, MajorModel>, MajorService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<HttpServiceBase, HttpService>();

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login";
        options.AccessDeniedPath = "/Users/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication
app.UseAuthentication();

app.UseAuthorization();

// Session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
