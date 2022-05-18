using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemedyAcknowledgement.Models;
using System.Data.Entity.Validation;
using System.IO;

namespace RemedyAcknowledgement.Controllers
{
    public class RemedyController : Controller
    {
        RemedyContext context = new RemedyContext();
        // GET: Remedy
        public ActionResult HomePage()
        {
            return View();
        }
       
        public ActionResult UserRegistration()
        {
            User obj = new User();
            obj.UserId = "User" + ((new Random()).Next(1000, 9999));
            return View(obj);
        }
        [HttpPost]
        public ActionResult UserRegistration(User model)
        {
            if (ModelState.IsValid)
            {
                Logins login = new Logins();
                login.UserId = model.UserId;
                login.Password = model.Password;
                login.Role = "User";
                login.Secretquestion = model.Secretquestion;
                login.Secretans = model.Secretanswer;
                login.ContactNumber = model.ContactNumber;
                login.Mailid=model.Mailid;
                context.Users.Add(model);
                int r = context.SaveChanges();
                if (r > 0)
                {
                    context.Logins.Add(login);
                    context.SaveChanges();
                    ViewBag.Success = "registered successfully";
                    ViewBag.userid = model.UserId;
                    return View("Success");
                }
                return View("Success");
            }
            else
            {
                return View();
            }
        }
        public ActionResult SupportRegistration()
        {
            SupportAnalyst obj = new SupportAnalyst();
            obj.UserId = "SUP" + ((new Random()).Next(1000, 9999));
            return View(obj);
        }
        [HttpPost]
        public ActionResult SupportRegistration(SupportAnalyst model)
        {
            if (ModelState.IsValid)
            {
                Logins login = new Logins();
                login.UserId = model.UserId;
                login.Password = model.Password;
                login.Role = "Support";
                login.Secretquestion = model.Secretquestion;
                login.Secretans = model.Secretanswer;
                login.ContactNumber = model.ContactNumber;
                //login.ContactNumber = model.ContactNumber;
                login.Mailid = model.Mailid;
                context.Supports.Add(model);
                int r = context.SaveChanges();
                if (r > 0)
                {
                    context.Logins.Add(login);
                    context.SaveChanges();
                    ViewBag.Success = "registered successfully";
                    ViewBag.userid = model.UserId;
                    return View("Success");

                }
                return View("Success");
            }
            else
            {
                return View();
            }
        }
       
