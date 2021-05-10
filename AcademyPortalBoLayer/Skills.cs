using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPortalBoLayer
{
    public enum SkillFamily { Technical, Functional, Behavioral };
    
    public class Skills
    {
        [Required]
        [Display(Name ="Skill ID")]
        [Key]
        public int Skill_ID { get; set; }

        [Required]
        [Display(Name = "Skill Family")]
        public SkillFamily skillFamily { get; set; }

        [Required(ErrorMessage ="Please Enter Skill Name")]
        [Display(Name = "Skill Name")]
        public string SkillName { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
