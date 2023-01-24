using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Database;
using Database.Repositories;
using Domain.Logic.Repositories;
using Domain.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])
            ),
            ValidateLifetime = true
        };
    });

builder.Services.AddDbContext<ApplicationContext>(
    options =>
        options.UseMySql(
            builder.Configuration.GetConnectionString("DefaulConnection"),
            new MariaDbServerVersion(new Version(10, 9, 4))
        )
);

builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IScheduleRepository, ScheduleRepository>();
builder.Services.AddTransient<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<AppointmentService>();
builder.Services.AddTransient<DoctorService>();
builder.Services.AddTransient<RoleService>();
builder.Services.AddTransient<ScheduleService>();
builder.Services.AddTransient<SpecializationService>();
builder.Services.AddTransient<UserService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
