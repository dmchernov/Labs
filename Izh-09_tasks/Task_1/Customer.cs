using System.Text;


namespace Izh_09_tasks.Task_1
{
    public enum ToStringFormat
    {
        Name = 1,
        Phone = 2,
        Revenue = 4,
        All = 7,
    }

    public class Customer
    {
        private string name;
        private string contactPhone;
        private decimal revenue;

        public Customer(string name, string contactPhone, decimal revenue)
        {
            this.name = name;
            this.contactPhone = contactPhone;
            this.revenue = revenue;
        }

        public string Name => name;

        public string ContactPhone => contactPhone;

        public decimal Revenue => revenue;

        public string ToString(ToStringFormat format)
        {
            StringBuilder sb = new StringBuilder();
            string delimiter = "==";
            string result = string.Empty;

            if (((int)format & (int)ToStringFormat.Name) == 1)
            {
                sb.Append(Name).Append(delimiter);
            }

            if (((int)format & (int)ToStringFormat.Phone) == 2)
            {
                sb.Append(ContactPhone).Append(delimiter);
            }

            if (((int)format & (int)ToStringFormat.Revenue) == 4)
            {
                sb.Append(string.Format("{0:N2}", Revenue)).Append(delimiter);
            }

            result = sb.ToString();

            if (result.Split(delimiter.ToCharArray()).Length > 1)
            {
                return result.Replace(delimiter, ", ").TrimEnd(", ".ToCharArray());
            }
            else
            {
                return result.Replace(delimiter, string.Empty);
            }
        }
    }
}
