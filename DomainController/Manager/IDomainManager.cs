using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;

namespace DomainController.Manager
{
    public interface IDomainManager
    {
        bool ValidateUser(string username, string password, out string message);
        bool IsUserInRole(string username, string roleName);
        string[] GetRolesForUser(string username);
        List<string> GetUsersForRole(string rolename);
        List<string> GetUsersForRoles(List<string> rolesname);
        UserPrincipal GetUser(string username);
        void CreateUser(string login, string name, string surName, string email, string phone);
        void EditUser(string login, string name, string surName, string email, string phone);
        void DeleteUser(string login);
        void ChangePassword(string login, string newPassword);
    }
}
