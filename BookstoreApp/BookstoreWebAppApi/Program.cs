using AutoMapper;
using BookstoreWebAppApi.Data;
using BookstoreWebAppApi.Helper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//query db
var connStrQuery = builder.Configuration.GetConnectionString("QueryDb");
builder.Services.AddDbContext<QueryDatabaseContext>(o => o.UseSqlite(connStrQuery));
//command db
var connStrCommand = builder.Configuration.GetConnectionString("CommandDb");
builder.Services.AddDbContext<CommandDatabaseContext>(o => o.UseSqlite(connStrCommand));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(mc => {
    mc.AddProfile(new MyAutoMapper());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
