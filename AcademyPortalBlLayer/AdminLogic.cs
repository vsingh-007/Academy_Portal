using AcademyPortalBoLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace AcademyPortalBlLayer
{
    public class AdminLogic
    {
        AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext();
        public bool RegisterAdmin(Admin admin)
        {

                admin.UserId = int.Parse(GenerateNewRandom());
                admin.ModifiedDate = null;
                admin.CreatedDate = DateTime.Now;
                admin.userCategory = 0;
               
                db.Admins.Add(admin);
                db.SaveChanges();
                return true;
         

        }
        public bool RegisterSkill(Skills skills)
        {
            try
            {
                skills.ModifiedDate = null;
                skills.CreatedDate = DateTime.Now;
                db.Skills.Add(skills);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool RegisterModule(Module module)
        {
            try
            {
                module.ModifiedDate = null;
                module.CreatedDate = DateTime.Now;
                db.Modules.Add(module);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool RegisterBatch(Batch batch)
        {
            try
            {
                batch.ModifiedDate = null;
                batch.AssignedEmployees = 0;
                batch.CreatedDate = DateTime.Now;
                batch.AssignFacultyStatus = "no";
                db.Batches.Add(batch);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool RaiseTicket(Help help)
        {
            try
            {
                db.Helps.Add(help);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool MapSkill(Mapping mapping)
        {

            db.Mappings.Add(mapping);
            db.SaveChanges();
            return true;
        }
        public IEnumerable<Skills> GetSkills()
        {
            return db.Skills.ToList();
        }
        public Skills GetSkillsBySkillName(string skillName)
        {
            return db.Skills.Where(s => s.SkillName == skillName).FirstOrDefault();
        }
        public IEnumerable<Module> GetModules()
        {
            return db.Modules.ToList();
        }
        public Module GetModulesByModuleName(string moduleName)
        {
            return db.Modules.Where(s => s.certificationName == moduleName).FirstOrDefault();
        }
        public IEnumerable<Admin> GetAdminDetails()
        {

            return db.Admins.Where(s => s.IsDeleted == false).ToList();

        }
        public IEnumerable<Admin> GetDeletedRecord()
        {
            return db.Admins.Where(s => s.IsDeleted == true).ToList();
        }
        public IEnumerable<Batch> GetFacultyBatch(int userId)
        {
            return db.Batches.Where(s => s.IsDeleted == false && s.FacultyId == userId && s.AssignFacultyStatus == "no").ToList();
        }

        public IEnumerable<Help> GetHelpRequest()
        {
            return db.Helps.Where(s => s.Status == "Pending").ToList();
        }
        public Help GetHelpRequestById(int id)
        {
            return db.Helps.Where(s => s.RequestId == id && s.Status == "Pending").FirstOrDefault();
        }
        public IEnumerable<Help> GetHelpRequestByUserId(int userId)
        {
            return db.Helps.Where(s => s.userId == userId).ToList();
        }
        public Admin GetAdminDetailsById(int id)
        {
            return db.Admins.Where(s => s.UserId == id).FirstOrDefault();
        }
        public IEnumerable<Batch> GetAllBatchDetails()
        {
            return db.Batches.Where(s => s.AssignFacultyStatus == "yes").ToList();
        }
        public Batch GetBatchById(int id)
        {
            return db.Batches.Where(s => s.BatchId == id).FirstOrDefault();
        }
        public IEnumerable<Batch> SearchBatch(int? searchData, string SearchCriteria)
        {
            if (SearchCriteria == "SkillId")
            {
                return db.Batches.Where(stu => stu.SkillId == (searchData)).ToList();
            }
            else if (SearchCriteria == "ModuleId")
            {
                return db.Batches.Where(stu => stu.ModuleId == (searchData)).ToList();
            }
            else if (SearchCriteria == "BatchId")
            {
                return db.Batches.Where(stu => stu.BatchId == (searchData)).ToList();
            }
            else if (SearchCriteria == "FacultyId")
            {
                return db.Batches.Where(stu => stu.FacultyId == (searchData)).ToList();
            }
            else
            {
                return db.Batches.Where(s => s.AssignFacultyStatus == "yes").ToList();
            }

        }
        public IEnumerable<Batch> GetPendingEmployeeNomination()
        {
            AcademyPortalBlLayer.EmployeeLogic empLogic = new AcademyPortalBlLayer.EmployeeLogic();
            var employee = empLogic.GetEmployeeDetails();
            var listofBatchId = new List<int?>();
            foreach (var item in employee)
            {
                if (item.NominationStatus == "Pending")
                {
                    listofBatchId.Add(item.BatchId);
                }
            }
            var batch = db.Batches.Where(s => s.AssignFacultyStatus == "yes").ToList();
            var finalResult = new List<Batch>();
            foreach (var item in batch)
            {
                foreach (var id in listofBatchId)
                {
                    finalResult.Add(db.Batches.Where(s => s.BatchId == id).FirstOrDefault());
                }
            }

            return finalResult;
        }

        public bool Login(Admin admin)
        {

            var status = db.Admins.Where(s => s.UserId == admin.UserId && s.Password == admin.Password).FirstOrDefault();
            if (status != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteAdmin(int id, Admin admin1)
        {

            var admin = db.Admins.Find(id);
            if (admin != null)
            {
                admin.IsDeleted = true;
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool UpdateRequest(Help help)
        {
            try
            {
                if (help != null)
                {
                    db.Entry(help).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public string UpdateBatch(Batch batch, int userId)
        {
            AcademyPortalBlLayer.EmployeeLogic empLogic = new AcademyPortalBlLayer.EmployeeLogic();
            var emp = empLogic.GetEmployeeDetailsById(userId);
            var batchObject = GetBatchById(batch.BatchId);

            if (emp.UserId == userId && emp.BatchId == batch.BatchId)
            {
                return "Exiest";
            }
            else if (batch.BatchCapacity > batch.AssignedEmployees && emp.BatchId == null)
            {
                batch.AssignedEmployees = batch.AssignedEmployees + 1;
                db.Entry(batch).State = EntityState.Modified;
                db.SaveChanges();
                emp.NominationStatus = "Pending";
                emp.BatchId = batch.BatchId;
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
                return "Success";
            }
            else
            {
                return "Failed";
            }

        }
        public string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(100050, 999950).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;

        }
        public IEnumerable<Batch> SortBy(string sortOrder, string CurrentSort)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                switch (sortOrder)
                {
                    case "BatchId":
                        if (sortOrder.Equals(CurrentSort))
                            return db.Batches.OrderByDescending(s => s.BatchId).ToList();
                        else
                            return db.Batches.OrderBy(s => s.BatchId).ToList();
                        break;
                    case "SkillId":
                        if (sortOrder.Equals(CurrentSort))
                            return db.Batches.OrderByDescending(s => s.SkillId).ToList();
                        else
                            return db.Batches.OrderBy(s => s.SkillId).ToList();
                        break;
                    case "ModuleId":
                        if (sortOrder.Equals(CurrentSort))
                            return db.Batches.OrderByDescending(s => s.ModuleId).ToList();
                        else
                            return db.Batches.OrderBy(s => s.ModuleId).ToList();
                        break;
                    case "Technology":
                        if (sortOrder.Equals(CurrentSort))
                            return db.Batches.OrderByDescending(s => s.Technology).ToList();
                        else
                            return db.Batches.OrderBy(s => s.Technology).ToList();
                        break;
                    case "FacultyId":
                        if (sortOrder.Equals(CurrentSort))
                            return db.Batches.OrderByDescending(s => s.FacultyId).ToList();
                        else
                            return db.Batches.OrderBy(s => s.FacultyId).ToList();
                        break;
                    case "BatchStartDate":
                        if (sortOrder.Equals(CurrentSort))
                            return db.Batches.OrderByDescending(s => s.StartDate).ToList();
                        else
                            return db.Batches.OrderBy(s => s.StartDate).ToList();
                        break;
                    case "BatchEndDate":
                        if (sortOrder.Equals(CurrentSort))
                            return db.Batches.OrderByDescending(s => s.EndDate).ToList();
                        else
                            return db.Batches.OrderBy(s => s.EndDate).ToList();
                        break;
                    case "BatchCapacity":
                        if (sortOrder.Equals(CurrentSort))
                            return db.Batches.OrderByDescending(s => s.BatchCapacity).ToList();
                        else
                            return db.Batches.OrderBy(s => s.BatchCapacity).ToList();
                        break;
                    case "ClassroomName":
                        if (sortOrder.Equals(CurrentSort))
                            return db.Batches.OrderByDescending(s => s.ClassroomName).ToList();
                        else
                            return db.Batches.OrderBy(s => s.ClassroomName).ToList();
                        break;
                    default:
                        return db.Batches.ToList();
                        break;
                }
            }
        }
        public int GetFacultyId(Admin adminModel)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                var userId = db.Admins.Where(s => s.SecurityQuestion == adminModel.SecurityQuestion && s.SecurityQueAnswer == adminModel.SecurityQueAnswer && (s.Contact == adminModel.Contact || s.Email == adminModel.Email)).Select(s => s.UserId).SingleOrDefault();
                return userId;
            }
        }
        public Admin VerifyPasswordRecoveryDetails(Admin adminModel)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                var userId = db.Admins.Where(s => s.SecurityQuestion == adminModel.SecurityQuestion && s.SecurityQueAnswer == adminModel.SecurityQueAnswer && s.UserId == adminModel.UserId).FirstOrDefault();
                return userId;
            }
        }
        public bool GetPasswordReset(string password, Admin adminModel)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                try
                {
                    if (adminModel != null)
                    {
                        adminModel.Password = password;
                        adminModel.ModifiedDate = DateTime.Now;
                        db.Entry(adminModel).State = EntityState.Modified;
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
