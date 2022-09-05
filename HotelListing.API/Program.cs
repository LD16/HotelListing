using HotelListing.API.Configurations;
using HotelListing.API.Data;
using HotelListing.API.Interfaces;
using HotelListing.API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<HotelListingDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Add services to the container.

//builder.Services.AddIdentityCore<ApiUser>()
//        .AddRoles<IdentityRole>()
//        .AddEntityFrameworkStores<HotelListingDbContext>();


builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().OrderBy();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        b=>b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});




builder.Host.UseSerilog((ctx,lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IHotelsRepository, HotelsRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
