using Microsoft.Extensions.Options;
using Middleware.Configuration.Filters;
using Middleware.CustomMiddleware;
using Middleware.Factory;
using Middleware.Interfaces;
using Middleware.Services;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new MyFilter("Global Filter"));
});
builder.Services.AddSwaggerGen();


//using condition
#if DEBUG == true
//builder.Services.AddTransient<IMailService, LocalMailService>();
//builder.Services.Configure<ServiceProviderOptions>(options =>
//{
//    options.ValidateScopes = true;
//});
#else
//builder.Services.AddTransient<IMailService, CloudMailService>();
#endif





builder.Services.AddScoped<MailFactory>();

builder.Services.AddScoped<LocalMailService>()
                .AddScoped<IMailService, LocalMailService>(s => s.GetService<LocalMailService>());

builder.Services.AddScoped<CloudMailService>()
            .AddScoped<IMailService, CloudMailService>(s => s.GetService<CloudMailService>());

builder.Services.AddSingleton<SingletonService>();
builder.Services.AddScoped<ScopedService>();
builder.Services.AddTransient<TransientService>();

var app = builder.Build();
//options.ValidateScopes = true;
// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Middleware 1.0"));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Use(async (context, next) =>
//{
//    // Get all the services and increase their counters...
//    //var singleton = context.RequestServices.GetRequiredService<SingletonService>();
//    //var scoped = context.RequestServices.GetRequiredService<ScopedService>();
//    //var transient = context.RequestServices.GetRequiredService<TransientService>();


//    await next.Invoke();
//});

//app.UseMiddleware<RedirectMiddleware>();

//app.Run(async ctx =>
//{
//    //then do it again...
//    var singleton = ctx.RequestServices.GetRequiredService<SingletonService>();
//    var scoped = ctx.RequestServices.GetRequiredService<ScopedService>();
//    var transient = ctx.RequestServices.GetRequiredService<TransientService>();

//    singleton.Counter++;
//    scoped.Counter++;
//    transient.Counter++;

//    // ...and display the counter values.
//    await ctx.Response.WriteAsync($"Singleton: {singleton.Counter}\n"); // 2 4 
//    await ctx.Response.WriteAsync($"Scoped: {scoped.Counter}\n"); // 2  2
//    await ctx.Response.WriteAsync($"Transient: {transient.Counter}\n"); // 1  1

//});

app.Run();
