using System;

namespace Bubbio.Core.Exceptions
{
    public class InvalidEventException : Exception
    {
        public InvalidEventException(string message) : base(message)
        {
        }
    }
}