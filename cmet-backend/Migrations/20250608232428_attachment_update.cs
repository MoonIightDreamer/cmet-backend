using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cmet_backend.Migrations
{
    /// <inheritdoc />
    public partial class attachment_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_name",
                table: "attachment");

            migrationBuilder.RenameColumn(
                name: "file_path",
                table: "attachment",
                newName: "name");

            migrationBuilder.AddColumn<DateTime>(
                name: "uploaded_at",
                table: "attachment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uploaded_at",
                table: "attachment");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "attachment",
                newName: "file_path");

            migrationBuilder.AddColumn<string>(
                name: "file_name",
                table: "attachment",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
