using FocoTotal.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using FocoTotal.Services;
using FocoTotal.Exceptions;
using FocoTotal.Enums;
using System.Linq;

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
            Console.Write("Descrição: ");
            var descricaotarefa = Console.ReadLine();
            DateTime datatarefa = DateTime.Now;
            
            Menu.MenuOpc("Urgente", "Media", "Normal", "Baixa");
            Console.Write("Qual nivel de prioridade da sua tarefa: ");
            int.TryParse(Console.ReadLine(), out int entrada);


            var prioridadetarefa = entrada switch//Switch expression!
            {
                1 => EnumPrioridade.Urgente,
                2 => EnumPrioridade.Media,
                3 => EnumPrioridade.Normal,
                4 or _ => EnumPrioridade.Baixa
            };
            
            //gerando ID da tarefa unico com uso de LINQ
            var idtarefa = new Random().Next(1, 200);
            while(tarefas.Any(tarefa => tarefa.IdTarefa == idtarefa))
            {
                var novoidtarefa = new Random().Next(1, 200);
                idtarefa = novoidtarefa;
            }
            Tarefa tarefa = new Tarefa(titulotarefa, descricaotarefa, prioridadetarefa, datatarefa, idtarefa);
            tarefas.Add(tarefa);
            Console.WriteLine($"O ID dessa tarefa: {idtarefa}");
            Console.WriteLine("Tarefa adicionada com sucesso!");
        }

        public void DeleteTarefa()
        {
            Console.Write("Qual id da tarefa que quer remover: ");
            int.TryParse(Console.ReadLine(), out int identrada);
            var tarefaachada = tarefas.Find(tarefa => tarefa.IdTarefa == identrada);
            if (tarefaachada == null)
            {
                Console.WriteLine("Tarefa inexistente");
                return;
            }
            tarefas.Remove(tarefaachada);
            Thread.Sleep(0800);
            Console.WriteLine($"Tarefa id[{identrada}] excluida com sucesso!");
        }
        
        
        public void ExibirTarefas()
        {
            Menu.Linha("Tarefas");

            foreach (var tarefa in tarefas)
            {
                Menu.Linha($"{tarefa.TituloTarefa}");
                Console.WriteLine($"Titulo: {tarefa.TituloTarefa}");
                Console.WriteLine($"Descrição: {tarefa.DescricaoTarefa}");
                Console.WriteLine($"Prioridade: {tarefa.TipoPrioridade}");
                Console.WriteLine($"Data: {tarefa.DataTarefa}");
                Menu.Linha("------");
            }

            Menu.Linha("------");
        }

        public void TarefasPersonalizadas()
        {
            //metodos LINQs tipo tarefas mais urgentes e tals
        }
    }
}
