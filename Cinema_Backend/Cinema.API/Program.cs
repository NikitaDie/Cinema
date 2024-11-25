using Cinema_Backend.Filters;
using Cinema.Core;
using Cinema.Core.DTOs;
using Cinema.Core.DTOs.Branch;
using Cinema.Core.Interfaces;
using Cinema.Core.Interfaces.Extra;
using Cinema.Core.Services;
using Cinema.Core.Services.Extra;
using Cinema.Core.Validators;
using Cinema.Core.Validators.Branch;
using Cinema.Core.Validators.Movie;
using Cinema.Storage;
using Cinema.Storage.Contexts;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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

#region Services

builder.Services.AddScoped<IFileUploadService, FileLocalUploadService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IBranchService, BranchService>();

#endregion

#region Validators
// builder.Services.AddValidatorsFromAssemblyContaining<CreateMovieDtoValidator>();

//Movie
builder.Services.AddScoped<IValidator<CreateMovieDto>, CreateMovieDtoValidator>();

//Branch
builder.Services.AddScoped<IValidator<CreateBranchDto>, CreateBranchDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateBranchDto>, UpdateBranchDtoValidator>();

#endregion

builder.Services.AddScoped<ValidationFilter>();
//--------------------------------------------------------

// builder.Services.AddScoped<IUserService, UserService>();

// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = "NoAuth";  // A fake scheme for bypassing auth
//         options.DefaultChallengeScheme = "NoAuth";    // Use a dummy scheme if not needed
//     })
//     .AddScheme<AuthenticationSchemeOptions, NoAuthHandler>("NoAuth", options => {});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

// Turns off automatic Validation Check
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

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