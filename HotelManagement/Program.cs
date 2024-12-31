using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using HotelManagement.Config;
using HotelManagement.Core.MappingProfiles;
using HotelManagement.Core.ViewModels.Authentication;
using HotelManagement.Extensions;
using HotelManagement.Middlewares;
using HotelManagement.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
#region AddSwaggerGen
#endregion
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule(new AutofacModule());
});
builder.Services.AddAutoMapper(typeof(RoomProfile).Assembly);

builder.Services.AddCompressionServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseStaticFiles();


app.MapControllers();

MappingExtensions.Mapper = app.Services.GetRequiredService<IMapper>();

#region Custom Middleware
app.UseMiddleware<GlobalErrorHandlerMiddleware>();
app.UseMiddleware<TransactionMiddleware>();
#endregion
app.UseAuthorization();

app.MapControllers();

app.Run();
