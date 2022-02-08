using Nagad.Models;

namespace Nagad
{
    public interface ICreatable<TModel, TRequest> : IResource<TModel> where TModel : BaseModel where TRequest : Request
    {
    }
}
