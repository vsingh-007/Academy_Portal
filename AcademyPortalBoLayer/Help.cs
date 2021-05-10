using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPortalBoLayer
{
    public class Help
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestId { get; set; }

        [Required(ErrorMessage ="Please Enter Issue")]
        [DataType(DataType.Text)]
        public string Issue { get; set; }

        [Required(ErrorMessage ="Please Enter Description")]
        [DataType(DataType.Text)]
        public string Discription { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfTicket { get; set; }

        [Display(Name ="Resolution")]
        public string Resolution { get; set; }
        public string Status { get; set; }
        public string userCategory { get; set; }
        public int userId { get; set; }



    }
}
