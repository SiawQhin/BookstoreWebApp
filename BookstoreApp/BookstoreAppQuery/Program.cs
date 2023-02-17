using BookstoreAppQuery.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

builder.Services.AddSession();

//query db
var connStrQuery = builder.Configuration.GetConnectionString("QueryDb");
builder.Services.AddDbContext<QueryDatabaseContext>(o => o.UseSqlite(connStrQuery));
//command db
var connStrCommand = builder.Configuration.GetConnectionString("CommandDb");
builder.Services.AddDbContext<CommandDatabaseContext>(o => o.UseSqlite(connStrCommand));

builder.Services.AddScoped<IBookQuery, DbBookQueryRepo>();
builder.Services.AddScoped<IBookCommand, DbBookCommandRepo>();
//builder.Services.AddScoped<IBook, InMemoryBookRepo>();

var app = builder.Build();

CustomData.SeedData(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//Seed Data
app.UseSession();
GlobalData.SeedData();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
