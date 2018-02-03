namespace UserManagment.Providers
{
    public interface IUserManagmentProvider
    {
        bool ValidateUser(string username, string password, string message);
    }
}
