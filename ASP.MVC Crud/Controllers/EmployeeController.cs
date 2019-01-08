using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ASP.MVC_Crud.Models;
using System;
using System.Data.Entity;

namespace ASP.MVC_Crud.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (ModelDB db = new ModelDB())
            {
                List<ASPMVCEmployee> employeeList = db.ASPMVCEmployees.ToList<ASPMVCEmployee>();
                return Json(new { data = employeeList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new ASPMVCEmployee());
            else
            {
                using (ModelDB db = new ModelDB())
                {
                    return View(db.ASPMVCEmployees.Where(x => x.EmployeeID==id).FirstOrDefault<ASPMVCEmployee>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(ASPMVCEmployee employee)
        {
            using (ModelDB db = new ModelDB())
            {
                if (employee.EmployeeID == 0)
                {
                    db.ASPMVCEmployees.Add(employee);
                    db.SaveChanges();
                    return Json(new { succes = true, message = "Employee Added" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { succes = true, message = "Employee Updated" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (ModelDB db = new ModelDB())
            {
                ASPMVCEmployee employee = db.ASPMVCEmployees.Where(x => x.EmployeeID == id).FirstOrDefault<ASPMVCEmployee>();
                db.ASPMVCEmployees.Remove(employee);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}