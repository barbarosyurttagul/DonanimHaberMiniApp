using DH.Business.Abstract;
using DH.Business.Concrete;
using DH.Core.CrossCuttingConcerns.Caching;
using DH.Core.CrossCuttingConcerns.Caching.Redis;
using DH.DataAccess.Abstract;
using DH.DataAccess.Concrete.AdoNet;
using DH.MvcUI.Utilities;
using RabbitMQ.Client;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Dependency Injections
builder.Services.AddScoped<IPostService, PostManager>();
builder.Services.AddScoped<IPostDal, AdoPostDal>();
builder.Services.AddSingleton<ICacheManager, RedisCacheManager>();
builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();


//Redis Caching
IConfiguration configuration = builder.Configuration;
var multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
