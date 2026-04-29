using lievee.Database;
using lievee.Middleware;
using lievee.Repositories;
using lievee.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(Database.Pool);
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// auth middleware
app.UseMiddleware<Authentication>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
