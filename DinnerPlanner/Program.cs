using DinnerPlanner.Data;
using DinnerPlanner.Data.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DinnerPlannerContext>(options => 
    options.UseSqlServer("Server=localhost;Database=DinnerPlanner;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"));

builder.Services.AddScoped<IDinnerPlanRepo, DinnerPlanRepo>();
builder.Services.AddScoped<IDishRepo, DishRepo>();
builder.Services.AddScoped<IDishCategoryRepo, DishCategoryRepo>();
builder.Services.AddScoped<IPersonRepo, PersonRepo>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DinnerPlannerContext>();    
    context.Database.Migrate();
}

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