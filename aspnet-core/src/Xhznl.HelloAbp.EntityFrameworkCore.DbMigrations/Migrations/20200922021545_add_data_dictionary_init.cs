using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xhznl.HelloAbp.Migrations
{
    public partial class add_data_dictionary_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbpDataDictionary",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    Sort = table.Column<short>(nullable: false, defaultValue: (short)0),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpDataDictionary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpDataDictionaryDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Pid = table.Column<Guid>(nullable: false),
                    Label = table.Column<string>(maxLength: 50, nullable: false),
                    Value = table.Column<string>(maxLength: 256, nullable: false),
                    Sort = table.Column<short>(nullable: false, defaultValue: (short)0),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpDataDictionaryDetail", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpDataDictionary_Name",
                table: "AbpDataDictionary",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDataDictionaryDetail_Pid",
                table: "AbpDataDictionaryDetail",
                column: "Pid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpDataDictionary");

            migrationBuilder.DropTable(
                name: "AbpDataDictionaryDetail");
        }
    }
}
