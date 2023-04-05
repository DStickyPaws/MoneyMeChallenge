var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(swaggerGenOptions => {
    swaggerGenOptions.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "MoneyMeChallenge API",
        Description = "",
        TermsOfService = new Uri("https://github.com/DStickyPaws/MoneyMeChallenge/blob/RestServerCoding/TermsOfService.md"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "D`StickyPaws",
            Url = new Uri("https://github.com/DStickyPaws")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://github.com/DStickyPaws/MoneyMeChallenge/blob/RestServerCoding/LICENSE")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(swaggerOptions => {
        swaggerOptions.SerializeAsV2 = true;
    });

    app.UseSwaggerUI(swaggerUIOptions => {
        swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        swaggerUIOptions.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();