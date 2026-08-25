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


        public Sistema()
        {
            UploadContasDisc();
        } 

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


            var path = $@"C:\Users\kauan\Documents\DEV\C#\FocoTotal\Contas\conta_{nomeUsuario}.txt";
            string[] dados = { nomeUsuario, senhaUsuario };
            if (!File.Exists(path))
            {
                File.WriteAllLines(path, dados);
            }


            Thread.Sleep(0600);
            Menu.MensagemPersonalizada($"Usuario {usuario.NomeUsuario} cadastrado com sucesso!");
        }

        public void UploadContasDisc()
        {
            var diretorio = @"C:\Users\kauan\Documents\DEV\C#\FocoTotal\Contas";
            var arquivos = Directory.GetFiles(diretorio);
            foreach (var arquivo in arquivos)
            {
                string[] linhas = File.ReadAllLines(arquivo);
                var usuario = new Usuario(linhas[0], linhas[1]);

                if(linhas.Length > 2)
                {
                    for (int i = 2; i < linhas.Length; i++)
                    {
                        var partes = linhas[i].Split(';');

                        var parte2 = Enum.Parse<EnumPrioridade>(partes[2]);
                        var parte3 = DateTime.Parse(partes[3]);
                        int.TryParse(Console.ReadLine(), out int parte4);
                        var tarefa = new Tarefa(partes[0], partes[1], parte2, parte3, parte4);
                        usuario.tarefas.Add(tarefa);
                    }
                }

                Usuarios.Add(usuario);

            }
        }
    }
}
