using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
    public class CustomerService : ICustomerService
    {
        private DatabaseContext _databaseContext;

        public CustomerService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public Customer Create(Customer customer)
        {
            _databaseContext.Customers.Add(customer);
            _databaseContext.SaveChanges();
            return customer;
        }

        public dynamic FindAll()
        {
            return _databaseContext.Customers.Select(i => new
            {
                i.Id,
                i.Name,
                i.Birthday,
                i.Gender,
                i.Street,
                i.City,
                i.State,
                i.ZipCode,
                i.Occupation,
                i.CredentialId,
                i.CitizenId

            }).ToList();
        }

        public dynamic FindById(int id)
        {
            return _databaseContext.Customers.Select(i => new
            {
                i.Id,
                i.Name,
                i.Birthday,
                i.Gender,
                i.Street,
                i.City,
                i.State,
                i.ZipCode,
                i.Occupation,
                i.CredentialId,
                i.CitizenId

            }).SingleOrDefault(i => i.Id == id);
        }

        public dynamic FindByCredentialId(int id)
        {
            return _databaseContext.Customers.Select(i => new
            {
                i.Id,
                i.Name,
                i.Birthday,
                i.Gender,
                i.Street,
                i.City,
                i.State,
                i.ZipCode,
                i.Occupation,
                i.CredentialId,
                i.CitizenId

            }).SingleOrDefault(i => i.CredentialId == id);
        }

        public dynamic Update(int id,Customer customer)
        {
            var customerUpdate = _databaseContext.Customers.Find(id);
            if (customerUpdate != null)
            {
                customerUpdate.Name = customer.Name;
                customerUpdate.Birthday = customer.Birthday;
                customerUpdate.Gender = customer.Gender;
                customerUpdate.Street = customer.Street;
                customerUpdate.City = customer.City;
                customerUpdate.State = customer.State;
                customerUpdate.ZipCode = customer.ZipCode;
                customerUpdate.Occupation = customer.Occupation;
                customerUpdate.CredentialId = customer.CredentialId;
                customerUpdate.CitizenId = customer.CitizenId;
                _databaseContext.Entry(customerUpdate).State = EntityState.Modified;
                _databaseContext.SaveChanges();
                return new
                {
                    customerUpdate.Id,
                    customerUpdate.Name,
                    customerUpdate.Birthday,
                    customerUpdate.Gender,
                    customerUpdate.Street,
                    customerUpdate.City,
                    customerUpdate.State,
                    customerUpdate.ZipCode,
                    customerUpdate.Occupation,
                    customerUpdate.CredentialId,
                    customerUpdate.CitizenId
                };
            }

            return null;
        }

        public int Count()
        {
            return _databaseContext.Customers.Count();
        }
        
    }
}
