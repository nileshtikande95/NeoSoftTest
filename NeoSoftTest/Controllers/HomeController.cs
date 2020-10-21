using NeoSoftTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NeoSoftTest.Controllers
{
    public class HomeController : Controller
    {
        NeoSoftDBEntities db = new NeoSoftDBEntities();
        // GET: Home
        SqlHelper helper = new SqlHelper();
        public ActionResult Index()
        {
    
            return View();
        }

        public ActionResult DisplayList()
        {
           var result= helper.GetList().ToList();
           // return View(result);
           return Json(new { data = result },JsonRequestBehavior.AllowGet);
        }

        public List<tblCountry> GetCountryList()
        {
           var countris =db.tblCountries.ToList();
            return countris;
        }

     
        [HttpPost]
        public ActionResult GetStateList(int CountryId)
        {
           List<tblState> Slist =db.tblStates.Where(x => x.CountryId == CountryId).ToList();
            ViewBag.StateList = new SelectList(Slist, "Row_Id", "StateName");
            return PartialView("DisplayState");
        }

        public ActionResult GetCityList(int StateId)
        {
            List<tblCity> Clist = db.tblCities.Where(x => x.StateId == StateId).ToList();
            ViewBag.CityList = new SelectList(Clist, "Row_Id", "CityName");
            return PartialView("DisplayCity");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CountryList = new SelectList(GetCountryList(), "Row_Id", "CountryName");
            return View();
        }
        [HttpPost]
        [HandleError]
        public ActionResult Create(HttpPostedFileBase file,tblNeoData detail)
        {
            
                string filename = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                string path =Path.Combine( Server.MapPath("~/Image"), filename);
                detail.ProfileImage = "~/Image/" + filename;
                if(extension.ToLower()==".jpg"|| extension.ToLower() == ".jpeg"||extension.ToLower() == ".png"){

                    if (file.ContentLength <= 1000000)
                    {
                    file.SaveAs(path);
             
                    helper.InsertData(detail);

                    }
            }
             
            return View();

        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNeoData detail = db.tblNeoDatas.Find(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblNeoData data)
        {
            if (ModelState.IsValid)
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DisplayList");
            }
            return View(data);
        }

    }
}