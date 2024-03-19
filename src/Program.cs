//using API_EF.Database;
//global: evita tener que declarar estos usings 
global using API_EF.Database;
global using API_EF.Database.Repository;
global using API_EF.Database.Repository.Contracts;
global using API_EF.Models;
global using API_EF.Models.Request;
global using API_EF.Services;
using Newtonsoft.Json;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBContext>();
builder.Services.AddTransient(typeof(ICommonRepository<>), typeof(CommonRepository<>));
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IProductoRepository, ProductoRepository>();
builder.Services.AddTransient<IProductoService, ProductoService>();
builder.Services.AddCors(policy =>
{
    policy.AddDefaultPolicy(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

builder.Host.UseSerilog();


//builder.Services.AddDbContext<DBContext>(
//    o => o.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1); // Oculta los schemas
        options.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
    });
}

app.UseCors(options =>
{
    options
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
