using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Infrastructure.Dtto
{
    public class AtendenteDttoGet
    {
        public int Codigo { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public bool? EstadoAtendente { get; set; }
        public DepartamentoDttoGet? Departamento { get; set; }
        public LoginDttoGetForView? Login { get; set; }
    }

    public class AtendenteDttoForPost
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Imagem { get; set; }
        public bool? EstadoAtendente { get; set; }
        public int? CodigoDepartamento { get; set; }
        public int? CodigoLogin { get; set; }
    }

    public class AtendenteDttoForPut
    {
        public int Codigo { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Imagem { get; set; }
        public bool? EstadoAtendente { get; set; }
        public int? CodigoDepartamento { get; set; }
        public int? CodigoLogin { get; set; }
    }
}
