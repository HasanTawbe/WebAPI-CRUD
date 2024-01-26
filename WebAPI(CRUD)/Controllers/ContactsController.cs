using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_CRUD_.DB;
using WebAPI_CRUD_.Model;

namespace WebAPI_CRUD_.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ContactsController : Controller
    {

        private ContactsAPIDbContext dbContext;
        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(Contact addContactRequest)
        {
            var contact = new Contact()
            {
                id = Guid.NewGuid(),
                name = addContactRequest.name,
                address = addContactRequest.address,
                email = addContactRequest.email,
                phone = addContactRequest.phone,
            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact=await dbContext.Contacts.FindAsync(id);
            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> EditContact([FromRoute] Guid id,Contact updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            contact.name =updateContactRequest.name;
            contact.address =updateContactRequest.address;
            contact.email =updateContactRequest.email;
            contact.phone =updateContactRequest.phone;
            
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            dbContext.Remove(contact);
            return Ok(contact);
        }

        



    }
  
}
