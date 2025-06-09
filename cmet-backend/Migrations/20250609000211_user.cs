using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cmet_backend.Migrations
{
    /// <inheritdoc />
    public partial class user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "\"user\"",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "\"user\"",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "\"user\"",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "\"user\"",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "\"user\"",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "\"user\"");

            migrationBuilder.DropColumn(
                name: "email",
                table: "\"user\"");

            migrationBuilder.DropColumn(
                name: "password",
                table: "\"user\"");

            migrationBuilder.DropColumn(
                name: "role",
                table: "\"user\"");

            migrationBuilder.DropColumn(
                name: "username",
                table: "\"user\"");
        }
    }
}
