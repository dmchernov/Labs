using System;

namespace Izh_11_tasks.Task_3
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите временной интвервал, через который будут отправляться сообщения (сек.): ");
            int delay;
            if (!int.TryParse(Console.ReadLine(), out delay))
            {
                throw new ArithmeticException("Не удалось преобразовать введённое значение в число.");
            }

            Countdown cnt = new Countdown(delay);

            Receiver rcv = new Receiver(cnt);
            Listener lst = new Listener();
            lst.Register(cnt);

            System.Threading.Thread.Sleep((delay * 1000) + 1000);
            rcv.Unregister();
            Console.WriteLine("Unregister Reciever!");
            System.Threading.Thread.Sleep((delay * 1000) + 1000);
            lst.Unregister(cnt);
            Console.WriteLine("Unregister Listener!");
            System.Threading.Thread.Sleep((delay * 1000) + 1000);
            rcv.Register();
            Console.WriteLine("Register Reciever!");

            Console.ReadKey();
        }
    }
}
