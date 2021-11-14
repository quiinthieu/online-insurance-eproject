using System.Linq;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
	public class RoleService : IRoleService
	{
		private DatabaseContext _databaseContext;

		public RoleService(DatabaseContext databaseContext)
		{
			this._databaseContext = databaseContext;
		}
		public dynamic FindAll()
		{
			return _databaseContext.Roles.Select(role => new
			{
				role.Id,
				role.Name
			});
		}

		public dynamic FindById(int id)
		{
			return _databaseContext.Roles.Select(role => new
			{
				role.Id,
				role.Name
			}).SingleOrDefault(role => role.Id == id);
		}

		public dynamic FindByName(string name)
		{
			return _databaseContext.Roles.Select(role => new
			{
				role.Id,
				role.Name
			}).SingleOrDefault(role => role.Name == name);
		}

		public dynamic Create(Role role)
		{
			_databaseContext.Roles.Add(role);
			_databaseContext.SaveChanges();
			return role;
		}

		public dynamic Update(Role role)
		{
			_databaseContext.Roles.Add(role);
			_databaseContext.Entry(role).State = EntityState.Modified;
			_databaseContext.SaveChanges();
			return role;
		}

		public int Count()
		{
			return _databaseContext.Roles.Count();
		}
	}
}