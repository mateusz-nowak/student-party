using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Party.Models;

namespace Party.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Event");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Dashboard", "Event");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Dashboard", "Event");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
					          return "Użytkownik o takiej nazwie już istnieje. Wybierz inną nazwę użytkownika.";

                case MembershipCreateStatus.DuplicateEmail:
					          return "Użytkownik o takim adresie e-mail już istnieje. Wybierz inny adres e-mail.";

                case MembershipCreateStatus.InvalidPassword:
					          return "Hasło podane przez ciebie jest nieprawidłowe.";

                case MembershipCreateStatus.InvalidEmail:
					          return "Adres e-mail podany przez ciebie jest nieprawidłowy.";

                case MembershipCreateStatus.InvalidAnswer:
					          return "Odpowiedź na pytanie pomocnicze jest niepoprawne. Spróbuj ponownie.";

                case MembershipCreateStatus.InvalidQuestion:
					          return "Pytanie pomocnicze do hasła jest nieprawidłowe. Spróbuj ponownie.";

                case MembershipCreateStatus.InvalidUserName:
					          return "Niepoprawna nazwa użytkownika. Spróbuj ponownie.";

                case MembershipCreateStatus.ProviderError:
					          return "Podczas logowania wystąpił nieznany błąd. Spróbuj ponownie. Jeżeli problem będzie się powtarzał, poinformuj o tym administratora.";

                case MembershipCreateStatus.UserRejected:
					          return "Stworzenie użytkownika zostało wstrzymane. Spróbuj ponownie. Jeżeli problem będzie się powtarzał, poinformuj o tym administratora.";

                default:
					          return "Wystąpił nieznany błąd. Spróbuj ponownie. Jeżeli problem będzie się powtarzał, poinformuj o tym administratora.";
            }
        }
        #endregion
    }
}
