using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPortalBoLayer
{
    public class Batch
    {
        [Required(ErrorMessage = "Please Enter Batch Id")]
        [Display(Name = "Batch Id")]
        public int BatchId { get; set; }

        [Required(ErrorMessage = "Please Enter Skill Id")]
        [Display(Name = "Skill Id")]
        public int SkillId { get; set; }

        [Required(ErrorMessage = "Please Enter Module Id")]
        [Display(Name = "Module Id")]
        public int ModuleId { get; set; }

        [Required(ErrorMessage = "Please Enter Technology")]
        public string Technology { get; set; }

        [Required(ErrorMessage = "Please Enter Faculty Id")]
        [Display(Name = "Faculty Id")]
        public int FacultyId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Batch Capacity")]
        public int BatchCapacity { get; set; }

        [Required]
        [Display(Name = "Classroom Name")]
        public string ClassroomName { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string AssignFacultyStatus { get; set; }
        public int? AssignedEmployees { get; set; }
    }
}
