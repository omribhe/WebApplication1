﻿using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/transfer")]
    [ApiController]
    public class TransferController : ControllerBase
    {

        public class TransferBody
        {
            public string from { get; set; }
            public string to { get; set; }
                
            public string content { get; set; }
        }


        // POST api/<TransferController>
        [HttpPost]
        public void Post([FromBody] TransferBody postTransfer)
        {
            User u = Database.users.Find(x => x.Name == postTransfer.to);
            if (null == u)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                if (u.Contacts.Exists(x => x.id == postTransfer.from))
                {
                    Database.addTransfer(postTransfer.to, postTransfer.from, postTransfer.content);
                    base.Response.StatusCode = (int)HttpStatusCode.Created;
                }
                else
                {
                    base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }
            }
        }
    }
}