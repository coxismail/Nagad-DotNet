using System.Net.Http;
using System.Threading.Tasks;

namespace Nagad
{
    public class DefaultRoundtripper : IHttpRequester
    {
        readonly HttpClient client = new HttpClient();

        public HttpRequestMessage MakeHttpRequest(string method, string uri)
        {

            return new HttpRequestMessage(new HttpMethod(method), uri);
        }

        public Task<HttpResponseMessage> ExecuteHttpRequest(HttpRequestMessage request)
        {
            return client.SendAsync(request);
        }
    }
}
