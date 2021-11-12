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
        private DatabaseContext db;

        public CustomerService(DatabaseContext _db)
        {
            db = _db;
        }
        public Customer Create(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return customer;
        }

        public dynamic FindAll()
        {
            return db.Customers.Select(i => new
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
            return db.Customers.Select(i => new
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
            return db.Customers.Select(i => new
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
            var customerUpdate = db.Customers.Find(id);
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
                db.Entry(customerUpdate).State = EntityState.Modified;
                db.SaveChanges();
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
    }
}
