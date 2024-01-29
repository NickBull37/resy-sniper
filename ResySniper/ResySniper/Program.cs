using ResySniper.Client;
using ResySniper.Configuration;
using ResySniper.Workflow;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure DI for application services
builder.Services.AddSingleton<ResyConfig>();
builder.Services.AddScoped<IResyClient, ResyClient>();

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => true)
                            .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var resyClient = scope.ServiceProvider.GetRequiredService<IResyClient>();
    var resyConfig = app.Services.GetRequiredService<ResyConfig>();

    var reservationWorkflow = new ReservationWorkflow(resyClient, resyConfig);
    await reservationWorkflow.ExecuteSniperWorkflow();
}

app.Run();
