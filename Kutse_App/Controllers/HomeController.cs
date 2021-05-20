using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Kutse_App.Models;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string month = DateTime.Now.ToString("MMMM");

            ViewBag.Message = month == "Март" ? "Ootan sind minu peole! Palun tule!!! " + "С 8 Марта, милые дамы!" :
                month == "Апрель" ? "Ootan sind minu peole! Palun tule!!!" + "Апрель принёс нам радость и веселье": 
                "Ootan sind minu peole! Palun tule!!!";
            int hour = DateTime.Now.Hour;
            

            ViewBag.Greeting = 
                hour < 10 & hour > 6 ? "Tere hommikust!" :
                hour < 17 & hour > 10 ? "Tere päevast!" : 
                hour < 23 & hour > 17 ? "Head õhtut!" : 
                "Head ööd";
            return View();
        }
        

        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }
        
        GuestContext db = new GuestContext();
        [Authorize]
        public ActionResult Guests()
        {
            IEnumerable<Guest> guests = db.Guests;
            return View(guests);
        }
        
        
        
        HolidayContext dbHoli = new HolidayContext();
        [Authorize]
        public ActionResult Holidays()
        {
            IEnumerable<Holidays> holidayses = dbHoli.Holidayses;
            return View(holidayses);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }

            return View(g);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }

            db.Guests.Remove(g);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }

            return View(g);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Guest guest)
        {
            db.Entry(guest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Guests");
        }

        [HttpGet]
        public ActionResult Accept()
        {
            IEnumerable<Guest> guests = db.Guests.Where(g => g.WillAttend == true);
            return View(guests);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }

            return View(g);
        }

        [HttpGet]
        public ActionResult CreateHoliday()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateHoliday(Holidays holidays)
        {
            dbHoli.Holidayses.Add(holidays);
            dbHoli.SaveChanges();
            return RedirectToAction("Holidays");
        }

        [HttpGet]
        public ActionResult DeleteHoliday(int id)
        {
            Holidays h = dbHoli.Holidayses.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }

            return View(h);
        }

        [HttpPost, ActionName("DeleteHoliday")]
        public ActionResult DeleteHolidayConfirmed(int id)
        {
            Holidays h = dbHoli.Holidayses.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }

            dbHoli.Holidayses.Remove(h);
            dbHoli.SaveChanges();
            return RedirectToAction("Holidays");
        }

        [HttpGet]
        public ActionResult EditHoliday(int? id)
        {
            Holidays h = dbHoli.Holidayses.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }

            return View(h);
        }

        [HttpPost, ActionName("EditHoliday")]
        public ActionResult EditHolidayConfirmed(Holidays holidays)
        {
            dbHoli.Entry(holidays).State = EntityState.Modified;
            dbHoli.SaveChanges();
            return RedirectToAction("Holidays");
        }
        
        [HttpGet]
        public ActionResult DetailsHoliday(int id)
        {
            Holidays h = dbHoli.Holidayses.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }

            return View(h);
        }
        
        [HttpGet]
        public ViewResult Meeldetuletus(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "e-mail";
                WebMail.Password = "pass";
                WebMail.From = "e-mail";

                WebMail.Send("e-mail", "Meeldetuletus",  guest.Name + ", ara unusta. Pidu toimub 12.03.20! Sind ootavad väga!",
                    "e-mail", "e-mail",
                    filesToAttach: new String[] { Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName("ran.jpeg")) }
                );
                
                ViewBag.Message = "saatnud";
            }
            catch (Exception e)
            {
                ViewBag.Message = e;
            }
            return View();
        }

        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            if (ModelState.IsValid)
            {
                db.Guests.Add(guest);
                db.SaveChanges();
                return View("Thanks", guest);
            }
            else
            {
                return View();
            }
        }

        public void E_mail(Guest guest)
        {
            
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "e-mail";
                WebMail.Password = "pass";
                WebMail.From = "e-mail";
                WebMail.Send(guest.Email, "Vastus kutsele", guest.Name + " vastus " + ((guest.WillAttend ?? false) ?
                    "tuleb peole ": "ei tule peole"));

                ViewBag.Message = "kiri on saatnud";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahjuks! Ei saa kirja saada";
            }
        }
    }
}