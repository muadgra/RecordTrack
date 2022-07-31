using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordTrack.Persistance.Migrations
{
    public partial class mig_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Width",
                table: "Files");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Files",
                type: "int",
                nullable: true);
        }
    }
}
