namespace EmployeeWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nqobile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.People", new[] { "Employee_EmployeeId" });
            AlterColumn("dbo.Employees", "Terminated", c => c.DateTime());
            CreateIndex("dbo.Employees", "PersonId");
            AddForeignKey("dbo.Employees", "PersonId", "dbo.People", "PersonId", cascadeDelete: true);
            DropColumn("dbo.People", "Employee_EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Employee_EmployeeId", c => c.Int());
            DropForeignKey("dbo.Employees", "PersonId", "dbo.People");
            DropIndex("dbo.Employees", new[] { "PersonId" });
            AlterColumn("dbo.Employees", "Terminated", c => c.DateTime(nullable: false));
            CreateIndex("dbo.People", "Employee_EmployeeId");
            AddForeignKey("dbo.People", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
        }
    }
}
