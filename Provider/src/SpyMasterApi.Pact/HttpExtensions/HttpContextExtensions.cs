namespace SpyMasterApi.Pact.HttpExtensions
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public static class HttpContextExtensions
    {
        public static async Task OkResponse(this HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsync(string.Empty);
        }

        public static bool IsPost(this HttpContext context)
        {
            return context.Request.Method == HttpMethod.Post.ToString();
        }
    }
}