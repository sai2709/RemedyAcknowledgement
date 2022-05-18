using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemedyAcknowledgement.Models;

namespace RemedyAcknowledgement.Controllers
{
    public class AdminController : Controller
    {
        RemedyContext context = new RemedyContext();
        // GET: Admin
        public ActionResult AdminPortal()
        {
            string userid = ((List<Logins>)Session["login"])[0].UserId;
            ViewBag.UserId = userid;
            return View();
        }
        public ActionResult RemedyDetailsView(int id)
        {
            RemedyDetails model = context.Remedies.Find(id);
            return View(model);
        }
        public ActionResult EditRemedyDetails(int id )
        {
            RemedyDetails model = context.Remedies.Find(id);
            return View(model);

        }
        [HttpPost]
        public ActionResult EditRemedyDetails(RemedyDetails model)
        {
            RemedyDetails rmd = context.Remedies.Find(model.Remedyid);
            rmd.Category= model.Category;
            rmd.Description= model.Description;
            context.SaveChanges();
            return RedirectToAction("ViewRemedyDetails", "Remedy");

        }
         
        public ActionResult AssignRemedy(int id)
        {
            RemedyDetails rmd = context.Remedies.Find(id);
            RemedyList rmdList = new RemedyList();
            rmdList.UserId = rmd.UserId;
            rmdList.Description = rmd.Description;
            rmdList.Remedyid=rmd.Remedyid;
            // rmdList.Status = "Open";
            return View(rmdList);
        }
        [HttpPost]
        public ActionResult AssignRemedy(RemedyList model)
        {
            if (ModelState.IsValid)
            {
                context.RemedyLists.Add(model);
                context.SaveChanges();
                RemedyDetails rmd = context.Remedies.Find(model.Remedyid);
                rmd.RemedyStatus = "Assigned";
                context.SaveChanges();
                return RedirectToAction("ViewRemedyDetails", "Remedy");
            }
            return View(model);
        }
        public ActionResult ChangeRemedyStatus(int id)
        {
            RemedyList rList = context.RemedyLists.Where(l=>l.Remedyid==id).ToList()[0];
            return View(rList);
        }
        [HttpPost]
        public ActionResult ChangeRemedyStatus(RemedyList model)
        {
            RemedyList rList = context.RemedyLists.Where(l => l.Remedyid == model.Remedyid).ToList()[0];
            rList.Status = model.Status;
            rList.AssignedUserId=model.AssignedUserId;
            context.SaveChanges();
            RemedyDetails rmd = context.Remedies.Find(model.Remedyid);
            rmd.RemedyStatus = rList.Status;
            context.SaveChanges();  
           return RedirectToAction("ViewRemedyDetails", "Remedy");

        }
        public PartialViewResult FlashAlert()
        {

            return PartialView();
        }

    }
}