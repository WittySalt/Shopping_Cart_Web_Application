using Shopping_Cart_Web_Application_V1._0.Data;

namespace Shopping_Cart_Web_Application_V1._0.Middleware
{
    public class TrackLoginMiddleware
    {
        private readonly RequestDelegate next;

        public TrackLoginMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ApplicationDbContext db)
        {
            await next(context);
        }
    }

}
