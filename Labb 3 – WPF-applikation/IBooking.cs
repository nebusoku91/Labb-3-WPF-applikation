using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3___WPF_applikation
{
    public interface IBooking
    {
        string Name { get; set; }
        string TableNumber { get; set; }
        string Date { get; set; }
        string Time { get; set; }

    }
}
