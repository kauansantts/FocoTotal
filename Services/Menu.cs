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
            Linha("Opções");
            Thread.Sleep(0800);
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
            //Console.WriteLine($"==================={mensagem}===================");
            int tamanhoMensagem = mensagem.Length;
            int lado = (tamanhoMensagem + 20) / 2;

            string resultado = $"{new string('=', lado)} {mensagem} {new string('=', lado)}";
            Console.WriteLine(resultado);

        }

        public static void MenuLogado(Usuario usuario)
        {
            while (true)
            {
                Menu.MenuOpc("Adicionar tarefa", "Remover tarefa", "Exibir tarefas", "Exibir tarefas personalizadas", "Encerrar");
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
                }
            }
        }
    }

}
