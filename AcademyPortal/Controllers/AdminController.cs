using AcademyPortalBoLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace AcademyPortal.Controllers
{
    public class AdminController : Controller
    {
        private AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
        
        //For Fetching all Record
        public ActionResult Index()
        {
            return View(adminLogic.GetAdminDetails());
        }

        //Admin Dashboard 
        public ActionResult Dashboard()
        {
            return View();
        }

        //Register Admin 
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Admin admin)
        {
           
                bool status = false;
                if (ModelState.IsValid)
                {
                    status = adminLogic.RegisterAdmin(admin);
                }

                if (status)
                {
                        TempData["Msg"] = "Your details are submitted successfully  <br/> Your User Id Generated is :" + admin.UserId;
                        return RedirectToAction("Login");
                }               
                else
                {
                    return View();
                }

         
        }

        
       //Login Admin
        [HttpGet]
        public ActionResult Login()
        {
            try { return View(); }
            catch {
                TempData["exception"] = "true";
                return View();
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin admin,string returnUrl)
        {
            try
            {   
                bool status = adminLogic.Login(admin);
                if (status)
                {
                    FormsAuthentication.SetAuthCookie(admin.UserId.ToString(), false);
                    FormsAuthentication.RedirectFromLoginPage(admin.UserId.ToString(), false);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Dashboard");
                    }
                }
                else
                {
                    TempData["status"] = "false";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception)
            {
                TempData["exception"] = "true";
                return View();
            }
        }

        //Logout Admin
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(adminLogic.GetAdminDetailsById(id));
        }
        [HttpPost]
        public ActionResult Delete(int id, Admin admin)
        {
            try
            {
                bool status = adminLogic.DeleteAdmin(id, admin);
                if (status)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View("Index", "Admin");
            }
        }

        //For Checking Faculty Request
        public ActionResult FacultyRequest()
        {
            AcademyPortalBlLayer.FacultyLogic facultyLogic = new AcademyPortalBlLayer.FacultyLogic();
            var facultyList = facultyLogic.GetFacultyStatus();
            if(facultyList.Count()>0)
            {
                return View(facultyList);
            }
            else
            {
                TempData["status"] = "false";
                return View();
            }
            
        }

        // For Approving And Reject Request
        public ActionResult ApproveOrRejectFaculty(int id,int signal)
        {
            AcademyPortalBlLayer.FacultyLogic facultyLogic = new AcademyPortalBlLayer.FacultyLogic();
            var faculty = facultyLogic.GetFacultyDetailsById(id);
            if (signal == 1)
            {
                string status = "Approved";
                bool delete = false;
                if (faculty != null)
                {
                    bool value=facultyLogic.EditFacultyDetails(faculty, status,delete);
                }
                return RedirectToAction("FacultyRequest", "Admin");
            }
            else
            {
                string status = "Rejected";
                bool delete = true;
                if(faculty!=null)
                {
                    bool value = facultyLogic.EditFacultyDetails(faculty, status, delete);
                }
                return RedirectToAction("FacultyRequest", "Admin");
            }
           
        }

        //For Checking Employee Request
        public ActionResult EmployeeRequest()
        {
            AcademyPortalBlLayer.EmployeeLogic empLogic = new AcademyPortalBlLayer.EmployeeLogic();
            var empList = empLogic.GetEmployeeStatus();
            if (empList.Count()>0)
            {
                return View(empList);
            }
            else
            {
                TempData["status"] = "false";
                return View();
            }
        }

        // For Approving And Reject Request
        public ActionResult ApproveOrRejectEmployee(int id, int signal)
        {
            AcademyPortalBlLayer.EmployeeLogic empLogic = new AcademyPortalBlLayer.EmployeeLogic();
            var employee = empLogic.GetEmployeeDetailsById(id);
            if (signal == 1)
            {
                string status = "Approved";
                bool delete = false;
                if (employee != null)
                {
                    bool value = empLogic.EditEmployeeDetails(employee, status, delete);
                }
                return RedirectToAction("EmployeeRequest", "Admin");
            }
            else
            {
                string status = "Rejected";
                bool delete = true;
                if (employee != null)
                {
                    bool value = empLogic.EditEmployeeDetails(employee, status, delete);
                }
                return RedirectToAction("EmployeeRequest", "Admin");
            }

        }

        //For Skill Registration
        [HttpGet]
        public ActionResult SkillRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SkillRegistration(Skills skills)
        {
            try
            {
                bool status = false;
                if (ModelState.IsValid)
                {
                   status= adminLogic.RegisterSkill(skills);
                }
                if (status)
                {
                    TempData["Msg"] = "Skill Registered successfully";
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch(Exception)
            {
                return View();
            }
            
        }

        // Skill Mapping started here  
        [HttpGet]
        public ActionResult SkillMapping()
        {
            var items=adminLogic.GetSkills();
            var module = adminLogic.GetModules();

            var listSkills = new List<string>();
            var listModule = new List<string>(); 
            foreach(var i in items)
            {
                if(i.SkillName!=null)
                {
                    listSkills.Add(i.SkillName);
                }
            }
            foreach (var i in module)
            {
                if (i.certificationName != null)
                {
                    listModule.Add(i.certificationName);
                }
            }
            if (listSkills != null && listModule!=null)
            {
                ViewBag.data = listSkills;
                ViewBag.modules = listModule;
            }
            return View();


        }
        [HttpPost]
        public ActionResult SkillMapping(SkillMapping skill)
        {
            var skilldata = adminLogic.GetSkillsBySkillName(skill.SkillName);
            var skillid = skilldata.Skill_ID;
            var moduledata = adminLogic.GetModulesByModuleName(skill.ModuleName);
            var moduleid = moduledata.ModuleId;

            Mapping map = new Mapping();
            map.SkillId = skillid;
            map.ModuleId = moduleid;
            bool status = adminLogic.MapSkill(map);
            if (status)
            {
                TempData["Msg"] = "Skill/Module mapped successfully";
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }
  
        }

        //for Module Registration
        [HttpGet]
        public ActionResult ModuleRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ModuleRegistration( Module module)
        {
            try
            {
                bool status = false;
                if (ModelState.IsValid)
                {
                    status = adminLogic.RegisterModule(module);
                }
                if (status)
                {
                    TempData["Msg"] = "Module Registered successfully";
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        //for Batch Registration
        [HttpGet]
        public ActionResult BatchRegistration()
        {

            return View();
        }
        [HttpPost]
        public ActionResult BatchRegistration(Batch batch)
        {

            try
            {
                bool status = false;
                if (ModelState.IsValid)
                {
                    status = adminLogic.RegisterBatch(batch);
                }
                if (status)
                {
                    TempData["Msg"] = "Batch Registered successfully";
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Somrthing Went Wrong! Please Try After Sometime";
                return View();
            }
        }


        //for Help Request
        public ActionResult HelpRequest()
        {
            var request = adminLogic.GetHelpRequest();
            if (request.Count() > 0)
            {
                return View(request);
            }
            else
            {
                TempData["status"] = "false";
                return View();
            }
        }


        [HttpGet]
        public ActionResult RequestDetails(int id)
        {
            return View(adminLogic.GetHelpRequestById(id));
        }
        [HttpPost]
        public ActionResult RequestDetails(int id,Help help1)
        {
            var help = adminLogic.GetHelpRequestById(id);
            help.Status="Resolved";
            help.Resolution = help1.Resolution;
            var status=adminLogic.UpdateRequest(help);
            if(status)
            {
                TempData["Msg"] = "Request Resolved";
                return RedirectToAction("HelpRequest");
            }
            else
            {
                TempData["Msg"] = "Something Went Wrong! Please try Again After some time";
                return View();
            }
        }
        public ActionResult BatchNomination()
        {
            var request = adminLogic.GetPendingEmployeeNomination();
            if (request.Count() > 0)
            {
                return View(request);
            }
            else
            {
                TempData["status"] = "false";
                return View();
            }
        }
        public ActionResult ApproveOrRejectNomination(int id, int signal)
        {
            AcademyPortalBlLayer.EmployeeLogic empLogic = new AcademyPortalBlLayer.EmployeeLogic();
            if (signal == 1)
            {
                string nominationStatus = "Accepted";
                int? batchId = id;
                if (batchId!=null)
                {
                    bool value = empLogic.UpdateEmployee(batchId, nominationStatus);
                }
                return RedirectToAction("BatchNomination", "Admin");
            }
            else
            {
                string nominationStatus = "Rejected";
                int? batchId = null;
                    bool value = empLogic.UpdateEmployee(batchId, nominationStatus);
                TempData["Msg"] = "Request Rejected";
                return RedirectToAction("BatchNomination", "Admin");
            }

        }
        //Finding UserId using specific values provided
        public ActionResult FindUserId()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FindUserId([Bind(Include = "SecurityQuestion,SecurityQueAnswer,Contact,Email")] Admin adminModel)
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
            var userId = adminLogic.GetFacultyId(adminModel);
            if (userId.ToString().Length == 6)
            {
                TempData["status"] = "success";
                TempData["UserId"] = userId;
                return View();
            }
            else
            {
                TempData["status"] = "failed";
                return View();
            }

        }

        //Verify User Id and sequerity questions to reset password
        public ActionResult VerifyDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VerifyDetails([Bind(Include = "UserId,SecurityQuestion,SecurityQueAnswer")] Admin adminModel)
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
            var status = adminLogic.VerifyPasswordRecoveryDetails(adminModel);
            if (status != null)
            {
                TempData["status"] = "true";
                TempData["module"] = status;
                return View();
            }
            else
            {
                TempData["status"] = "false";
                return View();
            }
        }

        //Reset Password
        public ActionResult ResetPassword(string password)
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
            Admin adminModel = (Admin)TempData["module"];
            var status = adminLogic.GetPasswordReset(password, adminModel);
            if (status)
            {
                TempData["msg"] = "success";
            }
            else
            {
                TempData["msg"] = "failed";
            }
            return RedirectToAction("Login", "Admin");
        }

    }
}