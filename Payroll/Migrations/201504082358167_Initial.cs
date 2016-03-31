namespace Payroll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vendor = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DueDate = c.DateTime(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SocialSecurityNumber = c.String(),
                        DateOfBirth = c.String(),
                        Wage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YearToDateIncome = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaycycleYear = c.Int(nullable: false),
                        Married = c.Boolean(nullable: false),
                        IsSalary = c.Boolean(nullable: false),
                        FedExemptions = c.Int(nullable: false),
                        StateExemptions = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Paychecks",
                c => new
                    {
                        PaycheckId = c.Int(nullable: false, identity: true),
                        PayDate = c.DateTime(nullable: false),
                        Gross = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FederalWithholding = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StateWithholding = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeSocialSecurity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeMedicare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployerSocialSecurity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployerMedicare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FederalUnemployment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StateUnemployment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Approved = c.Boolean(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaycheckId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paychecks", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Paychecks", new[] { "EmployeeId" });
            DropTable("dbo.Paychecks");
            DropTable("dbo.Employees");
            DropTable("dbo.Bills");
        }
    }
}
