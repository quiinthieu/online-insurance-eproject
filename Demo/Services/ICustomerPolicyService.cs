using Demo.Models;

namespace Demo.Services
{
	public interface ICustomerPolicyService
	{
		public dynamic FindAll();

		public dynamic FindById(int id);

		public dynamic Create(CustomerPolicy customerPolicy);

		public dynamic Update(CustomerPolicy customerPolicy);

		public dynamic FindByCustomerId(int customerId);

		public int Count();
	}
}