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

            Thread.Sleep(0100);
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
            Console.Write($"O ID dessa tarefa: ");
            Menu.MensagemPersonalizadaBlue($"{idtarefa}");
            SalvarDadosArquivo();

            Thread.Sleep(0120);
            Menu.MensagemPersonalizadaGreen("Tarefa adicionada com sucesso!");
        }

        public void DeleteTarefaId(int identrada)
        {
            var resultado = GetTarefa(identrada);
            tarefas.Remove(resultado);
            SalvarDadosArquivo();

            Thread.Sleep(0400);
            Menu.MensagemPersonalizadaGreen($"Tarefa id[{identrada}] excluida com sucesso!");
        }
        public void DeleteAllTarefa()
        {
            foreach(var tarefa in tarefas)
            {
                tarefas.Remove(tarefa);
            }
            SalvarDadosArquivo();

            Thread.Sleep(0400);
            Menu.MensagemPersonalizadaGreen("Tarefas excluidas com sucesso!");
        }
        public void DeleteVariasTarefa(params int[] identrada)
        {
            foreach(var id in identrada)
            {
                var resultado = GetTarefa(id);
                tarefas.Remove(resultado);
            }
            
            SalvarDadosArquivo();

            Thread.Sleep(0400);
            Menu.MensagemPersonalizadaGreen("Tarefas excluida com sucesso!");
        }
        
        
        public void ExibirTarefas()
        {
            if (!tarefas.Any()) 
            { 
                Menu.MensagemPersonalizadaRed("Você ainda não adicionou tarefas!");
                return;
            }
            
            Menu.Linha("Tarefas");
            Thread.Sleep(0300);

            foreach (var tarefa in tarefas)
            {
                Console.WriteLine($"ID: [{tarefa.IdTarefa}]");
                Console.WriteLine($"Titulo: {tarefa.TituloTarefa}");
                Console.WriteLine($"Descrição: {tarefa.DescricaoTarefa}");
                Console.WriteLine($"Prioridade: {tarefa.TipoPrioridade}");
                Console.WriteLine($"Data: {tarefa.DataTarefa}");
                Menu.Linha("------");
                Thread.Sleep(0150);
            }

            Menu.Linha("------");
        }


        public Tarefa GetTarefa(int id)
        {

            var tarefaachada = tarefas.Find(tarefa => tarefa.IdTarefa == id);
            if (tarefaachada == null)
            {
                throw new TarefaInexistenteException("Tarefa inexistente!");
            }
            return tarefaachada;
        }


        public void TarefasPersonalizadas()
        {
            Thread.Sleep(0300);
            Menu.MenuOpc("tarefas urgentes", "Tarefas medias", "Tarefas normais", "Tarefas baixas", "Exibir tarefa por ID");
            Console.Write("Opção: ");
            int.TryParse(Console.ReadLine(), out int entrada);
         
            switch (entrada)
            {
                case 1:
                {
                        var resultado = tarefas.Where(tarefa => tarefa.TipoPrioridade == EnumPrioridade.Urgente);

                        if (resultado == null)
                        {
                            Menu.MensagemPersonalizadaRed("Você ainda não adicionou tarefas dessa prioridade!");
                        }

                        Menu.Linha("Tarefas urgentes");
                        Thread.Sleep(0150);
                        foreach (var tarefa in resultado)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Menu.Linha($"{tarefa.TituloTarefa}");
                            Console.WriteLine($"Titulo: {tarefa.TituloTarefa}");
                            Console.WriteLine($"Descrição: {tarefa.DescricaoTarefa}");
                            Console.WriteLine($"Prioridade: {tarefa.TipoPrioridade}");
                            Console.WriteLine($"Data: {tarefa.DataTarefa}");
                            Menu.Linha("------");
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                        break;
                }
                case 2:
                {
                        var resultado = tarefas.Where(tarefa => tarefa.TipoPrioridade == EnumPrioridade.Media);

                        if (resultado == null)
                        {
                            Menu.MensagemPersonalizadaRed("Você ainda não adicionou tarefas dessa prioridade!");
                        }

                        Menu.Linha("Tarefas medianas");
                        Thread.Sleep(0150);
                        foreach (var tarefa in resultado)
                        {
                            Menu.Linha($"{tarefa.TituloTarefa}");
                            Console.WriteLine($"Titulo: {tarefa.TituloTarefa}");
                            Console.WriteLine($"Descrição: {tarefa.DescricaoTarefa}");
                            Console.WriteLine($"Prioridade: {tarefa.TipoPrioridade}");
                            Console.WriteLine($"Data: {tarefa.DataTarefa}");
                            Menu.Linha("------");
                        }
                        break;
                }
                case 3:
                {
                        var resultado = tarefas.Where(tarefa => tarefa.TipoPrioridade == EnumPrioridade.Normal);

                        if (resultado == null)
                        {
                            Menu.MensagemPersonalizadaRed("Você ainda não adicionou tarefas dessa prioridade!");
                        }

                        Menu.Linha("Tarefas normais");
                        Thread.Sleep(0150);
                        foreach (var tarefa in resultado)
                        {
                            Menu.Linha($"{tarefa.TituloTarefa}");
                            Console.WriteLine($"Titulo: {tarefa.TituloTarefa}");
                            Console.WriteLine($"Descrição: {tarefa.DescricaoTarefa}");
                            Console.WriteLine($"Prioridade: {tarefa.TipoPrioridade}");
                            Console.WriteLine($"Data: {tarefa.DataTarefa}");
                            Menu.Linha("------");
                        }
                        break;
                }
                case 4:
                {
                        var resultado = tarefas.Where(tarefa => tarefa.TipoPrioridade == EnumPrioridade.Baixa);


                        if (resultado == null)
                        {
                            Menu.MensagemPersonalizadaRed("Você ainda não adicionou tarefas dessa prioridade!");
                        }

                        Menu.Linha("Tarefas baixas");
                        Thread.Sleep(0150);
                        foreach (var tarefa in resultado)
                        {
                            Menu.Linha($"{tarefa.TituloTarefa}");
                            Console.WriteLine($"Titulo: {tarefa.TituloTarefa}");
                            Console.WriteLine($"Descrição: {tarefa.DescricaoTarefa}");
                            Console.WriteLine($"Prioridade: {tarefa.TipoPrioridade}");
                            Console.WriteLine($"Data: {tarefa.DataTarefa}");
                            Menu.Linha("------");
                        }
                        break;
                }
                case 5:
                {
                        Console.Write("ID da tarefa: ");
                        int.TryParse(Console.ReadLine(), out int identrada);
                        var tarefaachada = tarefas.Find(tarefa => tarefa.IdTarefa == identrada);

                        if (tarefaachada == null)
                        {
                            Menu.MensagemPersonalizadaRed("Tarefa inexistente!");
                            break;
                        }

                        Thread.Sleep(0150);
                        Menu.Linha($"Tarefa {tarefaachada.TituloTarefa}");
                        Console.WriteLine($"Titulo: {tarefaachada.TituloTarefa}");
                        Console.WriteLine($"Descrição: {tarefaachada.DescricaoTarefa}");
                        Console.WriteLine($"Prioridade: {tarefaachada.TipoPrioridade}");
                        Console.WriteLine($"Data: {tarefaachada.DataTarefa}");
                        Menu.Linha("------");
                        break;
                }
            }
        }

        public void SalvarDadosArquivo()
        {
            var path = @$"C:\Users\kauan\Documents\DEV\C#\FocoTotal\Contas\conta_{NomeUsuario}.txt";

            List<string> dados = new List<string>();
            dados.Add(NomeUsuario);
            dados.Add(SenhaUsuario);

            foreach (var tarefa in tarefas)
            {
                string task = $"{tarefa.TituloTarefa};{tarefa.DescricaoTarefa};{tarefa.TipoPrioridade.ToString()};{tarefa.DataTarefa.ToString()};{tarefa.IdTarefa.ToString()}";
                dados.Add(task);
            }

            File.WriteAllLines(path, dados);
        }
    }
}
