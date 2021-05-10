using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPortalBoLayer
{
    public enum Gender { Male, Female, others };
    public enum Usercategory { Admin, Faculty, Employee };
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invaild First Name")]
        public string First_name { get; set; }

        [Required(ErrorMessage = "Last Name Required")]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invaild Last Name")]
        public string Last_name { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        public Gender gender { get; set; }

        [Required(ErrorMessage = "Phone Number Required")]
        [Display(Name = "Contact")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number")]
        public long Contact { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
          ErrorMessage = "Invalid Email ID")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=^.{8,15}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$", ErrorMessage = "Passwors must be Between 8 and 15 inclusive, contains atleast one digit, atleast one upper case and atleast one lower case and no whitespace.")]
        public string Password { get; set; }

        [Display(Name = "Security Question")]
        public string SecurityQuestion { get; set; }

        [Display(Name = "Security Answer")]
        public string SecurityQueAnswer { get; set; }

        [Required(ErrorMessage = "User Category Required")]
        public Usercategory userCategory { get; set; }

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
