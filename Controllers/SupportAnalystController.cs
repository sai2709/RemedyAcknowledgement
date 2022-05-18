using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemedyAcknowledgement.Models;

namespace RemedyAcknowledgement.Controllers
{
    public class SupportAnalystController : Controller
    {
        RemedyContext context=new RemedyContext();
        // GET: SupportAnalyst
        public ActionResult AnalystPortal()
        {
            string userid = ((List<Logins>)Session["login"])[0].UserId;
            ViewBag.UserId = userid;
            return View();
        }
        public ActionResult ViewRemedyList()
        {
            string userId = ((List<Logins>)Session["login"])[0].UserId;
            List<RemedyList> rmdList=context.RemedyLists.Where(l=>l.AssignedUserId==userId).ToList();
            return View(rmdList);
        }
        public ActionResult GetRemedyDetails(int id)
        {
            RemedyList rList = context.RemedyLists.Where(l=>l.Remedyid==id).FirstOrDefault();
            RemedyDetails rmd = context.Remedies.Find(rList.Remedyid);
            return View(rmd);
        }
        public ActionResult ChangeRemedyStatus(int id)
        {
            RemedyList rList = context.RemedyLists.Find(id);
            return View(rList);
        }
        [HttpPost]
        public ActionResult ChangeRemedyStatus(RemedyList model)
        {
            RemedyList rList = context.RemedyLists.Where(l => l.Remedyid == model.Remedyid).ToList()[0];
            rList.Status = model.Status;
            rList.AssignedUserId = model.AssignedUserId;
            context.SaveChanges();
            RemedyDetails rmd = context.Remedies.Find(model.Remedyid);
            rmd.RemedyStatus = rList.Status;
            context.SaveChanges();
            string userid = ((List<Logins>)Session["login"])[0].UserId;
            if (rmd.RemedyStatus == "Closed")
            {
                Notification alert = new Notification();
                alert.CreationDate = DateTime.Now;
                alert.AlertText = "=> The Remedy Issue is closed - " + rList.Remedyid;
                alert.Details = "SupportAnalyst/Admin has resolved and closed  the remedy issue,the user can reopen the ticket";
                alert.SenderId = userid;
                alert.ReceiverId = rList.UserId;
                alert.NotifDate = DateTime.Now.AddDays(1);
                context.Notifications.Add(alert);
                context.SaveChanges();
            }
                return RedirectToAction("ViewRemedyList", "SupportAnalyst");
            
        }
        public ActionResult RequestDoc(int id)
        {
            string userid = ((List<Logins>)Session["login"])[0].UserId;
            RemedyList rList = context.RemedyLists.Find(id);
            List<Notification> l1 = context.Notifications.Where(l => l.AlertText.EndsWith(rList.Remedyid + " | ")).ToList();
            if(l1.Count>0)
            {
                ViewBag.Error = "The Notification is already sent for current remedy request";
                return View("Error");
            }
            Notification alert = new Notification();
            alert.CreationDate = DateTime.Now;
            alert.AlertText = "=> Need Document for Remedy issue - "+rList.Remedyid;
            alert.Details = "The documentary evidence is required in digital form to solve the problem associated with the remedy issue";
            alert.SenderId =userid;
            alert.ReceiverId = rList.UserId;
            alert.NotifDate= DateTime.Now.AddDays(1);
            context.Notifications.Add(alert);
            context.SaveChanges();
            return RedirectToAction("AnalystPortal", "SupportAnalyst");
            
        }
        public PartialViewResult FlashAlert()
        {
            
            return PartialView();
        }
        public ActionResult TransferTicket(int id)
        {

            //RemedyDetails rmd = context.Remedies.Find(id);
            RemedyList rmdl = context.RemedyLists.Find(id);
            RemedyList rmdList = new RemedyList();
            rmdList.UserId = rmdl.UserId;
            rmdList.Description = rmdl.Description;
            rmdList.Remedyid = rmdl.Remedyid;
            // rmdList.Status = "Open";

            
            //SupportAnalyst sup=new SupportAnalyst();

            return View(rmdList);

        }
        [HttpPost]
        public ActionResult TransferTicket(RemedyList model)
        {
            if (ModelState.IsValid)
            {
                context.RemedyLists.Add(model);
                context.SaveChanges();
                RemedyDetails rmd = context.Remedies.Find(model.Remedyid);
                rmd.RemedyStatus = "Assigned";
                context.SaveChanges();
                string userid = ((List<Logins>)Session["login"])[0].UserId;
                RemedyList rList = context.RemedyLists.Find(model.SerialNo);
                Notification alert = new Notification();
                alert.CreationDate = DateTime.Now;
                alert.AlertText = "=> The Remedy Issue is Transfered - " + rList.Remedyid;
                alert.Details = "New SupportAnalyst/Admin will solve the remedy issue";
                alert.SenderId = userid;
                alert.ReceiverId = rList.AssignedUserId;
                alert.NotifDate = DateTime.Now.AddDays(1);
                context.Notifications.Add(alert);
                context.SaveChanges();
                ViewBag.Message = "Transfered Successfully";
                return View("Message");
            }
            return View(model);

        }
        public ActionResult TicketTransferView()
        {
            string userid = ((List<Logins>)Session["login"])[0].UserId;
            var l = context.Supports.Find(userid);
            string level = l.SupportLevel;
            //var levels = from x in context.Supports
            //             where x.SupportLevel != level
            //             select x.UserId;
            if (level == "L1")
            {
                List<SupportAnalyst> levels = context.Supports.Where(s => s.SupportLevel != level).ToList();
                return View(levels);
            }
            else if (level == "L2")
            {
                List<SupportAnalyst> levels = context.Supports.Where(s => s.SupportLevel == "L3").ToList();
                return View(levels);
            }
            ViewBag.Error = "Contact the Admin for further reference";
            //List<SupportAnalyst> levels=context.Supports.Where(s=>s.SupportLevel!=level).ToList();
            return View("Error");
        }
    }
}