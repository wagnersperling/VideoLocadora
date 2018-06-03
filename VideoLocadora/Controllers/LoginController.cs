using System;
using System.Web.Mvc;
using VideoLocadora.Models;
using VideoLocadora.DAO;

namespace VideoLocadora.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            Login login = new Login();
            return View(login);
        }

        //método que cadastra novo usuário 
        public ActionResult Insert(string email, string password)
        {
            //valida campo email
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "E-mail em branco");
            }

            //valida campo senha
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Senha em branco");
            }

            if (ModelState.IsValid)
            {
                LoginDAO.Insert(email, password);

                return View("Index", new Login());
            }
            return View("Index");
        }

        //verifica se existe usuário cadastrado e faz login
        [HttpPost]
        public ActionResult Validate(Login login)
        {
            if (LoginDAO.ValidadeLogin(login))
            {
                Session["username"] = login.Email;
				return RedirectToAction("Index", "Movie");
                //return View("../Movie/Index", MovieDAO.FindAll());
            }
            ViewBag.Error = "Usuário ou senha inválidos  ";
            return View("Index", login);
        }

        public ActionResult Logout()
        {
            Session.Remove("username");
            Login login = new Login();
            return View("Index", login);
        }
    }
}