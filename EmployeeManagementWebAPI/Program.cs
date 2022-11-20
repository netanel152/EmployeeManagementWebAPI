using EmployeeManagementWebAPI.Models;
using EmployeeManagementWebAPI.Services.EmployeeServices;
using Microsoft.EntityFrameworkCore;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddDbContext<EmployeeManagementSystemDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3001", "http://localhost:3000").AllowAnyHeader().AllowAnyMethod().WithMethods("PUT", "DELETE", "GET", "POST");
                      });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.Run();