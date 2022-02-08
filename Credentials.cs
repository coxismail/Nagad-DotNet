using System;

namespace Nagad
{
    public sealed class Credentials
    {
      //  public delegate Key KeySelector(Credentials creds);
      //  public static readonly KeySelector UsePublicKey;
     //   public static readonly KeySelector UseSecretKey;

     
        public Credentials(string ma_id = null, string pub_key = null, string priv_key = null, string apiKey = null, string accountNumber = null)
        {
            if (string.IsNullOrEmpty(ma_id) && string.IsNullOrEmpty(pub_key))
            {
                throw new ArgumentException("Marchent Id and Public Key can't both be null");
            }

            merchantId = ma_id;
            publickey = pub_key;
            privatekey = priv_key;
            ApiKey = apiKey;
            AccountNumber = accountNumber;
        }

        public string merchantId { get; }
        public string publickey { get; }
        public string privatekey { get; }
        public string ApiKey { get; set; }
        public string AccountNumber { get; set; }
    }
    //public struct Key
    //{
    //    string value;
    //    public bool IsTest => value.Contains("_test_");
    //    public bool IsLive => !IsTest;

    //    public string EncodeForAuthorizationHeader()
    //    {
    //        var encoded = Encoding.GetEncoding("ISO-8859-1").GetBytes($"{value}:");
    //        return $"Basic {Convert.ToBase64String(encoded)}";
    //    }

    //    public static implicit operator Key(string value) => new Key { value = value };
    //    public static implicit operator string(Key key) => key.value;

    //    public override string ToString() => this;
    //}
}