        public ActionResult Login(string id)
        {
            Logins model = new Logins();
            model.Role = id;

            return View(model);
        }
        [HttpPost]
        public ActionResult Login(Logins model)
        {
            var login = context.Logins.Where(x => x.UserId == model.UserId && x.Password == model.Password).ToList();
            if (login.Count > 0)
            {
                if (login[0].Role == "User")
                {
                    Session.Add("login", login);
                    return RedirectToAction("Remedydetails", "Remedy", new{contact=login[0].ContactNumber,id=login[0].UserId});
                }
                else if (login[0].Role == "Support")
                {
                    Session.Add("login", login);
                    return RedirectToAction("AnalystPortal", "SupportAnalyst");
                }
                else
                {
                    Session.Add("login", login);
                    return RedirectToAction("AdminPortal", "Admin");
                }

            }
            ViewBag.Error = "Invalid Username or Password";
            return View("Error");
        }
        public ActionResult Remedydetails(string id, string contact)
        {
            string userid = ((List<Logins>)Session["login"])[0].UserId;
            ViewBag.UserId = userid;
            //var login = (Logins)Session["login"];
            //var support = context.Users.Find(login.UserId);
            RemedyDetails rmd = new RemedyDetails();
            rmd.UserId = id;
            rmd.ContactNo = contact;
            return View(rmd);
        }
        [HttpPost]
        public ActionResult Remedydetails(RemedyDetails model)
        {
            string userid = ((List<Logins>)Session["login"])[0].UserId;
            ViewBag.UserId = userid;
            model.RemedyStatus = "new";
            context.Remedies.Add(model);
            int r = context.SaveChanges();
            if (r > 0)
            {
                ViewBag.Message = "Details Added successfully";
                Notification alert = new Notification();
                alert.CreationDate = DateTime.Now;
                alert.AlertText = "|=> Remedy Details submitted with generted ticket number "+ model.Remedyid.ToString();
                alert.Details = model.Description;
                alert.SenderId = userid;
                alert.ReceiverId = "2068462";
                alert.NotifDate = DateTime.Now.AddMinutes(1);
                context.Notifications.Add(alert);
                context.SaveChanges();
                return View("Message");
            }
            return View(model);
        }
        public ActionResult Forgot()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Forgot(Logins model)
        {

                Logins login = context.Logins.Where(x => x.UserId == model.UserId).Single();

                bool available = false;

                if (login.Secretquestion.Equals(model.Secretquestion) && login.Secretans.Equals(model.Secretans) && login.Mailid.Equals(model.Mailid)&& login.ContactNumber.Equals(model.ContactNumber))
                {
                    available = true;

                }

                if (available)
                {

                    return RedirectToAction("ResetPassword");
                }
                else
                {
                    ViewBag.Message = "Secret Answer or Mail id or Contact Number is wrong";
                    return View("Message");
                }

        }
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(Logins model)
        {
            try
            {
                Logins login = context.Logins.Where(x => x.UserId == model.UserId).Single();
                login.Password = model.Password;

                int r = context.SaveChanges();
                if (r > 0)
                {
                    ViewBag.Message = "Password Reset successfull";
                    return View("Message");
                }
                else
                {
                    ViewBag.Message = "Server Error occurred";
                    return View("Message");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Message");
            }
        }
        public ActionResult ForgotUserId()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotUserId(Logins model)
        {
            try
            {

                ICollection<Logins> loginList = context.Logins.Where(m => m.Secretquestion.Equals(model.Secretquestion)).ToArray();
                bool available = false;
                string id = null;
                foreach (var item in loginList)
                {
                    if (item.Secretans.Equals(model.Secretans))
                    {
                        if (item.ContactNumber.Equals(model.ContactNumber))
                        {
                            available = true;
                            id = item.UserId;
                        }
                    }

                }
                if (available)
                {
                    ViewBag.Message = "User Id is:" + id;
                    return View("Message");
                }
                else
                {

                    ViewBag.Message = "Incorrect Secret Answer or ContactNumber or Mailid";
                    return View("Message");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Message");
            }

        }
        public ActionResult ViewRemedyDetails()
        {
            List<RemedyDetails> rmdetails = context.Remedies.ToList();
            
            if (rmdetails.Count == 0)
            {
                ViewBag.Error = "No Vendor is Available";
                return View("Error");
            }
            else
            {
                int pages = 0;
                if (rmdetails.Count % 5 == 0)
                {
                    pages = (rmdetails.Count / 5);
                }
                else
                {
                    pages = (rmdetails.Count / 5) + 1;
                }
                Session.Add("Pages", pages);

                return View(rmdetails);
            }
        }
        public ActionResult ViewRemedyDetails1(int id)
        {
            List<RemedyDetails> rmdetails = context.Remedies.ToList();

            if (id == 1)
            {
                rmdetails = context.Remedies.ToList().Take(5).ToList();


            }
            else
            {
                rmdetails = context.Remedies.ToList().Take(5 * id).Skip((id - 1) * 5).ToList();
            }
            return View(rmdetails);
        }
            
            public ActionResult Notification()
             {
                 return View();
            }
        public ActionResult Search(string data,string sort)
        {
            ViewBag.UserId = String.IsNullOrEmpty(sort) ? "UserId" : "";
            ViewBag.Category = String.IsNullOrEmpty(sort) ? "Category" : "";
            ViewBag.Remedyid = String.IsNullOrEmpty(sort) ? "Remedyid" : "";
            var query = from x in context.Remedies select x;
           // var details = from y in context.Remedies select y;
           

            List<RemedyList> rmd = context.RemedyLists.Where(s=>s.AssignedUserId.Equals(data)).ToList();
            int sda;
            if (rmd.Count > 0)
            {
                var det = rmd[0].Remedyid;
                sda = det;
            }
            else
            {
                var det =2;
                sda = det;
            }
           
            if (!String.IsNullOrEmpty(data))
            {
                    query = query.Where(s => s.UserId.Contains(data) || s.ContactNo.Contains(data) || s.Category.Contains(data) || s.Remedyid.Equals(sda));
                    // det = det.Where(s => s.AssignedUserId.Contains(data));
                    //query = query.Where(s => s.Remedyid.Equals(det.Where(y => y.Remedyid)));
            }


            switch (sort)
            {
                    case "UserId":
                        query = query.OrderBy(s => s.UserId);
                        break;
                    case "Category":
                        query = query.OrderBy(s => s.Category);
                        break;
                default:
                    query = query.OrderByDescending(s => s.Remedyid);
                    break;
            }
           
            return View(query.ToList());
            
        }
        public ActionResult UserSearch(Logins model)
        {
            
            string userid = ((List<Logins>)Session["login"])[0].UserId;   
            List<RemedyDetails> rmd = context.Remedies.Where(s => s.UserId.Equals(userid)).ToList();
            return View(rmd);

        }
        public ActionResult UserFilter(string data)
        {
            string userid = ((List<Logins>)Session["login"])[0].UserId;
            var query = from x in context.Remedies
                        where x.UserId == userid
                        select x;
            // var details = from y in context.Remedies select y;


            List<RemedyList> rmd = context.RemedyLists.Where(s => s.AssignedUserId.Equals(data)).ToList();
            int sda;
            if (rmd.Count > 0)
            {
                var det = rmd[0].Remedyid;
                sda = det;
            }
            else
            {
                var det = 2;
                sda = det;
            }
           
            if (!String.IsNullOrEmpty(data))
            {

                query = query.Where(s => s.UserId.Contains(data) || s.ContactNo.Contains(data) || s.Category.Contains(data));
               
            }
            else
            {
                return RedirectToAction("UserSearch", "Remedy");
            }
        return View(query.ToList());
        }
        public ActionResult GenerateReportCSV()
        {
            List<RemedyDetails> rmdList = context.Remedies.ToList();
            StreamWriter fs1 = new StreamWriter(Server.MapPath(Url.Content("~/Content/pics/RemedyReport.CSV")));
            foreach (var rmd in rmdList)
            {
                string data = string.Format("{0},{1},{2},{3},{4},{5},{6}", rmd.Remedyid, rmd.UserId, rmd.Category, rmd.Description, rmd.ContactNo,rmd.IPAddress,rmd.PCNumber);
                fs1.WriteLine(data);
            }
            fs1.Close();
            return RedirectToAction("AdminPortal","Admin");
        }
        public ActionResult ChangeRemedyStatus1(int id)
        {
            RemedyList rList = context.RemedyLists.Where(l => l.Remedyid == id).ToList()[0];
            return View(rList);
        }
        [HttpPost]
        public ActionResult ChangeRemedyStatus1(RemedyList model)
        {
            RemedyList rList = context.RemedyLists.Where(l => l.Remedyid == model.Remedyid).ToList()[0];
            rList.Status = model.Status;
            rList.AssignedUserId = model.AssignedUserId;
            context.SaveChanges();
            RemedyDetails rmd = context.Remedies.Find(model.Remedyid);
            rmd.RemedyStatus = rList.Status;
            context.SaveChanges();
            //return Content(rmd.RemedyStatus);
            string userid = ((List<Logins>)Session["login"])[0].UserId;
            if (rmd.RemedyStatus == "Re-Open")
            {
                Notification alert = new Notification();
                alert.CreationDate = DateTime.Now;
                alert.AlertText = "=> The Remedy Issue is Reopened - " + rList.Remedyid;
                alert.Details = "SupportAnalyst/Admin is requested to check the issue once again";
                alert.SenderId = userid;
                alert.ReceiverId = rList.AssignedUserId;
                alert.NotifDate = DateTime.Now.AddDays(1);
                context.Notifications.Add(alert);
                context.SaveChanges();
            }
            if (rmd.RemedyStatus == "Closed")
            {
                Notification alert = new Notification();
                alert.CreationDate = DateTime.Now;
                alert.AlertText = "=> The Remedy Issue is closed by user, Provide Review/Feedback for - " + rList.Remedyid;
                alert.Details = "The Remedy issue is considered resolved and hence it's file is closed";
                alert.SenderId = userid;
                alert.ReceiverId = userid;
                alert.NotifDate = DateTime.Now.AddDays(1);
                context.Notifications.Add(alert);
                context.SaveChanges();
                return RedirectToAction("SubmitFeedback","Feedback", new {id=userid});
            }
            return RedirectToAction("RemedyDetails", "Remedy");

        }
        //public ActionResult GetRemedyDetails(int id)
        //{
        //    RemedyList rList = context.RemedyLists.Where(l => l.Remedyid == id).FirstOrDefault();
        //    RemedyDetails rmd = context.Remedies.Find(rList.Remedyid);
        //    return View(rmd);
        //}
        public PartialViewResult FlashAlert()
        {

            return PartialView();
        }
    }
}

