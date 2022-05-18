using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemedyAcknowledgement.Models;

namespace RemedyAcknowledgement.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        RemedyContext context = new RemedyContext();
        public ActionResult CreateFeedBack()
        {
            string QuestionID = "Ques" + (new Random()).Next(100, 999);
            string AdminID = ((List<Logins>)Session["login"])[0].UserId;
            Session.Add("AdminID", AdminID);
            Session.Add("QuestionID", QuestionID);

            return View();
        }
        [HttpPost]
        public ActionResult CreateFeedBack(Questionnaire model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ViewBag.QList = context.Questionnaires.ToList();
            context.Questionnaires.Add(model);
            int r = context.SaveChanges();
            if (r > 0)
            {
                ViewBag.Message = "Feedback Question created Successfully.";
                return View("Message");
            }
            else
            {
                ViewBag.Error = "Server Error occurred";
                return View("Error");
            }

        }

        public ActionResult EditFeedBack()
        {

            return View(context.Questionnaires.ToList());
        }
        public ActionResult EditQuestions(string id)
        {

            return View(context.Questionnaires.Find(id));
        }
        [HttpPost]
        public ActionResult EditQuestions(Questionnaire model)
        {
            Questionnaire q = context.Questionnaires.Find(model.QuestionId);
            q.QuestionText = model.QuestionText;
            int r = context.SaveChanges();
            if (r > 0)
            {
                ViewBag.Message = "Feedback Question Edited Successfully.";
                return View("Message");
            }
            else
            {
                ViewBag.Error = "Server Error occurred";
                return View("Error");
            }
        }
        public ActionResult DeleteQuestions(String id)
        {
            context.Questionnaires.Remove(context.Questionnaires.Find(id));

            int r = context.SaveChanges();
            if (r > 0)
            {
                ViewBag.Message = "Feedback Question Deleted Successfully.";
                return View("Message");
            }
            else
            {
                ViewBag.Error = "Server Error occurred";
                return View("Error");
            }

        }

        public ActionResult Feedback()
        {
            List<Questionnaire> QnsList = context.Questionnaires.ToList();
            return View(QnsList);
        }
        public ActionResult FeedBackAnswerDetails()
        {

            return View(context.Feedbacks.ToList());
        }

        public ActionResult SubmitFeedback(string id)
        {
            List<Questionnaire> qns = context.Questionnaires.ToList();
            Feedback fb = new Feedback();
            fb.FeedbackId = "Feed" + (new Random()).Next(10000, 99999);
            fb.UserId = id;
            ViewBag.FeedbackId = fb.FeedbackId;
            ViewBag.UserId = id;
            ViewBag.List = qns;
            return View();
        }
        [HttpPost]
        public ActionResult SubmitFeedback(FormCollection frm)
        {
            Feedback fb = null;
            List<Questionnaire> qns = context.Questionnaires.ToList();
            for (int i = 0; i < qns.Count; i++)
            {
                fb = new Feedback();
                fb.FeedbackId = frm["FeedbackId"];
                fb.UserId = frm["UserId"];
                fb.QuestionId = qns[i].QuestionId;
                fb.Answer = frm["A " + qns[i].QuestionId];
                context.Feedbacks.Add(fb);
                context.SaveChanges();
            }
            ViewBag.Message = "Feedback submited successfully.";

            return View("Message");
        }
    }
}
