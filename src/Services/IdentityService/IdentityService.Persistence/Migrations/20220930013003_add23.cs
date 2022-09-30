using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.Persistence.Migrations
{
    public partial class add23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    AuthenticatorType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonRevoked = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OperationClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "ProductOwnerRole" },
                    { 3, "CatalogOwnerRole" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Status" },
                values: new object[,]
                {
                    { 1, 0, "test@gmail.com", "test-admint", "testLast", new byte[] { 109, 69, 10, 143, 32, 154, 232, 142, 105, 66, 112, 86, 110, 79, 250, 240, 169, 41, 76, 64, 228, 172, 226, 135, 140, 44, 133, 231, 0, 215, 174, 144, 213, 173, 161, 8, 141, 111, 127, 168, 242, 148, 148, 129, 234, 199, 128, 99, 7, 231, 166, 38, 128, 147, 33, 65, 118, 73, 178, 131, 145, 211, 86, 31 }, new byte[] { 220, 179, 206, 133, 62, 16, 47, 223, 145, 174, 106, 226, 179, 17, 45, 22, 188, 217, 50, 198, 23, 16, 13, 101, 161, 151, 40, 251, 47, 99, 141, 7, 139, 245, 197, 179, 142, 82, 36, 254, 162, 57, 161, 117, 62, 191, 235, 64, 158, 235, 66, 29, 135, 94, 92, 116, 234, 243, 84, 162, 212, 143, 52, 174, 250, 134, 20, 192, 195, 116, 78, 112, 139, 120, 70, 26, 72, 195, 249, 209, 89, 66, 104, 78, 159, 213, 37, 247, 76, 214, 46, 1, 66, 104, 28, 35, 186, 75, 17, 219, 95, 216, 169, 172, 104, 213, 224, 187, 64, 152, 162, 156, 46, 182, 125, 189, 84, 178, 6, 55, 15, 144, 87, 187, 209, 173, 221, 128 }, true },
                    { 2, 0, "test-catalog@gmail.com", "testCatalowOwner", "testCatalowOwnerlast", new byte[] { 253, 98, 212, 248, 199, 254, 217, 156, 205, 131, 150, 96, 15, 17, 5, 187, 117, 24, 72, 109, 123, 50, 139, 119, 155, 196, 115, 210, 72, 10, 5, 114, 179, 208, 98, 224, 215, 6, 250, 91, 24, 40, 124, 113, 5, 201, 27, 133, 116, 52, 154, 206, 148, 210, 10, 152, 56, 90, 102, 140, 193, 160, 24, 243 }, new byte[] { 194, 166, 238, 167, 4, 121, 206, 216, 142, 120, 144, 76, 66, 63, 100, 16, 170, 47, 254, 115, 24, 208, 191, 234, 184, 174, 37, 112, 16, 176, 214, 150, 153, 92, 173, 115, 80, 53, 191, 0, 27, 142, 135, 177, 253, 91, 215, 212, 174, 230, 149, 229, 179, 24, 16, 144, 236, 28, 186, 195, 32, 86, 58, 170, 77, 35, 61, 120, 164, 226, 211, 165, 236, 240, 165, 19, 213, 198, 193, 71, 124, 202, 9, 214, 201, 60, 234, 223, 245, 59, 104, 66, 248, 147, 69, 164, 154, 155, 56, 159, 114, 205, 35, 44, 169, 235, 20, 177, 194, 58, 73, 37, 7, 7, 185, 39, 60, 201, 63, 254, 170, 45, 14, 36, 196, 125, 161, 23 }, true },
                    { 3, 0, "test-product@gmail.com", "testProductOwner", "testProductOwnerLast", new byte[] { 70, 186, 97, 140, 255, 25, 109, 214, 182, 191, 198, 171, 14, 145, 163, 10, 46, 204, 69, 51, 152, 120, 69, 97, 81, 212, 65, 210, 248, 123, 36, 232, 232, 221, 250, 243, 249, 222, 155, 213, 78, 147, 107, 217, 208, 51, 37, 195, 140, 14, 17, 51, 198, 138, 62, 191, 191, 122, 130, 32, 70, 202, 3, 154 }, new byte[] { 213, 19, 192, 62, 113, 94, 18, 77, 121, 28, 63, 63, 245, 102, 203, 76, 252, 17, 158, 131, 123, 191, 175, 172, 27, 128, 190, 234, 167, 37, 30, 229, 146, 225, 14, 164, 198, 15, 246, 250, 61, 87, 253, 37, 173, 121, 174, 118, 24, 119, 112, 213, 237, 150, 133, 55, 27, 201, 38, 77, 153, 159, 231, 60, 122, 191, 11, 95, 138, 132, 164, 112, 251, 86, 231, 52, 33, 118, 249, 203, 85, 109, 99, 215, 226, 115, 219, 87, 32, 33, 139, 226, 130, 212, 74, 81, 243, 158, 34, 207, 245, 16, 25, 40, 212, 121, 201, 157, 150, 130, 232, 106, 208, 43, 13, 182, 177, 81, 234, 100, 97, 210, 145, 26, 42, 196, 227, 1 }, true }
                });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "OperationClaimId", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "OperationClaimId", "UserId" },
                values: new object[] { 2, 2, 2 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "OperationClaimId", "UserId" },
                values: new object[] { 3, 3, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_OperationClaimId",
                table: "UserOperationClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
