using System;

namespace Izh_12_tasks.Task_7
{
    public class Book : IComparable
    {
        public string title;
        public int pages;

        public Book(string title, int pages)
        {
            this.title = title;
            this.pages = pages;
        }

        public int CompareTo(object obj)
        {
            if (this.pages > ((Book)obj).pages)
            {
                return 1;
            }

            if (this.pages < ((Book)obj).pages)
            {
                return -1;
            }

            return 0;

        }
    }
}
