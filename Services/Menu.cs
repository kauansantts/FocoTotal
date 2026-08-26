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
                    Thread.Sleep(1000);
                    break;
                }else if (entrada == 1)
                {
                    usuario.AddTarefa();
                }else if (entrada == 3)
                {
                    usuario.ExibirTarefas();
                }else if (entrada == 2)
                {
                    Console.Write("Qual id da tarefa que quer remover: ");
                    int.TryParse(Console.ReadLine(), out int identrada);
                    try
                    {
                    usuario.DeleteTarefa(identrada);
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }else if (entrada == 4)
                {
                    usuario.TarefasPersonalizadas();
                }
            }
        }

        public static void MensagemPersonalizada(string texto)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(texto);
            Console.ResetColor();
            Console.WriteLine();
        }
    }

}
