using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPortalBoLayer
{
    public enum ExecutionType { ELearning , Classroom };
    public enum CertificationType { Internal,External };
    public class Module
    {

        [Required]
        [Key]
        public int ModuleId { get; set; }

        [Required(ErrorMessage ="Please Enter Technology")]
        public String Technology { get; set; }

        [Required]
        public string proficiencyLevel { get; set; }

        [Required]
        public ExecutionType executionType { get; set; }

        [Required]
        public CertificationType certificationType { get; set; }

        [Display(Name = "Certification Name")]
        [Required(ErrorMessage ="Please Enter Cetification Name")]
        public string certificationName { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
