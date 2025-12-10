using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatbot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migrationinicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "login",
                columns: table => new
                {
                    log_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    log_senha = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    log_img = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    log_plano = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    log_user = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    log_waid = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__login__9E2397E0969F58EA", x => x.log_id);
                });

            migrationBuilder.CreateTable(
                name: "contatos",
                columns: table => new
                {
                    con_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    con_WaId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    con_nome = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    con_DataCadastro = table.Column<DateTime>(type: "datetime", nullable: true),
                    con_BloqueadoStatus = table.Column<bool>(type: "bit", nullable: true),
                    log_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__contatos__081B0F1A19F19A4D", x => x.con_id);
                    table.ForeignKey(
                        name: "FK__contatos__log_id__4BAC3F29",
                        column: x => x.log_id,
                        principalTable: "login",
                        principalColumn: "log_id");
                });

            migrationBuilder.CreateTable(
                name: "departamento",
                columns: table => new
                {
                    dep_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dep_descricao = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    log_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__departam__BB4BD8F8671B8661", x => x.dep_id);
                    table.ForeignKey(
                        name: "FK__departame__log_i__52593CB8",
                        column: x => x.log_id,
                        principalTable: "login",
                        principalColumn: "log_id");
                });

            migrationBuilder.CreateTable(
                name: "menus",
                columns: table => new
                {
                    men_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    men_header = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    men_footer = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    men_body = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    log_id = table.Column<int>(type: "int", nullable: true),
                    men_tipo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    men_title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__menus__387DDE00860AE6CD", x => x.men_id);
                    table.ForeignKey(
                        name: "FK__menus__log_id__5EBF139D",
                        column: x => x.log_id,
                        principalTable: "login",
                        principalColumn: "log_id");
                });

            migrationBuilder.CreateTable(
                name: "atendentes",
                columns: table => new
                {
                    ate_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ate_email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ate_Nome = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ate_img = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ate_senha = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ate_estado = table.Column<bool>(type: "bit", nullable: true),
                    log_id = table.Column<int>(type: "int", nullable: true),
                    dep_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__atendent__895194D66F1FCCAC", x => x.ate_id);
                    table.ForeignKey(
                        name: "FK__atendente__dep_i__5629CD9C",
                        column: x => x.dep_id,
                        principalTable: "departamento",
                        principalColumn: "dep_id");
                    table.ForeignKey(
                        name: "FK__atendente__log_i__5535A963",
                        column: x => x.log_id,
                        principalTable: "login",
                        principalColumn: "log_id");
                });

            migrationBuilder.CreateTable(
                name: "options",
                columns: table => new
                {
                    opt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    men_id = table.Column<int>(type: "int", nullable: true),
                    log_id = table.Column<int>(type: "int", nullable: true),
                    opt_data = table.Column<DateTime>(type: "datetime", nullable: true),
                    opt_descricao = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    opt_finalizar = table.Column<bool>(type: "bit", nullable: true),
                    opt_resposta = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    opt_tipo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    opt_title = table.Column<string>(type: "varchar(24)", unicode: false, maxLength: 24, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__options__84DB9F9BC598D2A7", x => x.opt_id);
                    table.ForeignKey(
                        name: "FK__options__log_id__619B8048",
                        column: x => x.log_id,
                        principalTable: "login",
                        principalColumn: "log_id");
                    table.ForeignKey(
                        name: "FK__options__men_id__628FA481",
                        column: x => x.men_id,
                        principalTable: "menus",
                        principalColumn: "men_id");
                });

            migrationBuilder.CreateTable(
                name: "Atendimento",
                columns: table => new
                {
                    aten_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aten_estado = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    aten_data = table.Column<DateTime>(type: "datetime", nullable: true),
                    ate_id = table.Column<int>(type: "int", nullable: true),
                    dep_id = table.Column<int>(type: "int", nullable: true),
                    con_id = table.Column<int>(type: "int", nullable: true),
                    log_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Atendime__F4B66A40AF98F0D3", x => x.aten_id);
                    table.ForeignKey(
                        name: "FK__Atendimen__ate_i__59063A47",
                        column: x => x.ate_id,
                        principalTable: "atendentes",
                        principalColumn: "ate_id");
                    table.ForeignKey(
                        name: "FK__Atendimen__con_i__5AEE82B9",
                        column: x => x.con_id,
                        principalTable: "contatos",
                        principalColumn: "con_id");
                    table.ForeignKey(
                        name: "FK__Atendimen__dep_i__59FA5E80",
                        column: x => x.dep_id,
                        principalTable: "departamento",
                        principalColumn: "dep_id");
                    table.ForeignKey(
                        name: "FK__Atendimen__log_i__5BE2A6F2",
                        column: x => x.log_id,
                        principalTable: "login",
                        principalColumn: "log_id");
                });

            migrationBuilder.CreateTable(
                name: "chat",
                columns: table => new
                {
                    cha_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ate_id = table.Column<int>(type: "int", nullable: true),
                    log_id = table.Column<int>(type: "int", nullable: true),
                    con_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__chat__5AF8FDEA98C97753", x => x.cha_id);
                    table.ForeignKey(
                        name: "FK__chat__ate_id__5DCAEF64",
                        column: x => x.ate_id,
                        principalTable: "atendentes",
                        principalColumn: "ate_id");
                    table.ForeignKey(
                        name: "FK__chat__con_id__5FB337D6",
                        column: x => x.con_id,
                        principalTable: "contatos",
                        principalColumn: "con_id");
                    table.ForeignKey(
                        name: "FK__chat__log_id__5EBF139D",
                        column: x => x.log_id,
                        principalTable: "login",
                        principalColumn: "log_id");
                });

            migrationBuilder.CreateTable(
                name: "Mensagens",
                columns: table => new
                {
                    mens_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mens_data = table.Column<DateTime>(type: "datetime", nullable: true),
                    mens_descricao = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    men_tipo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    con_id = table.Column<int>(type: "int", nullable: true),
                    log_id = table.Column<int>(type: "int", nullable: true),
                    cha_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mensagen__763E9E0AF88E2D22", x => x.mens_id);
                    table.ForeignKey(
                        name: "FK__Mensagens__cha_i__6477ECF3",
                        column: x => x.cha_id,
                        principalTable: "chat",
                        principalColumn: "cha_id");
                    table.ForeignKey(
                        name: "FK__Mensagens__con_i__628FA481",
                        column: x => x.con_id,
                        principalTable: "contatos",
                        principalColumn: "con_id");
                    table.ForeignKey(
                        name: "FK__Mensagens__log_i__6383C8BA",
                        column: x => x.log_id,
                        principalTable: "login",
                        principalColumn: "log_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_atendentes_dep_id",
                table: "atendentes",
                column: "dep_id");

            migrationBuilder.CreateIndex(
                name: "IX_atendentes_log_id",
                table: "atendentes",
                column: "log_id");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_ate_id",
                table: "Atendimento",
                column: "ate_id");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_con_id",
                table: "Atendimento",
                column: "con_id");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_dep_id",
                table: "Atendimento",
                column: "dep_id");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_log_id",
                table: "Atendimento",
                column: "log_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_ate_id",
                table: "chat",
                column: "ate_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_con_id",
                table: "chat",
                column: "con_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_log_id",
                table: "chat",
                column: "log_id");

            migrationBuilder.CreateIndex(
                name: "IX_contatos_log_id",
                table: "contatos",
                column: "log_id");

            migrationBuilder.CreateIndex(
                name: "IX_departamento_log_id",
                table: "departamento",
                column: "log_id");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_cha_id",
                table: "Mensagens",
                column: "cha_id");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_con_id",
                table: "Mensagens",
                column: "con_id");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_log_id",
                table: "Mensagens",
                column: "log_id");

            migrationBuilder.CreateIndex(
                name: "IX_menus_log_id",
                table: "menus",
                column: "log_id");

            migrationBuilder.CreateIndex(
                name: "IX_options_log_id",
                table: "options",
                column: "log_id");

            migrationBuilder.CreateIndex(
                name: "IX_options_men_id",
                table: "options",
                column: "men_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atendimento");

            migrationBuilder.DropTable(
                name: "Mensagens");

            migrationBuilder.DropTable(
                name: "options");

            migrationBuilder.DropTable(
                name: "chat");

            migrationBuilder.DropTable(
                name: "menus");

            migrationBuilder.DropTable(
                name: "atendentes");

            migrationBuilder.DropTable(
                name: "contatos");

            migrationBuilder.DropTable(
                name: "departamento");

            migrationBuilder.DropTable(
                name: "login");
        }
    }
}
