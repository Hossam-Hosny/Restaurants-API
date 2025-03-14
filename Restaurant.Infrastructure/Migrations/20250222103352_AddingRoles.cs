using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
        INSERT INTO AspNetUserRoles(UserId, RoleId)
        VALUES 
        ('28a3a5d0-bc99-4f04-ad28-6466395563ed','27e82d17-d5b6-4135-8bf7-e27966a53594'),
        ('9221fa30-b0af-43f4-a19d-fddbbe66db36','fb8c7d9e-84ff-4906-9ee4-53e991f86444'),
        ('c0bdf2b3-b8e8-4800-9661-ea1686267927','77c1be72-83c6-43ce-a7c2-44b36acf3ff5');
                             ");



        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        DELETE FROM AspNetUserRoles
        WHERE UserId IN (
            '28a3a5d0-bc99-4f04-ad28-6466395563ed',
            '9221fa30-b0af-43f4-a19d-fddbbe66db36',
            'c0bdf2b3-b8e8-4800-9661-ea1686267927'
        );
               ");
        }
    }
}
