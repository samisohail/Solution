namespace Web.Setup
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;
        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await _next(context);
        }
    }
}
