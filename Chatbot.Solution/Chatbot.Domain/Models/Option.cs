using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chatbot.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Domain.Models;

[Table("options")]
public partial class Option
{
    [Key]
    [Column("opt_id")]
    public int OptId { get; set; }

    [Column("opt_title")]
    [StringLength(24)]
    [Unicode(false)]
    public string? OptTitle { get; set; }

    [Column("opt_descricao")]
    [StringLength(500)]
    [Unicode(false)]
    public string? OptDescricao { get; set; }

    [Column("opt_resposta")]
    [StringLength(500)]
    [Unicode(false)]
    public string? OptResposta { get; set; }

    [Column("opt_data", TypeName = "datetime")]
    public DateTime? OptData { get; set; }

    [Column("opt_finalizar")]
    public bool? OptFinalizar { get; set; }

    [Column("opt_tipo")]
    [Unicode(false)]
    public ETiposDeOptions? OptTipo { get; set; }

    [Column("log_id")]
    public int? LogId { get; set; }

    [Column("men_id")]
    public int? MenId { get; set; }

    [ForeignKey("LogId")]
    [InverseProperty("Options")]
    public virtual Login? Log { get; set; }

    [ForeignKey("MenId")]
    [InverseProperty("Options")]
    public virtual Menu? Men { get; set; }
}
