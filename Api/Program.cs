using Audit.Core;
using Audit.Core.Providers;
using Audit.PostgreSql.Providers;
using Business.Services.UserServices;
using DataAccess.Concrete;
using DataAccess.Discrete;
using Microsoft.EntityFrameworkCore;

Configuration.Setup()
    .UseConditional(c => c
        .When(a => true, new PostgreSqlDataProvider(p =>
        {
            p.ConnectionString(Environment.GetEnvironmentVariable("POSTGRE_URI"));
            p.TableName("AuditHistory");
            p.IdColumnName("Id");
            p.DataColumn("Data");
        }))
        .Otherwise(new FileDataProvider(f =>
        {
            f.FilenameBuilder(file => $"{DateTime.Now:dd.MM.yyyy HH mm ss}_{DateTime.Now.Ticks}");
            f.FilenamePrefix("AuditLogs-");
            f.Directory("AuditLogs");
        })));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<DataContext>().Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
