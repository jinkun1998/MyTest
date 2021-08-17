using Microsoft.AspNetCore.Mvc;
using MyFirstTest.Entities;
using MyFirstTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstTest.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly MyDbContext _myDbContext;
        public UserController()
        {
            _myDbContext = new MyDbContext();
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_myDbContext.Users);
        }

        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(string username)
        {
            var user = _myDbContext.Users.FirstOrDefault(it => it.Username == username.Trim());
            if (user != null)
                return Ok(user);
            else
                return NotFound("User not found.");
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] User user)
        {
            if (user == null)
                return BadRequest();
            _myDbContext.Add(user);
            _myDbContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Edit([FromBody] User apiUser)
        {
            var user = _myDbContext.Users.FirstOrDefault(it => it.Username == apiUser.Username);
            if (user != null)
            {
                user.Age = apiUser.Age;
                user.Name = apiUser.Name;
                _myDbContext.Update(user);
                _myDbContext.SaveChanges();
                return Ok();
            }
            else
                return NotFound("User not found.");
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(string username)
        {
            var user = _myDbContext.Users.FirstOrDefault(it => it.Username == username.Trim());
            if (user != null)
            {
                _myDbContext.Remove(user);
                _myDbContext.SaveChanges();
                return Ok();
            }
            else
                return NotFound("User not found.");
        }
    }
}
