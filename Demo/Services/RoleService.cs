using System.Linq;
using Demo.Models;

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
	}
}