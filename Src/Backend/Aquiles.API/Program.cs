using Aquiles.Application;
using Aquiles.Infrastructure;
using Aquiles.Application.Servicos;
using Aquiles.Infrastructure.Migrations;
using Aquiles.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddScoped(x => new AutoMapper.MapperConfiguration(builder => builder.AddProfile(new AutoMapperConfig())).CreateMapper());

#if DEBUG
builder.Services.AddCors(x => x.AddPolicy("aquiles", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#if DEBUG
app.UseCors("aquiles");
#endif

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

app.Run();

void MigrateDatabase()
{
    var connectionString = builder.Configuration.GetConnectionString("Connection");
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    Database.Migrate(connectionString, serviceScope.ServiceProvider);
}
