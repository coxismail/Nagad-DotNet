using Nagad;

namespace WebApplication1testnagad.Models
{
    public class NagadHelperClass
    {
        public NagadClient client { get; set; }

        public NagadHelperClass()
        {

            client = new NagadClient(new Credentials(NagadInfo.marchendId, NagadInfo.publickey, NagadInfo.privatekey, null, NagadInfo.AcNo), BaseMode.SandBox); client.TimeZone = System.TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            client.APIVersion = "v-0.2.0";
        }
    }
    public static class NagadInfo
    {
        public static string marchendId { get; set; }
        public static string publickey { get; set; }
        public static string privatekey { get; set; }
        public static string AcNo { get; set; }
        static NagadInfo()
        {
            AcNo = " ";
            marchendId = " ";
            publickey = " ";
            privatekey = " ";
        }
    }
}