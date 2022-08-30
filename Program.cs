// <snippet_UsingModels>
using BookStoreApi.Models;
using Microsoft.OpenApi.Models;
// </snippet_UsingModels>
// <snippet_UsingServices>
using BookStoreApi.Services;
// </snippet_UsingServices>
// <snippet_UsingHelpers>
using BookStoreApi.Helpers;
// </snippet_UsingHelpers>

// <snippet_AddControllers>
// <snippet_BooksService>
// <snippet_BookStoreDatabaseSettings>
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<BookStoreDatabaseSettings>(
    builder.Configuration.GetSection("BookStoreDatabase"));
// </snippet_BookStoreDatabaseSettings>

builder.Services.AddSingleton<BooksService>();
// </snippet_BooksService>

builder.Services.AddControllers()
    .AddJsonOptions(
  options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// </snippet_AddControllers>

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
  // option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
  option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "Bearer"
  });
  option.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type=ReferenceType.SecurityScheme,
          Id="Bearer"
        }
      },
      new string[]{}
    }
  });
});

{
  var services = builder.Services;
  services.AddCors();
  services.AddControllers();

  // configure strongly typed settings object
  services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

  // configure DI for application services
  services.AddScoped<IUserService, UserService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();

}

// configure HTTP request pipeline
{
  // global cors policy
  app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

  // custom jwt auth middleware
  app.UseMiddleware<JwtMiddleware>();

  app.MapControllers();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("https://localhost:4012");