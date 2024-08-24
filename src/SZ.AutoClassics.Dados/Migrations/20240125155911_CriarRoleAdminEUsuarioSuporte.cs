using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using SZ.AutoClassics.Dominio.Models;

#nullable disable

namespace SZ.AutoClassics.Dados.Migrations
{
    public partial class CriarRoleAdminEUsuarioSuporte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();

			migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('f9beb1b0-559c-4811-9fd8-406c6aa3fedd', 'Admin', 'ADMIN')");
			migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('339d0610-6923-42fa-bbda-4adc829ff345', 'Membro', 'MEMBRO')");
			migrationBuilder.Sql($"INSERT INTO AspNetUsers (Id, Bloqueado, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockOutEnabled, AccessFailedCount) " +
								 $"VALUES ('b1af5356-7c99-4328-ab1f-424199fcc32c', 0, 'Suporte', 'SUPORTE', 'suporte@gmail.com', 'SUPORTE@GMAIL.COM', 1," +
								 $" '{passwordHasher.HashPassword(null, "Suporte@2023")}', '{Guid.NewGuid()}', 0, 0, 0, 0)");

			migrationBuilder.Sql("INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('b1af5356-7c99-4328-ab1f-424199fcc32c', 'f9beb1b0-559c-4811-9fd8-406c6aa3fedd')");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DELETE FROM AspNetUserRoles WHERE UserId = 'b1af5356-7c99-4328-ab1f-424199fcc32c' AND RoleId = 'f9beb1b0-559c-4811-9fd8-406c6aa3fedd'");
			migrationBuilder.Sql("DELETE FROM AspNetUsers WHERE Id = 'b1af5356-7c99-4328-ab1f-424199fcc32c'");
			migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Id = '339d0610-6923-42fa-bbda-4adc829ff345'");
			migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Id = 'f9beb1b0-559c-4811-9fd8-406c6aa3fedd'");
		}
    }
}
