using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chatbot.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Domain.Models;

[Table("Atendimento")]
public partial class Atendimento
{
    [Key]
    [Column("aten_id")]
    public int AtenId { get; set; }

    [Column("aten_estado")]
    [Unicode(false)]
    public EEstadoAtendimento? AtenEstado { get; set; }

    [Column("aten_data", TypeName = "datetime")]
    public DateTime? AtenData { get; set; }

    [Column("ate_id")]
    public int? AteId { get; set; }

    [Column("dep_id")]
    public int? DepId { get; set; }

    [Column("con_id")]
    public int? ConId { get; set; }

    [Column("log_id")]
    public int? LogId { get; set; }

    [ForeignKey("AteId")]
    [InverseProperty("Atendimentos")]
    public virtual Atendente? Ate { get; set; }

    [InverseProperty("Aten")]
    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    [ForeignKey("ConId")]
    [InverseProperty("Atendimentos")]
    public virtual Contato? Con { get; set; }

    [ForeignKey("DepId")]
    [InverseProperty("Atendimentos")]
    public virtual Departamento? Dep { get; set; }

    [ForeignKey("LogId")]
    [InverseProperty("Atendimentos")]
    public virtual Login? Log { get; set; }
}
