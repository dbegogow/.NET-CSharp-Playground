﻿using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class UserData
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
}
