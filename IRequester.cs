using System.Threading.Tasks;

namespace Nagad
{
    public interface IRequester
    {
        string APIVersion { get; set; }
        string MarchendId { get; set; } //add for test
        string AccountNumber { get; set; }
         Serializer Serializer { get;  set; }
        BaseMode BaseMode { get;  set; }
        Task<TResult> Request<TPayload, TResult>(BaseMode endpoint, string method, string path, TPayload payload)
            where TPayload : class
            where TResult : class;
        Task<TResult> Request<TResult>(BaseMode endpoint, string method, string path) where TResult : class;
    }
}
