using Nagad.Models;
using System.Threading.Tasks;

namespace Nagad.Resources
{
    public class NagadRecharge : BaseResources<Recharge>, ICreatable<Recharge, CreateRechargeRequest>
    {

        public NagadRecharge(IRequester requester, BaseMode mode) : base(requester, mode, TypeOfTrans.Recharge, "/api/")
        {

        }
        public async Task<Recharge> Initialize(CreateRechargeRequest createRechargeRequest)
        {
            return await Requester.Request<CreateRechargeRequest, Recharge>(
               this.Requester.BaseMode,
                "POST",
                $"{ContextPath}/recharge/", createRechargeRequest
            );
        }

        public async Task<Recharge> Deposits()
        {
            return await Requester.Request<Recharge>(
                 this.Requester.BaseMode,
                "POST",
                $"{ContextPath}/secure-session/dfs/recharge/remittance/"
            );
        }
    }

}
