using DOCSAN.API;
using DOCSAN.APPLICATION;
using DOCSAN.INFRASTRUCTURE;
using DOCSAN.INFRASTRUCTURE.Migrations;
using DOCSAN.INFRASTRUCTURE.Services;
using DOCSAN.SHARED;
using FluentMigrator.Runner;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSharedServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllersWithViews();
builder.Services.AddFluentMigratorCore()
                    .ConfigureRunner(
                    migrationBuilder => migrationBuilder
                    .AddSqlServer()
                    .WithGlobalConnectionString(builder.Configuration.GetConnectionString("SqlConnection"))
                    .WithMigrationsIn(typeof(TableMigration).Assembly));

var app = builder.Build();
app.MigrateDatabase(builder.Configuration);

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseAuthorization();

app.MapControllers();

app.Run();
