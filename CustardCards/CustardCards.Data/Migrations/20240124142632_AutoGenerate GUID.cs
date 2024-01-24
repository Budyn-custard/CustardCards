using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustardCards.Data.Migrations
{
    /// <inheritdoc />
    public partial class AutoGenerateGUID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("37b9cb06-18bf-410c-8eb3-d7ec9d48f2a5"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("37b9cb06-18bf-410c-8eb3-d7ec9d48f2a5"));
        }
    }
}
