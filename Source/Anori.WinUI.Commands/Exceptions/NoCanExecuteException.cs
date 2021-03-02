using System;

namespace Anori.WinUI.Commands.Exceptions
{
   [Serializable]
    public class NoCanExecuteException : Exception
    {
        public NoCanExecuteException(string message) : base(message) { }
        public NoCanExecuteException() : base() { }
    }
}
