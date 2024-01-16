using Reddot.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Supabase connection info
var url = Environment.GetEnvironmentVariable("SUPABASE_URL") ?? "";
var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<Supabase.Client>(_ => new Supabase.Client(url, key));

var app = builder.Build();
var port = Environment.GetEnvironmentVariable("port") ?? "3000";

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run($"https://localhost:{port}");
