using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPortFolio.Data;
using MVCPortFolio.Models;

namespace MVCPortFolio.Controllers
{
    public class AccountController : Controller
    {
        private readonly MVCPortFolioContext _context;

        public AccountController(MVCPortFolioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get Login 뷰
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            var account = new Account();
            return View(account);

        }
        /// <summary>
        /// Get 회원가입 뷰
        /// </summary>
        /// <returns></returns>
        public IActionResult SignUp()
        {
            var user = new User();
            return View(user);
        }

        /// <summary>
        /// DB에 저장하는 회원가입페이지
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SignUp([Bind("UserNo, UserName,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var result = checkAccount(user.Email, user.Password);
                if (result == null)
                {
                    // 계정이 없을 경우 화면을 Login 제자리
                    ViewBag.Message = "해당계정이 없습니다";
                    return View("Login");
                }
                else
                {
                    // 로그인할 경우 Home/Home으로 이동
                    HttpContext.Session.SetString("UserEmail", result.Email);
                    return RedirectToAction("Home", "Home");
                }
            }
            return View("Login");
        }

        /// <summary>
        /// Email과 password를 확인하는 함수
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private User checkAccount(string email, string password)
        {
            return _context.User.SingleOrDefault(a => a.Email.Equals(email) && a.Password.Equals(password));
        }

        /// <summary>
        /// Logout후 화면
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Home", "Home"); // Home/Home으로 이동
        }

    }
}
