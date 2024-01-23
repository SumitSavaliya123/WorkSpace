using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gender",
                columns: table => new
                {
                    id = table.Column<byte>(type: "tinyint", nullable: false),
                    title = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender_Id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(name: "first_name", type: "varchar(16)", maxLength: 16, nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "varchar(16)", maxLength: 16, nullable: false),
                    email = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    phonenumber = table.Column<string>(name: "phone_number", type: "varchar(13)", maxLength: 13, nullable: false),
                    address = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    dob = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    role = table.Column<byte>(type: "tinyint", nullable: false),
                    gender = table.Column<byte>(type: "tinyint", nullable: true),
                    avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    otp = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: true),
                    expirytime = table.Column<DateTimeOffset>(name: "expiry_time", type: "datetimeoffset", nullable: true),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    createdon = table.Column<DateTimeOffset>(name: "created_on", type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    updatedon = table.Column<DateTimeOffset>(name: "updated_on", type: "datetimeoffset", nullable: true),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_gender_gender",
                        column: x => x.gender,
                        principalTable: "gender",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_user_created_by",
                        column: x => x.createdby,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_user_updated_by",
                        column: x => x.updatedby,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "userRefreshToken",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    refreshtoken = table.Column<string>(name: "refresh_token", type: "nvarchar(max)", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: false),
                    createdon = table.Column<DateTimeOffset>(name: "created_on", type: "datetimeoffset", nullable: false),
                    updatedon = table.Column<DateTimeOffset>(name: "updated_on", type: "datetimeoffset", nullable: true),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRefreshToken", x => x.id);
                    table.ForeignKey(
                        name: "FK_userRefreshToken_user_created_by",
                        column: x => x.createdby,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_userRefreshToken_user_updated_by",
                        column: x => x.updatedby,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "gender",
                columns: new[] { "id", "title" },
                values: new object[,]
                {
                    { (byte)1, "Male" },
                    { (byte)2, "Female" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_gender_title",
                table: "gender",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_created_by",
                table: "user",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_gender",
                table: "user",
                column: "gender");

            migrationBuilder.CreateIndex(
                name: "IX_user_updated_by",
                table: "user",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_userRefreshToken_created_by",
                table: "userRefreshToken",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_userRefreshToken_id",
                table: "userRefreshToken",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_userRefreshToken_updated_by",
                table: "userRefreshToken",
                column: "updated_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userRefreshToken");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "gender");
        }
    }
}
