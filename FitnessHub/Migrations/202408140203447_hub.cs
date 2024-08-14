namespace FitnessHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class hub : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.Bookings",
                c => new
                {
                    BookingID = c.Int(nullable: false, identity: true),
                    UserID = c.String(maxLength: 128),
                    DanceClassID = c.Int(),
                    SwimmingLessonID = c.Int(),
                    BookingDate = c.DateTime(nullable: false),
                    AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Status = c.String(),
                })
                .PrimaryKey(t => t.BookingID)

                .ForeignKey("dbo.DanceClasses", t => t.DanceClassID)
                .ForeignKey("dbo.SwimmingLessons", t => t.SwimmingLessonID)
                .Index(t => t.UserID)
                .Index(t => t.DanceClassID)
                .Index(t => t.SwimmingLessonID);

            CreateTable(
                "dbo.DanceClasses",
                c => new
                {
                    ClassID = c.Int(nullable: false, identity: true),
                    StudioID = c.Int(nullable: false),
                    Name = c.String(),
                    Instructor = c.String(),
                    Schedule = c.DateTime(nullable: false),
                    Duration = c.Int(nullable: false),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Status = c.String(),
                })
                .PrimaryKey(t => t.ClassID)
                .ForeignKey("dbo.Studios", t => t.StudioID)
                .Index(t => t.StudioID);

            CreateTable(
                "dbo.Payments",
                c => new
                {
                    PaymentID = c.Int(nullable: false, identity: true),
                    BookingID = c.Int(nullable: false),
                    UserID = c.String(maxLength: 128),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    PaymentDate = c.DateTime(nullable: false),
                    PaymentMethod = c.String(),
                })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.Bookings", t => t.BookingID, cascadeDelete: true)

                .Index(t => t.BookingID)
                .Index(t => t.UserID);

            CreateTable(
                "dbo.Pools",
                c => new
                {
                    PoolID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Location = c.String(),
                    Description = c.String(),
                    ImageUrl = c.String(),
                })
                .PrimaryKey(t => t.PoolID);

            CreateTable(
                "dbo.Studios",
                c => new
                {
                    StudioID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Location = c.String(),
                    Description = c.String(),
                    ImageUrl = c.String(),
                })
                .PrimaryKey(t => t.StudioID);

            CreateTable(
                "dbo.SwimmingLessons",
                c => new
                {
                    LessonID = c.Int(nullable: false, identity: true),
                    PoolID = c.Int(nullable: false),
                    Name = c.String(),
                    Instructor = c.String(),
                    Schedule = c.DateTime(nullable: false),
                    Duration = c.Int(nullable: false),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Status = c.String(),
                })
                .PrimaryKey(t => t.LessonID)
                .ForeignKey("dbo.Pools", t => t.PoolID)
                .Index(t => t.PoolID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.SwimmingLessons", "PoolID", "dbo.Pools");
            DropForeignKey("dbo.Payments", "UserID");
            DropForeignKey("dbo.Payments", "BookingID", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "SwimmingLessonID", "dbo.SwimmingLessons");
            DropForeignKey("dbo.Bookings", "DanceClassID", "dbo.DanceClasses");
            DropForeignKey("dbo.Bookings", "UserID");
            DropForeignKey("dbo.DanceClasses", "StudioID", "dbo.Studios");
            DropIndex("dbo.SwimmingLessons", new[] { "PoolID" });
            DropIndex("dbo.Payments", new[] { "UserID" });
            DropIndex("dbo.Payments", new[] { "BookingID" });
            DropIndex("dbo.DanceClasses", new[] { "StudioID" });
            DropIndex("dbo.Bookings", new[] { "SwimmingLessonID" });
            DropIndex("dbo.Bookings", new[] { "DanceClassID" });
            DropIndex("dbo.Bookings", new[] { "UserID" });
            DropTable("dbo.SwimmingLessons");
            DropTable("dbo.Studios");
            DropTable("dbo.Pools");
            DropTable("dbo.Payments");
            DropTable("dbo.DanceClasses");
            DropTable("dbo.Bookings");

        }
    }
}