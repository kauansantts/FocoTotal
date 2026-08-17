using System;
using System.Collections.Generic;
using System.Text;

namespace FocoTotal.Models
{
    public class Usuario
    {
        public string NomeUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public Tarefa tarefa = new Tarefa();
        public List<Tarefa> tarefas = new List<Tarefa>();
    }
}
