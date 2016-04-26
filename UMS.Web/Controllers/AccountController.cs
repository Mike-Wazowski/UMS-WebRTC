using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UMS.Database.DAL;
using UMS.Database.Models;
using UMS.Security;
using UMS.Web.ViewModels;

namespace UMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUmsDbContext _context;
        private IPasswordManager _passwordManager;
        private readonly object _registerUserSyncRoot = new object();

        public AccountController(IUmsDbContext context, IPasswordManager passwordManager)
        {
            _context = context;
            _passwordManager = passwordManager;
        }
        // GET: Account
        public ActionResult Login()
        {
            if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            return await AuthorizeUser(model);
        }

        public ActionResult Register()
        {
            if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register (RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            return RegisterUser(model);
        }

        private async Task<ActionResult> AuthorizeUser(LoginViewModel model)
        {
            string email = model.Email.ToLower();
            var user = await _context.Users.Where(x => x.Email == email).AsNoTracking().FirstOrDefaultAsync();
            if(user != null)
            {
                if(_passwordManager.AreEqual(model.Password, user.Password, user.Salt))
                {
                    CreateCookie(user);
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            model.ShowMessage(ToastrType.Error, "Błąd", "Nieprawidłowy login lub hasło.");
            model.Password = string.Empty;
            return View(model);
        }

        private void CreateCookie(User user)
        {
            FormsAuthentication.SetAuthCookie(user.Email, false);
            Response.Cookies.Clear();
            DateTime expDate = DateTime.Now.AddHours(2);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    2, user.Email, DateTime.Now, expDate, true, string.Empty);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authenticationCookie);
        }

        private ActionResult RegisterUser(RegisterViewModel model)
        {
            model.Email = model.Email.ToLower();
            lock(_registerUserSyncRoot)
            {
                var user = _context.Users.Where(x => x.Email == model.Email).AsNoTracking().FirstOrDefault();
                if(user == null)
                {
                    var securePassword = _passwordManager.EncryptPassword(model.Password);
                    user = new User(model.Email, model.FirstName, model.LastName, securePassword.HashedPassword, securePassword.Salt);
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    var loginViewModel = new LoginViewModel();
                    loginViewModel.ShowMessage(ToastrType.Success, "Sukces", "Twoje konto zostało utworzone. Możesz się zalogować.");
                    return View("Login", loginViewModel);
                }
            }
            model.ShowMessage(ToastrType.Error, "Błąd", "Podany email istanieje już w bazie.");
            model.Password = model.PasswordConfirmation = string.Empty;
            return View(model);
        }
    }
}