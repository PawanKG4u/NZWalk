using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NZWeb2.Api.BAL;
using NZWeb2.Api.Data;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Step 12
builder.Services.AddSwaggerGen(options => {

    var SecuritySch = new OpenApiSecurityScheme
    {
        Name = "Jwt Authentication",
        Description = "Enter a vaild JWT bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "Jwt",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(SecuritySch.Reference.Id, SecuritySch);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            SecuritySch,new string[]{ }
        }
    });
});


//Fluent Validation Implements Here
builder.Services.AddFluentValidation(Options => Options.RegisterValidatorsFromAssemblyContaining<Program>());

//NZWalksConn
builder.Services.AddDbContext<NZWalksDBContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConn"));
});


builder.Services.AddScoped<IRegionRespository, RegionRespository>();
builder.Services.AddScoped<IWalkRespository, WalkRespository>();
builder.Services.AddScoped<IWalkDifficultyRespository, WalkDifficultyRespository>();

//Step - 9
builder.Services.AddScoped<ITokenHandler, NZWeb2.Api.BAL.TokenHandler>();

//Step - 6
builder.Services.AddScoped<IUserRespository, UserRespository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Step - 3
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    });
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Step - 4
app.UseAuthentication();
//
app.UseAuthorization();

app.MapControllers();

app.Run();
