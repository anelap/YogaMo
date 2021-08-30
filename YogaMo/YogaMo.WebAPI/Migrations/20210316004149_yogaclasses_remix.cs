using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YogaMo.WebAPI.Migrations
{
    public partial class yogaclasses_remix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YogaInstructor",
                table: "Yoga");

            migrationBuilder.DropIndex(
                name: "IX_Yoga_InstructorId",
                table: "Yoga");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "YogaClass");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Yoga");

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "YogaClass",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeFrom",
                table: "YogaClass",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeTo",
                table: "YogaClass",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_YogaClass_InstructorId",
                table: "YogaClass",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_YogaClassInstructor",
                table: "YogaClass",
                column: "InstructorId",
                principalTable: "Instructor",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YogaClassInstructor",
                table: "YogaClass");

            migrationBuilder.DropIndex(
                name: "IX_YogaClass_InstructorId",
                table: "YogaClass");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "YogaClass");

            migrationBuilder.DropColumn(
                name: "TimeFrom",
                table: "YogaClass");

            migrationBuilder.DropColumn(
                name: "TimeTo",
                table: "YogaClass");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "YogaClass",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Yoga",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Yoga_InstructorId",
                table: "Yoga",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_YogaInstructor",
                table: "Yoga",
                column: "InstructorId",
                principalTable: "Instructor",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
