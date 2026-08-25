using System;
using System.Collections.Generic;
using System.Text;

namespace FocoTotal.Exceptions
{
    public class TarefaInexistenteException : Exception
    {
        public TarefaInexistenteException(string message) : base (message) { }
    }
}
