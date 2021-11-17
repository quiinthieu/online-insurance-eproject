using System.Linq;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
	public class ClaimService : IClaimService
	{
		private DatabaseContext _databaseContext;

		public ClaimService(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
		
		public dynamic FindAll()
		{
			return _databaseContext.Claims.Select(claim => new
			{
				claim.Id,
				claim.CustomerPolicyId,
				claim.Amount,
				claim.ClaimedDate
			});
		}

		public dynamic FindById(int id)
		{
			return _databaseContext.Claims.Select(claim => new
			{
				claim.Id,
				claim.CustomerPolicyId,
				claim.Amount,
				claim.ClaimedDate
			}).SingleOrDefault(claim => claim.Id == id);
		}

		public dynamic Create(Claim claim)
		{
			_databaseContext.Claims.Add(claim);
			_databaseContext.SaveChanges();
			return claim;
		}

		public dynamic Update(Claim claim)
		{
			_databaseContext.Claims.Add(claim);
			_databaseContext.Entry(claim).State = EntityState.Modified;
			_databaseContext.SaveChanges();
			return claim;
		}

		public dynamic FindByCustomerPolicyId(int customerPolicyId)
		{
			return _databaseContext.Claims.Select(claim => new
			{
				claim.Id,
				claim.CustomerPolicyId,
				claim.Amount,
				claim.CustomerPolicy.StartDate,
				claim.CustomerPolicy.EndDate,
				claim.ClaimedDate,
				claim.CustomerPolicy.Policy.InsuranceType.Name,

			}).SingleOrDefault(claim => claim.CustomerPolicyId == customerPolicyId);
		}

        public dynamic FindByCustomerId(int customerId)
        {
			return _databaseContext.Claims.Where(c => c.CustomerPolicy.CustomerId == customerId).Select(c => new
		   {
				c.Id,
				c.CustomerPolicyId,
				c.CustomerPolicy.Policy.InsuranceType.Name,
				c.Amount,
				c.ClaimedDate
		   }).OrderByDescending(c => c.ClaimedDate);
        }

        public int Count()
        {
	        return _databaseContext.Claims.Count();
        }
    }
}