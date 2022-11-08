using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronos.Domain.Entities;

    public class Tarefa
    {
        public int TarefaId { get; set; }
        public int Usuario_ProjetoId { get; set; }
        public DateTime DataInicial { get; set; }  
        public DateTime DataFinal { get; set; }
        public Usuario_Projeto Usuario_Projeto { get; set;}

        
    }
