using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using Application.Interfaces;
using Application.UseCases.AccountServices;
using Application.UseCases.CategoryServices;
using Application.UseCases.GoalServices;
using Application.UseCases.TransactionServices;
using Application.UseCases.UserServices;
using Domain;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SecuritySettings>(builder.Configuration.GetSection("SecuritySettings"));
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();

builder.Services.AddScoped<createUser>();
builder.Services.AddScoped<getUser>();
builder.Services.AddScoped<updateUser>();
builder.Services.AddScoped<deleteUser>();
builder.Services.AddScoped<createAccount>();
builder.Services.AddScoped<getAccount>();
builder.Services.AddScoped<getAccounts>();
builder.Services.AddScoped<updateAccount>();
builder.Services.AddScoped<deleteAccount>();
builder.Services.AddScoped<createCategory>();
builder.Services.AddScoped<getCategory>();
builder.Services.AddScoped<getCategories>();
builder.Services.AddScoped<updateCategory>();
builder.Services.AddScoped<deleteCategory>();
builder.Services.AddScoped<createTransaction>();
builder.Services.AddScoped<getTransaction>();
builder.Services.AddScoped<getTransactionByDate>();
builder.Services.AddScoped<getTransactionsByTimePeriod>();
builder.Services.AddScoped<updateTransaction>();
builder.Services.AddScoped<deleteTransaction>();
builder.Services.AddScoped<createGoal>();
builder.Services.AddScoped<getGoal>();
builder.Services.AddScoped<getGoals>();
builder.Services.AddScoped<updateGoal>();
builder.Services.AddScoped<deleteGoal>();

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<HashingServices>();
builder.Services.AddScoped<IHashingService, HashingServices>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddSingleton<JwtSecurityTokenHandler>();

builder.Services.AddDbContext<Context>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["SecuritySettings:jwtSecretKey"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.FromMinutes(2)
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
