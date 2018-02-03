using DomainController.Manager;
using SupportAsu.Repository;
using SupportAsu.User.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagment.Managers;
using SupportAsu.DTO.User;

namespace SupportAsu.User.Services
{
    public class UserService : IUserService
    {
        private IGenericRepository _repository;
        private IUserManager _userManager;
        public UserService(IUserManager userManager, IGenericRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }
        public bool CheckLogin(string login)
        {
            var user = _repository.TableNoTracking<Model.User>().FirstOrDefault(x => x.Login == login);
            return user == null;
        }

        public void DeleteUser(string login)
        {
            _userManager.DeleteUser(login);
            var user = _repository.TableNoTracking<Model.User>().FirstOrDefault(x => x.Login == login);
            if(user!=null)
            {
                _repository.Delete(user);
            }
        }

        public UserModel GetUser(string login)
        {
            var user = _userManager.GetUserModal(login);
            return user;
        }

        public List<UserModel> GetUsers(string filter)
        {
            var users = _repository.TableNoTracking<Model.User>().Where(x => (x.Login.Contains(filter) || x.Name.Contains(filter))).Select(x => new UserModel
            {
                Login = x.Login,
                Email = x.Email,
                DisplayName = x.Name,
                Phone = x.Phone
            }).ToList();
            return users;
        }

        public void InsertOrUpdate(UserModel model, bool isCreate)
        {
            if (isCreate)
            {
                _userManager.CreateUser(model);
            }
            else
            {
                _userManager.EditUser(model);
            }
            _userManager.GetUser(model.Login);
        }
    }
}
