namespace Jobs.Offer.Wbesite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "Category_Id" });
            RenameColumn(table: "dbo.Jobs", name: "Category_Id", newName: "CategoryId");
            AlterColumn("dbo.Jobs", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "CategoryId");
            AddForeignKey("dbo.Jobs", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            DropColumn("dbo.Jobs", "CaregoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "CaregoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Jobs", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "CategoryId" });
            AlterColumn("dbo.Jobs", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.Jobs", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.Jobs", "Category_Id");
            AddForeignKey("dbo.Jobs", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
