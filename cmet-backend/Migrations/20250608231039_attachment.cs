using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cmet_backend.Migrations
{
    /// <inheritdoc />
    public partial class attachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attachment_article_fk_attachment_to_content",
                table: "attachment");

            migrationBuilder.DropIndex(
                name: "IX_attachment_fk_attachment_to_content",
                table: "attachment");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "attachment");

            migrationBuilder.DropColumn(
                name: "fk_attachment_to_content",
                table: "attachment");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "attachment",
                newName: "reference_type");

            migrationBuilder.AddColumn<string>(
                name: "file_name",
                table: "attachment",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "file_path",
                table: "attachment",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "reference_id",
                table: "attachment",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_name",
                table: "attachment");

            migrationBuilder.DropColumn(
                name: "file_path",
                table: "attachment");

            migrationBuilder.DropColumn(
                name: "reference_id",
                table: "attachment");

            migrationBuilder.RenameColumn(
                name: "reference_type",
                table: "attachment",
                newName: "name");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "attachment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "fk_attachment_to_content",
                table: "attachment",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_attachment_fk_attachment_to_content",
                table: "attachment",
                column: "fk_attachment_to_content");

            migrationBuilder.AddForeignKey(
                name: "FK_attachment_article_fk_attachment_to_content",
                table: "attachment",
                column: "fk_attachment_to_content",
                principalTable: "article",
                principalColumn: "id");
        }
    }
}
