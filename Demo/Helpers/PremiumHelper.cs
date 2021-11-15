using System;
using System.Collections.Generic;
using System.Diagnostics;
using Demo.Models;
using Demo.Services;

namespace Demo.Helpers
{
    public class PremiumHelper
    {
        private IPolicyService _policyService;
        public PremiumHelper(IPolicyService policyService)
        {
            _policyService = policyService;
        }
        static List<PremiumTransaction> premiumTransactions = new List<PremiumTransaction>();
        public static List<PremiumTransaction> PremiumTransactionScheludler(int term, CustomerPolicy customerPolicy)
        {
            try
            {

                premiumTransactions.Clear();
                // Lay ve premium type id
                var premiumTypeId = customerPolicy.PremiumTypeId;
                // Khai bao so tien moi han = 0
                decimal? amountTransaction = 0;
                // Khai bao so thang cach nhau
                int monthToAdd = 0;

                // process generate
                if (premiumTypeId == 1)
                {
                    term *= 12;
                    amountTransaction = customerPolicy.PremiumAmount / term;
                    monthToAdd = 1;
                }
                else if (premiumTypeId == 2)
                {
                    term *= 4;
                    amountTransaction = customerPolicy.PremiumAmount / term;
                    monthToAdd = 3;
                }
                else if (premiumTypeId == 3)
                {
                    term *= 2;
                    amountTransaction = customerPolicy.PremiumAmount / term;
                    monthToAdd = 6;
                }
                else if (premiumTypeId == 4)
                {
                    term *= 1;
                    amountTransaction = customerPolicy.PremiumAmount / term;
                    monthToAdd = 12;
                }
                else
                {
                    Debug.WriteLine("Invalid premium type");
                }
                var startDate = customerPolicy.StartDate;

                for (int i = 0; i < term; i++)
                {
                    var premiumTransaction = new PremiumTransaction();
                    premiumTransaction.CustomerPolicyId = customerPolicy.Id;
                    premiumTransaction.Amount = amountTransaction;
                    premiumTransaction.PaidDate = null;

                    startDate = startDate.Value.AddMonths(monthToAdd);
                    premiumTransaction.DueDate = startDate;

                    premiumTransactions.Add(premiumTransaction);
                }
                return premiumTransactions;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error msg: {ex}");
                return null;
            }
        }
    }
}
