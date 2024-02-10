using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWALKS.DB;
using NZWALKS.IRepository;
using NZWALKS.Mappings;
using NZWALKS.Models;
using System.Text;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NZDBContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRegionRepository,RegionRepositoryImpl>();
builder.Services.AddScoped<IWalkRepository,WalksRepositoryImpl>();
builder.Services.AddAutoMapper(typeof(automapping));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
options.TokenValidationParameters= new Microsoft.IdentityModel.Tokens.TokenValidationParameters
{
    ValidateIssuer=true,
    ValidateAudience=true,
    ValidateLifetime=true,
    ValidateIssuerSigningKey=true,
    ValidIssuer = builder.Configuration["JWT:Issuer"],
    ValidAudience = builder.Configuration["JWT:Audience"],
    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
});

builder.Services.AddDbContext<AuthDBContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection")));

builder.Services.AddAuthentication().AddBearerToken();
builder.Services.AddAuthorizationBuilder();
builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NzWalks")
    .AddEntityFrameworkStores<AuthDBContext>();

 builder.Services.Configure<IdentityOptions>(options =>
 {
     options.Password.RequireDigit = true;
     options.Password.RequireLowercase = true;
     options.Password.RequireUppercase = true;
     options.Password.RequireNonAlphanumeric = true;
     options.Password.RequiredLength = 8;
     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
     options.Lockout.MaxFailedAccessAttempts = 5;
     options.Lockout.AllowedForNewUsers = true;
 });

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
