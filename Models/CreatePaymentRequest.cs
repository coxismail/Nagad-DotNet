using Newtonsoft.Json;
using System;

namespace Nagad.Models
{
    public class CreatePaymentRequest : Request
    {
        [JsonProperty("accountNumber")]
        public string accountNumber { get; set; }
        [JsonProperty("dateTime")]
        public string dateTime { get; set; }
        /// <summary>
        /// You Don't need to pass anything here
        /// </summary>
        public string sensitiveData { get; set; }
        /// <summary>
        /// You Don't need to pass anything here
        /// </summary>
        public string signature { get; set; }
    }
    public class PaymentRequestResult : BaseModel
    {
        [JsonProperty("paymentReferenceId")]
        public string PaymentReferenceId { get; set; }
        [JsonProperty("acceptDateTime")]
        public string acceptDateTime { get; private set; }
        [JsonProperty("Random")]
        public string Random { get; set; }

    }
    [Serializable]
    public class snd // for initilaize request
    {
        public string merchantId { get; set; }
        public string orderId { get; set; }
        public string challenge = RsaOperation.GenerateRandomString(40);
        public string datetime { get; set; }
        public snd(TimeZoneInfo timezone)
        {
            var zone = timezone ?? System.TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone).ToString("yyyyMMddHHmmss");
        }
    }

    public class CompletePaymentRequest 
    {
        public string merchantId { get; set; }
        public string currencyCode { get; private set; }
        public decimal amount { get; private set; }
        public string challenge { get;  private set; }
        public string orderId { get; private set; }
        public CompletePaymentRequest(string CurrencyCode, decimal Amount, string Challenge, string OrderId)
        {
            this.amount = Amount;
            this.currencyCode = CurrencyCode;
            this.challenge = Challenge;
            this.orderId = OrderId;

        }


    }
    public class CompletePayment : Request// for complete request               
    {
        public string sensitiveData { get; set; }
        public string signature { get; set; }
        public string merchantCallbackURL { get; set; }
        public string additionalMerchantInfo { get; set; }
    }

    public class PaymentCompleteResult :BaseModel
    {
  
    }
}
