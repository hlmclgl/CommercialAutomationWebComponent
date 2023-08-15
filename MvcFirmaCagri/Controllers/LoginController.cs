using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcFirmaCagri.Models.Entity;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcFirmaCagri.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        DbServisEntities db = new DbServisEntities();

        [HttpPost]
        public ActionResult Index(TblFirms f)
        {
            var info = db.TblFirms.FirstOrDefault(x => x.Mail == f.Mail && x.Password == f.Password);
            if (info != null)
            {
                FormsAuthentication.SetAuthCookie(info.Mail, false);
                Session["Mail"]=info.Mail.ToString();
                return RedirectToAction("ActiveCalls", "Default");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}