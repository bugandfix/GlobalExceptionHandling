
using GlobalExceptionHandling.Implementation;
using GlobalExceptionHandling.Interface;

var builder = WebApplication.CreateBuilder(args);

// Register the exception handler in DI
builder.Services.AddSingleton<IExceptionHandler, GlobalExceptionHandler>();
builder.Services.AddLogging();

var app = builder.Build();

// Use the global exception handler middleware
app.Use(async (context, next) =>
{
    var exceptionHandler = context.RequestServices.GetRequiredService<IExceptionHandler>();
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        await exceptionHandler.TryHandleAsync(context, ex, context.RequestAborted);
    }
});

// Define a test endpoint
app.MapGet("/test", () =>
{
    throw new InvalidOperationException("Simulated exception for testing!");
});

app.Run();
