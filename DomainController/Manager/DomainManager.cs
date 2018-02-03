using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace DomainController.Manager
{
    public class DomainManager : IDomainManager
    {
        private static PrincipalContext GetContext()
        {
            try
            {
                return new PrincipalContext(ContextType.Domain, Config.Configurator.GetDomainPath, Config.Configurator.GetDomainUser, Config.Configurator.GetDomainPassword);
            }
            catch
            {
                return null;
            }
        }
        public bool ValidateUser(string username, string password, out string message)
        {
            bool isValid = false;
            message = string.Empty;
            try
            {
                using (var context = GetContext())
                {
                    var user = UserPrincipal.FindByIdentity(context, username);
                    if (user.Enabled == false)
                    {
                        message = "Ваш аккаунт заблокований";
                    }
                    else
                    {
                        if (!context.ValidateCredentials(username, password))
                        {
                            message = "Невірний логін або пароль";
                        }
                        else
                        {
                            isValid = true;
                        }
                    }
                }
            }
            catch (DirectoryServicesCOMException e)
            {
                message = "Невірний логін або пароль";
            }
            catch (ArgumentNullException e)
            {
                message = "Сервер тимчасово не доступний";
            }
            catch (ArgumentException e)
            {
                message = "Невірний логін або пароль";
            }
            catch (NullReferenceException e)
            {
                message = "Невірний логін або пароль";
            }
            catch
            {
                message = "Сервер тимчасово не доступний";
            }
            return isValid;
        }
        public bool IsUserInRole(string username, string roleName)
        {
            try
            {
                using (var context = GetContext())
                {
                    var group = GroupPrincipal.FindByIdentity(context, roleName);
                    var user = group.Members.FirstOrDefault(u => u.SamAccountName == username);
                    if (user != null)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        public string[] GetRolesForUser(string username)
        {
            try
            {
                using (var context = GetContext())
                {
                    var user = UserPrincipal.FindByIdentity(context, username);
                    var groups = user.GetGroups().Select(x => x.Name);
                    if (groups.Any(x => x == "Domain Users"))
                    {
                        return groups.Where(x => x != "Domain Users").ToArray();
                    }
                    return groups.ToArray();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<string> GetUsersForRole(string rolename)
        {
            var users = new List<string>();
            try
            {
                using (var context = GetContext())
                {
                    var group = GroupPrincipal.FindByIdentity(context, rolename);
                    users = group.Members.Where(u => u.StructuralObjectClass == "user").Select(u => u.SamAccountName).ToList();
                }
            }
            catch
            {
                return new List<string>();
            }
            return users;
        }
        public List<string> GetUsersForRoles(List<string> rolesname)
        {
            var users = new List<string>();
            try
            {
                using (var context = GetContext())
                {
                    foreach (var rolename in rolesname)
                    {
                        var group = GroupPrincipal.FindByIdentity(context, rolename);
                        users.AddRange(group.Members.Select(u => u.SamAccountName).ToList());
                    }
                }
            }
            catch
            {
                return new List<string>();
            }
            return users;
        }

        public UserPrincipal GetUser(string username)
        {
            try
            {
                using (var context = GetContext())
                {
                    var user = UserPrincipal.FindByIdentity(context, username);
                    return user;
                }
            }
            catch
            {
                return null;
            }
        }

        public void CreateUser(string login, string name, string surName, string email, string phone)
        {
            try
            {
                using (var context = GetContext())
                {
                    var user = new UserPrincipal(context, login, "123456", true);
                    user.UserPrincipalName = $"{login}@asoiu.kpi.ua";
                    user.GivenName = name;
                    user.Surname = surName;
                    user.DisplayName = $"{name} {surName}";
                    user.EmailAddress = email;
                    user.VoiceTelephoneNumber = phone;
                    user.PasswordNeverExpires = true;
                    user.ExpirePasswordNow();
                    user.Save();
                }
            }
            catch
            {

            }
        }
        public void EditUser(string login, string name, string surName, string email, string phone)
        {
            try
            {
                using (var context = GetContext())
                {
                    var user = UserPrincipal.FindByIdentity(context, login);
                    user.UserPrincipalName = $"{login}@asoiu.kpi.ua";
                    user.GivenName = name;
                    user.Surname = surName;
                    user.DisplayName = $"{name} {surName}";
                    user.EmailAddress = email;
                    user.VoiceTelephoneNumber = phone;
                    user.PasswordNeverExpires = true;
                    user.ExpirePasswordNow();
                    user.Save();
                }
            }
            catch
            {

            }
        }
        public void DeleteUser(string login)
        {
            try
            {
                using (var context = GetContext())
                {
                    var user = UserPrincipal.FindByIdentity(context, login);
                    user.Delete();
                }
            }
            catch(Exception e)
            {

            }
        }

        public void ChangePassword(string login, string newPassword)
        {
            try
            {
                using (var context = GetContext())
                {
                    var user = UserPrincipal.FindByIdentity(context, login);
                    user.SetPassword(newPassword);
                    user.Save();
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
