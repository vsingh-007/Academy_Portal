using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPortalBoLayer
{

    public class Admin
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
        [Display(Name = "Gender")]
        [EnumDataType(typeof(Gender))]
        public Gender gender { get; set; }

        [Required(ErrorMessage = "Phone Number Required")]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Email ID Required")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
          ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=^.{8,15}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$", ErrorMessage = "Passwors must be between 8 and 15 inclusive, contains atleast one digit, atleast one upper case and atleast one lower case and no whitespace.")]
        public string Password { get; set; }

        [Display(Name = "Security Question")]
        public string SecurityQuestion { get; set; }

        [Display(Name = "Security Answer")]
        public string SecurityQueAnswer { get; set; }

        [Required]
        [EnumDataType(typeof(UserCategory))]
        [Display(Name = "User Category")]
        public UserCategory userCategory { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
