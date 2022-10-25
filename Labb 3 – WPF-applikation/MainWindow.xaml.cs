using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labb_3___WPF_applikation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Booking> bookings = new List<Booking>();
        
        public MainWindow()
        {
            InitializeComponent();
            DisplayContent();
        }
        private void DisplayContent()
        {
            lbox_Bookings_Content.Items.Clear();
            foreach (Booking booking in bookings)
            {
                lbox_Bookings_Content.Items.Add(String.Format("{0} {1} {2} {3}", booking.Name, booking.TableNumber, booking.Date, booking.Time));
            }

        }
        private void btn_Boka_Click(object sender, RoutedEventArgs e)
        {
            string input1 = txt_nameInput.Text;
            string input2 = cmbox_Table.Text;
            string input3 = btn_DatePicker.Text;
            string input4 = cbox_Time.Text;

            bookings.Add(new Booking(input1, input2, input3, input4));
;
            
            DisplayContent();
        }

        private void btn_Visa_Click(object sender, RoutedEventArgs e)
        {
            DisplayContent();
        }

        private void btn_Avboka_Click(object sender, RoutedEventArgs e)
        {
            int index = lbox_Bookings_Content.SelectedIndex;
            if (lbox_Bookings_Content.SelectedItem == null)
                return;

            bookings.RemoveAt(index);

            DisplayContent();
        }
    }
}
