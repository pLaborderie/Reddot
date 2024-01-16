using System.Text;
using Microsoft.IdentityModel.Tokens;
using Reddot.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Supabase connection info
var url = Environment.GetEnvironmentVariable("SUPABASE_URL") ?? "";
var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");
var supabaseSecretKey = Environment.GetEnvironmentVariable("SUPABASE_SECRET_KEY");
var supabaseSignatureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(supabaseSecretKey!));
var validAudiences = new List<string>() { "authenticated" };
var validIssuer = Environment.GetEnvironmentVariable("SUPABASE_URL") + "/auth/v1";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserVoteRepository, UserVoteRepository>();
builder.Services.AddSingleton(_ => new Supabase.Client(url, key));
builder.Services.AddAuthentication().AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = supabaseSignatureKey,
        ValidAudiences = validAudiences,
        ValidIssuer = validIssuer
    };
});

var app = builder.Build();
var port = Environment.GetEnvironmentVariable("port") ?? "3000";

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(corsPolicyBuilder => corsPolicyBuilder
        .WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run($"https://localhost:{port}");
