using Demo.Models;

namespace Demo.Services
{
	public interface IPolicyService
	{
		public dynamic FindAll();
		public dynamic FindById(int id);
		public dynamic Create(Policy policyService);
		public dynamic Update(Policy policyService);
	}
}