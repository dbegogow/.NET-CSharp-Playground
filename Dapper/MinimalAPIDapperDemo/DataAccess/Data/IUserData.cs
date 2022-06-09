using DataAccess.Models;

namespace DataAccess.Data;
public interface IUserData
{
    Task DeleteUser(int id);

    Task<UserModel> GetUser(int id);

    Task<IEnumerable<UserModel>> GetUsers();

    Task InsertUser(UserModel user);

    Task UpdateUser(UserModel user);
}