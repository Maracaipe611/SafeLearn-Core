using Microsoft.Extensions.Options;
using SafeLearn.Configurations;
using SafeLearn.Data;
using Microsoft.EntityFrameworkCore;
using SafeLearn.Usuario;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<TwilioConfiguration>(opt =>
{
    opt.Auth = builder.Configuration.GetSection($"{nameof(TwilioConfiguration)}:{nameof(TwilioConfiguration.Auth)}").Value;
    opt.AccountSID = builder.Configuration.GetSection($"{nameof(TwilioConfiguration)}:{nameof(TwilioConfiguration.AccountSID)}").Value;
    opt.Number = builder.Configuration.GetSection($"{nameof(TwilioConfiguration)}:{nameof(TwilioConfiguration.Number)}").Value;
});
builder.Services.AddSingleton<ITwilioConfiguration>(sp => sp.GetRequiredService<IOptions<TwilioConfiguration>>().Value);
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddDbContext<SafeLearnContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SafeLearnConnection"));
});
builder.Services.AddScoped<IDbConnection>(c => new SqlConnection(builder.Configuration.GetConnectionString("SafeLearnConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAnyOrigin");
app.UseRouting();
app.Run();
