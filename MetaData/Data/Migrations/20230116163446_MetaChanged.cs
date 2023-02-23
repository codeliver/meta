using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetaData.Data.Migrations
{
    public partial class MetaChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File_Type",
                table: "MetaDataTable",
                newName: "ResponseCode");

            migrationBuilder.RenameColumn(
                name: "File_Name",
                table: "MetaDataTable",
                newName: "ImageURL");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateGenerated",
                table: "MetaDataTable",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "MetaDataTable",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateGenerated",
                table: "MetaDataTable");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "MetaDataTable");

            migrationBuilder.RenameColumn(
                name: "ResponseCode",
                table: "MetaDataTable",
                newName: "File_Type");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "MetaDataTable",
                newName: "File_Name");
        }
    }
}
