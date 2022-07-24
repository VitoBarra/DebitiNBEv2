using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace DebitiNBE_API.Migrations
{
    public partial class Debitimigratin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    Username = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    Password = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 45, nullable: true),
                    Lastname = table.Column<string>(unicode: false, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "friendlist",
                columns: table => new
                {
                    user_ID = table.Column<int>(type: "int(11)", nullable: false),
                    user_ID1 = table.Column<int>(type: "int(11)", nullable: false),
                    Stato = table.Column<string>(type: "enum('active','accepted','ignored','blocked')", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.user_ID, x.user_ID1 });
                    table.ForeignKey(
                        name: "fk_user_has_user_user",
                        column: x => x.user_ID,
                        principalTable: "user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_has_user_user1",
                        column: x => x.user_ID1,
                        principalTable: "user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "request",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ID_mandante = table.Column<int>(type: "int(11)", nullable: false),
                    ID_ricevente = table.Column<int>(type: "int(11)", nullable: false),
                    Credito = table.Column<float>(nullable: false),
                    Stato = table.Column<string>(type: "enum('waiting','accepted','dennied','completed')", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_request", x => x.ID);
                    table.ForeignKey(
                        name: "fk_Request_user1",
                        column: x => x.ID_mandante,
                        principalTable: "user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_Request_user2",
                        column: x => x.ID_ricevente,
                        principalTable: "user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "paymentrequest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Request_ID = table.Column<int>(type: "int(11)", nullable: false),
                    Credito = table.Column<float>(nullable: false),
                    Stato = table.Column<string>(type: "enum('active','accepted','dennied')", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentrequest", x => x.ID);
                    table.ForeignKey(
                        name: "fk_PaymentRequest_Request1",
                        column: x => x.Request_ID,
                        principalTable: "request",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "fk_user_has_user_user_idx",
                table: "friendlist",
                column: "user_ID");

            migrationBuilder.CreateIndex(
                name: "fk_user_has_user_user1_idx",
                table: "friendlist",
                column: "user_ID1");

            migrationBuilder.CreateIndex(
                name: "fk_PaymentRequest_Request1_idx",
                table: "paymentrequest",
                column: "Request_ID");

            migrationBuilder.CreateIndex(
                name: "fk_Request_user1_idx",
                table: "request",
                column: "ID_mandante");

            migrationBuilder.CreateIndex(
                name: "fk_Request_user2_idx",
                table: "request",
                column: "ID_ricevente");

            migrationBuilder.CreateIndex(
                name: "Email_UNIQUE",
                table: "user",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Username_UNIQUE",
                table: "user",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friendlist");

            migrationBuilder.DropTable(
                name: "paymentrequest");

            migrationBuilder.DropTable(
                name: "request");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
