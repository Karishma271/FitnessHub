﻿using System.Threading.Tasks;
using System.Web.Mvc;
using PayPal.Api;
using FitnessHub.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FitnessHub.Controllers
{
    public class PaymentController : Controller
    {
        private const string ClientId = "YOUR_PAYPAL_CLIENT_ID";
        private const string ClientSecret = "YOUR_PAYPAL_CLIENT_SECRET";

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Pay(BookingDto bookingDto)
        {
            var apiContext = GetApiContext();

            var payment = CreatePayment(apiContext, bookingDto);
            var redirectUrl = payment.links.FirstOrDefault(link => link.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;

            return Redirect(redirectUrl);
        }

        public ActionResult Execute(string paymentId, string PayerID)
        {
            var apiContext = GetApiContext();

            var paymentExecution = new PayPal.Api.PaymentExecution
            {
                payer_id = PayerID
            };

            var payment = new PayPal.Api.Payment { id = paymentId };
            var executedPayment = payment.Execute(apiContext, paymentExecution);

            if (executedPayment.state.ToLower() != "approved")
            {
                return View("PaymentFailure");
            }

            return View("PaymentSuccess");
        }

        private APIContext GetApiContext()
        {
            var config = new Dictionary<string, string>
            {
                { "mode", "sandbox" },
                { "clientId", ClientId },
                { "clientSecret", ClientSecret }
            };

            var accessToken = new OAuthTokenCredential(ClientId, ClientSecret, config).GetAccessToken();
            return new APIContext(accessToken);
        }

        private PayPal.Api.Payment CreatePayment(APIContext apiContext, BookingDto bookingDto)
        {
            var payer = new Payer { payment_method = "paypal" };
            var redirUrls = new RedirectUrls
            {
                cancel_url = Url.Action("Checkout", "Payment", null, Request.Url.Scheme),
                return_url = Url.Action("Execute", "Payment", null, Request.Url.Scheme)
            };

            var amount = new Amount
            {
                currency = "USD",
                total = bookingDto.AmountPaid.ToString("F2")
            };

            var transaction = new Transaction
            {
                description = "Payment for booking",
                amount = amount
            };

            var payment = new PayPal.Api.Payment
            {
                intent = "sale",
                payer = payer,
                transactions = new List<Transaction> { transaction },
                redirect_urls = redirUrls
            };

            return payment.Create(apiContext);
        }
    }
}