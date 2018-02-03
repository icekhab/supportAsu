using SupportAsu.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.User.Services.Abstract
{
    public interface IUserService
    {
        bool CheckLogin(string login);
        void InsertOrUpdate(UserModel model, bool isCreate);
        List<UserModel> GetUsers(string filter);
        UserModel GetUser(string login);
        void DeleteUser(string login);
    }
}
