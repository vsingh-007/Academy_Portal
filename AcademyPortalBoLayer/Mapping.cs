using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPortalBoLayer
{
    public class Mapping
    {
        [Key]
        public int MappingId { get; set; }

        public int SkillId { get; set; }

        public int ModuleId { get; set; }
    }
}
