using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ChatterChat.Models;

namespace ChatterChat.Controllers
{
    public class MessagesController : ApiController
    {
        private ChatServiceContext db = new ChatServiceContext();

        // GET: api/Messages
        public IQueryable<MessageDTO> GetMessages()
        {
            var messages = from m in db.Messages
                           select new MessageDTO()
                           {
                               ID = m.ID,
                               Content = m.Content,
                               UserName = m.User.Name
                           };
            return messages;
            //return db.Messages.Include(m => m.User);
        }

        // GET: api/Messages/5
        [ResponseType(typeof(MessageDTO))]
        public async Task<IHttpActionResult> GetMessage(int id)
        {
            //Message message = await db.Messages.FindAsync(id);
            var message = await db.Messages.Include(m => m.User).Select(m =>
            new MessageDTO()
            {
                ID = m.ID,
                Content = m.Content,
                UserName = m.User.Name
            }).SingleOrDefaultAsync(m => m.ID == id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.ID)
            {
                return BadRequest();
            }

            db.Entry(message).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messages.Add(message);
            await db.SaveChangesAsync();

            // New code:
            // Load user name
            db.Entry(message).Reference(x => x.User).Load();
            var dto = new MessageDTO()
            {
                ID = message.ID,
                Content = message.Content,
                UserName = message.User.Name
            };
            return CreatedAtRoute("DefaultApi", new { id = message.ID }, dto);
        }

        // DELETE: api/Messages/5
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> DeleteMessage(int id)
        {
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            await db.SaveChangesAsync();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Messages.Count(e => e.ID == id) > 0;
        }
    }
}