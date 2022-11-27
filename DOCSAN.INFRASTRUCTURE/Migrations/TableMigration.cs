using FluentMigrator;

namespace DOCSAN.INFRASTRUCTURE.Migrations
{
    [Migration(1, "InitialTableMigration")]
    public class TableMigration : Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            CreateUserTable();
            CreateProjectTable();
        }

        private void CreateUserTable()
        {
            Create.Table("Users")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsString().Nullable()
                .WithColumn("LastModified").AsDateTime().Nullable()
                .WithColumn("LastModifiedBy").AsString().Nullable()
                .WithColumn("Status").AsBoolean().Nullable()
                .WithColumn("Deleted").AsBoolean().NotNullable()
                .WithColumn("Mail").AsString(50).NotNullable().Unique()
                .WithColumn("Password").AsString(20).NotNullable()
                .WithColumn("FirstName").AsString(30).Nullable()
                .WithColumn("LastName").AsString(30).Nullable()
                .WithColumn("BirthDate").AsDateTime().Nullable()
                .WithColumn("Gender").AsInt16().Nullable()
                .WithColumn("ImageUrl").AsString().Nullable()
                .WithColumn("Role").AsInt16().NotNullable();
        }

        private void CreateProjectTable()
        {
            Create.Table("Projects")
               .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
               .WithColumn("Created").AsDateTime().NotNullable()
               .WithColumn("CreatedBy").AsString().Nullable()
               .WithColumn("LastModified").AsDateTime().Nullable()
               .WithColumn("LastModifiedBy").AsString().Nullable()
               .WithColumn("Status").AsBoolean().Nullable()
               .WithColumn("Deleted").AsBoolean().NotNullable()
               .WithColumn("Name").AsString(50).NotNullable().Unique()
               .WithColumn("ImageUrl").AsString(255).Nullable();
        }
    }
}
