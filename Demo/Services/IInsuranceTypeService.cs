using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;

namespace Demo.Services
{
     public interface IInsuranceTypeService
    {
        public dynamic FindAll();

        public dynamic FindById(int id);

        public dynamic Create(InsuranceType insuranceType);

        public dynamic Update(InsuranceType insuranceType);
    }
}
