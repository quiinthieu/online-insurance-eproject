namespace Demo.Services
{
	public interface IMessageService
	{
		public dynamic FindAll();

		public dynamic FindById(int id);

		public dynamic FindByStatus(bool status);

		public int Count();
	}
}