using Public.Service.User.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UserModel = Public.Entities.Models.User;

namespace Public.Service.User.Extensions
{
    public static class MapingExtension
    {
        public static UserModel MappingToNewUserEntityModel(this ApiUserModel apiUserModel)
        {
            return new UserModel()
            {
                Age = apiUserModel.Age,
                Name = apiUserModel.Name,
                Username = apiUserModel.Username
            };
        }

        public static void MappingToUserEntityModel(this UserModel user, ApiUserModel apiUserModel)
        {
            user.Age = apiUserModel.Age;
            user.Name = apiUserModel.Name;
        }

        public static ApiUserModel MappingUserEntityToApiUserModel(this UserModel user)
        {
            return new ApiUserModel()
            {
                Age = user.Age,
                Name = user.Name,
                Username = user.Username
            };
        }

        public static List<ApiUserModel> MappingUserEntityToApiUserModel(this List<UserModel> users)
        {
            List<ApiUserModel> ret = new List<ApiUserModel>();
            foreach (var user in users)
            {
                ret.Add(new ApiUserModel()
                {
                    Age = user.Age,
                    Name = user.Name,
                    Username = user.Username
                });
            }
            return ret;
        }
    }
}
