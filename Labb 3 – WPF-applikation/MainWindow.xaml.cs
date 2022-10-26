using System;
using System.Collections.Generic;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace Labb_3___WPF_applikation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Booking> bookings = new List<Booking>() 
        {
        new Booking ("Jan Eriksson", "Bord 2", "2022-11-09", "13:00"),
        new Booking ("Margareta Persson", "Bord 4", "2022-12-12", "16:00"),
        new Booking ("Stina Lundgren", "Bord 5", "2022-10-22", "11:00"),
        };

        // Order =  Date > Name > Table > Time

        public MainWindow()
        {
            InitializeComponent();
            DisplayContent();
        }

        private void DisplayContent()
        {
            try
            {
                lbox_Bookings_Content.Items.Clear();

                foreach (Booking x in bookings)
                {
                    lbox_Bookings_Content.Items.Add(String.Format("{0}, {1}, {2}, kl. {3}.", x.Date, x.Name, x.TableNumber, x.Time));
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error", ex.Message, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void btn_Boka_Click(object sender, RoutedEventArgs e)
        {
            string input_Name = txt_nameInput.Text;
            string input_Table = cmbox_Table.Text;
            string input_Date = btn_DatePicker.Text;
            string input_Time = cbox_Time.Text;
            int x = 0;
            if(input_Name == "" || input_Table == "" || input_Date == "" || input_Time == "")
            {
                return;
            }
            foreach (Booking xy in bookings)
            {
                if (xy.Date == input_Date && xy.Time == input_Time && xy.TableNumber == input_Table)
                {

                    System.Windows.MessageBox.Show("I'm afraid I can't let you do that, David.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }


            }

            bookings.Add(new Booking(input_Name, input_Table, input_Date, input_Time));
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
