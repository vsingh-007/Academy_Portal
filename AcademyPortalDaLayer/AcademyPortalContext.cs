using AcademyPortalBoLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPortalDaLayer
{
    public class AcademyPortalContext:DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Mapping> Mappings { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Help> Helps { get; set; }
        public DbSet<Employee> EmployeeInfo { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
    }
}
