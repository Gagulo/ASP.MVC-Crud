using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ASP.MVC_Crud.Models;

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
            return View(new ASPMVCEmployee());
        }

        [HttpPost]
        public ActionResult AddOrEdit(ASPMVCEmployee employee)
        {
            using (ModelDB db = new ModelDB())
            {
                db.ASPMVCEmployees.Add(employee);
                db.SaveChanges();
                return Json(new { succes = true, message = "Employee Added" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}