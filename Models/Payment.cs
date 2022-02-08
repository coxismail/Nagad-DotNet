namespace Nagad.Models
{
    public partial class Payment : BaseModel
    {

        public string ReturnURI { get; set; }
        public string OrderId { get; set; }
        public string paymentReferenceId { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public long Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var another = obj as Payment;
            if (another == null) return false;

            return base.Equals(obj) &&
                object.Equals(this.Status, another.Status) &&
                object.Equals(this.Amount, another.Amount) &&
                object.Equals(this.CurrencyCode, another.CurrencyCode) &&
                object.Equals(this.Description, another.Description) &&
                object.Equals(this.Status, another.Status) &&
                object.Equals(this.OrderId, another.OrderId) &&
                true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                if (Status != default(PaymentStatus))
                {
                    hash = hash * 23 + Status.GetHashCode();
                }
                if (Amount != default(long))
                {
                    hash = hash * 23 + Amount.GetHashCode();
                }
                if (CurrencyCode != default(string))
                {
                    hash = hash * 23 + CurrencyCode.GetHashCode();
                }
                if (Description != default(string))
                {
                    hash = hash * 23 + Description.GetHashCode();
                }
                return hash;
            }
        }
    }
}

