using FreelancerArticle.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FreelancerArticle.Web.ServiceReference;

namespace FreelancerArticle.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserServiceClient _clientUser = new UserServiceClient("BasicHttpBinding_IUserService");

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model, string role, string numberWallet)
        {
            try
            {
                if (ModelState.IsValid)
                    if (!_clientUser.CheckUserLogin(model.Login))
                    {
                        _clientUser.AddUser(role, model.Login, model.Password, model.Name, model.LastName, model.Patronymic, numberWallet);
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home", new { role });
                    }
                    else
                        ModelState.AddModelError(String.Empty, "Такой пользователь уже существует");
            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
			ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string role, string returnUrl)
        {
			try
            {
                if (_clientUser.CheckUser(role, model.Login, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
					return RedirectToAction("Index", "Home", new { role });
                }
                else
                    ModelState.AddModelError(String.Empty, "Неверный логин или пароль");
            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }	
    }
}