using AcademyPortalBoLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AcademyPortalBlLayer
{
    public class EmployeeLogic
    {
        AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext();

        public bool RegisterEmployee(Employee employee)
        {
            try
            {
                employee.IsDeleted = false;
                employee.CreatedDate = DateTime.Now;
                employee.ModifiedDate = null;
                employee.RegistrationStatus ="Pending";
                employee.UserId = int.Parse(GenerateNewRandom());
                db.EmployeeInfo.Add(employee);
                db.SaveChanges();
                return true;
               }
            catch(Exception e)
            {
                  return false;
            }
        }

        public IEnumerable<Employee> GetEmployeeDetails()
        {
                return db.EmployeeInfo.Where(s => s.IsDeleted == false).ToList();
        }
        public Employee GetEmployeeDetailsByBatchId(int? id)
        {
            return db.EmployeeInfo.Where(s => s.BatchId==id).FirstOrDefault();
        }
        public IEnumerable<Employee> GetEmployeeStatus()
        {
                var employee = db.EmployeeInfo.Where(s => s.RegistrationStatus == "Pending" && s.IsDeleted==false).ToList();
                return employee;
            
        }
        public IEnumerable<Employee> GetDeletedRecord()
        {
            var deletedRecord = db.EmployeeInfo.Where(s => s.IsDeleted == true).ToList();
            return deletedRecord;
        }

        public Employee GetEmployeeDetailsById(int id)
        { 
                var employee = db.EmployeeInfo.Where(s => s.UserId == id).FirstOrDefault();
                return  employee;
         
        }
        public bool Login(Employee employee)
        {
            var data = db.EmployeeInfo.Find(employee.UserId);
            if(data.UserId==employee.UserId && data.Password==employee.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EditEmployeeDetails(Employee employee, string status, bool isdeleted)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                if (employee != null)
                {
                    employee.RegistrationStatus = status;
                    employee.IsDeleted = isdeleted;
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteEmployee(int id, Employee emp)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                var employee = db.EmployeeInfo.Find(id);
                if (employee != null)
                {
                    employee.IsDeleted = true;
                    employee.RegistrationStatus = "Rejected";
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
        public bool UpdateEmployee(int? batchId,string nominationstatus)
        {
           if(batchId!=null)
           {
                var employee = GetEmployeeDetailsByBatchId(batchId);
                employee.BatchId = batchId;
                employee.NominationStatus = nominationstatus;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetFacultyId(Employee empModel)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                var userId = db.EmployeeInfo.Where(s => s.SecurityQuestion == empModel.SecurityQuestion && s.SecurityQueAnswer == empModel.SecurityQueAnswer && (s.Contact == empModel.Contact || s.Email == empModel.Email)).Select(s => s.UserId).SingleOrDefault();
                return userId;
            }
        }
        public Employee VerifyPasswordRecoveryDetails(Employee empModel)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                var userId = db.EmployeeInfo.Where(s => s.SecurityQuestion == empModel.SecurityQuestion && s.SecurityQueAnswer == empModel.SecurityQueAnswer && s.UserId == empModel.UserId).FirstOrDefault();
                return userId;
            }
        }
        public bool GetPasswordReset(string password, Employee empModel)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                try
                {
                    if (empModel != null)
                    {
                        empModel.Password = password;
                        empModel.ModifiedDate = DateTime.Now;
                        db.Entry(empModel).State = EntityState.Modified;
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
