using Demo.Models;

namespace Demo.Services
{
	public interface IRoleService
	{
		public dynamic FindAll();

		public dynamic FindById(int id);

		public dynamic Create(Role role);

		public dynamic Update(Role role);
		public dynamic FindByName(string name);

		public int Count();

	}
}