namespace AcademyPortalDaLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Admins");
            DropTable("dbo.Batches");
            DropTable("dbo.Helps");
            DropTable("dbo.Mappings");
            DropTable("dbo.Modules");
            DropTable("dbo.Skills");
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
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
                "dbo.Batches",
                c => new
                    {
                        BatchId = c.Int(nullable: false, identity: true),
                        SkillId = c.Int(nullable: false),
                        ModuleId = c.Int(nullable: false),
                        Technology = c.String(nullable: false),
                        FacultyId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        BatchCapacity = c.Int(nullable: false),
                        ClassroomName = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AssignFacultyStatus = c.String(),
                        AssignedEmployees = c.Int(),
                    })
                .PrimaryKey(t => t.BatchId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
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
                        UserId = c.Int(nullable: false, identity: true),
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
            
            CreateTable(
                "dbo.Helps",
                c => new
                    {
                        RequestId = c.Int(nullable: false),
                        Issue = c.String(nullable: false),
                        Discription = c.String(nullable: false),
                        DateOfTicket = c.DateTime(nullable: false),
                        Resolution = c.String(),
                        Status = c.String(),
                        userCategory = c.String(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequestId);
            
            CreateTable(
                "dbo.Mappings",
                c => new
                    {
                        MappingId = c.Int(nullable: false, identity: true),
                        SkillId = c.Int(nullable: false),
                        ModuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MappingId);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        ModuleId = c.Int(nullable: false, identity: true),
                        Technology = c.String(nullable: false),
                        proficiencyLevel = c.String(nullable: false),
                        executionType = c.Int(nullable: false),
                        certificationType = c.Int(nullable: false),
                        certificationName = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ModuleId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Skill_ID = c.Int(nullable: false, identity: true),
                        skillFamily = c.Int(nullable: false),
                        SkillName = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Skill_ID);
           
        }
        
        public override void Down()
        {
            DropTable("dbo.Skills");
            DropTable("dbo.Modules");
            DropTable("dbo.Mappings");
            DropTable("dbo.Helps");
            DropTable("dbo.Faculties");
            DropTable("dbo.Employees");
            DropTable("dbo.Batches");
            DropTable("dbo.Admins");
        }
    }
}
