using System;
using System.Collections.Generic;
using System.Text;

namespace FocoTotal.Exceptions
{
    public class UsuarioInexistenteException : Exception
    {
        public UsuarioInexistenteException(string message) : base(message) { }
    }
}
