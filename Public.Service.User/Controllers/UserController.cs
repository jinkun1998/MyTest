using Microsoft.AspNetCore.Mvc;
using Public.Service.User.Models;
using Public.Service.User.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public.Service.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser([FromBody] ApiUserModel apiUserModel)
        {
            (ApiUserModel returnUser, string result, int statusCode) = _userService.AddUser(apiUserModel);
            if (returnUser == null)
                return StatusCode(statusCode, result);
            return StatusCode(statusCode, returnUser);
        }

        [HttpPut]
        [Route("EditUser")]
        public IActionResult EditUser([FromBody] ApiUserModel apiUserModel)
        {
            (ApiUserModel returnUser, string result, int statusCode) = _userService.EditUser(apiUserModel);
            if (returnUser == null)
                return StatusCode(statusCode, result);
            return StatusCode(statusCode, returnUser);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public IActionResult DeleteUser(string username)
        {
            (ApiUserModel returnUser, string result, int statusCode) = _userService.DeleteUser(username);
            if (returnUser == null)
                return StatusCode(statusCode, result);
            return StatusCode(statusCode, returnUser);
        }

        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(string username)
        {
            (ApiUserModel returnUser, string result, int statusCode) = _userService.GetUser(username);
            if (returnUser == null)
                return StatusCode(statusCode, result);
            return StatusCode(statusCode, returnUser);
        }

        [HttpGet]
        [Route("GetAllUser")]
        public IActionResult GetAllUser()
        {
            (List<ApiUserModel> returnUsers, string result, int statusCode) = _userService.GetAllUsers();
            return StatusCode(statusCode, returnUsers);
        }
    }
}
