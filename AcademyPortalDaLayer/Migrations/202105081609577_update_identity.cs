namespace AcademyPortalDaLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_identity : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Admins");
            DropTable("dbo.Faculties");
            DropTable("dbo.Employees");
            CreateTable(
              "dbo.Admins",
              c => new
              {
                  UserId = c.Int(nullable: false),
                  First_name = c.String(nullable: false),
                  Last_name = c.String(nullable: false),
                  DOB = c.DateTime(nullable: false),
                  gender = c.Int(nullable: false),
                  Contact = c.String(nullable: false),
                  Email = c.String(nullable: false),
                  Password = c.String(nullable: false),
                  SecurityQuestion = c.String(),
                  SecurityQueAnswer = c.String(),
                  userCategory = c.Int(nullable: false),
                  IsDeleted = c.Boolean(nullable: false),
                  CreatedDate = c.DateTime(nullable: false),
                  ModifiedDate = c.DateTime(),
              })
              .PrimaryKey(t => t.UserId);

            CreateTable(
                "dbo.Employees",
                c => new
                {
                    UserId = c.Int(nullable: false),
                    First_name = c.String(nullable: false),
                    Last_name = c.String(nullable: false),
                    DOB = c.DateTime(nullable: false),
                    gender = c.Int(nullable: false),
                    Contact = c.Long(nullable: false),
                    Email = c.String(nullable: false),
                    Password = c.String(nullable: false),
                    SecurityQuestion = c.String(),
                    SecurityQueAnswer = c.String(),
                    userCategory = c.Int(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    ModifiedDate = c.DateTime(),
                    RegistrationStatus = c.String(nullable: false),
                    BatchId = c.Int(),
                    NominationStatus = c.String(),
                })
                .PrimaryKey(t => t.UserId);

            CreateTable(
                "dbo.Faculties",
                c => new
                {
                    UserId = c.Int(nullable: false),
                    FirstName = c.String(nullable: false),
                    LastName = c.String(nullable: false),
                    Dob = c.DateTime(nullable: false),
                    gender = c.Int(nullable: false),
                    Contact = c.Long(nullable: false),
                    Email = c.String(nullable: false),
                    Password = c.String(nullable: false),
                    SecurityQuestion = c.String(),
                    SecurityQueAnswer = c.String(),
                    UserCatagory = c.Int(nullable: false),
                    skillFamily = c.Int(nullable: false),
                    ModuleName = c.String(nullable: false),
                    Proficiency = c.Int(nullable: false),
                    TeachingHours = c.Int(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    ModifiedDate = c.DateTime(),
                    RegistrationStatus = c.String(nullable: false),
                    BatchId = c.Int(),
                    NominationStatus = c.String(),
                })
                .PrimaryKey(t => t.UserId);
        }
        
        public override void Down()
        {
        }
    }
}
