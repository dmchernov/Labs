using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izh_11_tasks.Task_3
{
    /// <summary>
    /// Тестовый класс для подписки на события класса Countdown
    /// </summary>
    class Receiver
    {
        private Countdown countdownInstance;

        public Receiver(Countdown countdown)
        {
            countdownInstance = countdown;
            this.Register();
        }

        /// <summary>
        /// Подписка на события.
        /// </summary>
        public void Register()
        {
            countdownInstance.NewMessage += ReadMessage;
        }

        /// <summary>
        /// Отписка от событий.
        /// </summary>
        public void Unregister()
        {
            countdownInstance.NewMessage -= ReadMessage;
        }

        /// <summary>
        /// Метод срабатывает при получении события.
        /// </summary>
        /// <param name="sender">Объект отправителя.</param>
        /// <param name="e">Аргументы события.</param>
        private void ReadMessage(object sender, NewMessageEventArgs e)
        {
            Console.WriteLine(string.Format("Receiver instance. Message is {0}", e.Message));
        }
    }
}
