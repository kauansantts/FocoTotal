using FocoTotal.Enums;
using FocoTotal.Enums;
using FocoTotal.Exceptions;
using FocoTotal.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var painel = new Panel("[bold yellow]1[/] - Urgente\n[bold yellow]2[/] - Media\n[bold yellow]3[/] - Normal\n[bold yellow]3[/] - Baixa")
            {
                Header = new PanelHeader("[bold cyan] FOCO TOTAL [/]"),
                Border = BoxBorder.Rounded,
                Padding = new Padding(2, 1, 2, 1)
            };
            AnsiConsole.Write(painel);
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
            tarefas.Clear();
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
            Thread.Sleep(300);

            foreach (var tarefa in tarefas)
            {
                var painel = new Panel($" - ID: {tarefa.IdTarefa}\n - Titulo: {tarefa.TituloTarefa}\n - Descrição: {tarefa.DescricaoTarefa}\n - Prioridade: {tarefa.TipoPrioridade}\n - Data: {tarefa.DataTarefa}")
                {
                    Header = new PanelHeader($"[bold cyan] {tarefa.TituloTarefa} [/]"),
                    Border = BoxBorder.Rounded,
                    Padding = new Padding(2, 1, 2, 1)
                };
                AnsiConsole.Write(painel);
                Thread.Sleep(380);
            }
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
            var painel = new Panel("[bold yellow]1[/] - Tarefas urgentes\n[bold yellow]2[/] - Tarefas medias\n[bold yellow]3[/] - Tarefas normais\n[bold yellow]4[/] - Tarefas baixas\n[bold yellow]5[/] - Exibir tarefa por ID")
            {
                Header = new PanelHeader("[bold cyan] FOCO TOTAL [/]"),
                Border = BoxBorder.Rounded,
                Padding = new Padding(2, 1, 2, 1)
            };
            AnsiConsole.Write(painel);
            Console.Write("Opção: ");
            int.TryParse(Console.ReadLine(), out int entrada);
         
            switch (entrada)
            {
                case 1:
                {
                        var resultado = tarefas.Where(tarefa => tarefa.TipoPrioridade == EnumPrioridade.Urgente);

                        if (resultado.Count() == 0)
                        {
                            Menu.MensagemPersonalizadaRed("Você ainda não adicionou tarefas dessa prioridade!");
                        }

                        Menu.Linha("Tarefas urgentes");
                        Thread.Sleep(380);
                        foreach (var tarefa in resultado)
                        {
                            var painel2 = new Panel($"[bold red] - ID: {tarefa.IdTarefa}\n - Titulo: {tarefa.TituloTarefa}\n - Descrição: {tarefa.DescricaoTarefa}\n - Prioridade: {tarefa.TipoPrioridade}\n - Data: {tarefa.DataTarefa}[/]")
                            {
                                Header = new PanelHeader($"[bold cyan] {tarefa.TituloTarefa} [/]"),
                                Border = BoxBorder.Rounded,
                                Padding = new Padding(2, 1, 2, 1)
                            };
                            AnsiConsole.Write(painel2);
                            Thread.Sleep(380);
                        }
                        break;
                }
                case 2:
                {
                        var resultado = tarefas.Where(tarefa => tarefa.TipoPrioridade == EnumPrioridade.Media);

                        if (resultado.Count() == 0)
                        {
                            Menu.MensagemPersonalizadaRed("Você ainda não adicionou tarefas dessa prioridade!");
                        }

                        Menu.Linha("Tarefas medianas");
                        Thread.Sleep(0150);
                        foreach (var tarefa in resultado)
                        {
                            var painel2 = new Panel($" - ID: {tarefa.IdTarefa}\n - Titulo: {tarefa.TituloTarefa}\n - Descrição: {tarefa.DescricaoTarefa}\n - Prioridade: {tarefa.TipoPrioridade}\n - Data: {tarefa.DataTarefa}")
                            {
                                Header = new PanelHeader($"[bold cyan] {tarefa.TituloTarefa} [/]"),
                                Border = BoxBorder.Rounded,
                                Padding = new Padding(2, 1, 2, 1)
                            };
                            AnsiConsole.Write(painel2);
                            Thread.Sleep(380);
                        }
                        break;
                }
                case 3:
                {
                        var resultado = tarefas.Where(tarefa => tarefa.TipoPrioridade == EnumPrioridade.Normal);


                        if (resultado.Count() == 0)
                        {
                            Menu.MensagemPersonalizadaRed("Você ainda não adicionou tarefas dessa prioridade!");
                        }

                        Menu.Linha("Tarefas normais");
                        Thread.Sleep(0150);
                        foreach (var tarefa in resultado)
                        {
                            var painel2 = new Panel($" - ID: {tarefa.IdTarefa}\n - Titulo: {tarefa.TituloTarefa}\n - Descrição: {tarefa.DescricaoTarefa}\n - Prioridade: {tarefa.TipoPrioridade}\n - Data: {tarefa.DataTarefa}")
                            {
                                Header = new PanelHeader($"[bold cyan] {tarefa.TituloTarefa} [/]"),
                                Border = BoxBorder.Rounded,
                                Padding = new Padding(2, 1, 2, 1)
                            };
                            AnsiConsole.Write(painel2);
                            Thread.Sleep(380);
                        }
                        break;
                }
                case 4:
                {
                        var resultado = tarefas.Where(tarefa => tarefa.TipoPrioridade == EnumPrioridade.Baixa);



                        if (resultado.Count() == 0)
                        {
                            Menu.MensagemPersonalizadaRed("Você ainda não adicionou tarefas dessa prioridade!");
                        }

                        Menu.Linha("Tarefas baixas");
                        Thread.Sleep(0150);
                        foreach (var tarefa in resultado)
                        {
                            var painel2 = new Panel($" - ID: {tarefa.IdTarefa}\n - Titulo: {tarefa.TituloTarefa}\n - Descrição: {tarefa.DescricaoTarefa}\n - Prioridade: {tarefa.TipoPrioridade}\n - Data: {tarefa.DataTarefa}")
                            {
                                Header = new PanelHeader($"[bold cyan] {tarefa.TituloTarefa} [/]"),
                                Border = BoxBorder.Rounded,
                                Padding = new Padding(2, 1, 2, 1)
                            };
                            AnsiConsole.Write(painel2);
                            Thread.Sleep(380);
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
                        var painel2 = new Panel($" - ID: {tarefaachada.IdTarefa}\n - Titulo: {tarefaachada.TituloTarefa}\n - Descrição: {tarefaachada.DescricaoTarefa}\n - Prioridade: {tarefaachada.TipoPrioridade}\n - Data: {tarefaachada.DataTarefa}")
                        {
                            Header = new PanelHeader($"[bold cyan] {tarefaachada.TituloTarefa} [/]"),
                            Border = BoxBorder.Rounded,
                            Padding = new Padding(2, 1, 2, 1)
                        };
                        AnsiConsole.Write(painel2);
                        Thread.Sleep(380);
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
