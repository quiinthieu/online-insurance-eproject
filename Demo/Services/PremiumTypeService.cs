using System.Linq;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
	public class PremiumTypeService : IPremiumTypeService
	{
		private DatabaseContext _databaseContext;

		public PremiumTypeService(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
		
		public dynamic FindAll()
		{
			return _databaseContext.PremiumTypes.Select(premiumType => new
			{
				premiumType.Id,
				premiumType.Name
			});
		}

		public dynamic FindById(int id)
		{
			return _databaseContext.PremiumTypes.Select(premiumType => new
			{
				premiumType.Id,
				premiumType.Name
			}).SingleOrDefault(premiumType => premiumType.Id == id);
		}

		public dynamic Create(PremiumType premiumType)
		{
			_databaseContext.PremiumTypes.Add(premiumType);
			_databaseContext.SaveChanges();
			return premiumType;
		}

		public dynamic Update(PremiumType premiumType)
		{
			_databaseContext.PremiumTypes.Add(premiumType);
			_databaseContext.Entry(premiumType).State = EntityState.Modified;
			_databaseContext.SaveChanges();
			return premiumType;
		}

		public int Count()
		{
			return _databaseContext.PremiumTransactions.Count();
		}
	}
}