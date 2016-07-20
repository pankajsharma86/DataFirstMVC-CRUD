using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseFirst.Models;
using System.Net;

namespace DatabaseFirst.Controllers
{
    public class Student2Controller : Controller
    {
        // GET: Student2
        public ActionResult Index()
        {
            using (var db = new CrudEntities())
                return View(db.Students.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ViewModel.Student student)
        {
            if (ModelState.IsValid)
            {
                using (var db = new CrudEntities())
                {

                    var st = new Student()
                    {
                        FirstMiddleName = student.FirstName,
                        LastName = student.LastName,
                        EnrollmentDate = DateTime.Now,
                    };
                    db.Students.Add(st);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Students");
                }
            }
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new CrudEntities())
            {
                DatabaseFirst.Models.Student student = db.Students.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new CrudEntities())
            {
                DatabaseFirst.Models.Student student = db.Students.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                using (var db = new CrudEntities())
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(student);
         }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new CrudEntities())
            {
                DatabaseFirst.Models.Student student = db.Students.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }
         }
        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new CrudEntities())
            {
                DatabaseFirst.Models.Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                using (var db = new CrudEntities())
                { db.Dispose(); }
            }
            base.Dispose(disposing);
        }
    }
}