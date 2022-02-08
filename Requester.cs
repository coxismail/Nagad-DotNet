using Nagad.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Nagad
{
    public class Requester : IRequester
    {

        public Credentials Credentials { get; private set; }
        public string APIVersion { get; set; }
        public string MarchendId { get;  set; }
        public string PublicKey { get; private set; }
        public string AccountNumber { get;  set; }
        public string PrivateKey { get; private set; }
        public string Ip { get; private set; }
        public IHttpRequester Roundtripper { get; private set; }
        public BaseMode BaseMode { get;  set; }
        public Serializer Serializer { get;   set; }


        public Requester(Credentials creds, BaseMode mode, IHttpRequester roundtripper = null, string apiVersion = null)
        {
            if (creds == null) throw new ArgumentNullException(nameof(creds));
            Credentials = creds;
            APIVersion = apiVersion;
            MarchendId = creds.merchantId;
            PublicKey = creds.publickey;
            PrivateKey = creds.privatekey;
            AccountNumber = creds.AccountNumber;
            BaseMode = mode;
            Roundtripper = roundtripper ?? new DefaultRoundtripper();
            Serializer = new Serializer();
            Ip = GetIPAddress();
            RsaOperation.Init(creds.publickey, creds.privatekey);
        }
        public async Task<TResult> Request<TResult>(BaseMode baseUrl, string method, string path) where TResult : class
        {
            return await Request<object, TResult>(baseUrl, method, path, null);
        }
        public async Task<TResult> Request<TPayload, TResult>(BaseMode endpoint, string method, string path, TPayload payload) where TPayload : class where TResult : class
        {
            var apiPrefix = ResolveEndpoint(endpoint);
            var request = Roundtripper.MakeHttpRequest(method, apiPrefix + path);
            request.Headers.Add("X-KM-IP-V4", "10.55.247.69");
            request.Headers.Add("X-KM-Client-Type", ClientType.PC_WEB.ToString());
            request.Headers.Add("X-KM-Api-Version", APIVersion);
            if (!string.IsNullOrEmpty(APIVersion)) request.Headers.Add("X-KM-Api-Version", APIVersion);
            if (payload != null)
            {
                using (var ms = new MemoryStream())
                {
                   var content =  JsonConvert.SerializeObject(payload);
                    request.Content = new StringContent(content, Encoding.UTF8, "application/json");
                }
            }

            // roundtrips the request
            try
            {
                var response = await Roundtripper.ExecuteHttpRequest(request);
                var stream = await response.Content.ReadAsStreamAsync();
                if (!response.IsSuccessStatusCode)
                {
                    var error = Serializer.JsonDeserialize<ErrorResult>(stream) ?? new ErrorResult();
                    error.HttpStatusCode = response.StatusCode;
                    throw new NagadError(error, null);
                }

                var result = Serializer.JsonDeserialize<TResult>(stream);
                var model = result as BaseModel;
                if (model != null)
                {
                    model.Requester = this;
                }

                return result;

            }
            catch (HttpRequestException e)
            {
                throw new NagadException("Error while making HTTP request", e);
            }
        }


        string ResolveEndpoint(BaseMode mode)
        {
            if (mode == BaseMode.Api)
            {
                return "https://api.mynagad.com/";
            }
            else
            {
                return "http://sandbox.mynagad.com:10080/";
            }
        }
        string GetIPAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            return ipAddress.ToString();
        }
    }


   
}

