using Microsoft.AspNetCore.Mvc;
using Phonebook.Data;
using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    public class ContactsController : Controller
    {
        private ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Index" , "Accounts" , new { area = "" });
            }
            else 
            {
                var contacts = await _context.Contacts.ToListAsync();
                return View(contacts);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Index" , "Accounts" , new { area = "" });
            }
            else 
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if(ModelState.IsValid)
            {
                try 
                {
                    await _context.Contacts.AddAsync(contact);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(String.Empty, $"Something went wrong {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, "Something went wrong");
            return View(contact);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Index" , "Accounts" , new { area = "" });
            }
            else 
            {
                var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
                return View(contact);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {   
                    var exist = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == contact.Id);
                    
                    if (exist != null)
                    {
                        exist.FirstName = contact.FirstName;
                        exist.LastName = contact.LastName;
                        exist.Email = contact.Email;
                        exist.Mobiles = contact.Mobiles;

                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid contact to update");
                    return View();
                } 
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Invalid contact to update {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, "Something went wrong");
            return View(contact);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Index" , "Accounts" , new { area = "" });
            }
            else 
            {
                var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
                return View(contact);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == contact.Id);

                    if (exist != null)
                    {
                        _context.Contacts.Remove(exist);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    } 
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                    return View(contact);
                }
            }
            
            ModelState.AddModelError(string.Empty, "Something went wrong");
            return View(contact);

        }
    }
}