namespace HyundaiGarageVer2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Car",
                c => new
                {
                    LicensePlate = c.Int(nullable: false),
                    Model = c.Int(nullable: false),
                    ManufYear = c.DateTime(nullable: false),
                    CustomerID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.LicensePlate)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);

            CreateTable(
                "dbo.Customer",
                c => new
                {
                    CustomerID = c.Int(nullable: false, identity: true),
                    FirstName = c.String(maxLength: 20),
                    LastName = c.String(maxLength: 20),
                    Phone = c.String(),
                })
                .PrimaryKey(t => t.CustomerID);

            CreateTable(
                "dbo.Treatment",
                c => new
                {
                    TreatmentID = c.Int(nullable: false, identity: true),
                    WorkHours = c.Int(nullable: false),
                    TreatmentDate = c.DateTime(nullable: false),
                    CarID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.TreatmentID)
                .ForeignKey("dbo.Car", t => t.CarID, cascadeDelete: true)
                .Index(t => t.CarID);

            CreateTable(
                "dbo.Part",
                c => new
                {
                    PartID = c.Int(nullable: false, identity: true),
                    PartName = c.String(maxLength: 20),
                    ManuDate = c.DateTime(nullable: false),
                    PartPrice = c.Int(nullable: false),
                    TreatmentID = c.Int(nullable: true),
                })
                .PrimaryKey(t => t.PartID)
                .ForeignKey("dbo.Treatment", t => t.TreatmentID)
                .Index(t => t.TreatmentID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Part", "TreatmentID", "dbo.Treatment");
            DropForeignKey("dbo.Treatment", "CarID", "dbo.Car");
            DropForeignKey("dbo.Car", "CustomerID", "dbo.Customer");
            DropIndex("dbo.Part", new[] { "TreatmentID" });
            DropIndex("dbo.Treatment", new[] { "CarID" });
            DropIndex("dbo.Car", new[] { "CustomerID" });
            DropTable("dbo.Part");
            DropTable("dbo.Treatment");
            DropTable("dbo.Customer");
            DropTable("dbo.Car");
        }
    }
}
