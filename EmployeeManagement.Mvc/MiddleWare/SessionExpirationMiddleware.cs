namespace EmployeeManagement.Mvc.MiddleWare
{
    public class SessionExpirationMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionExpirationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var tokenExpiryStr = context.Session.GetString("TokenExpiry");

            if (!string.IsNullOrEmpty(tokenExpiryStr) &&
                DateTime.TryParse(tokenExpiryStr, out DateTime tokenExpiry))
            {
                if (DateTime.UtcNow >= tokenExpiry)
                {
                    context.Session.Clear(); 
                    context.Response.Redirect("/User/LogInPage");
                    return;
                }
            }

            await _next(context);
        }
    }

}
