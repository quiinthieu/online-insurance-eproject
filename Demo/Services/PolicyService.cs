using System.Linq;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
	public class PolicyService : IPolicyService
	{
		private DatabaseContext _databaseContext;

		public PolicyService(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
		
		public dynamic FindAll()
		{
			return _databaseContext.Policies.Select(policy => new
			{
				policy.Id,
				policy.InsuranceTypeId,
				InsuranceTypeName = policy.InsuranceType.Name,
				policy.Name,
				policy.Term,
				policy.Amount,
				policy.InterestRate,
				policy.Description
			}).ToList();
		}

		public dynamic FindById(int id)
		{
			return _databaseContext.Policies.Select(policy => new
			{
				policy.Id,
				policy.InsuranceTypeId,
				InsuranceTypeName = policy.InsuranceType.Name,
				InsuranceTypeDesc = policy.InsuranceType.Description,
				policy.Name,
				policy.Term,
				policy.Amount,
				policy.InterestRate,
				policy.Description,
			}).SingleOrDefault(policy => policy.Id == id);
		}

		public dynamic Create(Policy policy)
		{
			_databaseContext.Policies.Add(policy);
			_databaseContext.SaveChanges();
			return policy;
		}

		public dynamic Update(Policy policy)
		{
			_databaseContext.Policies.Add(policy);
			_databaseContext.Entry(policy).State = EntityState.Modified;
			_databaseContext.SaveChanges();
			return policy;
		}

        public dynamic FindByInsuranceTypeId(int id)
        {
			return _databaseContext.Policies.Select(policy => new
			{
				policy.Id,
				policy.InsuranceTypeId,
				InsuranceTypeName = policy.InsuranceType.Name,
				policy.Name,
				policy.Term,
				policy.Amount,
				policy.InterestRate,
				policy.Description
			}).Where(p => p.InsuranceTypeId == id).ToList();
		}

        public int Count()
        {
	        return _databaseContext.Policies.Count();
        }
    }
}