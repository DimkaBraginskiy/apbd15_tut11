using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apbd11.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDescriptionFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Prescription_Medicament",
                newName: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "Dose",
                table: "Prescription_Medicament",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Prescription_Medicament",
                newName: "Details");

            migrationBuilder.AlterColumn<int>(
                name: "Dose",
                table: "Prescription_Medicament",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
