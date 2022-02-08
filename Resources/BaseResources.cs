#region Assembly Nagad, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
#endregion
using Nagad.Models;
using System;

namespace Nagad.Resources
{
    public class BaseResources<TModel> : IResource<TModel> where TModel : BaseModel
    {
        public IRequester Requester { get; private set; }
        public TimeZoneInfo TimeZone { get; private set; }
        public string ContextPath { get; private set; }

        public BaseResources(IRequester requester, BaseMode mode, TypeOfTrans tmode, string basePath)
        {
            Requester = requester;
            if (mode == BaseMode.SandBox && tmode == TypeOfTrans.Payment)
            {
                switch (tmode)
                {
                    case TypeOfTrans.Payment:
                        ContextPath = "remote-payment-gateway-1.0" + basePath;
                        break;
                    case TypeOfTrans.Recharge:
                        ContextPath = "external-gateway-1.0/secure-handshake/dfs" + basePath;
                        break;
                    default:
                        break;
                }
                
            }
            else
            {
                ContextPath = basePath;
            }

           
        }
    }
}
