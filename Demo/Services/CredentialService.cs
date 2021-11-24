using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using BCrypt.Net;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Claim = System.Security.Claims.Claim;

namespace Demo.Services
{
    public class CredentialService : ICredentialService
    {
        // Declare a variable to hold a DatabaseContext object
        private DatabaseContext _databaseContext;
        private IRoleService _roleService;
        // Constructor --> Initialize the above variable with DatabaseContext object
        public CredentialService(DatabaseContext databaseContext, IRoleService roleService)
        {
            _databaseContext = databaseContext;
            _roleService = roleService;
        }

        // Used to check user's credential before logging in
        public dynamic FindByEmailAndPassword(string email, string password)
        {
            try
            {
                dynamic credential = _databaseContext.Credentials
            .SingleOrDefault(credential =>
                credential.Email.Equals(email));
                bool c1 = credential != null;
                bool c2 = BCrypt.Net.BCrypt.Verify(password, credential.Password);
                if (credential != null && BCrypt.Net.BCrypt.Verify(password, credential.Password))
                {
                    credential = new
                    {
                        credential.Id,
                        credential.Email,
                        credential.Password,
                        credential.Status,
                        credential.RoleId,
                        RoleName = credential.Role.Name,
                        credential.ActivationCode,
                        credential.Customers,
                        BranchId = credential.BranchId
                    };
                }
                else
                {
                    credential = null;
                }


                return credential;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Used to check user's credential to activate account, reset password, or check if email has already existed
        public dynamic FindByEmail(string email)
        {
            var credential = _databaseContext.Credentials.SingleOrDefault(credential => credential.Email.Equals(email));
            if (credential != null)
            {
                return new
                {
                    credential.Id,
                    credential.Email,
                    credential.Password,
                    credential.Status,
                    credential.RoleId,
                    RoleName = credential.Role.Name,
                    credential.ActivationCode
                };
            }
            return null;
        }

        public dynamic Create(Credential credential)
        {
            var existCredential = _databaseContext.Credentials.SingleOrDefault(cre => cre.Email.Equals(credential.Email));
            if (existCredential != null)
            {
                throw new UnauthorizedAccessException("Email already exist");
            }

            var ROLE_NAME = "Customer";
            credential.Password = BCrypt.Net.BCrypt.HashPassword(credential.Password);
            credential.Status = false;
            credential.RoleId = _roleService.FindByName(ROLE_NAME).Id;
            // cai branch id cua em la 3 nen em sua lai la 3
            //credential.BranchId = 1;
            credential.BranchId = 3;
            _databaseContext.Credentials.Add(credential);
            _databaseContext.SaveChanges();
            return credential;
        }

        // For customers or agent to update customers' profile
        public dynamic Update(Credential credential)
        {
            var updatedCredential = _databaseContext.Credentials.SingleOrDefault(cre => cre.Email.Equals(credential.Email));

            updatedCredential.Email = credential.Email;
            updatedCredential.Password = credential.Password;
            updatedCredential.Status = credential.Status;
            updatedCredential.RoleId = credential.RoleId;
            updatedCredential.ActivationCode = credential.ActivationCode;


            _databaseContext.Entry(updatedCredential).State = EntityState.Modified;
            _databaseContext.SaveChanges();
            return new
            {
                updatedCredential.Id,
                updatedCredential.Email,
                updatedCredential.Password,
                updatedCredential.Status,
                updatedCredential.RoleId,
                RoleName = updatedCredential.Role.Name,
                updatedCredential.ActivationCode
            };
        }

        // For agent to view the list of customers
        public dynamic Find()
        {
            return _databaseContext.Credentials.Select(credential => new
            {
                credential.Id,
                credential.Email,
                credential.Status,
                credential.RoleId,
                RoleName = credential.Role.Name,
                credential.ActivationCode
            });
        }

        // For agent to find a specific customer using his or her id
        public dynamic FindById(int id)
        {
            return _databaseContext.Credentials.Select(credential => new
            {
                credential.Id,
                credential.Email,
                credential.Status,
                credential.RoleId,
                RoleName = credential.Role.Name,
                credential.ActivationCode,
                BranchId = credential.BranchId
            }).SingleOrDefault(credential => credential.Id == id);
        }

        public IEnumerable<Claim> GetUserClaims(dynamic credential)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, ((object)credential.Email).ToString()));

            claims.Add(new Claim(ClaimTypes.Role, ((object)credential.RoleName).ToString()));
            return claims;
        }

        public dynamic FindByActivationCodeAndEmail(string activationCode, string email)
        {
            return _databaseContext.Credentials.SingleOrDefault(cre => cre.ActivationCode == activationCode && cre.Email == email);
        }

        public int Count()
        {
            return _databaseContext.Credentials.Count();
        }

        public dynamic FindAll()
        {
            throw new NotImplementedException();
        }
    }
}