using System;
using Demo.Models;

namespace Demo.Services
{
    public interface IBranchService
    {
        public dynamic FindAll();
        public dynamic FindById(int id);
        public Branch Create(Branch branch);
        public Branch Update(int id, Branch branch);

        public int Count();
    }
}
