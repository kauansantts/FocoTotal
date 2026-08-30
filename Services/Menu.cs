using Figgle.Fonts;
using FocoTotal.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace FocoTotal.Services
{
    public class Menu
    {
        
        //public static void MenuOpc(params string[] opc)
        //{
        //    Linha("Foco Total");
        //    var i = 1;
        //    foreach (var item in opc)
        //    {
        //        Console.WriteLine($"{i} - {item}");
        //        i++;
        //    }

        //    Menu.Linha("------");
        //}


        public static void Linha(string mensagem)
        {
            int tamanhoMensagem = mensagem.Length;
            int lado = (tamanhoMensagem + 20) / 2;

            string resultado = $"{new string('=', lado)} {mensagem} {new string('=', lado)}";
            Console.WriteLine(resultado);

        }


        public static void MenuLogado(Usuario usuario)
        {

            while (true)
            {
               
                Console.ForegroundColor = ConsoleColor.Cyan;

                string titulo = FiggleFonts.Standard.Render("Foco Total");
                Console.WriteLine(titulo);

                Console.ResetColor();
                var painel = new Panel("[bold yellow]1[/] - Adicionar tarefa\n[bold yellow]2[/] - Remover tarefa\n[bold yellow]3[/] - Exibir tarefas\n[bold yellow]4[/] - Exibir tarefas personalizadas\n[bold yellow]5[/] - Sair do sistema")
                {
                    Header = new PanelHeader("[bold cyan] FOCO TOTAL [/]"),
                    Border = BoxBorder.Rounded,
                    Padding = new Padding(2, 1, 2, 1)
                };
                AnsiConsole.Write(painel);
                Console.Write("Opção: ");
                int.TryParse(Console.ReadLine(), out int entrada);

                if(entrada == 5)
                {
                    Menu.MensagemPersonalizadaRed("Você quer mesmo sair do sistema? ");
                    Console.Write("[S/N]: ");
                    var validarSaida = Console.ReadLine();
                    if(validarSaida == "S" || validarSaida == "s")
                    {
                        Menu.Linha("Saindo do sistema");
                        Thread.Sleep(0600);
                        Console.Clear();
                        break;
                    }                 
                }else if (entrada == 1)
                {
                    usuario.AddTarefa();
                }else if (entrada == 3)
                {
                    usuario.ExibirTarefas();
                }else if (entrada == 2)
                {
                    var painel2 = new Panel("[bold yellow]1[/] - Remover tarefa por ID\n[bold yellow]2[/] - Remover varias tarefas por ID\n[bold yellow]3[/] - Remover todas tarefas")
                    {
                        Header = new PanelHeader("[bold cyan] FOCO TOTAL [/]"),
                        Border = BoxBorder.Rounded,
                        Padding = new Padding(2, 1, 2, 1)
                    };
                    AnsiConsole.Write(painel2);
                    Console.Write("Opção: ");
                    int.TryParse(Console.ReadLine(), out int resposta);

                    if (resposta == 1)
                    {
                        Menu.Linha("------");
                        Console.Write("Qual ID da tarefa que quer remover: ");
                        int.TryParse(Console.ReadLine(), out int identrada);
                        try
                        {
                            usuario.DeleteTarefaId(identrada);
                        }
                        catch (Exception ex)
                        {
                            Menu.MensagemPersonalizadaRed(ex.Message);
                        }
                    }else if(resposta == 2)
                    {
                        try
                        {
                            bool idsvalidos = true;
                            List<int> ids = new List<int>();
                            Console.WriteLine("Quais IDs das tarefas que quer remover");
                            Console.WriteLine("obs:Numero 'espaco' outro numero");
                            Console.Write("Digite: ");
                            var idsEntrada = Console.ReadLine();
                            var splitado = idsEntrada.Split(' ');

                            foreach(var split in splitado)
                            {
                                 var resultadoatual = int.TryParse(split, out int numero);
                                if(resultadoatual == false)
                                {
                                    idsvalidos = resultadoatual;
                                }
        
                                if(idsvalidos == true)
                                {
                                    ids.Add(numero);
                                }
                            }
                            if (idsvalidos == false)
                            {
                                Console.WriteLine("Valor invalido!");
                                continue;
                            }

                            usuario.DeleteVariasTarefa(ids.ToArray());
                        }
                        catch(Exception ex)
                        {
                            Menu.MensagemPersonalizadaRed(ex.Message);
                        }
                    }else if (resposta == 3)
                    {
                        Console.Write("Você deseja realmente apagar todas tarefas[S/N]: ");
                        var requestUser = Console.ReadLine();
                        if (requestUser == "S" || requestUser == "s")
                        {
                            usuario.DeleteAllTarefa();
                        }
                        else
                        {
                            Console.WriteLine("Você não apagou nenhuma tarefa!");
                        }
                    }
                }
                else if (entrada == 4)
                {
                    usuario.TarefasPersonalizadas();
                }
            }
        }

        public static void MensagemPersonalizadaGreen(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(texto);
            Console.ResetColor();
            Console.WriteLine();
        }
        public static void MensagemPersonalizadaBlue(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(texto);
            Console.ResetColor();
            Console.WriteLine();
        }
        public static void MensagemPersonalizadaRed(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(texto);
            Console.ResetColor();
            Console.WriteLine();
        }
    }

}
