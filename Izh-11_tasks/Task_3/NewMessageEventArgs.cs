using System;

namespace Izh_11_tasks.Task_3
{
    /// <summary>
    /// Класс аргументов события.
    /// </summary>
    internal sealed class NewMessageEventArgs : EventArgs
    {
        private readonly string message;

        public NewMessageEventArgs(string message)
        {
            this.message = message;
        }

        public string Message { get { return message; } }
    }
}
