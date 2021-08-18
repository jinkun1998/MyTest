using Public.Entities.Contexts;
using Public.Service.User.Extensions;
using Public.Service.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserModel = Public.Entities.Models.User;

namespace Public.Service.User.Services
{
    public interface IUserService
    {
        (ApiUserModel, string, int) AddUser(ApiUserModel userModel);
        (ApiUserModel, string, int) EditUser(ApiUserModel userModel);
        (ApiUserModel, string, int) DeleteUser(string username);
        (ApiUserModel, string, int) GetUser(string username);
        (List<ApiUserModel>, string, int) GetAllUsers();
    }

    public class UserService : IUserService
    {
        private readonly UserDbContext _dbContext;
        public UserService(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public (ApiUserModel, string, int) AddUser(ApiUserModel userModel)
        {
            var user = _dbContext.Users.FirstOrDefault(it => it.Username == userModel.Username);
            if (user != null)
                return (null, "User already exist", 403);
            user = userModel.MappingToNewUserEntityModel();
            _dbContext.Add(user);
            _dbContext.SaveChanges();
            return (userModel, "SUCCESS", 200);
        }

        public (ApiUserModel, string, int) DeleteUser(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(it => it.Username == username.Trim());
            if (user == null)
                return (null, "User not found", 403);
            _dbContext.Remove(user);
            _dbContext.SaveChanges();
            return (user.MappingUserEntityToApiUserModel(), "SUCCESS", 200);
        }

        public (ApiUserModel, string, int) EditUser(ApiUserModel userModel)
        {
            var user = _dbContext.Users.FirstOrDefault(it => it.Username == userModel.Username);
            if (user == null)
                return (null, "User not found", 404);
            user.MappingToUserEntityModel(userModel);
            _dbContext.Update(user);
            _dbContext.SaveChanges();
            return (userModel, "SUCCESS", 200);
        }

        public (List<ApiUserModel>, string, int) GetAllUsers()
        {
            List<ApiUserModel> ret = new List<ApiUserModel>();
            foreach (var user in _dbContext.Users.ToList())
            {
                ret.Add(user.MappingUserEntityToApiUserModel());
            }
            return (ret, "SUCCESS", 200);
        }

        public (ApiUserModel, string, int) GetUser(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(it => it.Username == username);
            if (user == null)
                return (null, "User not found", 404);
            return (user.MappingUserEntityToApiUserModel(), "SUCCESS", 200);
        }
    }
}
