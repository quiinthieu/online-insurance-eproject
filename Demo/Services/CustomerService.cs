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

        public Customer Update(int id,Customer customer)
        {
            var customerUpdate = db.Customers.Find(id);
            if (customerUpdate != null)
            {
                customerUpdate.Name = customer.Name;
                customerUpdate.Birthday = customer.Birthday;
                customerUpdate.Gender = customer.Gender;
                customerUpdate.CredentialId = customer.CredentialId;
                customerUpdate.CitizenId = customer.CitizenId;
                db.Entry(customerUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return customerUpdate;

            }

            return null;
        }
    }
}
