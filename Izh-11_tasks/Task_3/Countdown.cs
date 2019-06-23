using System.Threading;

namespace Izh_11_tasks.Task_3
{
    /// <summary>
    /// Класс-источник событий.
    /// </summary>
    class Countdown
    {
        string message;
        int delay;

        public delegate void NewMessageEventHandler(object sender, NewMessageEventArgs e);

        public event NewMessageEventHandler NewMessage;

        public Countdown(int delay)
        {
            this.delay = delay * 1000;
            message = "Event fired message";

            System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(
                () => { while (true) { Thread.Sleep(this.delay); SendMessage(); } });
            task.Start();
        }

        /// <summary>
        /// Рассылка уведомлений о событии зарегистрированным объектам.
        /// </summary>
        /// <param name="sender">Объект отправителя.</param>
        /// <param name="e">Аргументы события.</param>
        protected virtual void OnNewMessage(object sender, NewMessageEventArgs e)
        {
            NewMessage?.Invoke(sender, e);
        }

        /// <summary>
        /// Симуляция отправки сообщения.
        /// </summary>
        public void SendMessage()
        {
            OnNewMessage(this, new NewMessageEventArgs(this.message));
        }
    }
}
