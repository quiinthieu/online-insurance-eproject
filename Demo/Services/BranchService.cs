using System;
using Demo.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
    public class BranchService:IBranchService
    {
        private DatabaseContext _databaseContext;

        public BranchService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Branch Create(Branch branch)
        {
            _databaseContext.Branches.Add(branch);
            int codeSuccess = _databaseContext.SaveChanges();
            if (codeSuccess != 0)
                return branch;
            return null;
        }

        public dynamic FindAll()
        {
            return _databaseContext.Branches.Select(b => new
            {
                b.Id,
                b.Name,
                b.Phone,
                b.Street,
                b.City,
                b.State,
                b.ZipCode
            }).ToList();
        }

        public dynamic FindById(int id)
        {
            return _databaseContext.Branches.Select(b => new
            {
                b.Id,
                b.Name,
                b.Phone,
                b.Street,
                b.City,
                b.State,
                b.ZipCode
            }).SingleOrDefault(b=>b.Id==id);
        
        }

        public Branch Update(int id, Branch branch)
        {
            var branchToUpdate = FindById(id);
            if (branchToUpdate != null)
            {
                branchToUpdate.Name = branch.Name;
                branchToUpdate.Phone = branch.Phone;
                branchToUpdate.Gender = branch.Street;
                branchToUpdate.BranchId = branch.City;
                branchToUpdate.State = branch.State;
                branchToUpdate.ZipCode = branch.ZipCode;
                _databaseContext.Entry(branchToUpdate).State = EntityState.Modified;
                return branchToUpdate;
            }
            return null;
        }

        public int Count()
        {
            return _databaseContext.Branches.Count();
        }
    }
}
