using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/invitations")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        public class InvitationsJson
        {
            public string from { get; set; }

            public string to { get; set; }

            public string server { get; set; }

        }

        // POST api/<InvitationsController>
        [HttpPost]
        public void Post([FromBody] InvitationsJson invitationsJson)
        {
            User u = Database.users.Find(x => x.Name == invitationsJson.to);
            if (null == u)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                if (u.Contacts.Exists(x => x.id == invitationsJson.from) == false)
                {
                    Database.addInvitation(invitationsJson.to, invitationsJson.from, invitationsJson.server);
                }
                base.Response.StatusCode = (int)HttpStatusCode.Created;
            }
        }
    }
}
