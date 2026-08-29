using System;
using System.Collections.Generic;
using System.Text;
using FocoTotal.Enums;
using FocoTotal.Models;
using FocoTotal.Services;
using System.Threading;
using Spectre.Console;


namespace FocoTotal
{
    class Program
    {
        
        public static void Main(string[] args)
        {
            Sistema sistema = new Sistema();
            while (true)
            {              
                var painel = new Panel("[bold yellow]1[/] - Login\n[bold yellow]2[/] - Cadastrar conta\n[bold yellow]3[/] - Encerrar")
                {
                    Header = new PanelHeader("[bold cyan] FOCO TOTAL [/]"),
                    Border = BoxBorder.Rounded,
                    Padding = new Padding(2, 1, 2, 1)
                };
                AnsiConsole.Write(painel);
                Console.Write("Opção: ");
                int.TryParse(Console.ReadLine(), out int entrada);

                if (entrada == 1)
                {
                    try
                    {
                        Console.Write("Nome de usuario: ");
                        var nome = Console.ReadLine();
                        Console.Write("Senha: ");
                        var senha = Console.ReadLine();
                        Thread.Sleep(0100);
                        var logou = sistema.Login(nome, senha);
                        Thread.Sleep(0800);
                        Console.Clear();
                        Menu.MenuLogado(logou);
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }else if (entrada == 2)
                {
                    sistema.CadastroUsuario();
                }else if (entrada == 3)
                {
                    Menu.Linha("Encerrando o sistema");
                    Thread.Sleep(0600);
                    break;
                }
            }
        }
    }

}

