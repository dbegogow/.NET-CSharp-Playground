using DataAccess.Models;
using DataAccess.DbAccess;

namespace DataAccess.Data;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<UserModel>> GetUsers()
        => this._db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });

    public async Task<UserModel> GetUser(int id)
    {
        var result = await this._db.LoadData<UserModel, dynamic>(
            "dbo.spUser_Get",
            new { Id = id });

        return result.FirstOrDefault();
    }

    public Task InsertUser(UserModel user)
        => this._db.SaveData(
            "dbo.spUser_Insert",
            new { user.FirstName, user.LastName });

    public Task UpdateUser(UserModel user)
        => this._db.SaveData(
            "dbo.spUser_Update",
            user);

    public Task DeleteUser(int id)
        => this._db.SaveData(
            "dbo.spUser_Delete", new { Id = id });
}
