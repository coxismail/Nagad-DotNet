using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication1testnagad.Models;
using Nagad.Models;
namespace WebApplication1testnagad.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
           
            var Nagadhelper = new NagadHelperClass();
            try
            {
                var p = await Nagadhelper.client.Payment.Initialize("6534");
                if (p.PaymentReferenceId != null && p.PaymentReferenceId != string.Empty)
                {
                    decimal amount = Convert.ToDecimal(100.00);
                    var c = new CompletePaymentRequest("050", amount, p.Random, "6534");
                  var comp =  await Nagadhelper.client.Payment.Complete(p.PaymentReferenceId, c);
                }
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
            }
            //var p = await myclass.client.Payment.Complete("6534");

            return View();
        }


    }
}