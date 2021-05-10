using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPortalBoLayer
{
    public enum ProficiencyType { Beginner=1, Intermediate=2, Advance=3 };
    public enum UserCategory { Admin, Faculty, Employee };
    public class Faculty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invaild First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invaild First Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Date of Birth")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime Dob { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender gender { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public long Contact { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=^.{8,15}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$", ErrorMessage = "Passwors must be Between 8 and 15 inclusive, contains atleast one digit, atleast one upper case and atleast one lower case and no whitespace.")]
        public string Password { get; set; }

        [Display(Name = "Security Question")]
        public string SecurityQuestion { get; set; }

        [Display(Name = "Security Question Answer")]
        public string SecurityQueAnswer{ get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "User Category")]
        public UserCategory UserCatagory { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Skill Family")]
        public SkillFamily skillFamily { get; set; }

        [Required(ErrorMessage = "Please Enter Module Name")]
        [Display(Name = "Module Name")]
        public string ModuleName { get; set; }

        [Required(ErrorMessage = "*")]
        public ProficiencyType Proficiency { get; set; }

        [Required(ErrorMessage = "Please Enter Teaching Hours")]
        [Display(Name = "Teaching Hours")]
        public int TeachingHours { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [Required]
        public string RegistrationStatus { get; set; }
        public int? BatchId { get; set; }
        public string NominationStatus { get; set; }

    }
}
