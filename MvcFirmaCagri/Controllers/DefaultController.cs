using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcFirmaCagri.Models.Entity;
using System.Web.Mvc;

namespace MvcFirmaCagri.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        DbServisEntities db = new DbServisEntities();
        public ActionResult ActiveCalls()
        {
            var calls = db.TblCalls.Where(x=>x.Status==true && x.CallFirm ==5).ToList();
            return View(calls);
        }

        public ActionResult PassiveCalls()
        {
            var calls = db.TblCalls.Where(x => x.Status == false ).ToList();
            return View(calls);
        }

        [HttpGet]
        public ActionResult NewCall()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCall(TblCalls c)
        {
            c.Status = true;
            c.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.CallFirm = 5;
            db.TblCalls.Add(c);
            db.SaveChanges();
            return RedirectToAction("ActiveCalls");
        }

        public ActionResult CallDetail(int id)
        {
            var calls = db.TblCallDetails.Where(x => x.Call == id).ToList();
            return View(calls);
        }

        public ActionResult GetCall(int id)
        {
            var call = db.TblCalls.Find(id);
            return View("GetCall", call);
        }

        public ActionResult EditCall(TblCalls c)
        {
            var call = db.TblCalls.Find(c.ID);
            call.Issue = c.Issue;
            call.Description = c.Description;
            db.SaveChanges();
            return RedirectToAction("ActiveCalls");
        }
    }
}