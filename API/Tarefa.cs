using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Tarefa
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public bool IsComplete { get; set; }
    }
    public class TarefaDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public bool IsComplete { get; set; }
    }
}
