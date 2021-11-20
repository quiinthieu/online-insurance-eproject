using BraintreeHttp;
using Demo.Models;
using Microsoft.Extensions.Configuration;
using PayPal.Core;
using PayPal.v1.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PayPal
{
    public class PayPalService
    {
        private static string FAILED_URL = "paypal/CheckoutFail";
        private static string SUCCESS_URL = "customer/customer-policy";
        public static PayPalConfig getPayPalConfig(IConfiguration configuration)
        {
            return new PayPalConfig()
            {
                ClientKey = configuration["PayPal:ClientKey"],
                SecretKey = configuration["PayPal:SecretKey"],
            };
        }

        public static Payment createPayment(List<PremiumTransaction> transactions,string hostname)
        {
            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };
            var paypalOrderId = DateTime.Now.Ticks;
            var total = transactions.Sum(t=>t.Amount);

            foreach(var tran in transactions)
            {
                itemList.Items.Add(new Item()
                {
                    Name = tran.Id.ToString(),
                    Currency = "USD",
                    Price = tran.Amount.ToString(),
                    Quantity = 1.ToString(),
                    Sku = "sku",
                    Tax = "0"
                });
            }


            return new Payment()
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
                    CancelUrl = $"{hostname}/{FAILED_URL}",
                    ReturnUrl = $"{hostname}/{SUCCESS_URL}"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };
        }
   
    }
}
