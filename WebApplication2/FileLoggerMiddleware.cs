using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication
{
    public class FileLoggerMiddleware
    {
        private readonly string key = "HiddenInfo";
        private readonly RequestDelegate _next;

        public FileLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using(var streamWritter = new StreamWriter("calls.txt", true))
            {
                if (!context.Request.Cookies.ContainsKey(key))
                {
                    context.Response.Cookies.Append(key, "MyHiddenValue");
                }

                streamWritter.WriteLine(
                    $"{context.Request.Scheme}{context.Request.Host}{context.Request.Path}{context.Request.QueryString}");
            }

            await _next(context);
        }
    }
}
