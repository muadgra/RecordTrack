using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordTrack.Persistance.Migrations
{
    public partial class mig_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Records_RecordId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_RecordId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "Files");

            migrationBuilder.CreateTable(
                name: "RecordRecordImageFile",
                columns: table => new
                {
                    RecordImageFilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordRecordImageFile", x => new { x.RecordImageFilesId, x.RecordsId });
                    table.ForeignKey(
                        name: "FK_RecordRecordImageFile_Files_RecordImageFilesId",
                        column: x => x.RecordImageFilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordRecordImageFile_Records_RecordsId",
                        column: x => x.RecordsId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordRecordImageFile_RecordsId",
                table: "RecordRecordImageFile",
                column: "RecordsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordRecordImageFile");

            migrationBuilder.AddColumn<Guid>(
                name: "RecordId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_RecordId",
                table: "Files",
                column: "RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Records_RecordId",
                table: "Files",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
