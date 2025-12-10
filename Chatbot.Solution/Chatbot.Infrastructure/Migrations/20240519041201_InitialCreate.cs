using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatbot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__atendente__dep_i__5629CD9C",
                table: "atendentes");

            migrationBuilder.DropForeignKey(
                name: "FK__atendente__log_i__5535A963",
                table: "atendentes");

            migrationBuilder.DropForeignKey(
                name: "FK__Atendimen__ate_i__59063A47",
                table: "Atendimento");

            migrationBuilder.DropForeignKey(
                name: "FK__Atendimen__con_i__5AEE82B9",
                table: "Atendimento");

            migrationBuilder.DropForeignKey(
                name: "FK__Atendimen__dep_i__59FA5E80",
                table: "Atendimento");

            migrationBuilder.DropForeignKey(
                name: "FK__Atendimen__log_i__5BE2A6F2",
                table: "Atendimento");

            migrationBuilder.DropForeignKey(
                name: "FK__chat__ate_id__5DCAEF64",
                table: "chat");

            migrationBuilder.DropForeignKey(
                name: "FK__chat__con_id__5FB337D6",
                table: "chat");

            migrationBuilder.DropForeignKey(
                name: "FK__chat__log_id__5EBF139D",
                table: "chat");

            migrationBuilder.DropForeignKey(
                name: "FK__contatos__log_id__4BAC3F29",
                table: "contatos");

            migrationBuilder.DropForeignKey(
                name: "FK__departame__log_i__52593CB8",
                table: "departamento");

            migrationBuilder.DropForeignKey(
                name: "FK__Mensagens__cha_i__6477ECF3",
                table: "Mensagens");

            migrationBuilder.DropForeignKey(
                name: "FK__Mensagens__con_i__628FA481",
                table: "Mensagens");

            migrationBuilder.DropForeignKey(
                name: "FK__Mensagens__log_i__6383C8BA",
                table: "Mensagens");

            migrationBuilder.DropForeignKey(
                name: "FK__menus__log_id__5EBF139D",
                table: "menus");

            migrationBuilder.DropForeignKey(
                name: "FK__options__log_id__619B8048",
                table: "options");

            migrationBuilder.DropForeignKey(
                name: "FK__options__men_id__628FA481",
                table: "options");

            migrationBuilder.DropPrimaryKey(
                name: "PK__options__84DB9F9BC598D2A7",
                table: "options");

            migrationBuilder.DropPrimaryKey(
                name: "PK__menus__387DDE00860AE6CD",
                table: "menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Mensagen__763E9E0AF88E2D22",
                table: "Mensagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK__login__9E2397E0969F58EA",
                table: "login");

            migrationBuilder.DropPrimaryKey(
                name: "PK__departam__BB4BD8F8671B8661",
                table: "departamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK__contatos__081B0F1A19F19A4D",
                table: "contatos");

            migrationBuilder.DropPrimaryKey(
                name: "PK__chat__5AF8FDEA98C97753",
                table: "chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Atendime__F4B66A40AF98F0D3",
                table: "Atendimento");

            migrationBuilder.DropPrimaryKey(
                name: "PK__atendent__895194D66F1FCCAC",
                table: "atendentes");

            migrationBuilder.AddColumn<int>(
                name: "aten_id",
                table: "chat",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__options__84DB9F9B4CD6FC31",
                table: "options",
                column: "opt_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__menus__387DDE002DE1152B",
                table: "menus",
                column: "men_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Mensagen__763E9E0AC3136BFE",
                table: "Mensagens",
                column: "mens_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__login__9E2397E023C6542C",
                table: "login",
                column: "log_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__departam__BB4BD8F8D55B4951",
                table: "departamento",
                column: "dep_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__contatos__081B0F1A9391D70E",
                table: "contatos",
                column: "con_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__chat__5AF8FDEA92B3BDD0",
                table: "chat",
                column: "cha_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Atendime__F4B66A4080C48752",
                table: "Atendimento",
                column: "aten_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__atendent__895194D674414920",
                table: "atendentes",
                column: "ate_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_aten_id",
                table: "chat",
                column: "aten_id");

            migrationBuilder.AddForeignKey(
                name: "FK__atendente__dep_i__403A8C7D",
                table: "atendentes",
                column: "dep_id",
                principalTable: "departamento",
                principalColumn: "dep_id");

            migrationBuilder.AddForeignKey(
                name: "FK__atendente__log_i__3F466844",
                table: "atendentes",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Atendimen__ate_i__4316F928",
                table: "Atendimento",
                column: "ate_id",
                principalTable: "atendentes",
                principalColumn: "ate_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Atendimen__con_i__44FF419A",
                table: "Atendimento",
                column: "con_id",
                principalTable: "contatos",
                principalColumn: "con_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Atendimen__dep_i__440B1D61",
                table: "Atendimento",
                column: "dep_id",
                principalTable: "departamento",
                principalColumn: "dep_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Atendimen__log_i__45F365D3",
                table: "Atendimento",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat__ate_id__48CFD27E",
                table: "chat",
                column: "ate_id",
                principalTable: "atendentes",
                principalColumn: "ate_id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat__aten_id__4BAC3F29",
                table: "chat",
                column: "aten_id",
                principalTable: "Atendimento",
                principalColumn: "aten_id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat__con_id__4AB81AF0",
                table: "chat",
                column: "con_id",
                principalTable: "contatos",
                principalColumn: "con_id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat__log_id__49C3F6B7",
                table: "chat",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__contatos__log_id__398D8EEE",
                table: "contatos",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__departame__log_i__3C69FB99",
                table: "departamento",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Mensagens__cha_i__5070F446",
                table: "Mensagens",
                column: "cha_id",
                principalTable: "chat",
                principalColumn: "cha_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Mensagens__con_i__4E88ABD4",
                table: "Mensagens",
                column: "con_id",
                principalTable: "contatos",
                principalColumn: "con_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Mensagens__log_i__4F7CD00D",
                table: "Mensagens",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__menus__log_id__534D60F1",
                table: "menus",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__options__log_id__5629CD9C",
                table: "options",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__options__men_id__571DF1D5",
                table: "options",
                column: "men_id",
                principalTable: "menus",
                principalColumn: "men_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__atendente__dep_i__403A8C7D",
                table: "atendentes");

            migrationBuilder.DropForeignKey(
                name: "FK__atendente__log_i__3F466844",
                table: "atendentes");

            migrationBuilder.DropForeignKey(
                name: "FK__Atendimen__ate_i__4316F928",
                table: "Atendimento");

            migrationBuilder.DropForeignKey(
                name: "FK__Atendimen__con_i__44FF419A",
                table: "Atendimento");

            migrationBuilder.DropForeignKey(
                name: "FK__Atendimen__dep_i__440B1D61",
                table: "Atendimento");

            migrationBuilder.DropForeignKey(
                name: "FK__Atendimen__log_i__45F365D3",
                table: "Atendimento");

            migrationBuilder.DropForeignKey(
                name: "FK__chat__ate_id__48CFD27E",
                table: "chat");

            migrationBuilder.DropForeignKey(
                name: "FK__chat__aten_id__4BAC3F29",
                table: "chat");

            migrationBuilder.DropForeignKey(
                name: "FK__chat__con_id__4AB81AF0",
                table: "chat");

            migrationBuilder.DropForeignKey(
                name: "FK__chat__log_id__49C3F6B7",
                table: "chat");

            migrationBuilder.DropForeignKey(
                name: "FK__contatos__log_id__398D8EEE",
                table: "contatos");

            migrationBuilder.DropForeignKey(
                name: "FK__departame__log_i__3C69FB99",
                table: "departamento");

            migrationBuilder.DropForeignKey(
                name: "FK__Mensagens__cha_i__5070F446",
                table: "Mensagens");

            migrationBuilder.DropForeignKey(
                name: "FK__Mensagens__con_i__4E88ABD4",
                table: "Mensagens");

            migrationBuilder.DropForeignKey(
                name: "FK__Mensagens__log_i__4F7CD00D",
                table: "Mensagens");

            migrationBuilder.DropForeignKey(
                name: "FK__menus__log_id__534D60F1",
                table: "menus");

            migrationBuilder.DropForeignKey(
                name: "FK__options__log_id__5629CD9C",
                table: "options");

            migrationBuilder.DropForeignKey(
                name: "FK__options__men_id__571DF1D5",
                table: "options");

            migrationBuilder.DropPrimaryKey(
                name: "PK__options__84DB9F9B4CD6FC31",
                table: "options");

            migrationBuilder.DropPrimaryKey(
                name: "PK__menus__387DDE002DE1152B",
                table: "menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Mensagen__763E9E0AC3136BFE",
                table: "Mensagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK__login__9E2397E023C6542C",
                table: "login");

            migrationBuilder.DropPrimaryKey(
                name: "PK__departam__BB4BD8F8D55B4951",
                table: "departamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK__contatos__081B0F1A9391D70E",
                table: "contatos");

            migrationBuilder.DropPrimaryKey(
                name: "PK__chat__5AF8FDEA92B3BDD0",
                table: "chat");

            migrationBuilder.DropIndex(
                name: "IX_chat_aten_id",
                table: "chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Atendime__F4B66A4080C48752",
                table: "Atendimento");

            migrationBuilder.DropPrimaryKey(
                name: "PK__atendent__895194D674414920",
                table: "atendentes");

            migrationBuilder.DropColumn(
                name: "aten_id",
                table: "chat");

            migrationBuilder.AddPrimaryKey(
                name: "PK__options__84DB9F9BC598D2A7",
                table: "options",
                column: "opt_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__menus__387DDE00860AE6CD",
                table: "menus",
                column: "men_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Mensagen__763E9E0AF88E2D22",
                table: "Mensagens",
                column: "mens_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__login__9E2397E0969F58EA",
                table: "login",
                column: "log_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__departam__BB4BD8F8671B8661",
                table: "departamento",
                column: "dep_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__contatos__081B0F1A19F19A4D",
                table: "contatos",
                column: "con_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__chat__5AF8FDEA98C97753",
                table: "chat",
                column: "cha_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Atendime__F4B66A40AF98F0D3",
                table: "Atendimento",
                column: "aten_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__atendent__895194D66F1FCCAC",
                table: "atendentes",
                column: "ate_id");

            migrationBuilder.AddForeignKey(
                name: "FK__atendente__dep_i__5629CD9C",
                table: "atendentes",
                column: "dep_id",
                principalTable: "departamento",
                principalColumn: "dep_id");

            migrationBuilder.AddForeignKey(
                name: "FK__atendente__log_i__5535A963",
                table: "atendentes",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Atendimen__ate_i__59063A47",
                table: "Atendimento",
                column: "ate_id",
                principalTable: "atendentes",
                principalColumn: "ate_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Atendimen__con_i__5AEE82B9",
                table: "Atendimento",
                column: "con_id",
                principalTable: "contatos",
                principalColumn: "con_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Atendimen__dep_i__59FA5E80",
                table: "Atendimento",
                column: "dep_id",
                principalTable: "departamento",
                principalColumn: "dep_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Atendimen__log_i__5BE2A6F2",
                table: "Atendimento",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat__ate_id__5DCAEF64",
                table: "chat",
                column: "ate_id",
                principalTable: "atendentes",
                principalColumn: "ate_id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat__con_id__5FB337D6",
                table: "chat",
                column: "con_id",
                principalTable: "contatos",
                principalColumn: "con_id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat__log_id__5EBF139D",
                table: "chat",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__contatos__log_id__4BAC3F29",
                table: "contatos",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__departame__log_i__52593CB8",
                table: "departamento",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Mensagens__cha_i__6477ECF3",
                table: "Mensagens",
                column: "cha_id",
                principalTable: "chat",
                principalColumn: "cha_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Mensagens__con_i__628FA481",
                table: "Mensagens",
                column: "con_id",
                principalTable: "contatos",
                principalColumn: "con_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Mensagens__log_i__6383C8BA",
                table: "Mensagens",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__menus__log_id__5EBF139D",
                table: "menus",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__options__log_id__619B8048",
                table: "options",
                column: "log_id",
                principalTable: "login",
                principalColumn: "log_id");

            migrationBuilder.AddForeignKey(
                name: "FK__options__men_id__628FA481",
                table: "options",
                column: "men_id",
                principalTable: "menus",
                principalColumn: "men_id");
        }
    }
}
