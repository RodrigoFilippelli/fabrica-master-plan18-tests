var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "healthy", service = "plan18-synthetic-fixture" }));

app.Run();

public partial class Program { }
