using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Folders",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(),
                    UserId = table.Column<string>(),
                    Path = table.Column<string>(),
                    ParentFolderId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                    table.ForeignKey(
                        "FK_Folders_Folders_ParentFolderId",
                        x => x.ParentFolderId,
                        "Folders",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Roles",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>()
                },
                constraints: table => { table.PrimaryKey("PK_Roles", x => x.Id); });

            migrationBuilder.CreateTable(
                "Files",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(),
                    AccessLevel = table.Column<bool>(),
                    IsBlocked = table.Column<bool>(),
                    FolderId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        "FK_Files_Folders_FolderId",
                        x => x.FolderId,
                        "Folders",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(),
                    Password = table.Column<string>(),
                    RoleId = table.Column<int>(),
                    MemorySize = table.Column<long>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        "FK_Users_Roles_RoleId",
                        x => x.RoleId,
                        "Roles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Files_FolderId",
                "Files",
                "FolderId");

            migrationBuilder.CreateIndex(
                "IX_Folders_ParentFolderId",
                "Folders",
                "ParentFolderId");

            migrationBuilder.CreateIndex(
                "IX_Users_RoleId",
                "Users",
                "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Files");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "Folders");

            migrationBuilder.DropTable(
                "Roles");
        }
    }
}