using AcademyPortalBoLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AcademyPortalBlLayer
{
    public class FacultyLogic
    {
        AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext();

        public bool SaveFacultyDetails(Faculty facultyModel)
        {
          
                facultyModel.ModifiedDate = null;
                facultyModel.CreatedDate = DateTime.Now;
                facultyModel.RegistrationStatus = "Pending";
                facultyModel.UserId = int.Parse(GenerateNewRandom());
                db.Faculties.Add(facultyModel);
                db.SaveChanges();
                return true;
        
        }

        public IEnumerable<Faculty> GetFacultyDetails()
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                var facultyModel = db.Faculties.Where(s => s.IsDeleted == false).ToList();
                return facultyModel;
            }
        }
        public IEnumerable<Faculty> GetFacultyStatus()
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                var facultyModel = db.Faculties.Where(s => s.RegistrationStatus=="Pending" && s.IsDeleted==false).ToList();
                return facultyModel;
            }
        }
      
        public IEnumerable<Faculty> GetDeletedRecord()
        {
            var deletedRecord = db.Faculties.Where(s => s.IsDeleted == true).ToList();
            return deletedRecord;
        }


        public Faculty GetFacultyDetailsById(int id)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                var facultyModel = db.Faculties.Where(s => s.UserId == id).FirstOrDefault();
                return facultyModel;
            }
        }

        public bool LoginCredentials(Faculty facultyModel)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                var status = db.Faculties.Where(s => s.UserId == facultyModel.UserId && s.Password == facultyModel.Password).FirstOrDefault();
                if (status != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool EditFacultyDetails(Faculty faculty,string status,bool isdeleted)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                if (faculty!=null)
                {
                    faculty.RegistrationStatus = status;
                    faculty.IsDeleted = isdeleted;
                    db.Entry(faculty).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool EditFacultyNominationStatus(Faculty faculty, string status, int? batchId,Batch batch)
        {

            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                if (faculty != null && batch != null)
                {
                    faculty.NominationStatus = status;
                    faculty.BatchId = batchId;
                    if (status == "Accepted")
                    {
                        batch.AssignFacultyStatus = "yes";
                    }
                    else
                    {
                        batch.AssignFacultyStatus = "no";
                    }
                     db.Entry(batch).State = EntityState.Modified;
                    db.SaveChanges();

                    db.Entry(faculty).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteFaculty(int id, Faculty faculty)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                var facultyModel = db.Faculties.Find(id);
                if (facultyModel != null)
                {
                    facultyModel.IsDeleted = true;
                    facultyModel.RegistrationStatus = "Rejected";
                    db.Entry(facultyModel).State = EntityState.Modified;
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
        public int GetFacultyId(Faculty facultyModel)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                var userId = db.Faculties.Where(s => s.SecurityQuestion == facultyModel.SecurityQuestion && s.SecurityQueAnswer == facultyModel.SecurityQueAnswer && (s.Contact == facultyModel.Contact || s.Email == facultyModel.Email)).Select(s => s.UserId).SingleOrDefault();
                return userId;
            }
        }
        public Faculty VerifyPasswordRecoveryDetails(Faculty facultyModel)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                var userId = db.Faculties.Where(s => s.SecurityQuestion == facultyModel.SecurityQuestion && s.SecurityQueAnswer == facultyModel.SecurityQueAnswer && s.UserId == facultyModel.UserId).FirstOrDefault();
                return userId;
            }
        }
        public bool GetPasswordReset(string password, Faculty facultyModel)
        {
            using (AcademyPortalDaLayer.AcademyPortalContext db = new AcademyPortalDaLayer.AcademyPortalContext())
            {
                try
                {
                    if (facultyModel != null)
                    {
                        facultyModel.Password = password;
                        facultyModel.ModifiedDate = DateTime.Now;
                        db.Entry(facultyModel).State = EntityState.Modified;
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
