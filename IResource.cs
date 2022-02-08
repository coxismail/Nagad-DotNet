using Nagad.Models;

namespace Nagad
{
    public interface IResource<TModel> where TModel : BaseModel
    {
        IRequester Requester { get; }
        string ContextPath { get; }
    }

    public enum BaseMode
    {
        SandBox = 0,
        Api = 1
    }
    public enum TypeOfTrans { 
        Payment, Recharge
    }
}
