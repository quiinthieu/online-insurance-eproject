using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using BCrypt.Net;
using Demo.Models;
using Claim = System.Security.Claims.Claim;

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
                credential.Email.Equals(email) );
            if (credential != null && BCrypt.Net.BCrypt.Verify(password, credential.Password))
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
            else
            {
                credential = null;
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
            credential.Password = BCrypt.Net.BCrypt.HashPassword(credential.Password);
            _databaseContext.Credentials.Add(credential);
            _databaseContext.SaveChanges();
            return credential;
        }

        public IEnumerable<Claim> GetUserClaims(dynamic credential)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, ((object)credential.Email).ToString()));

            claims.Add(new Claim(ClaimTypes.Role, ((object)credential.RoleName).ToString()));
            return claims;
        }

    }
}