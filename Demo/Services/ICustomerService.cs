using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;

namespace Demo.Services
{
     public interface ICustomerService
    {
        public dynamic FindAll();

        public dynamic FindById(int id);

        public Customer Create(Customer customer);

        public Customer Update(int id,Customer customer);
    }
}
