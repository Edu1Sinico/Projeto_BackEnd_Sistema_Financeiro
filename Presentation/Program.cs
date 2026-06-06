using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.UseCases.AccountServices;
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

builder.Services.Configure<SecuritySettings>(
    builder.Configuration.GetSection("SecuritySettings")
);
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();


builder.Services.AddScoped<createUser>();
builder.Services.AddScoped<getUser>();
builder.Services.AddScoped<updateUser>();
builder.Services.AddScoped<deleteUser>();


builder.Services.AddScoped<createAccount>();
builder.Services.AddScoped<updateAccount>();
builder.Services.AddScoped<deleteAccount>();


builder.Services.AddScoped<createGoal>();
builder.Services.AddScoped<getGoal>();
builder.Services.AddScoped<getGoals>();
builder.Services.AddScoped<updateGoal>();
builder.Services.AddScoped<deleteGoal>();


builder.Services.AddScoped<createTransaction>();
builder.Services.AddScoped<getTransaction>();
builder.Services.AddScoped<getTransactionByDate>();
builder.Services.AddScoped<getTransactionsByTimePeriod>();


builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<HashingServices>();
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
    app.MapOpenApi();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
