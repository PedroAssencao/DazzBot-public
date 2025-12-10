using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Domain.Models;

[Table("contatos")]
public partial class Contato
{
    [Key]
    [Column("con_id")]
    public int ConId { get; set; }

    [Column("con_WaId")]
    [StringLength(255)]
    [Unicode(false)]
    public string? ConWaId { get; set; }

    [Column("con_nome")]
    [StringLength(255)]
    [Unicode(false)]
    public string? ConNome { get; set; }

    [Column("con_DataCadastro", TypeName = "datetime")]
    public DateTime? ConDataCadastro { get; set; }

    [Column("con_BloqueadoStatus")]
    public bool? ConBloqueadoStatus { get; set; }

    [Column("log_id")]
    public int? LogId { get; set; }

    [InverseProperty("Con")]
    public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();

    [InverseProperty("Con")]
    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    [ForeignKey("LogId")]
    [InverseProperty("Contatos")]
    public virtual Login? Log { get; set; }

    [InverseProperty("Con")]
    public virtual ICollection<Mensagen> Mensagens { get; set; } = new List<Mensagen>();
}
