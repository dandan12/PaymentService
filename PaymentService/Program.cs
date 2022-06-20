using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using PaymentService.Contexts;
using PaymentService.Models;
using PaymentService.Repositories;
using PaymentService.Repositories.Interface;
using PaymentService.Services;
using PaymentService.Services.Interface;
using PaymentService.Utils.Seed;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
var authSetting = new AuthenticationSetting();
builder.Configuration.Bind("AuthenticationSetting", authSetting);

var transactionDbSetting = new TransactionDbSetting();
builder.Configuration.Bind("TransactionDbSetting", transactionDbSetting);
services.AddScoped(x => transactionDbSetting);

var transactionDbContext = new TransactionDbContext(transactionDbSetting);
services.AddScoped(x => transactionDbContext);

var notificationDbSetting = new NotificationDbSetting();
builder.Configuration.Bind("NotificationDbSetting", notificationDbSetting);
services.AddScoped(x => notificationDbSetting);

var notificationDbContext = new NotificationDbContext(notificationDbSetting);
services.AddScoped(x => notificationDbContext);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
	var Key = Encoding.ASCII.GetBytes(authSetting.SecretKey);
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = authSetting.Issuer,
		ValidAudience = authSetting.Audience,
		IssuerSigningKey = new SymmetricSecurityKey(Key)
	};
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});

services.AddScoped<IPaymentRepository, PaymentRepository>();
services.AddScoped<INotificationRepository, NotificationRepository>();
services.AddScoped<IPaymentService, PaymentsService>();

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

await transactionDbSetting.ConfigureCosmosDb();
await notificationDbSetting.ConfigureCosmosDb();
//await PaymentSeed.Seed(cosmosDbContext);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
