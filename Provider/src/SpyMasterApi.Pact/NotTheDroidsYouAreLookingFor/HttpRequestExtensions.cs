using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SpyMasterApi.Pact
{
    public static class HttpRequestExtensions
    {
        public static bool HasBody(this HttpRequest request)
        {
            return request.Body != null;
        }
        public static T GetBodyAsync<T>(this HttpRequest request)
        {
            string jsonRequestBody;
            using (var reader = new StreamReader(request.Body, Encoding.UTF8))
            {
                jsonRequestBody = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<T>(jsonRequestBody);
        } 
    }
}