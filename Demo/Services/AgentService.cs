using System;
using Demo.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
    public class AgentService : IAgentService
    {
        private DatabaseContext _databaseContext;
        public AgentService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Agent Create(Agent agent)
        {
            _databaseContext.Agents.Add(agent);
            int codeSuccess = _databaseContext.SaveChanges();
            if (codeSuccess != 0)
                return agent;
            return null;
        }

        public dynamic FindAll()
        {
            return _databaseContext.Agents.Select(a => new
            {
                a.Id,
                a.Name,
                a.Birthday,
                a.Gender,
                a.BranchId,
            }).ToList();
        }

        public dynamic FindById(int id)
        {
            return _databaseContext.Agents.AsNoTracking().Select(a => new
            {
                a.Id,
                a.Name,
                a.Birthday,
                a.Gender,
                a.BranchId,
            }).ToList().SingleOrDefault(a => a.Id == id);
        }



        public Agent Update(int id, Agent agent)
        {
            //var agentToUpdate = _databaseContext.Agents.AsNoTracking().SingleOrDefault(a => a.Id == id);
            var agentToUpdate = FindById(id);
            if(agentToUpdate != null)
            {
                agentToUpdate.Name = agent.Name; 
                agentToUpdate.Birthday = agent.Birthday; 
                agentToUpdate.Gender = agent.Gender; 
                agentToUpdate.BranchId = agent.BranchId; 
                _databaseContext.Entry(agentToUpdate).State = EntityState.Modified;
                return agentToUpdate;
            }
            return null;
        }

        public int Count()
        {
            return _databaseContext.Agents.Count();
        }

        public dynamic FindByBranchId(int id)
        {
            return _databaseContext.Agents.AsNoTracking().Select(a => new
            {
                a.Id,
                a.Name,
                a.Birthday,
                a.Gender,
                a.BranchId,
            }).ToList().SingleOrDefault(a => a.BranchId == id);
        }
    }
}

