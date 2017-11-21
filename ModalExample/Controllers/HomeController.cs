using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModalExample.Model;
using System.Web.Services;
using System.Web.Script.Services;
using ModalExample.Models;

namespace ModalExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IndexVM model = new IndexVM();
            model.vols = Volunteers.vols;
            //Volunteer vol1 = new Volunteer();
            DateTime time;
            

            return View(model);
        }


        [HttpPost]
        public void TimePost(IndexVM vm)
        {

        }

        [HttpGet]
        public ActionResult GetPartial(int id)
        {
            //Volunteers vols = new Volunteers();
            Volunteer volunteer = Volunteers.vols.ElementAt(id);
            return PartialView("_ModalPartial", volunteer);
        }


        [HttpPost]
        public ActionResult PostVolunteer(Volunteer vol)
        {
            //insert breakpoint here

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}