using System.Linq;
using BCrypt.Net;
using Demo.Models;

namespace Demo.Services
{
	public class CredentialService : ICredentialService
	{
		// Declare a variable to hold a DatabaseContext object
		private DatabaseContext _databaseContext;
		
		// Constructor --> Initialize the above variable with DatabaseContext object
		public CredentialService(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
		
		// Used to check user's credential before logging in
		public dynamic FindByEmailAndPassword(string email, string password)
		{
			dynamic credential = _databaseContext.Credentials
				.SingleOrDefault(credential =>
					credential.Email.Equals(email) &&
					BCrypt.Net.BCrypt.Verify(password, credential.Password, false, HashType.SHA256));
			if (credential != null)
			{
				credential = new
				{
					credential.Email,
					credential.Password,
					credential.Status,
					credential.RoleId,
					RoleName = credential.Role.Name,
					credential.ActivationCode
				};
			}

			return credential;
		}
		
		// Used to check user's credential to activate account, reset password, or check if email has already existed
		public dynamic FindByEmail(string email)
		{
			return _databaseContext.Credentials
				.Where(credential => credential.Email.Equals(email)).Select(
					credential => new
					{
						credential.Email,
						credential.Status,
						credential.RoleId,
						RoleName = credential.Role.Name,
						credential.ActivationCode
					});
		}

		public dynamic Create(Credential credential)
		{
			_databaseContext.Credentials.Add(credential);
			_databaseContext.SaveChanges();
			return credential;
		}

		
	}
}