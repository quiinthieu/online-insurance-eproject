using System.Linq;
using Demo.Models;

namespace Demo.Services
{
	public class MessageService : IMessageService
	{
		private DatabaseContext _databaseContext;

		public MessageService(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
		
		public dynamic FindAll()
		{
			return _databaseContext.Messages;
		}

		public dynamic FindById(int id)
		{
			return _databaseContext.Messages.Where(message => message.Id == id);
		}

		public dynamic FindByStatus(bool status)
		{
			return _databaseContext.Messages.Where(message => message.Status == status);
		}

		public int Count()
		{
			return _databaseContext.Messages.Count();
		}
	}
}