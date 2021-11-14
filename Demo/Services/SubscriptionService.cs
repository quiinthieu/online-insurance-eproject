using System.Linq;
using Demo.Models;

namespace Demo.Services
{
	public class SubscriptionService : ISubscriptionService
	{
		private DatabaseContext _databaseContext;

		public SubscriptionService(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
		
		public dynamic FindAll()
		{
			return _databaseContext.Subscriptions;
		}

		public dynamic Unsubscribe(string email)
		{
			Subscription deletedSubscription =
				_databaseContext.Subscriptions.SingleOrDefault(subscription => subscription.Email.Equals(email));
			if (deletedSubscription != null)
			{
				_databaseContext.Remove(deletedSubscription);
				return _databaseContext.SaveChanges() > 0;
			}
			else
			{
				return false;
			}
		}

		public int Count()
		{
			return _databaseContext.Subscriptions.Count();
		}
	}
}