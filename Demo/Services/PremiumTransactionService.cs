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
				PolicyName = premiumTransaction.CustomerPolicy.Policy.Name,
				PolicyTerm = premiumTransaction.CustomerPolicy.Policy.Term,

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

			var transactions = _databaseContext.PremiumTransactions.Where(premiumTransaction => premiumTransaction.CustomerPolicyId == customerPolicyId).Select(premiumTransaction => new
			{
				premiumTransaction.Id,
				premiumTransaction.CustomerPolicyId,
				PolicyName = premiumTransaction.CustomerPolicy.Policy.Name,
				PolicyTerm = premiumTransaction.CustomerPolicy.Policy.Term,
				premiumTransaction.Amount,
				premiumTransaction.PaidDate,
				premiumTransaction.DueDate,
			}).ToList();

			var total = _databaseContext.PremiumTransactions.Where(premiumTransaction => premiumTransaction.CustomerPolicyId == customerPolicyId).Sum(trans => trans.Amount);

			return new
			{
				transactions,
				total
			};
				
			
		}

        public dynamic FindByCustomerId(int customerId)
        {
			return _databaseContext.PremiumTransactions.Where(p => p.CustomerPolicy.CustomerId == customerId).Select(p => new
			{
				p.Id,
				p.CustomerPolicyId,
				p.CustomerPolicy.Policy.InsuranceType.Name,
				p.Amount,
				p.PaidDate,
				p.DueDate
			}).OrderBy(p=> p.Id);
		}

        public int Count()
        {
	        return _databaseContext.PremiumTransactions.Count();
        }
    }
}
