using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProblemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EquipmentTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProblemTypes_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentTypeId = table.Column<int>(type: "integer", nullable: false),
                    ProblemTypeId = table.Column<int>(type: "integer", nullable: false),
                    RepairStatusId = table.Column<int>(type: "integer", nullable: false),
                    ProblemDescription = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairRequests_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairRequests_ProblemTypes_ProblemTypeId",
                        column: x => x.ProblemTypeId,
                        principalTable: "ProblemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairRequests_RepairStatuses_RepairStatusId",
                        column: x => x.RepairStatusId,
                        principalTable: "RepairStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EquipmentTypes",
                columns: new[] { "Id", "CreatedOn", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(478), "Персональный ПК", null },
                    { 2, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(485), "Смартфоны", null },
                    { 3, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(487), "Умные часы", null },
                    { 4, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(488), "Умные колонки", null },
                    { 5, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(490), "Беспроводные наушники", null },
                    { 6, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(493), "Проводные наушники наушники", null }
                });

            migrationBuilder.InsertData(
                table: "RepairStatuses",
                columns: new[] { "Id", "CreatedOn", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 19, 15, 58, 43, 323, DateTimeKind.Utc).AddTicks(918), "В ожидание", null },
                    { 2, new DateTime(2024, 4, 19, 15, 58, 43, 323, DateTimeKind.Utc).AddTicks(922), "В процессе", null },
                    { 3, new DateTime(2024, 4, 19, 15, 58, 43, 323, DateTimeKind.Utc).AddTicks(924), "Выполнена", null },
                    { 4, new DateTime(2024, 4, 19, 15, 58, 43, 323, DateTimeKind.Utc).AddTicks(926), "Отменена", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOn", "Description", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 19, 15, 58, 43, 323, DateTimeKind.Utc).AddTicks(5206), "admin", "admin", null },
                    { 2, new DateTime(2024, 4, 19, 15, 58, 43, 323, DateTimeKind.Utc).AddTicks(5220), "guest", "guest", null }
                });

            migrationBuilder.InsertData(
                table: "ProblemTypes",
                columns: new[] { "Id", "CreatedOn", "EquipmentTypeId", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4569), 1, "Не работает USB порт", null },
                    { 2, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4575), 1, "Не включается", null },
                    { 3, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4577), 1, "Другое", null },
                    { 4, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4578), 2, "Не идёт зарядка", null },
                    { 5, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4580), 2, "Разбит экран", null },
                    { 6, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4584), 2, "Другое", null },
                    { 7, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4585), 3, "Не идёт зарядка", null },
                    { 8, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4586), 3, "Не работают внешние кнопки", null },
                    { 9, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4588), 3, "Другое", null },
                    { 10, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4590), 4, "Не идёт звук из колонки", null },
                    { 11, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4591), 4, "Не работает Блютуз модуль", null },
                    { 12, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4593), 4, "Другое", null },
                    { 13, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4594), 5, "Не идёт звук из наушников", null },
                    { 14, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4596), 5, "Не работает Блютуз модуль", null },
                    { 15, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4597), 5, "Другое", null },
                    { 16, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4598), 6, "Не идёт звук из наушников", null },
                    { 17, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4600), 6, "Сломан 3.5  мм порт", null },
                    { 18, new DateTime(2024, 4, 19, 15, 58, 43, 321, DateTimeKind.Utc).AddTicks(4602), 6, "Другое", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "Email", "FirstName", "IsActive", "LastName", "MiddleName", "PasswordHash", "RoleId", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("571cd1c2-db6e-48d6-a76b-4d3ae8c4df07"), new DateTime(2024, 4, 19, 15, 58, 43, 620, DateTimeKind.Utc).AddTicks(5168), "guest@example.com", "Имя", true, "Фамилия", "Отчество", "$2a$11$CKFOZT/POUwkqG2BcP4ivucbW9oXhqRMjq1WrnvjWzCDOJmNYFZMS", 2, null },
                    { new Guid("779d694c-82b5-4802-b650-b519a786e64f"), new DateTime(2024, 4, 19, 15, 58, 43, 471, DateTimeKind.Utc).AddTicks(5885), "admin@example.com", "Имя", true, "Фамилия", "Отчество", "$2a$11$3t3TjI4gAR9FT6J4WSy0hOS6vahC.REudOasjMHaWsXjsxFnXkszu", 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypes_Name",
                table: "EquipmentTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProblemTypes_EquipmentTypeId",
                table: "ProblemTypes",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshSessions_Token",
                table: "RefreshSessions",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshSessions_UserId",
                table: "RefreshSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_EquipmentTypeId",
                table: "RepairRequests",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_ProblemTypeId",
                table: "RepairRequests",
                column: "ProblemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_RepairStatusId",
                table: "RepairRequests",
                column: "RepairStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_UserId",
                table: "RepairRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairStatuses_Name",
                table: "RepairStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Description",
                table: "Roles",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshSessions");

            migrationBuilder.DropTable(
                name: "RepairRequests");

            migrationBuilder.DropTable(
                name: "ProblemTypes");

            migrationBuilder.DropTable(
                name: "RepairStatuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
