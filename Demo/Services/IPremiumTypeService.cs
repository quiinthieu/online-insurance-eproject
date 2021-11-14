using Demo.Models;

namespace Demo.Services
{
	public interface IPremiumTypeService
	{
		public dynamic FindAll();

		public dynamic FindById(int id);

		public dynamic Create(PremiumType premiumType);

		public dynamic Update(PremiumType premiumType);

		public int Count();
	}
}