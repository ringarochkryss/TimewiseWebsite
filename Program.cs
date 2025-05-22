using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Salto.Data;

var builder = WebApplication.CreateBuilder(args);

// Detect Heroku environment
var isHeroku = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DYNO"));

// Load environment variables from .env file (for local dev)
//DotNetEnv.Env.Load();

// Configure Forwarded Headers (Heroku proxy)
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    if (isHeroku)
    {
        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();
    }
});

// Configure HTTPS redirection
builder.Services.AddHttpsRedirection(options =>
{
    if (isHeroku)
    {
        options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
        options.HttpsPort = 443;
    }
});

// Get connection string
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string not found.");
var serverVersion = ServerVersion.AutoDetect(connectionString);

// Register DbContext (MySQL)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// Register Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Stores.MaxLengthForKeys = 128;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

// Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(80);
    options.Lockout.MaxFailedAccessAttempts = 15;
    options.Lockout.AllowedForNewUsers = false;

    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.AddRazorPages();
builder.Services.AddScoped<ArticleRepository>();

// Set port for Heroku
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://*:{port}");
}

var app = builder.Build();

// Use Forwarded Headers and HTTPS Redirection early in the pipeline
app.UseForwardedHeaders();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

// Seed data (optional)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    // context.Database.Migrate();

    var userMgr = services.GetRequiredService<UserManager<IdentityUser>>();
    var rolerMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
    // IdentitySeedData.Initialize(context, userMgr, rolerMgr).Wait();
}

app.Run();