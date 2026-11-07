using Microsoft.EntityFrameworkCore;
using UniversityCafeteria2.Models;
using UniversityCafeteria2.CodeFirst;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database First
builder.Services.AddDbContext<CafeteriaContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CafeteriaConnection")));

// Code First
builder.Services.AddDbContext<CafeteriaEmployeesContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("EmployeesConnection")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();