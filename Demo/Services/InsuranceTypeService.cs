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
        private DatabaseContext db;

        public InsuranceTypeService(DatabaseContext _db)
        {
            db = _db;
        }
        public dynamic Create(InsuranceType insuranceType)
        {
            db.InsuranceTypes.Add(insuranceType);
            db.SaveChanges();
            return insuranceType;
        }

        public dynamic FindAll()
        {
            return db.InsuranceTypes.Select(i => new
            {
                i.Id,
                i.Name,
                i.Description
                
            }).ToList();
        }

        public dynamic FindById(int id)
        {
            return db.InsuranceTypes.Select(i => new
            {
                i.Id,
                i.Name,
                i.Description
            }).SingleOrDefault(i => i.Id == id);
        }

        public dynamic Update(InsuranceType insuranceType)
        {
            db.InsuranceTypes.Add(insuranceType);
            db.Entry(insuranceType).State = EntityState.Modified;
            db.SaveChanges();
            return insuranceType;
        }
    }
}
