using Microsoft.EntityFrameworkCore;
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
        #region LINQ
        (ApiUserModel, string, int) AddUser(ApiUserModel userModel);
        (ApiUserModel, string, int) EditUser(ApiUserModel userModel);
        (ApiUserModel, string, int) DeleteUser(string username);
        (ApiUserModel, string, int) GetUser(string username);
        (List<ApiUserModel>, string, int) GetAllUsers();
        #endregion

        #region SQLRAW
        (ApiUserModel, string, int) AddUser_FromSqlRaw(ApiUserModel userModel);
        (ApiUserModel, string, int) EditUser_FromSqlRaw(ApiUserModel userModel);
        (ApiUserModel, string, int) DeleteUser_FromSqlRaw(string username);
        (ApiUserModel, string, int) GetUser_FromSqlRaw(string username);
        (List<ApiUserModel>, string, int) GetAllUsers_FromSqlRaw();
        #endregion
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

        public (ApiUserModel, string, int) AddUser_FromSqlRaw(ApiUserModel userModel)
        {
            try
            {
                _dbContext.Users.FromSqlRaw($"EXEC ADD_USER '{userModel.Username}', '{userModel.Name}', {userModel.Age}");
                //if (users.Count > 0)
                return (userModel, "SUCCESS", 200);
                //else
                //    return (null, "An errror has occorred.", 500);
            }
            catch (Exception ex)
            {
                return (null, ex.Message, 500);
            }
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

        public (ApiUserModel, string, int) DeleteUser_FromSqlRaw(string username)
        {
            try
            {
                var users = _dbContext.Users.FromSqlRaw($"EXEC DELETE_USER '{username}'").ToList();
                if (users.Count > 0)
                    return (users.FirstOrDefault().MappingUserEntityToApiUserModel(), "SUCCESS", 200);

                return (null, "An error has occorred.", 500);
            }
            catch (Exception ex)
            {
                return (null, ex.Message, 500);
            }
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

        public (ApiUserModel, string, int) EditUser_FromSqlRaw(ApiUserModel userModel)
        {
            try
            {
                var users = _dbContext.Users.FromSqlRaw($"EXEC UPDATE_USER '{userModel.Username}', '{userModel.Name}', {userModel.Age}").ToList();
                if (users.Count > 0)
                    return (users.FirstOrDefault().MappingUserEntityToApiUserModel(), "SUCCESS", 200);

                return (null, "An error has occorred.", 500);
            }
            catch (Exception ex)
            {
                return (null, ex.Message, 500);
            }
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

        public (List<ApiUserModel>, string, int) GetAllUsers_FromSqlRaw()
        {
            try
            {
                var users = _dbContext.Users.FromSqlRaw($"EXEC GET_USER").ToList();
                if (users.Count > 0)
                    return (users.MappingUserEntityToApiUserModel(), "SUCCESS", 200);

                return (null, "There are no users.", 200);
            }
            catch (Exception ex)
            {
                return (null, ex.Message, 500);
            }
        }

        public (ApiUserModel, string, int) GetUser(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(it => it.Username == username);
            if (user == null)
                return (null, "User not found", 404);
            return (user.MappingUserEntityToApiUserModel(), "SUCCESS", 200);
        }

        public (ApiUserModel, string, int) GetUser_FromSqlRaw(string username)
        {
            try
            {
                var users = _dbContext.Users.FromSqlRaw($"EXEC GET_USER {username}").ToList();
                if (users.Count > 0)
                    return (users.FirstOrDefault().MappingUserEntityToApiUserModel(), "SUCCESS", 200);

                return (null, "User was not found.", 404);
            }
            catch (Exception ex)
            {
                return (null, ex.Message, 500);
            }
        }
    }
}
