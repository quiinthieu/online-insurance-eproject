namespace Demo.Services
{
	public interface ISubscriptionService
	{
		public dynamic FindAll();

		public dynamic Unsubscribe(string email);

		public int Count();
	}
}