using FocoTotal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FocoTotal.Services
{
    public class Menu
    {
        
        public static void MenuOpc(params string[] opc)
        {
            Linha("Foco Total");
            var i = 1;
            foreach (var item in opc)
            {
                Console.WriteLine($"{i} - {item}");
                i++;
            }

            Menu.Linha("------");
        }


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
                Menu.MenuOpc("Adicionar tarefa", "Remover tarefa", "Exibir tarefas", "Exibir tarefas personalizadas", "Sair do sistema");
                Console.Write("Opção: ");
                int.TryParse(Console.ReadLine(), out int entrada);

                if(entrada == 5)
                { 
                    Menu.Linha("Saindo do sistema");
                    Thread.Sleep(0600);
                    break;
                }else if (entrada == 1)
                {
                    usuario.AddTarefa();
                }else if (entrada == 3)
                {
                    usuario.ExibirTarefas();
                }else if (entrada == 2)
                {
                    Menu.MenuOpc("Remover tarefa por ID", "Remover varias tarefas por ID", "Remover todas tarefas");
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
