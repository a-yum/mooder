using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Mooder.Data;
using Mooder.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<MooderDBContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("MooderContext")));
}
else
{
    // In production, use different connection string/DB.
}


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MooderDBContext>();

    if (!context.UserMoodEntry.Any())
    {
        ApplicationDbContextSeed.SeedAsync(context).Wait();
    }

}

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
