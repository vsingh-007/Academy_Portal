using AcademyPortalBoLayer;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace AcademyPortal.Controllers
{
    public class EmployeeController : Controller
    {

        AcademyPortalBlLayer.EmployeeLogic empLogic = new AcademyPortalBlLayer.EmployeeLogic();
        
        //For Fetching All Records
        public ActionResult Index()
        {
            var emp = empLogic.GetEmployeeDetails();
            return View(emp);
        }

        // for Registration
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Employee employee)
        {

             var status = empLogic.RegisterEmployee(employee);
            if (status)
            {
                TempData["Msg"] = "Your details are submitted successfully  <br/> Your User Id Generated is :" + employee.UserId + "<br/> Please Wait For Request Approval <br/> Thank You";
                return RedirectToAction("HomePage");
            }
            else
            {
                return View();
            }
         
        }

        // for Login Employee
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Employee employee,string returnUrl)
        {
            try
            {
                bool status = empLogic.Login(employee);
                var data = empLogic.GetEmployeeDetailsById(employee.UserId);
                if (status)
                {
                    FormsAuthentication.SetAuthCookie(employee.UserId.ToString(), false);
                    FormsAuthentication.RedirectFromLoginPage(employee.UserId.ToString(), false);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (data.RegistrationStatus == "Approved")
                        {
                            TempData["Msg"] = "Welcome to Employee Deshboard <br/> Your User id is : " + employee.UserId;
                            TempData["userId"] = employee.UserId;
                            TempData["userCategory"] = employee.userCategory;
                            return RedirectToAction("Index");
                        }
                        else if (data.RegistrationStatus == "Rejected")
                        {
                            TempData["Msg"] = "Request rejected ! Contact Admin for more information.";
                            return RedirectToAction("Login");
                        }
                        else if (data.RegistrationStatus == "Pending")
                        {
                            TempData["Msg"] = "Hello " + data.First_name + ", <br/> Your Request is still pending <br/> Please Wait For Request Approval <br/> Thank You";
                            return RedirectToAction("HomePage");
                        }
                        else
                        {
                            TempData["status"] = "false";
                            return View();
                        }
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
                TempData["status"] = "false";
                return View();
            }
        }

        // for Deleting Employee
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(empLogic.GetEmployeeDetailsById(id));
        }
        [HttpPost]
        public ActionResult Delete(int id,Employee employee)
        {
            try
            {
                bool status = empLogic.DeleteEmployee(id,employee);
                if (status)
                {
                    TempData["deleteStatus"] = "true";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["deleteStatus"] = "false";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["deleteStatus"] = "false";
                return RedirectToAction("Index", "Employee");
            }
        }
        public ActionResult HomePage()
        {
            return View();
        }

        //For Raise a Ticket
        public ActionResult Help()
        {
            return View();
        }

        //For Help View
        [HttpGet]
        public ActionResult HelpEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HelpEmployee(Help help)
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();

            help.RequestId = Convert.ToInt32(empLogic.GenerateNewRandom());
            help.DateOfTicket = DateTime.Now;
            help.userId = Convert.ToInt32(TempData["userId"]);
            help.userCategory = Convert.ToString(TempData["userCategory"]);
            help.Status = "Pending";
            //TempData["requestId"] = help.RequestId;
            var status = adminLogic.RaiseTicket(help);
            if (status)
            {
                TempData["Msg"] = "Your Ticket has been Raised <br/> your Request Id is" + help.RequestId + "<br/> Response Will be sent to you within 24 Hours <br/> if didn't Receive any Response Within 24 Hours <br/> Please Dial our Toll Free number 1800576576 <br/> Thank you";
                return RedirectToAction("Help", "Employee");
            }
            else
            {
                TempData["Msg"] = "Something Went Wrong ! Please try after sometime";
                return View();
            }
        }

        //For Checking Ticket Status
        public ActionResult TicketStatus()
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
            var userId = Convert.ToInt32(TempData["userId"]);
            //var requestId = Convert.ToInt64(TempData["requestId"]);
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

        //For Checking  all Batch Details
        [HttpGet]
        public ActionResult AllBatchDetails()
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
            var batch = adminLogic.GetAllBatchDetails();
            return View(batch);
        }
        [HttpPost]
        public ActionResult AllBatchDetails(string Search_Data, string SearchCriteria)
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
            if (Search_Data != "" && SearchCriteria != "")
            {
                int searchData;
                Int32.TryParse(Search_Data, out searchData);
                return View(adminLogic.SearchBatch(searchData, SearchCriteria));
            }
            else
            {
                return View(adminLogic.GetAllBatchDetails());
            }
        }

        //For Sorting Records
        public ActionResult Sorting(string sortOrder, string CurrentSort)
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "BatchId" : sortOrder;
            return View("AllBatchDetails", adminLogic.SortBy(sortOrder, CurrentSort));
        }

        //For Fetching Batch Details
        [HttpGet]
        public ActionResult BatchDetails(int id)
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
            return View(adminLogic.GetBatchById(id));
        }
        [HttpPost]
        public ActionResult BatchDetails(int id, Batch batchModel)
        {
            AcademyPortalBlLayer.AdminLogic adminLogic = new AcademyPortalBlLayer.AdminLogic();
            var userId=0;
            Int32.TryParse(TempData["userId"].ToString(),out userId);
            var batch = adminLogic.GetBatchById(id);

            var status = adminLogic.UpdateBatch(batch, userId);
            if (status == "Exiest")
            {
                TempData["Msg"] = "User is Already Sent a Registration Request for this course";
                return RedirectToAction("ELearning", "Employee");
            }
            else if (status == "Success")
            {
                TempData["Msg"] = "Nomination Sent Successfully";
                return RedirectToAction("ELearning", "Employee");
            }
            else if (status == "Failed")
            {
                TempData["Msg"] = "Batch Full, please try next time";
                return RedirectToAction("ELearning", "Employee");
            }
            else
            {
                TempData["Msg"] = "Something went Wrong!, please try after some time";
                return RedirectToAction("ELearning", "Employee");
            }
        }
        public ActionResult ELearning()
        {
            return View();
        }

        //Finding UserId using specific values provided
        public ActionResult FindUserId()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FindUserId([Bind(Include = "SecurityQuestion,SecurityQueAnswer,Contact,Email")] Employee empModel)
        {
            AcademyPortalBlLayer.EmployeeLogic empLogic = new AcademyPortalBlLayer.EmployeeLogic();
            var userId = empLogic.GetFacultyId(empModel);
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
        public ActionResult VerifyDetails([Bind(Include = "UserId,SecurityQuestion,SecurityQueAnswer")] Employee empModel)
        {
            AcademyPortalBlLayer.EmployeeLogic empLogic = new AcademyPortalBlLayer.EmployeeLogic();
            var status = empLogic.VerifyPasswordRecoveryDetails(empModel);
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
            AcademyPortalBlLayer.EmployeeLogic empLogic = new AcademyPortalBlLayer.EmployeeLogic();
            Employee empModel = (Employee)TempData["module"];
            var status = empLogic.GetPasswordReset(password, empModel);
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