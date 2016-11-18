using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _Zadatko.Models;
using Microsoft.AspNet.Identity;


namespace _Zadatko.Controllers
{
    public class ZadaciController : Controller
    {
        private ZadaciContext db = new ZadaciContext();

        // GET: Zadaci
        [Authorize]
        public ActionResult Index(string searchBy, string search, int? page)
        {
           

            /*
             var zadaci = from s in db.Zadacis
                           select s;
                */
        

            if (searchBy == null)
            {
                return View(db.Zadacis.ToList());
            }
            else
            {
               
                

                switch (searchBy)
                {
                    case "Svi":
                        return View(db.Zadacis);

                    case "Utijeku":
                        //zadaci u tijeku su oni zadaci ciji je progress manji od 100%, a rok obavljavanja je u buducnosti negdje
                        return View(db.Zadacis.Where(s => s.PostotnaRjesenostZadatka < 100).Where(s => s.RokObavljanja > DateTime.Now));

                    case "Obavljene":
                        //obavljeni zadaci su oni ciji je progress 100%
                        return View(db.Zadacis.Where(s => s.PostotnaRjesenostZadatka == 100));

                    case "Istekli":
                        //istekli zadaci su oni ciji je progress manji od 100%, a rok obavljanja je negdje u proslosti
                        return View(db.Zadacis.Where(s => s.PostotnaRjesenostZadatka < 100).Where(s => s.RokObavljanja < DateTime.Now));

                    case "Kljucne":
                        //ako se odabere pretraga po kljucnim rijecima,onda gledam sadrzi li tekst objave tu rijec
                        //ako ne , onda provjeravam sadrzi li naslov,ako ne ,onda se vraca prazan model
                        if (db.Zadacis.Where(x => x.Naziv.Contains(search)).ToList().Count() >= 1)
                        {
                            return View(db.Zadacis.Where(x => x.Naziv.Contains(search) || search == null).ToList());
                        }
                        else if (db.Zadacis.Where(x => x.KratkiOpis.Contains(search)).ToList().Count() >= 1)
                        {
                            return View(db.Zadacis.Where(x => x.KratkiOpis.Contains(search) || search == null));
                        }
                        else
                        {
                            List<Zadaci> prazniModel = new List<Zadaci>();
                            return View(prazniModel);
                        }
                        

                    default:
                        //po defuatlu inace vracam sve zadatke
                        return View(db.Zadacis.ToList());

                }
            }


        }

        // GET: Zadaci/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zadaci zadaci = db.Zadacis.Find(id);
            if (zadaci == null)
            {
                return HttpNotFound();
            }
            return View(zadaci);
        }

        // GET: Zadaci/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zadaci/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Tezina,Vaznost,KratkiOpis,RokObavljanja,PostotnaRjesenostZadatka,KorisnikID")] Zadaci zadaci)
        {
            
            zadaci.KorisnikID= User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Zadacis.Add(zadaci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zadaci);

          
        }


        // GET: Zadaci/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zadaci zadaci = db.Zadacis.Find(id);
            if (zadaci == null)
            {
                return HttpNotFound();
            }
            return View(zadaci);
        }

        // POST: Zadaci/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Tezina,Vaznost,KratkiOpis,RokObavljanja,PostotnaRjesenostZadatka,KorisnikID")] Zadaci zadaci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zadaci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(zadaci);
        }

        // GET: Zadaci/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zadaci zadaci = db.Zadacis.Find(id);
            if (zadaci == null)
            {
                return HttpNotFound();
            }
            return View(zadaci);
        }

        // POST: Zadaci/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zadaci zadaci = db.Zadacis.Find(id);
            db.Zadacis.Remove(zadaci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
