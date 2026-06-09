using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Interfaces;
using Application.UseCases.AccountServices;
using Application.UseCases.CategoryServices;
using Application.UseCases.GoalServices;
using Application.UseCases.TransactionServices;
using Application.UseCases.UserServices;
using Domain;
using Domain.Models;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new System.Globalization.CultureInfo("pt-BR");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.Configure<SecuritySettings>(
    builder.Configuration.GetSection("SecuritySettings")
);

builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonDateConverter());
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IHashingService, HashingServices>();

builder.Services.AddScoped<createUser>();
builder.Services.AddScoped<getUser>();
builder.Services.AddScoped<updateUser>();
builder.Services.AddScoped<deleteUser>();

builder.Services.AddScoped<createAccount>();
builder.Services.AddScoped<updateAccount>();
builder.Services.AddScoped<deleteAccount>();
builder.Services.AddScoped<getAccount>();
builder.Services.AddScoped<getAccounts>();

builder.Services.AddScoped<createGoal>();
builder.Services.AddScoped<getGoal>();
builder.Services.AddScoped<getGoals>();
builder.Services.AddScoped<updateGoal>();
builder.Services.AddScoped<deleteGoal>();

builder.Services.AddScoped<createTransaction>();
builder.Services.AddScoped<getTransaction>();
builder.Services.AddScoped<getTransactionByDate>();
builder.Services.AddScoped<getTransactionsByTimePeriod>();
builder.Services.AddScoped<deleteTransaction>();
builder.Services.AddScoped<updateTransaction>();

builder.Services.AddScoped<createCategory>();
builder.Services.AddScoped<getCategory>();
builder.Services.AddScoped<getCategories>();
builder.Services.AddScoped<deleteCategory>();
builder.Services.AddScoped<updateCategory>();

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<HashingServices>();
builder.Services.AddScoped<AuthenticationService>(); 
builder.Services.AddSingleton<JwtSecurityTokenHandler>();

builder.Services.AddAuthentication(auth =>
    {
        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(be =>
{
    be.RequireHttpsMetadata = false;
    be.SaveToken = true;
    be.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["SecuritySettings:jwtSecretKey"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        NameClaimType = System.Security.Claims.ClaimTypes.Name,
        ClockSkew = TimeSpan.FromMinutes(2)
    };
});

builder.Services.AddAuthorization(options => options.FallbackPolicy = null);
builder.Services.AddDbContext<Context>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Context>();
    context.Database.Migrate(); 
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "API de contas e transações bancárias";
        options.Theme = ScalarTheme.Mars;
        options.DefaultHttpClient =
            new KeyValuePair<ScalarTarget, ScalarClient>(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();