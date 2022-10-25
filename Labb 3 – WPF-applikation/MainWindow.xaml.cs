using System;
using System.Collections.Generic;
using System.Windows;

namespace Labb_3___WPF_applikation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Booking> bookings = new List<Booking>();

        // Order =  Name > Table > Date > Time

        public MainWindow()
        {
            InitializeComponent();
            DisplayContent();
        }

        private void DisplayContent()
        {
            lbox_Bookings_Content.Items.Clear();

            foreach (Booking x in bookings)
            {
                lbox_Bookings_Content.Items.Add(String.Format("{0}, {1}, {2}, {3}.", x.Name, x.TableNumber, x.Date, x.Time));
            }
        }

        private void btn_Boka_Click(object sender, RoutedEventArgs e)
        {
            string input_Name = txt_nameInput.Text;
            string input_Table = cmbox_Table.Text;
            string input_Date = btn_DatePicker.Text;
            string input_Time = cbox_Time.Text;
            if(input_Name == "" || input_Table == "" || input_Date == "" || input_Time == "")
            {
                return;
            }
            else
            {
                bookings.Add(new Booking(input_Name, input_Table, input_Date, input_Time));
            }
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
