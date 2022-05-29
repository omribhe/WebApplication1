﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public List<User> Get()
        {
            return Database.users;
        }

        // GET api/<UserController>/5
        [HttpGet("{name}")]
        public User Get(string name)
        {
            if (Database.users.Exists(x => x.Name == name))
            {
                base.Response.StatusCode = (int)HttpStatusCode.OK;
            } 
            else
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            return Database.users.Find(x => x.Name == name);
        }
        public class CreateAccount
        {
            public string name { get; set; }
            public string nickname { get; set; }
            public string picture { get; set; }
            public string password { get; set; }
            public string server { get; set; }
        }

        public class Login
        {
            public string username { get; set; }
            public string password { get; set; }
        }


        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] CreateAccount c)
        {
            if (!Database.users.Exists(x => x.Name == c.name))
            {
                User u = new User();
                u.Name = c.name;
                u.Nickname = c.nickname;
                u.Picture = c.picture;
                u.Password = c.password;
                u.Server = c.server;
                u.Contacts = new List<Contact>();
                Database.users.Add(u);
                base.Response.StatusCode = (int)HttpStatusCode.OK;

            }
            else
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
