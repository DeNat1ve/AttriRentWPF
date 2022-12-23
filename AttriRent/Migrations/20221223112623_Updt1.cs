using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttriRent.Migrations
{
    /// <inheritdoc />
    public partial class Updt1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_user_password_password_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_user_role_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_password_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_role_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "password_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "u_id",
                table: "user_role",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "u_id",
                table: "user_password",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_user_role_u_id",
                table: "user_role",
                column: "u_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_password_u_id",
                table: "user_password",
                column: "u_id",
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_password_users_u_id",
                table: "user_password");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_users_u_id",
                table: "user_role");

            migrationBuilder.DropIndex(
                name: "IX_user_role_u_id",
                table: "user_role");

            migrationBuilder.DropIndex(
                name: "IX_user_password_u_id",
                table: "user_password");

            migrationBuilder.DropColumn(
                name: "u_id",
                table: "user_role");

            migrationBuilder.DropColumn(
                name: "u_id",
                table: "user_password");

            migrationBuilder.AddColumn<int>(
                name: "password_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_users_password_id",
                table: "users",
                column: "password_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_user_password_password_id",
                table: "users",
                column: "password_id",
                principalTable: "user_password",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_user_role_role_id",
                table: "users",
                column: "role_id",
                principalTable: "user_role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
