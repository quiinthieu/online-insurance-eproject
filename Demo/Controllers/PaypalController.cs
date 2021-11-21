using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BraintreeHttp;
using Demo.Models;
using Demo.PayPal;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PayPal.Core;
using PayPal.v1.Payments;

namespace Demo.Controllers
{
    [ApiController]
    [Route("payment/paypal")]
    public class PaypalController : Controller
    {
        private IConfiguration _configuration;
        private IPremiumTransactionService _premiumTransactionService;
        private static string FAILED_URL = "paypal/CheckoutFail";
        private static string SUCCESS_URL = "paypal/CheckoutSuccess";

        public PaypalController(IConfiguration configuration, IPremiumTransactionService premiumTransactionService)
        {
            _configuration = configuration;
            _premiumTransactionService = premiumTransactionService;
        }


        [HttpPost("check-out")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PaypalCheckout([FromBody] List<PremiumTransaction> transactions)
        {
            var hostname = _configuration["Client:ReturnUrl"].ToString();
            /*    var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";*/
            var payment = PayPalService.createPayment(transactions, hostname);
            var paypalConfigInfo = PayPalService.getPayPalConfig(_configuration);
            var environment = new SandboxEnvironment(paypalConfigInfo.ClientKey, paypalConfigInfo.SecretKey);
            var client = new PayPalHttpClient(environment);

            #region Process payment
            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = $"{hostname}/{FAILED_URL}";
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;


                        ///update
                        transactions.ForEach(trans =>
                        {
                            _premiumTransactionService.Update(new PremiumTransaction {
                                Id = trans.Id,
                                CustomerPolicyId = trans.CustomerPolicyId,
                                Amount = trans.Amount,
                                PaidDate = DateTime.Now,
                                DueDate = trans.DueDate,
                            }); ;

                        });
                    }
                }


                /*  return Redirect(paypalRedirectUrl);*/
                return Ok(
                    new
                    {
                        path = paypalRedirectUrl
                    });
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                /*    return Redirect($"{hostname}/{FAILED_URL}");*/
                return BadRequest(
            new
            {
                path = $"{hostname}/{FAILED_URL}"
            });
            }
            #endregion
        }

        /*
                #region example paypal

                [HttpGet("check-out")]
                [Produces("application/json")]
                public async Task<IActionResult> PaypalCheckout()
                {
                    var paypalConfigInfo = PayPalService.getPayPalConfig(_configuration);
                    var environment = new SandboxEnvironment(paypalConfigInfo.ClientKey, paypalConfigInfo.SecretKey);
                    var client = new PayPalHttpClient(environment);

                    #region Create Paypal Order
                    var itemList = new ItemList()
                    {
                        Items = new List<Item>()
                    };

                    var total = 1 + 2 + 3;
                    for (var i = 1; i <= 3; i++)
                    {
                        itemList.Items.Add(new Item()
                        {
                            Name = $"insurance {i}",
                            Currency = "USD",
                            Price = i.ToString(),
                            Quantity = 1.ToString(),
                            Sku = "sku",
                            Tax = "0"
                        });
                    }

                    #endregion

                    #region Setup payment config (just example)
                    var paypalOrderId = DateTime.Now.Ticks;
                    var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                    var payment = new Payment()
                    {
                        Intent = "sale",
                        Transactions = new List<Transaction>()
                        {
                            new Transaction()
                            {
                                Amount = new Amount()
                                {
                                    Total = total.ToString(),
                                    Currency = "USD",
                                    Details = new AmountDetails
                                    {
                                        Tax = "0",
                                        Shipping = "0",
                                        Subtotal = total.ToString()
                                    }
                                },
                                ItemList = itemList,
                                Description = $"Invoice #{paypalOrderId}",
                                InvoiceNumber = paypalOrderId.ToString()
                            }
                        },
                        RedirectUrls = new RedirectUrls()
                        {
                            CancelUrl = $"{hostname}/paypal/CheckoutFail",
                            ReturnUrl = $"{hostname}/paypal/CheckoutSuccess"
                        },
                        Payer = new Payer()
                        {
                            PaymentMethod = "paypal"
                        }
                    };
                    #endregion

                    #region Process payment (just example)
                    PaymentCreateRequest request = new PaymentCreateRequest();
                    request.RequestBody(payment);

                    try
                    {
                        var response = await client.Execute(request);
                        var statusCode = response.StatusCode;
                        Payment result = response.Result<Payment>();

                        var links = result.Links.GetEnumerator();
                        string paypalRedirectUrl = null;
                        while (links.MoveNext())
                        {
                            LinkDescriptionObject lnk = links.Current;
                            if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                            {
                                //saving the payapalredirect URL to which user will be redirected for payment  
                                paypalRedirectUrl = lnk.Href;
                            }
                        }

                        return Redirect(paypalRedirectUrl);
                    }
                    catch (HttpException httpException)
                    {
                        var statusCode = httpException.StatusCode;
                        var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                        //Process when Checkout with Paypal fails
                        return Redirect("/paypal/CheckoutFail");
                    }
                    #endregion

                }

                #endregion
        */
    }
}