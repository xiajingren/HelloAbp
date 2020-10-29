using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xhznl.FileManagement.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileManagementFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    FileName = table.Column<string>(maxLength: 256, nullable: false),
                    BlobName = table.Column<string>(maxLength: 128, nullable: false),
                    ByteSize = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileManagementFiles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileManagementFiles");
        }
    }
}
