using System.Collections.Generic;
using Demo.Models;

namespace Demo.Services
{
	public interface IPremiumTransactionService
	{
		public dynamic FindAll();

		public dynamic FindById(int id);

		public dynamic Create(PremiumTransaction premiumTransaction);

		public List<PremiumTransaction> Create(List<PremiumTransaction> premiumTransaction);

		public dynamic Update(PremiumTransaction premiumTransaction);

		public dynamic FindByCustomerPolicyId(int customerPolicyId);
		public dynamic FindByCustomerId(int customerId);

		public int Count();
	}
}