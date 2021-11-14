using Demo.Models;

namespace Demo.Services
{
	public interface IClaimService
	{
		public dynamic FindAll();

		public dynamic FindById(int id);

		public dynamic Create(Claim claim);

		public dynamic Update(Claim claim);

		public dynamic FindByCustomerPolicyId(int customerPolicyId);

		public dynamic FindByCustomerId(int customerId);

		public int Count();
	}
}