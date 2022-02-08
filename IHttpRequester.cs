using System.Net.Http;
using System.Threading.Tasks;

namespace Nagad
{
    public interface IHttpRequester
    {
        HttpRequestMessage MakeHttpRequest(string method, string uri);
        Task<HttpResponseMessage> ExecuteHttpRequest(HttpRequestMessage request);
    }
}
