using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemedyAcknowledgement.Models;

namespace RemedyAcknowledgement.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserPortal()
        {
            return View();
        }
    }
   
}