using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Labb_3___WPF_applikation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Booking> bookings = new List<Booking>()
        {
        new Booking ("Jan Eriksson", "Bord 2", "2023-11-09", "13:00"),
        new Booking ("Margareta Persson", "Bord 4", "2023-12-12", "16:00"),
        new Booking ("Stina Lundgren", "Bord 5", "2023-10-22", "11:00"),
        };

        string[] array = { "08:00", "08.30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00",
        "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:"};

        // Display Order =  Date > Name > Table > Time

        public MainWindow()
        {
            InitializeComponent();
            DisplayContent();
            main_DatePicker.DisplayDateStart = DateTime.Today;
            Loaded += (s, e) => Keyboard.Focus(main_nameInput);
        }

        private void DisplayContent()
        {
            try
            {
                main_Bookings_ListBox.Items.Clear();

                foreach (Booking x in bookings)
                {
                    main_Bookings_ListBox.Items.Add(String.Format("{0}, {1}, {2}, kl. {3}.", x.Date, x.Name, x.TableNumber, x.Time));
                }
            }
            catch (InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (FormatException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (ArgumentNullException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);

            }


        }

        private void main_BokaBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input_Name = main_nameInput.Text;
                string input_Table = main_Table.Text;
                string input_DatePicker = main_DatePicker.Text;
                string input_Time = main_Time.Text;

                if (input_Name == "" || input_Table == "" || input_DatePicker == "" || input_Time == "")
                {
                    System.Windows.MessageBox.Show("Namn, Datum, Tid, och Bordsnummer måste specificeras innan det kan läggas in i listan.", "Meddelande", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                foreach (Booking xy in bookings)
                {
                    if (xy.Date == input_DatePicker && xy.Time == input_Time && xy.TableNumber == input_Table)
                    {
                        System.Windows.MessageBox.Show("Det angivna datum, tid, eller bord är ej tillgänglig.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }

                bookings.Add(new Booking(input_Name, input_Table, input_DatePicker, input_Time));
                DisplayContent();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btn_Visa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DisplayContent();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        private void btn_Avboka_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = main_Bookings_ListBox.SelectedIndex;
                if (main_Bookings_ListBox.SelectedItem == null)
                    return;

                bookings.RemoveAt(index);

                DisplayContent();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void main_LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Document|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                using (FileStream fs = (FileStream)dlg.OpenFile())
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        main_Bookings_ListBox.Items.Add(sr.ReadToEnd());
                    }
                }
            }
        }

        private void main_SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog Save_File = new SaveFileDialog();
            Save_File.Filter = "Text Document|*.txt";
            Save_File.FilterIndex = 1;
            Save_File.RestoreDirectory = true;

            Nullable<bool> result = Save_File.ShowDialog();

            if (result == true)
            {
                using (FileStream fs = (FileStream)Save_File.OpenFile())
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach (Booking x in bookings)
                        {
                            sw.Write(String.Format("{0}, {1}, {2}, kl. {3}.\n", x.Date, x.Name, x.TableNumber, x.Time));
                        }
                    }
                }
            }

        }
    }
}
