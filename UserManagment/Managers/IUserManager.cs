using SupportAsu.DTO.User;
using SupportAsu.Model;
using SupportAsu.UserManagment.Models.Users;

namespace UserManagment.Managers
{
    public interface IUserManager
    {
        bool ValidateUser(string username, string password, out string message);
        User GetUser(string username);
        User IndentifyUser(User user);

        string GetUserName(string username);

        void InsertOrUpdateUser(User user);
        void SaveUsersByRole(string rolename);
        void CreateUser(UserModel model);
        void EditUser(UserModel model);
        UserModel GetUserModal(string login);
        void DeleteUser(string login);
        void ChangePassword(string login, string newPassword);
    }
}
