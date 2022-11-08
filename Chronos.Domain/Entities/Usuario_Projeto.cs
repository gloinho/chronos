using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronos.Domain.Entities;

    public class Usuario_Projeto
    {
        public int Usuario_ProjetoId { get; set; }
        public int UsuarioId { get; set; }
        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }
        public Usuario Usuario { get; set; }
    }
