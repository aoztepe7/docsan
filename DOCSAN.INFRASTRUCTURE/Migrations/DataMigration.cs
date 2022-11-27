using DOCSAN.CORE.Entities;
using FluentMigrator;

namespace DOCSAN.INFRASTRUCTURE.Migrations
{
    [Migration(2, "InitialDataSeed")]
    public class DataMigration : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            SeedUsers();
        }

        private void SeedUsers()
        {

            Insert.IntoTable("Users")
                .Row(new
                {
                    Id = new Guid("18e51e25-e858-4d0d-94b7-e524e5f6e1b3"),
                    Created = DateTime.Now,
                    CreatedBy = "Seed",
                    LastModified = (DateTime?)null,
                    LastModifiedBy = (String?)null,
                    Status = true,
                    Mail = "john@doe.com",
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = DateTime.Parse("1990-09-25"),
                    Password = "1234aaaa", 
                    Deleted = false,
                    Role = (int)SHARED.Enums.enmRole.Admin,
                    ImageUrl = String.Empty,
                    Gender = (int) SHARED.Enums.enmGender.Male
                });
        }
    }
}
