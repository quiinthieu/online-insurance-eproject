using System.Collections.Generic;
using System.Linq;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
	public class PremiumTransactionService : IPremiumTransactionService
	{
		private DatabaseContext _databaseContext;

		public PremiumTransactionService(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
		
		public dynamic FindAll()
		{
			return _databaseContext.PremiumTransactions.Select(premiumTransaction => new
			{
				premiumTransaction.Id,
				premiumTransaction.CustomerPolicyId,
				premiumTransaction.Amount,
				premiumTransaction.PaidDate,
				premiumTransaction.DueDate
			});
		}

		public dynamic FindById(int id)
		{
			return _databaseContext.PremiumTransactions.Select(premiumTransaction => new
			{
				premiumTransaction.Id,
				premiumTransaction.CustomerPolicyId,
				premiumTransaction.Amount,
				premiumTransaction.PaidDate,
				premiumTransaction.DueDate
			}).SingleOrDefault(premiumTransaction => premiumTransaction.Id == id);
		}

		public dynamic Create(PremiumTransaction premiumTransaction)
		{
			_databaseContext.PremiumTransactions.Add(premiumTransaction);
			_databaseContext.SaveChanges();
			return premiumTransaction;
		}

		public List<PremiumTransaction> Create(List<PremiumTransaction> premiumTransaction)
		{
			premiumTransaction.ForEach(pt =>
			{
				_databaseContext.PremiumTransactions.Add(pt);
				_databaseContext.SaveChanges();
			});
			return premiumTransaction;
		}

		public dynamic Update(PremiumTransaction premiumTransaction)
		{
			_databaseContext.PremiumTransactions.Add(premiumTransaction);
			_databaseContext.Entry(premiumTransaction).State = EntityState.Modified;
			_databaseContext.SaveChanges();
			return premiumTransaction;
		}

		public dynamic FindByCustomerPolicyId(int customerPolicyId)
		{
			return _databaseContext.PremiumTransactions.Select(premiumTransaction => new
			{
				premiumTransaction.Id,
				premiumTransaction.CustomerPolicyId,
				premiumTransaction.Amount,
				premiumTransaction.PaidDate,
				premiumTransaction.DueDate
			}).SingleOrDefault(premiumTransaction => premiumTransaction.CustomerPolicyId == customerPolicyId);
		}
	}
}