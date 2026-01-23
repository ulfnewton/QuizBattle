using QuizBattle.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructureRepositories();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
