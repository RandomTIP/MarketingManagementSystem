using MMS.API;
using MMS.API.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Setup(builder.Configuration, builder.Environment);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddCors(setup =>
    setup.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseMiddleware<ExceptionHandler>();
    app.UseHttpLogging();
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(swaggerOptions => {
    swaggerOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "MMS.API v1");
    swaggerOptions.DefaultModelsExpandDepth(0);
    swaggerOptions.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
