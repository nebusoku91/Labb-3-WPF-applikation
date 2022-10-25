using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3___WPF_applikation
{
    internal class Booking
    {
        public Booking(string name, string tableNumber, string date, string time)
        {
            this.Name = name;
            this.TableNumber = tableNumber;
            this.Date = date;
            this.Time = time;
        }
        public string Name { get; set; }
        public string TableNumber { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
