using System;
using Demo.Models;

namespace Demo.Services
{
    public interface IAgentService
    {
        public dynamic FindAll();
        public dynamic FindById(int id);
        public dynamic FindByBranchId(int id);
        public Agent Create(Agent agent);
        public Agent Update(int id, Agent agent);


        public int Count();
    }
}
