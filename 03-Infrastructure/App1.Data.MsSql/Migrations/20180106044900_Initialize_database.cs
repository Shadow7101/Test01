using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace App1.Data.MsSql.Migrations
{
    public partial class Initialize_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderId = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "LogTypes",
                columns: table => new
                {
                    LogTypeId = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTypes", x => x.LogTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TokenTypes",
                columns: table => new
                {
                    TypeId = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenTypes", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedIp = table.Column<string>(maxLength: 30, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Email = table.Column<string>(maxLength: 120, nullable: true),
                    LogTypeId = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_Logs_LogTypes_LogTypeId",
                        column: x => x.LogTypeId,
                        principalTable: "LogTypes",
                        principalColumn: "LogTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Avaliation = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Banned = table.Column<bool>(nullable: false),
                    BannedBy = table.Column<Guid>(nullable: true),
                    BannedIp = table.Column<string>(maxLength: 30, nullable: true),
                    BannedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Bloqued = table.Column<bool>(nullable: false),
                    BloquedBy = table.Column<Guid>(nullable: true),
                    BloquedIp = table.Column<string>(maxLength: 30, nullable: true),
                    BloquedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedIp = table.Column<string>(maxLength: 30, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    DeletedIp = table.Column<string>(maxLength: 30, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Email = table.Column<string>(maxLength: 120, nullable: false),
                    EmailValidaded = table.Column<bool>(nullable: false),
                    GenderId = table.Column<byte>(nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastAccessIp = table.Column<string>(maxLength: 30, nullable: true),
                    ModifyBy = table.Column<Guid>(nullable: true),
                    ModifyIp = table.Column<string>(maxLength: 30, nullable: true),
                    ModifyOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    RoleId = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    TokenId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedIp = table.Column<string>(maxLength: 30, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TokenData = table.Column<string>(type: "text", nullable: true),
                    TypeId = table.Column<byte>(nullable: false),
                    Validate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_Tokens_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tokens_TokenTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TokenTypes",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_LogTypeId",
                table: "Logs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_CreatedBy",
                table: "Tokens",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_TypeId",
                table: "Tokens",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "LogTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TokenTypes");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
