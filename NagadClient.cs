using Nagad.Resources;
using System;

namespace Nagad
{
    public class NagadClient
    {
        private string apiVersion = "v-0.2.0";
        readonly IRequester requester;

        /// <summary>
        /// Your Api Mode - If you are development preiod use sandbox otherwise API
        /// </summary>
        readonly BaseMode baseMode;
        /// <summary>
        /// Marchent ID, private and public key from NG is mandotory
        /// </summary>
        public readonly NagadPayment Payment;
        /// <summary>
        /// To use this feature You need provide Api key in credential class
        /// </summary>
        public readonly NagadRecharge Recharge;
        public TimeZoneInfo TimeZone { get; set; }

        public string APIVersion
        {
            get { return requester.APIVersion; }
            set { requester.APIVersion = value; }
        }

        public IRequester Requester
        {
            get { return requester; }
        }
        public BaseMode BaseMode
        {
            get { return baseMode; }
        }
        public NagadClient()
        {

        }
        public NagadClient(Credentials credential, BaseMode baseMode)
        {
            if (credential == null) throw new ArgumentNullException(nameof(credential));

            this.baseMode = baseMode == BaseMode.Api ? BaseMode.Api : BaseMode.SandBox;
            requester = new Requester(credential, this.baseMode, null, this.apiVersion );
            Payment = new NagadPayment(requester, baseMode);
            if (credential.ApiKey != null)
            {
                Recharge = new NagadRecharge(requester, baseMode);
            }
          
        }

    }
    
}
