using System;
using System.Collections.Generic;
using System.Text;
using FocoTotal.Models;
using FocoTotal.Enums;
using FocoTotal.Exceptions;
using System.Linq;

namespace FocoTotal.Services
{
    public class Sistema
    {
        List<Usuario> Usuarios = new List<Usuario>();

        public Usuario Login(string username, string password)
        {
            var usuario = BuscarUser(username, password);
            Console.WriteLine($"Bem vindo {usuario.NomeUsuario}");
            Menu.Linha("------");
            return usuario;
        }

        public Usuario BuscarUser(string username, string password)
        {
            foreach (var usuario in Usuarios)
            {
                if(username == usuario.NomeUsuario && password == usuario.SenhaUsuario)
                {
                    Console.WriteLine($"Conta encontrada {usuario.NomeUsuario}");
                    return usuario;
                }
            }
            Menu.Linha("------");
            throw new UsuarioInexistenteException("Usuario inexistente!");
        }

        public void CadastroUsuario()
        {
            Menu.Linha("Cadastro");
            Console.Write("Digite seu nome: ");
            var nomeUsuario = Console.ReadLine();
            Console.Write("Sua senha: ");
            var senhaUsuario = Console.ReadLine();

            while (Usuarios.Any(usuario => usuario.NomeUsuario == nomeUsuario))
            {
                Console.WriteLine($"Nome de usuario ja cadastrado[{nomeUsuario}]");
                Console.Write("Informe um outro nome de usuario: ");
                var novoNome = Console.ReadLine();
                nomeUsuario = novoNome;
            }

            var usuario = new Usuario(nomeUsuario, senhaUsuario);
            Usuarios.Add(usuario);
            Thread.Sleep(1000);
            Console.WriteLine($"Usuario {usuario.NomeUsuario} cadastrado com sucesso!");
        }
    }
}

//depois add persistencia