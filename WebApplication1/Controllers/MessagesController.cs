﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication1.Models;

using System;
using System.IO;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/contacts/{contact}/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        // GET: api/<MessagesController>
        [HttpGet]
        public IEnumerable<Message> Get(string username, string contact)
        {
            User u = Database.users.Find(x => x.Name == username);
            if (u == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
            Contact c = u.Contacts.Find(x => x.id == contact);
            if (c == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;

            }
            else
            {
                base.Response.StatusCode = (int)HttpStatusCode.OK;
                return c.messages;
            }



        }

        // GET api/<MessagesController>/5
        [HttpGet("{id}")]
        public Message Get(string username, string contact, int id)
        {
            User u = Database.users.Find(x => x.Name == username);
            if (u == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
            Contact c = u.Contacts.Find(x => x.id == contact);
            if (c == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;

            }
            Message m = c.messages.Find(x => x.id == id);
            if (m == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;

            }
            else
            {
                base.Response.StatusCode = (int)HttpStatusCode.OK;
                return m;
            }
        }



        public class TransferBody
        {
            public string from { get; set; }
            public string to { get; set; }

            public string content { get; set; }
        }

        
        public class stringMessage
        {
            public string content { get; set; }
        }

        // POST api/<MessagesController>
        [HttpPost]
        public void Post(string username, string contact, [FromBody] stringMessage content)
        {
            User u = Database.users.Find(x => x.Name == username);
            if (u == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            Contact c = u.Contacts.Find(x => x.id == contact);
            if (c == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;

            }
            Database.addTransfer(username, contact, content.content);
            base.Response.StatusCode = (int)HttpStatusCode.Created;
        }

        // PUT api/<MessagesController>/5
        [HttpPut("{id}")]
        public void Put(string username, string contact,int id, [FromBody] string content)
        {
            User u = Database.users.Find(x => x.Name == username);
            if (u == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            Contact c = u.Contacts.Find(x => x.id == contact);
            if (c == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            Message m = c.messages.Find(x=> x.id == id);
            if (m == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            m.content = content;
            c.last = c.messages.Last().content;
            base.Response.StatusCode = (int)HttpStatusCode.NoContent;


        }

        // DELETE api/<MessagesController>/5
        [HttpDelete("{id}")]
        public void Delete(string username, string contact, int id)
        {
            User u = Database.users.Find(x => x.Name == username);
            if (u == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            Contact c = u.Contacts.Find(x => x.id == contact);
            if (c == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;

            }
            Message m = c.messages.Find(x => x.id == id);
            if (m == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;

            }
            else
            {
                c.messages.Remove(m);
                if (c.messages.Count(x=> x!= null) != 0)
                    c.last = c.messages.Last().content;
                else
                    c.last = null;
                base.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
        }
    }
}
