﻿using System.Linq;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
	public class PolicyService : IPolicyService
	{
		private DatabaseContext _databaseContext;

		public PolicyService(DatabaseContext databaseContext)
		{
			databaseContext = _databaseContext;
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
			});
		}

		public dynamic FindById(int id)
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
	}
}