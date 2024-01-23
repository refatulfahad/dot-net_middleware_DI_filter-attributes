using Middleware.Configuration.Filters;
using Middleware.CustomMiddleware;
using Middleware.Factory;
using Middleware.Interfaces;
using Middleware.Services;

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
#else
//builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

builder.Services.AddScoped<MailFactory>();

builder.Services.AddScoped<LocalMailService>()
                .AddScoped<IMailService, LocalMailService>(s => s.GetService<LocalMailService>());

builder.Services.AddScoped<CloudMailService>()
            .AddScoped<IMailService, CloudMailService>(s => s.GetService<CloudMailService>());


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Middleware 1.0"));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Use(async (context, next) =>
//{
//    if (!context.Request.Path.Value.Contains("1"))
//    {
//        context.Response.Redirect(context.Request.Path.Value + "/1");
//        return;
//    }

//    await next.Invoke();
//});

app.UseMiddleware<RedirectMiddleware>();

app.Run();
