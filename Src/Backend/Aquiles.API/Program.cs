using Aquiles.Application;
using Aquiles.Infrastructure;
using Aquiles.Application.Servicos;
using Aquiles.Infrastructure.Migrations;
using Aquiles.API.Filters;

var cadastrosDevCors = "myHosts";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: cadastrosDevCors,
                        policy =>
                        {
                            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                        });
});

builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Aquiles API", Version = "1.0" });
    option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header utilizando o Bearer token. Exemplo: \"Authorization: Bearer {token}\""
    });
    option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthorization();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddScoped(x => new AutoMapper.MapperConfiguration(builder => builder.AddProfile(new AutoMapperConfig())).CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cadastrosDevCors);

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

MigrateDatabase();

app.UseRouting();

app.UseAuthorization();

app.Run();

void MigrateDatabase()
{
    var connectionString = builder.Configuration.GetConnectionString("Connection");
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    Database.Migrate(connectionString, serviceScope.ServiceProvider);
}
