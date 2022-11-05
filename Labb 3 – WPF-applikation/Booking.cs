using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3___WPF_applikation
{
    internal class Booking
    {
        public Booking(string date, string tableNumber, string name, string time)
        {
            this.Date = date;
            this.Name = name;
            this.TableNumber = tableNumber;
            this.Time = time;
        }
        public string Name { get; set; }
        public string TableNumber { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public override string ToString()
        {
            return $"{Date}, {TableNumber}, {Name}, kl. {Time}.";
        }
    }
    // Order =  Date > Name > Table > Time
   
}
