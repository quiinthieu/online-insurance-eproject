using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
    public class InsuranceTypeService : IInsuranceTypeService
    {
        private DatabaseContext _databaseContext;

        public InsuranceTypeService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public dynamic Create(InsuranceType insuranceType)
        {
            _databaseContext.InsuranceTypes.Add(insuranceType);
            _databaseContext.SaveChanges();
            return insuranceType;
        }

        public dynamic FindAll()
        {
            return _databaseContext.InsuranceTypes.Select(i => new
            {
                i.Id,
                i.Name,
                i.Description
                
            }).ToList();
        }

        public dynamic FindById(int id)
        {
            return _databaseContext.InsuranceTypes.Select(i => new
            {
                i.Id,
                i.Name,
                i.Description
            }).SingleOrDefault(i => i.Id == id);
        }

        public dynamic Update(InsuranceType insuranceType)
        {
            _databaseContext.InsuranceTypes.Add(insuranceType);
            _databaseContext.Entry(insuranceType).State = EntityState.Modified;
            _databaseContext.SaveChanges();
            return insuranceType;
        }

        public int Count()
        {
            return _databaseContext.InsuranceTypes.Count();
        }
    }
}
