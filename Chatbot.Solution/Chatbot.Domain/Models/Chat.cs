using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Domain.Models;

[Table("chat")]
public partial class Chat
{
    [Key]
    [Column("cha_id")]
    public int ChaId { get; set; }

    [Column("ate_id")]
    public int? AteId { get; set; }

    [Column("log_id")]
    public int? LogId { get; set; }

    [Column("con_id")]
    public int? ConId { get; set; }

    [Column("aten_id")]
    public int? AtenId { get; set; }

    [ForeignKey("AteId")]
    [InverseProperty("Chats")]
    public virtual Atendente? Ate { get; set; }

    [ForeignKey("AtenId")]
    [InverseProperty("Chats")]
    public virtual Atendimento? Aten { get; set; }

    [ForeignKey("ConId")]
    [InverseProperty("Chats")]
    public virtual Contato? Con { get; set; }

    [ForeignKey("LogId")]
    [InverseProperty("Chats")]
    public virtual Login? Log { get; set; }

    [InverseProperty("Cha")]
    public virtual ICollection<Mensagen> Mensagens { get; set; } = new List<Mensagen>();
}
