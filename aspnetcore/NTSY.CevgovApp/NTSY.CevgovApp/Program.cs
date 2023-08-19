
using Azure;
using NTSY.CevgovApp.Domain;
using NTSY.CevgovApp.Infastructure;
using NTSY.CevgovApp.Application;
using NTSY.CevgovApp;
using Newtonsoft.Json.Converters;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().
    WithExposedHeaders("Content-Disposition"); ;
}));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// chuyển về tên đối tượng là PascalCase
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// lấy connectionString từ file appsetting.json
var connectionString = builder.Configuration["ConnectionString"];

builder.Services.AddScoped<IUnitOfWork>(sp =>
new UnitOfWork(connectionString));
builder.Services.AddScoped<IEmulationTitleRepository,EmulationTitleRepository>();

builder.Services.AddScoped<IEmultionTitleService, EmulationTitleService>();
builder.Services.AddScoped<IEmulationTitleManager, EmulationTitleManager>();
builder.Services.AddScoped<IAwardService, AwardService>();
builder.Services.AddScoped<IAwardRespository, AwardRespository>();



// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors();
app.Run();
