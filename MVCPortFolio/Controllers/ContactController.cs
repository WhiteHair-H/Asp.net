using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCPortFolio.Data;
using MVCPortFolio.Models;

namespace MVCPortFolio.Controllers
{
    public class ContactController : Controller
    {
        private readonly MVCPortFolioContext _context;

        public ContactController(MVCPortFolioContext context)
        {
            _context = context;
            
        }

        // GET: Contact
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Index([Bind("Id,Name,Email,Contents")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    contact.Regdate = DateTime.Now;
                    _context.Add(contact); // 메모리상에 데이터가 올라감
                    await _context.SaveChangesAsync(); // DB저장 , 커밋
                    


                    ViewBag.Message = "감사합니다. 연락드리겠습니다.";
                    ModelState.Clear();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"예외가 발생했습니다. {ex.InnerException}";
                    ModelState.Clear();
                }
                //return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
