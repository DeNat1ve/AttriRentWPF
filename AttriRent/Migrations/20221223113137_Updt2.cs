using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttriRent.Migrations
{
    /// <inheritdoc />
    public partial class Updt2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_password_users_u_id",
                table: "user_password");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_users_u_id",
                table: "user_role");

            migrationBuilder.RenameColumn(
                name: "u_id",
                table: "user_role",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_u_id",
                table: "user_role",
                newName: "IX_user_role_user_id");

            migrationBuilder.RenameColumn(
                name: "u_id",
                table: "user_password",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_password_u_id",
                table: "user_password",
                newName: "IX_user_password_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_password_users_user_id",
                table: "user_password",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_users_user_id",
                table: "user_role",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_password_users_user_id",
                table: "user_password");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_users_user_id",
                table: "user_role");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "user_role",
                newName: "u_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_user_id",
                table: "user_role",
                newName: "IX_user_role_u_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "user_password",
                newName: "u_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_password_user_id",
                table: "user_password",
                newName: "IX_user_password_u_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_password_users_u_id",
                table: "user_password",
                column: "u_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_users_u_id",
                table: "user_role",
                column: "u_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
