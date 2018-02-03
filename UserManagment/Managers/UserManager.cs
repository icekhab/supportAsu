using DomainController.Manager;
using System.Linq;
using SupportAsu.DTO.Roles;
using SupportAsu.UserManagment.Models.Users;
using System;
using SupportAsu.Repository;
using SupportAsu.Model;
using SupportAsu.DTO.User;

namespace UserManagment.Managers
{
    public class UserManager : IUserManager
    {
        private IDomainManager _domainManager;
        private IGenericRepository _repository;
        public UserManager(IGenericRepository repository, IDomainManager domainManager)
        {
            _repository = repository;
            _domainManager = domainManager;
        }

        public User GetUser(string username)
        {
            var adUser = _domainManager.GetUser(username);
            var roles = _domainManager.GetRolesForUser(username);
            User user = IndentifyUser(roles);
            user.Email = adUser.EmailAddress;
            user.IsEnabled = adUser.Enabled.Value;
            user.Login = username;
            user.Phone = adUser.VoiceTelephoneNumber;
            user.Name = adUser.DisplayName;

            #region InsertOrUpdateInDB
            InsertOrUpdateUser(user);
            #endregion

            return user;
        }
        public UserModel GetUserModal(string login)
        {
            var user = _domainManager.GetUser(login);
            return new UserModel
            {
                DisplayName = user.DisplayName,
                Email = user.EmailAddress,
                Login = user.SamAccountName,
                Name = user.GivenName,
                Phone = user.VoiceTelephoneNumber,
                SurName = user.Surname
            };
        }
        public void CreateUser(UserModel model)
        {
            _domainManager.CreateUser(model.Login, model.Name, model.SurName, model.Email, model.Phone);
        }
        public void EditUser(UserModel model)
        {
            _domainManager.EditUser(model.Login, model.Name, model.SurName, model.Email, model.Phone);
        }
        public bool ValidateUser(string username, string password, out string message)
        {
            return _domainManager.ValidateUser(username, password, out message);
        }
        public User IndentifyUser(User user)
        {
            if (user.Role == Role.Administrator)
            {
                return new Administrator()
                {
                    Id = user.Id,
                    Email = user.Email,
                    IsEnabled = user.IsEnabled,
                    Login = user.Login,
                    Name = user.Name,
                    Phone = user.Phone,
                    Role = user.Role
                };
            }
            else
            {
                if (user.Role == Role.Intern)
                {
                    return new Intern()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        IsEnabled = user.IsEnabled,
                        Login = user.Login,
                        Name = user.Name,
                        Phone = user.Phone,
                        Role = user.Role
                    };
                }
                else
                {
                    if (user.Role == Role.Director)
                    {
                        return new Director()
                        {
                            Id = user.Id,
                            Email = user.Email,
                            IsEnabled = user.IsEnabled,
                            Login = user.Login,
                            Name = user.Name,
                            Phone = user.Phone,
                            Role = user.Role
                        };
                    }
                    else
                    {
                        return new Public()
                        {
                            Id = user.Id,
                            Email = user.Email,
                            IsEnabled = user.IsEnabled,
                            Login = user.Login,
                            Name = user.Name,
                            Phone = user.Phone,
                            Role = user.Role
                        };
                    }
                }
            }
        }

        public void InsertOrUpdateUser(User user)
        {
            var dbUser = _repository.TableNoTracking<User>().SingleOrDefault(x => x.Login == user.Login);
            if (dbUser != null)
            {
                dbUser.Email = user.Email;
                dbUser.IsEnabled = user.IsEnabled;
                dbUser.Name = user.Name;
                dbUser.Phone = user.Phone;
                dbUser.Role = user.Role;
            }
            else
            {
                dbUser = new User
                {
                    Email = user.Email,
                    IsEnabled = user.IsEnabled,
                    Name = user.Name,
                    Phone = user.Phone,
                    Role = user.Role,
                    Login = user.Login
                };
            }
            _repository.InsertOrUpdate(dbUser);
            user.Id = dbUser.Id;
        }

        public void SaveUsersByRole(string rolename)
        {
            var users = _domainManager.GetUsersForRole(rolename);
            foreach (var username in users)
            {
                var user = GetUser(username);
            }
            var deletedUsers = _repository.TableNoTracking<User>().Where(x => (x.Role == rolename && !users.Contains(x.Login)));
            foreach (var user in deletedUsers)
            {
                _repository.Delete(user);
            }
        }

        #region Private Methods
        private User IndentifyUser(string[] roles)
        {
            if (roles.Contains(Role.Administrator))
            {
                return new Administrator() { Role = Role.Administrator };
            }
            else
            {
                if (roles.Contains(Role.Intern))
                {
                    return new Intern() { Role = Role.Intern };
                }
                else
                {
                    if (roles.Contains(Role.Director))
                    {
                        return new Director() { Role = Role.Director };
                    }
                    else
                    {
                        return new Public() { Role = Role.User };
                    }
                }
            }
        }

        public string GetUserName(string username)
        {
            return _domainManager.GetUser(username).DisplayName;
        }
        public void DeleteUser(string login)
        {
            _domainManager.DeleteUser(login);
        }

        public void ChangePassword(string login, string newPassword)
        {
            _domainManager.ChangePassword(login, newPassword);
        }
        #endregion

    }
}
