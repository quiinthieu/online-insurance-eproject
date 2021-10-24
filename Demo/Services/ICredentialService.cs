using Demo.Models;

namespace Demo.Services
{
	public interface ICredentialService
	{
		// Used to check user's credential before logging in
		public dynamic FindByEmailAndPassword(string email, string password);
		
		// Used to check user's credential to activate account, reset password, or check if email has already existed
		public dynamic FindByEmail(string email);
		
		// Used to create new credential
		public dynamic Create(Credential credential);
		
		

	}
}