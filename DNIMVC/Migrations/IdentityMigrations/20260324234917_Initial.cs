#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace DNIMVC.Migrations.IdentityMigrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "ApplicationRole",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                Name = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_ApplicationRole", x => x.Id); });

        migrationBuilder.CreateTable(
            "ApplicationUser",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                FirstName = table.Column<string>("nvarchar(max)", nullable: false),
                LastName = table.Column<string>("nvarchar(max)", nullable: false),
                CreateDate = table.Column<DateTime>("datetime2", nullable: false),
                IsActive = table.Column<bool>("bit", nullable: false),
                UserName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>("bit", nullable: false),
                PasswordHash = table.Column<string>("nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>("nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>("nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>("nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>("bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>("bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>("datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>("bit", nullable: false),
                AccessFailedCount = table.Column<int>("int", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_ApplicationUser", x => x.Id); });

        migrationBuilder.CreateTable(
            "ApplicationRoleClaim",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleId = table.Column<Guid>("uniqueidentifier", nullable: false),
                ClaimType = table.Column<string>("nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationRoleClaim", x => x.Id);
                table.ForeignKey(
                    "FK_ApplicationRoleClaim_ApplicationRole_RoleId",
                    x => x.RoleId,
                    "ApplicationRole",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ApplicationUserClaim",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>("uniqueidentifier", nullable: false),
                ClaimType = table.Column<string>("nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationUserClaim", x => x.Id);
                table.ForeignKey(
                    "FK_ApplicationUserClaim_ApplicationUser_UserId",
                    x => x.UserId,
                    "ApplicationUser",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ApplicationUserLogin",
            table => new
            {
                LoginProvider = table.Column<string>("nvarchar(450)", nullable: false),
                ProviderKey = table.Column<string>("nvarchar(450)", nullable: false),
                ProviderDisplayName = table.Column<string>("nvarchar(max)", nullable: true),
                UserId = table.Column<Guid>("uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    "FK_ApplicationUserLogin_ApplicationUser_UserId",
                    x => x.UserId,
                    "ApplicationUser",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ApplicationUserRole",
            table => new
            {
                UserId = table.Column<Guid>("uniqueidentifier", nullable: false),
                RoleId = table.Column<Guid>("uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationUserRole", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    "FK_ApplicationUserRole_ApplicationRole_RoleId",
                    x => x.RoleId,
                    "ApplicationRole",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_ApplicationUserRole_ApplicationUser_UserId",
                    x => x.UserId,
                    "ApplicationUser",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ApplicationUserToken",
            table => new
            {
                UserId = table.Column<Guid>("uniqueidentifier", nullable: false),
                LoginProvider = table.Column<string>("nvarchar(450)", nullable: false),
                Name = table.Column<string>("nvarchar(450)", nullable: false),
                Value = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    "FK_ApplicationUserToken_ApplicationUser_UserId",
                    x => x.UserId,
                    "ApplicationUser",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "RefreshToken",
            table => new
            {
                RefreshTokenId = table.Column<Guid>("uniqueidentifier", nullable: false),
                ApplicationUserId = table.Column<Guid>("uniqueidentifier", nullable: false),
                Token = table.Column<string>("nvarchar(450)", nullable: false),
                ExpireDate = table.Column<DateTime>("datetime2", nullable: false),
                CreateDate = table.Column<DateTime>("datetime2", nullable: false),
                RevokeDate = table.Column<DateTime>("datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RefreshToken", x => x.RefreshTokenId);
                table.ForeignKey(
                    "FK_RefreshToken_ApplicationUser_ApplicationUserId",
                    x => x.ApplicationUserId,
                    "ApplicationUser",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "RoleNameIndex",
            "ApplicationRole",
            "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            "IX_ApplicationRoleClaim_RoleId",
            "ApplicationRoleClaim",
            "RoleId");

        migrationBuilder.CreateIndex(
            "EmailIndex",
            "ApplicationUser",
            "NormalizedEmail");

        migrationBuilder.CreateIndex(
            "UserNameIndex",
            "ApplicationUser",
            "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            "IX_ApplicationUserClaim_UserId",
            "ApplicationUserClaim",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_ApplicationUserLogin_UserId",
            "ApplicationUserLogin",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_ApplicationUserRole_RoleId",
            "ApplicationUserRole",
            "RoleId");

        migrationBuilder.CreateIndex(
            "IX_RefreshToken_ApplicationUserId",
            "RefreshToken",
            "ApplicationUserId");

        migrationBuilder.CreateIndex(
            "IX_RefreshToken_Token",
            "RefreshToken",
            "Token",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "ApplicationRoleClaim");

        migrationBuilder.DropTable(
            "ApplicationUserClaim");

        migrationBuilder.DropTable(
            "ApplicationUserLogin");

        migrationBuilder.DropTable(
            "ApplicationUserRole");

        migrationBuilder.DropTable(
            "ApplicationUserToken");

        migrationBuilder.DropTable(
            "RefreshToken");

        migrationBuilder.DropTable(
            "ApplicationRole");

        migrationBuilder.DropTable(
            "ApplicationUser");
    }
}