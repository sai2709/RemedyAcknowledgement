namespace RemedyAcknowledgement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Userid = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Userid);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        Role = c.String(nullable: false),
                        Secretquestion = c.String(),
                        Secretans = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admin", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.SupportAnalyst", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SupportAnalyst",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Designation = c.String(nullable: false),
                        ContactNumber = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Dob = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        Secretquestion = c.String(nullable: false),
                        Secretanswer = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        NotifId = c.Int(nullable: false, identity: true),
                        CreationDate = c.Int(nullable: false),
                        AlertText = c.Int(nullable: false),
                        Details = c.Int(nullable: false),
                        ReceiverId = c.String(),
                        SenderId = c.String(),
                        NotifDate = c.DateTime(nullable: false),
                        Admin_Userid = c.String(maxLength: 128),
                        SupportAnalyst_UserId = c.String(maxLength: 128),
                        User_UserId = c.String(maxLength: 128),
                        RemedyList_SerialNo = c.Int(),
                    })
                .PrimaryKey(t => t.NotifId)
                .ForeignKey("dbo.Admin", t => t.Admin_Userid)
                .ForeignKey("dbo.SupportAnalyst", t => t.SupportAnalyst_UserId)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .ForeignKey("dbo.RemedyList", t => t.RemedyList_SerialNo)
                .Index(t => t.Admin_Userid)
                .Index(t => t.SupportAnalyst_UserId)
                .Index(t => t.User_UserId)
                .Index(t => t.RemedyList_SerialNo);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Designation = c.String(nullable: false),
                        ContactNumber = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Dob = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        Secretquestion = c.String(nullable: false),
                        Secretanswer = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.RemedyDetails",
                c => new
                    {
                        Remedyid = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        ContactNo = c.String(nullable: false),
                        AlternateNumber = c.String(nullable: false),
                        SeatNo = c.String(nullable: false),
                        PCNumber = c.String(nullable: false),
                        IPAddress = c.String(nullable: false),
                        User_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Remedyid)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.RemedyList",
                c => new
                    {
                        SerialNo = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Description = c.String(),
                        AssignedUserId = c.String(),
                        Remedyid = c.Int(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SerialNo)
                .ForeignKey("dbo.Admin", t => t.UserId)
                .ForeignKey("dbo.RemedyDetails", t => t.Remedyid, cascadeDelete: true)
                .ForeignKey("dbo.SupportAnalyst", t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Remedyid);
            
            CreateTable(
                "dbo.Questionnaire",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Questions = c.String(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Admin", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Feedback",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        UserId = c.String(),
                        Answers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FeedbackId, t.QuestionId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questionnaire", "UserId", "dbo.Admin");
            DropForeignKey("dbo.RemedyDetails", "User_UserId", "dbo.User");
            DropForeignKey("dbo.RemedyList", "UserId", "dbo.User");
            DropForeignKey("dbo.RemedyList", "UserId", "dbo.SupportAnalyst");
            DropForeignKey("dbo.RemedyList", "Remedyid", "dbo.RemedyDetails");
            DropForeignKey("dbo.Notification", "RemedyList_SerialNo", "dbo.RemedyList");
            DropForeignKey("dbo.RemedyList", "UserId", "dbo.Admin");
            DropForeignKey("dbo.Notification", "User_UserId", "dbo.User");
            DropForeignKey("dbo.Logins", "UserId", "dbo.User");
            DropForeignKey("dbo.Notification", "SupportAnalyst_UserId", "dbo.SupportAnalyst");
            DropForeignKey("dbo.Notification", "Admin_Userid", "dbo.Admin");
            DropForeignKey("dbo.Logins", "UserId", "dbo.SupportAnalyst");
            DropForeignKey("dbo.Logins", "UserId", "dbo.Admin");
            DropIndex("dbo.Questionnaire", new[] { "UserId" });
            DropIndex("dbo.RemedyList", new[] { "Remedyid" });
            DropIndex("dbo.RemedyList", new[] { "UserId" });
            DropIndex("dbo.RemedyDetails", new[] { "User_UserId" });
            DropIndex("dbo.Notification", new[] { "RemedyList_SerialNo" });
            DropIndex("dbo.Notification", new[] { "User_UserId" });
            DropIndex("dbo.Notification", new[] { "SupportAnalyst_UserId" });
            DropIndex("dbo.Notification", new[] { "Admin_Userid" });
            DropIndex("dbo.Logins", new[] { "UserId" });
            DropTable("dbo.Feedback");
            DropTable("dbo.Questionnaire");
            DropTable("dbo.RemedyList");
            DropTable("dbo.RemedyDetails");
            DropTable("dbo.User");
            DropTable("dbo.Notification");
            DropTable("dbo.SupportAnalyst");
            DropTable("dbo.Logins");
            DropTable("dbo.Admin");
        }
    }
}
