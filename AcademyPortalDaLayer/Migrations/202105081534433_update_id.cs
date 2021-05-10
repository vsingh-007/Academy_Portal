namespace AcademyPortalDaLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_id : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Admins");
            DropPrimaryKey("dbo.Employees");
            DropPrimaryKey("dbo.Faculties");
            AlterColumn("dbo.Admins", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Faculties", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Admins", "UserId");
            AddPrimaryKey("dbo.Employees", "UserId");
            AddPrimaryKey("dbo.Faculties", "UserId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Faculties");
            DropPrimaryKey("dbo.Employees");
            DropPrimaryKey("dbo.Admins");
            AlterColumn("dbo.Faculties", "UserId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Employees", "UserId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Admins", "UserId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Faculties", "UserId");
            AddPrimaryKey("dbo.Employees", "UserId");
            AddPrimaryKey("dbo.Admins", "UserId");
        }
    }
}
