using System;

namespace Izh_11_tasks.Task_3
{
    /// <summary>
    /// Тестовый класс для подписки на события класса Countdown
    /// </summary>
    class Listener
    {
        /// <summary>
        /// Подписка на события класса Countdown.
        /// </summary>
        /// <param name="countdown">Ссылка на экземпляр класса Countdown.</param>
        public void Register(Countdown countdown)
        {
            countdown.NewMessage += GetMessage;
        }

        /// <summary>
        /// Отписка от событий класса Countdown.
        /// </summary>
        /// <param name="countdown">Ссылка на экземпляр класса Countdown.</param>
        public void Unregister(Countdown countdown)
        {
            countdown.NewMessage -= GetMessage;
        }

        /// <summary>
        /// Метод срабатывает при получении события.
        /// </summary>
        /// <param name="sender">Объект отправителя.</param>
        /// <param name="e">Аргументы события.</param>
        private void GetMessage(object sender, NewMessageEventArgs e)
        {
            Console.WriteLine(string.Format("Listener instance. Message is {0}", e.Message));
        }
    }
}
