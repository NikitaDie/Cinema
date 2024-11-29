using Cinema_Backend.Filters;
using Cinema.Core;
using Cinema.Core.DTOs;
using Cinema.Core.DTOs.Branch;
using Cinema.Core.DTOs.Session;
using Cinema.Core.Interfaces;
using Cinema.Core.Interfaces.Extra;
using Cinema.Core.Services;
using Cinema.Core.Services.Extra;
using Cinema.Core.Validators.Branch;
using Cinema.Core.Validators.Movie;
using Cinema.Core.Validators.Pricelist;
using Cinema.Storage;
using Cinema.Storage.Contexts;
using Cinema.Storage.Utils;
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

#region Services
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IFileUploadService, FileLocalUploadService>();

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IAuditoriumService, AuditoriumService>();
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddScoped<DataSeeder>();

#endregion

#region Validators
// builder.Services.AddValidatorsFromAssemblyContaining<CreateMovieDtoValidator>();

//Movie
builder.Services.AddScoped<IValidator<CreateMovieDto>, CreateMovieDtoValidator>();

//Branch
builder.Services.AddScoped<IValidator<CreateBranchDto>, CreateBranchDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateBranchDto>, UpdateBranchDtoValidator>();

//Pricelist
builder.Services.AddScoped<IValidator<CreatePricelistDto>, CreatePricelistDtoValidator>();

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
    options.Filters.Add<ExceptionFilter>();
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
var staticFilesRequestPath = Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration["StaticFilesRequestPath"] ?? "/uploads");

if (!Directory.Exists(uploadsDirectory))
{
    Directory.CreateDirectory(uploadsDirectory);
}

// Serve files from the "UploadedFiles" directory
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsDirectory),
    RequestPath = staticFilesRequestPath // The URL route to access the files
});

app.UseStaticFiles();  // Enables serving static files from wwwroot by default

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    //Seed the database
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SeedAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();