using Cinema.Core;
using Cinema.Core.Interfaces;
using Cinema.Core.Services;
using Cinema.Storage;
using Cinema.Storage.Contexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Local")));

builder.Services.AddAutoMapper(typeof(MappingProfile));
    
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IMovieService, MovieService>();
// builder.Services.AddScoped<IUserService, UserService>();

// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = "NoAuth";  // A fake scheme for bypassing auth
//         options.DefaultChallengeScheme = "NoAuth";    // Use a dummy scheme if not needed
//     })
//     .AddScheme<AuthenticationSchemeOptions, NoAuthHandler>("NoAuth", options => {});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply CORS Middleware
app.UseCors("AllowFrontend");

// Configure Static File Middleware
var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration["UploadsDirectory"] ?? "public");

if (!Directory.Exists(uploadsDirectory))
{
    Directory.CreateDirectory(uploadsDirectory);
}

// Serve files from the "UploadedFiles" directory
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsDirectory),
    RequestPath = "/uploads" // The URL route to access the files
});

app.UseStaticFiles();  // Enables serving static files from wwwroot by default

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();