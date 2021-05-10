using AcademyPortalBoLayer;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace AcademyPortal.Controllers
{
    public class FacultyController : Controller
    {
        private AcademyPortalBlLayer.FacultyLogic facultyLogic = new AcademyPortalBlLayer.FacultyLogic();
        
        //For fetching All Records
        public ActionResult Index()
        {

            return View(facultyLogic.GetFacultyDetails());
        }

        //For Registering Faculty
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Faculty faculty)
        {

            var status = facultyLogic.SaveFacultyDetails(faculty);

            if (status)
            {
                TempData["Msg"] = "Your details are submitted successfully  <br/> Your User Id Generated is :" + faculty.UserId + "<br/> Please Wait For Request Approval <br/> Thank You";
                return RedirectToAction("HomePage");
            }
            else
            {
                return View();
            }


        }
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult DeletedRecord()
        {
            var record = facultyLogic.GetDeletedRecord();
            return View(record);
        }

        //For login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserId,Password")] Faculty facultyModel,string returnUrl)
        {
            try
            {

                var status = facultyLogic.LoginCredentials(facultyModel);
                var data = facultyLogic.GetFacultyDetailsById(facultyModel.UserId);
                if (status)
                {
                    FormsAuthentication.SetAuthCookie(facultyModel.UserId.ToString(), false);
                    FormsAuthentication.RedirectFromLoginPage(facultyModel.UserId.ToString(), false);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (data.RegistrationStatus == "Approved")
                        {
                            TempData["Msg"] = "Welcome to Faculty Deshboard <br/> Your User id is : " + facultyModel.UserId;
                            TempData["userId"] = facultyModel.UserId;
                            TempData["userCategory"] = facultyModel.UserCatagory;
                            return RedirectToAction("Index");
                        }
                        else if (data.RegistrationStatus == "Rejected")
                        {
                            TempData["Msg"] = "Registration Rejected";
                            return RedirectToAction("Login");
                        }
                        else if (data.RegistrationStatus == "Pending")
                        {
                            TempData["Msg"] = "Hello " + data.FirstName + ", <br/> Registration awaiting approval";
                            return RedirectToAction("HomePage");
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch
            {
                return View();
            }
        }

        //For Deleting Record
        public ActionResult Delete(int id)
        {
            var facultyModel = facultyLogic.GetFacultyDetailsById(id);
            return View(facultyModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, Faculty facultyModel)
        {
            try
            {
                var status = facultyLogic.DeleteFaculty(id, facultyModel);
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
                return View("Index", "Faculty");
            }
        }

        //For Checking Batch Registration
        public ActionResult BatchRegistration()
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
            var data = adminLogic.GetFacultyBatch(Convert.ToInt32(TempData["userId"]));
            if (data.Count() > 0)
            {
                return View(data);
            }
            else
            {
                TempData["status"] = "false";
                return View();
            }
        }

        //For Approving or Rejecting Batch Request
        public ActionResult ApproveOrRejectBatch(int id, int signal)
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
            var batch = adminLogic.GetBatchById(id); 
            var faculty = facultyLogic.GetFacultyDetailsById(batch.FacultyId);
            if (signal == 1)
            {
                string nominationStatus = "Accepted";
                int? batchId = id;
                if (faculty != null && batch != null)
                {
                    bool value = facultyLogic.EditFacultyNominationStatus(faculty, nominationStatus, batchId, batch);
                }
                return RedirectToAction("BatchRegistration", "Faculty");
            }
            else
            {
                string nominationStatus = "Rejected";
                int? batchId = null;
                if (faculty != null)
                {
                    bool value = facultyLogic.EditFacultyNominationStatus(faculty, nominationStatus, batchId, batch);
                }
                return RedirectToAction("BatchRegistration", "Faculty");
            }

        }

        public ActionResult Help()
        {
            return View();
        }

        //For Raising a Ticket By Faculty
        [HttpGet]
        public ActionResult HelpFaculty()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HelpFaculty(Help help)
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();

            help.RequestId = Convert.ToInt32(facultyLogic.GenerateNewRandom());
            help.DateOfTicket = DateTime.Now;
            help.userId = Convert.ToInt32(TempData["userId"]);
            help.userCategory = Convert.ToString(TempData["userCategory"]);
            help.Status = "Pending";
            TempData["requestId"] = help.RequestId;
            var status=adminLogic.RaiseTicket(help);
            if (status)
            {
                TempData["Msg"] = "Your Ticket has been Raised <br/> your Request Id is" + help.RequestId + "<br/> Response Will be sent to you within 24 Hours <br/> if didn't Receive any Response Within 24 Hours <br/> Please Dial our Toll Free number 1800576576 <br/> Thank you";
                return RedirectToAction("Help", "Faculty");
            }
            else
            {
                TempData["Msg"] = "Something Went Wrong ! Please try after sometime";
                return View();
            }
        }

        public ActionResult TicketStatus()
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
            var userId = Convert.ToInt32(TempData["userId"]);
            var data = adminLogic.GetHelpRequestByUserId(userId);
            if (data.Count() > 0)
            {
                return View(data);
            }
            else
            {
                TempData["status"] = "false";
                return View();
            }
        }

        //Finding UserId using specific values provided
        public ActionResult FindUserId()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FindUserId([Bind(Include = "SecurityQuestion,SecurityQueAnswer,Contact,Email")] Faculty facultyModel)
        {
            AcademyPortalBlLayer.FacultyLogic facultyLogic = new AcademyPortalBlLayer.FacultyLogic();
            var userId = facultyLogic.GetFacultyId(facultyModel);
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
        public ActionResult VerifyDetails([Bind(Include = "UserId,SecurityQuestion,SecurityQueAnswer")] Faculty facultyModel)
        {
            AcademyPortalBlLayer.FacultyLogic facultyLogic = new AcademyPortalBlLayer.FacultyLogic();
            var status = facultyLogic.VerifyPasswordRecoveryDetails(facultyModel);
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
            AcademyPortalBlLayer.FacultyLogic facultyLogic = new AcademyPortalBlLayer.FacultyLogic();
            Faculty facultyModel = (Faculty)TempData["module"];
            var status = facultyLogic.GetPasswordReset(password, facultyModel);
            if (status)
            {
                TempData["msg"] = "success";
            }
            else
            {
                TempData["msg"] = "failed";
            }
            return RedirectToAction("Login", "Faculty");
        }
    }
}