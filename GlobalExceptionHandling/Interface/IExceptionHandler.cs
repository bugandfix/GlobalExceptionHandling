namespace GlobalExceptionHandling.Interface;

public interface IExceptionHandler
{
    ValueTask<bool> TryHandleAsync(HttpContext httpContext,Exception exception, CancellationToken cancellationToken);

}