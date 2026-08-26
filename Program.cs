using System;
using System.Collections.Generic;
using System.Text;
using FocoTotal.Enums;
using FocoTotal.Models;
using FocoTotal.Services;
using System.Threading;


//faltando apenas add persistencia de dados

namespace FocoTotal
{
    class Program
    {
        
        public static void Main(string[] args)
        {
            Sistema sistema = new Sistema();
            while (true)
            {
                Menu.MenuOpc("Login", "Cadastrar conta", "Encerrar");
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
                        Thread.Sleep(0500);
                        var logou = sistema.Login(nome, senha);
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
                    Thread.Sleep(1000);
                    break;
                }
            }
        }
    }

}

