using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcFirmaCagri.Models.Entity;
using System.Web.Mvc;

namespace MvcFirmaCagri.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        DbServisEntities db = new DbServisEntities();
        [Authorize]
        public ActionResult ActiveCalls()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirms.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();

            var calls = db.TblCalls.Where(x=>x.Status==true && x.CallFirm ==id).ToList();
            ViewBag.c = calls.Count();
            return View(calls);
        }

        public ActionResult PassiveCalls()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirms.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var calls = db.TblCalls.Where(x => x.Status == false && x.CallFirm == id ).ToList();
            ViewBag.c = calls.Count();
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
            var mail = (string)Session["Mail"];
            var id = db.TblFirms.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            
            c.Status = true;
            c.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.CallFirm = id;
            db.TblCalls.Add(c);
            db.SaveChanges();
            return RedirectToAction("ActiveCalls");
        }

        public ActionResult CallDetail(int id)
        {
            var calls = db.TblCallDetails.Where(x => x.Call == id).ToList();
            ViewBag.c = calls.Count();
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

        [HttpGet]
        public ActionResult EditProfile()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirms.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var profile = db.TblFirms.Where(x=>x.ID == id).FirstOrDefault();
            return View(profile);
        }

        public ActionResult HomePage()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirms.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var totalcall = db.TblCalls.Where(x=>x.CallFirm == id).Count();
            var activecall = db.TblCalls.Where(x => x.CallFirm == id && x.Status == true).Count();
            var passivecall = db.TblCalls.Where(x => x.CallFirm == id && x.Status == false).Count();
            var sector = db.TblFirms.Where(x => x.ID == id).Select(y => y.Sector).FirstOrDefault();
            var officer = db.TblFirms.Where(x=>x.ID == id).Select(y => y.Officer).FirstOrDefault();
            ViewBag.tc = totalcall;
            ViewBag.ac = activecall;
            ViewBag.pc = passivecall;
            ViewBag.sc = sector;
            ViewBag.off = officer;
            return View();
        }

        public PartialViewResult Partial1()
        {
            var mail = (string)Session["Mail"];
            var messages = db.TblMessages.Where(x => x.Receiver == mail && x.Status == true).ToList();
            var messagecount = db.TblMessages.Where(x => x.Receiver == mail && x.Status == true).Count();
            ViewBag.m = messagecount;
            return PartialView(messages);
        }

        public PartialViewResult Partial2()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirms.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var calls = db.TblCalls.Where(x => x.CallFirm == id && x.Status == true).ToList();
            //var messagecount = db.TblMessages.Where(x => x.Receiver == mail && x.Status == true).Count();
            //ViewBag.m = messagecount;
            return PartialView(calls);
        }
    }
}