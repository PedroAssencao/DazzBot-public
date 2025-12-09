using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chatbot.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Domain.Models;

public partial class Mensagen
{
    [Key]
    [Column("mens_id")]
    public int MensId { get; set; }

    [Column("mens_data", TypeName = "datetime")]
    public DateTime? MensData { get; set; }

    [Column("mens_descricao")]
    [Unicode(false)]
    public string? MensDescricao { get; set; }

    [Column("men_tipo")]
    [Unicode(false)]
    public ETipoMensagem? MenTipo { get; set; }

    [Column("men_WaId")]
    [Unicode(false)]
    public string? mensWaId { get; set; }
    [Column("mens_status")]
    [Unicode(false)]
    public ETipoStatusMensagem? mensStatus { get; set; }
    [Column("con_id")]
    public int? ConId { get; set; }

    [Column("log_id")]
    public int? LogId { get; set; }

    [Column("cha_id")]
    public int? ChaId { get; set; }

    [ForeignKey("ChaId")]
    [InverseProperty("Mensagens")]
    public virtual Chat? Cha { get; set; }

    [ForeignKey("ConId")]
    [InverseProperty("Mensagens")]
    public virtual Contato? Con { get; set; }

    [ForeignKey("LogId")]
    [InverseProperty("Mensagens")]
    public virtual Login? Log { get; set; }
}
