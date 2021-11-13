using Demo.Models;
using System.Collections.Generic;
using Claim = System.Security.Claims.Claim;

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

		public dynamic Update(Credential credential);

		public dynamic FindAll();

		public dynamic FindById(int id);

		public dynamic FindByActivationCodeAndEmail(string activationCode,string email);
		public IEnumerable<Claim> GetUserClaims(dynamic credential);//chua 1 phan thong tin tai khoan

		public int Count();

	}
}