using Course_project_GYMAPP.DAL;
using Course_project_GYMAPP.DAL.Interfaces;
using Course_project_GYMAPP.DAL.Repositories;
using Course_project_GYMAPP.Domain.Entity;
using Course_project_GYMAPP.Service.Implementations;
using Course_project_GYMAPP.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITrainer, TrainerRepository>();
builder.Services.AddScoped<IAdmin, AdminRepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<IAdminService, AdminService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
