using Nagad.Models;
using System.IO;
using System.Threading.Tasks;

namespace Nagad.Resources
{
    public class NagadPayment : BaseResources<Payment>, ICreatable<PaymentRequestResult, CreatePaymentRequest>
    {

        public NagadPayment(IRequester requester, BaseMode mode) : base(requester, mode, TypeOfTrans.Payment, "/api/dfs/")
        {

        }
        /// <summary>
        /// First Step to Make Payment
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns>PaymentReference Id</returns>
        ///  <returns>Accepted DateTIme </returns>
        ///  <retrun>Random</retrun>
        public async Task<PaymentRequestResult> Initialize(string OrderId)
        {
            var se = new snd(System.TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time"))
            {
                merchantId = this.Requester.MarchendId,
                orderId = OrderId,
            };
            CreatePaymentRequest rs = new CreatePaymentRequest();
            using (var ms = new MemoryStream())
            {
                this.Requester.Serializer.JsonSerialize(ms, se);
                var buffer = ms.ToArray();
                rs.signature = RsaOperation.SignData(buffer);
                rs.sensitiveData = RsaOperation.Encrypt(buffer);
            }
            rs.accountNumber = this.Requester.AccountNumber ?? " ";
            rs.dateTime = se.datetime;


            return await Requester.Request<CreatePaymentRequest, PaymentRequestResult>(
               this.Requester.BaseMode,
                "POST",
                $"{ContextPath}check-out/initialize/{this.Requester.MarchendId}/{OrderId}", rs
            );
        }


        /// <summary>
        /// Second Step to make payment
        /// </summary>
        /// <param name="paymentReferenceId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PaymentCompleteResult> Complete(string paymentReferenceId, CompletePaymentRequest request)
        {
            request.merchantId = this.Requester.MarchendId;
            var completePay = new CompletePayment()
            {
                additionalMerchantInfo = "",
                merchantCallbackURL = "https://hellow.com",
            };
            using (var ms = new MemoryStream())
            {
                this.Requester.Serializer.JsonSerialize(ms, request);
                var buffer = ms.ToArray();
                completePay.signature = RsaOperation.SignData(buffer);
                completePay.sensitiveData = RsaOperation.Encrypt(buffer);
            }
            return await Requester.Request<CompletePayment, PaymentCompleteResult>(
                 this.Requester.BaseMode,
                "POST",
                $"{ContextPath}check-out/Complete/{paymentReferenceId}", completePay
            );
        }


        /// <summary>
        /// Payment Verification providing Payment Reference Id
        /// </summary>
        /// <param name="paymentReferenceId"></param>
        /// <returns></returns>
        public async Task<Payment> Verify(string paymentReferenceId)
        {
            return await Requester.Request<Payment>(
               this.Requester.BaseMode,
                "GET",
                $"{ContextPath}/verify/payment/{paymentReferenceId}/"
            );
        }
    }
}
