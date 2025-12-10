using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Domain.Models;

[Table("departamento")]
public partial class Departamento
{
    [Key]
    [Column("dep_id")]
    public int DepId { get; set; }

    [Column("dep_descricao")]
    [StringLength(255)]
    [Unicode(false)]
    public string? DepDescricao { get; set; }

    [Column("log_id")]
    public int? LogId { get; set; }

    [InverseProperty("Dep")]
    public virtual ICollection<Atendente> Atendentes { get; set; } = new List<Atendente>();

    [InverseProperty("Dep")]
    public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();

    [ForeignKey("LogId")]
    [InverseProperty("Departamentos")]
    public virtual Login? Log { get; set; }
}
