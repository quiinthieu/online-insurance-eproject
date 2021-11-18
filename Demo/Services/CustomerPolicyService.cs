using System.Linq;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
	public class CustomerPolicyService : ICustomerPolicyService
	{
		private DatabaseContext _databaseContext;

		public CustomerPolicyService(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
		
		public dynamic FindAll()
		{
			return _databaseContext.CustomerPolicies.Select(customerPolicy => new
			{
				customerPolicy.Id,
				customerPolicy.CustomerId,
				CustomerName = customerPolicy.Customer.Name,
				customerPolicy.PolicyId,
				PolicyName = customerPolicy.Policy.Name,
				InsuranceName = customerPolicy.Policy.InsuranceType.Name,
				customerPolicy.StartDate,
				customerPolicy.EndDate,
				customerPolicy.PremiumTypeId,
				PremiumTypeName = customerPolicy.PremiumType.Name,
				customerPolicy.AgentId
			});
		}

		public dynamic FindById(int id)
		{
			return _databaseContext.CustomerPolicies.Select(customerPolicy => new
			{
				customerPolicy.Id,
				customerPolicy.CustomerId,
				CustomerName = customerPolicy.Customer.Name,
				customerPolicy.PolicyId,
				PolicyName = customerPolicy.Policy.Name,
				InsuranceName = customerPolicy.Policy.InsuranceType.Name,
				customerPolicy.StartDate,
				customerPolicy.EndDate,
				customerPolicy.PremiumTypeId,
				PremiumTypeName = customerPolicy.PremiumType.Name,
				customerPolicy.AgentId
			}).SingleOrDefault(customerPolicy => customerPolicy.Id == id);
		}

		public dynamic Create(CustomerPolicy customerPolicy)
		{
			_databaseContext.CustomerPolicies.Add(customerPolicy);
			_databaseContext.SaveChanges();
			return customerPolicy;
		}

		public dynamic Update(CustomerPolicy customerPolicy)
		{
			_databaseContext.CustomerPolicies.Add(customerPolicy);
			_databaseContext.Entry(customerPolicy).State = EntityState.Modified;
			_databaseContext.SaveChanges();
			return customerPolicy;
		}


		public dynamic FindByCustomerId(int customerId)
		{
            /* return _databaseContext.CustomerPolicies.Select(customerPolicy => new
             {
                 customerPolicy.Id,
                 customerPolicy.CustomerId,
                 CustomerName = customerPolicy.Customer.Name,
                 customerPolicy.PolicyId,
                 PolicyName = customerPolicy.Policy.Name,
                 PolicyTerm = customerPolicy.Policy.Term,
                 PolicyAmount = customerPolicy.Policy.Amount,
                 PolicyInterest = customerPolicy.Policy.InterestRate,
                 PolicyDesc = customerPolicy.Policy.Description,
                 InsuranceName = customerPolicy.Policy.InsuranceType.Name,
                 customerPolicy.StartDate,
                 customerPolicy.EndDate,
                 customerPolicy.PremiumTypeId,
                 customerPolicy.PremiumAmount,
                 PremiumTypeName = customerPolicy.PremiumType.Name,
                 AgentId = customerPolicy.Agent.Id,
                 AgentName = customerPolicy.Agent.Name,
                 AgentGender = customerPolicy.Agent.Gender,
                 AgentBirday = customerPolicy.Agent.Birthday,
                 BranchId = customerPolicy.Agent.Branch.Id,
                 BranchName = customerPolicy.Agent.Branch.Name
             }).SingleOrDefault(customerPolicy => customerPolicy.CustomerId == customerId);*/

            return _databaseContext.CustomerPolicies.Where(customerPolicy => customerPolicy.CustomerId == customerId).Select(customerPolicy => new
            {
                customerPolicy.Id,
                customerPolicy.CustomerId,
                CustomerName = customerPolicy.Customer.Name,
                customerPolicy.PolicyId,
                PolicyName = customerPolicy.Policy.Name,
                PolicyTerm = customerPolicy.Policy.Term,
                PolicyAmount = customerPolicy.Policy.Amount,
                PolicyInterest = customerPolicy.Policy.InterestRate,
                PolicyDesc = customerPolicy.Policy.Description,
                InsuranceName = customerPolicy.Policy.InsuranceType.Name,
                customerPolicy.StartDate,
                customerPolicy.EndDate,
                customerPolicy.PremiumTypeId,
                customerPolicy.PremiumAmount,
                PremiumTypeName = customerPolicy.PremiumType.Name,
                AgentId = customerPolicy.Agent.Id,
                AgentName = customerPolicy.Agent.Name,
                AgentGender = customerPolicy.Agent.Gender,
                AgentBirday = customerPolicy.Agent.Birthday,
                BranchId = customerPolicy.Agent.Branch.Id,
                BranchName = customerPolicy.Agent.Branch.Name
            });
        }


		public int Count()
		{
			return _databaseContext.CustomerPolicies.Count();
		}
    }
}