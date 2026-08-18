using FocoTotal.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using FocoTotal.Services;
using FocoTotal.Exceptions;

namespace FocoTotal.Models
{
    public class Usuario
    {
        public string NomeUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public List<Tarefa> tarefas = new List<Tarefa>();

        public Usuario(string nomeUsuario, string senhaUsuario)
        {
            NomeUsuario = nomeUsuario;
            SenhaUsuario = senhaUsuario;
        }

        public void AddTarefa()
        {
            Console.Write("Titulo: ");
            var titulotarefa = Console.ReadLine();
            

        }
        
        
        public void ExibirTarefas()
        {
            Menu.Linha("Tarefas");

            foreach (var tarefa in tarefas)
            {
                Console.WriteLine($"{tarefa.TituloTarefa}");
                Console.WriteLine($"{tarefa.DescricaoTarefa}");
                Console.WriteLine($"{tarefa.TipoPrioridade}");
                Console.WriteLine($"{tarefa.DataTarefa}");
            }

            Menu.Linha("------");
        }

        public void TarefasPersonalizadas()
        {
            //metodos LINQs
        }
    }
}
