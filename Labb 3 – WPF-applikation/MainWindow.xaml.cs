using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        new Booking ("2011-04-17", "Ned Stark", "Bord 2", "13:00"),
        new Booking ("1999-07-14", "Thomas Anderson", "Bord 4", "16:00"),
        new Booking ("1979-11-02", "Ellen Ripley", "Bord 5", "11:00"),
        new Booking ("1985-02-08", "Sarah Connor", "Bord 1", "20:00"),
        };

        string[] time_Array = { "08:00", "08.30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00",
        "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
        string[] table_Array = { "Bord 1", "Bord 2", "Bord 3", "Bord 4", "Bord 5" };


        public MainWindow()
        {
            try
            {
                InitializeComponent();
                DisplayContent();
                main_Time.ItemsSource = time_Array;
                main_Table.ItemsSource = table_Array;
                main_DatePicker.DisplayDateStart = DateTime.Today;
                main_DatePicker.SelectedDate = DateTime.Now;
                Loaded += (s, e) => Keyboard.Focus(main_nameInput);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DisplayContent()
        {
            try
            {
                main_nameInput.Text = null;
                main_Table.Text = null;
                main_Time.Text = null;
                main_DatePicker.SelectedDate = DateTime.Now;
                main_Bookings_ListBox.ItemsSource = null;
                main_Bookings_ListBox.ItemsSource = bookings;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void main_BokaBtn_Click(object sender, RoutedEventArgs e)
        {
            string input_DatePicker;
            string input_Name;
            string input_Table;
            string input_Time;
            try
            {
                input_DatePicker = main_DatePicker.Text;
                input_Name = main_nameInput.Text.Trim();
                input_Table = main_Table.Text;
                input_Time = main_Time.Text;

                Regex rg = new Regex(@"^[a-zåäöA-ZÅÄÖ]{2,}\s[a-zåäöA-ZÅÄÖ]{2,}$");

                if (input_Name == "")
                {
                    System.Windows.MessageBox.Show("Namn måste specificeras innan det kan läggas in i listan.", "Meddelande", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (!rg.IsMatch(input_Name))
                {
                    System.Windows.MessageBox.Show("Skriv in För- och Efternamn med endast ett mellanslag.", "Meddelande", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (input_Table == "")
                {
                    System.Windows.MessageBox.Show("Bordsnummer måste specificeras innan det kan läggas in i listan.", "Meddelande", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (input_DatePicker == "")
                {
                    System.Windows.MessageBox.Show("Datum måste specificeras innan det kan läggas in i listan.", "Meddelande", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (input_Time == "")
                {
                    System.Windows.MessageBox.Show("Tid måste specificeras innan det kan läggas in i listan.", "Meddelande", MessageBoxButton.OK, MessageBoxImage.Information);
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

                bookings.Add(new Booking(input_DatePicker, input_Name, input_Table, input_Time));
                DisplayContent();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void main_VisaBtn_Click(object sender, RoutedEventArgs e)
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

        private void main_AvbokaBtn_Click(object sender, RoutedEventArgs e)
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
            try
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
                            string read_File = sr.ReadToEnd();
                            System.Windows.MessageBox.Show(read_File, "Bokningar", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void main_SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
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
                DisplayContent();
            }
            catch (ObjectDisposedException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (NotSupportedException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (IOException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ArgumentNullException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (FormatException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        public void sort_Date_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                sort_Table.IsChecked = false;
                sort_Time.IsChecked = false;
                sort_Name.IsChecked = false;
                List<Booking> sortByDate = new List<Booking>(bookings.OrderBy(date => date.Date));

                bookings.Clear();

                foreach (var x in sortByDate)
                {
                    bookings.Add(new Booking(x.Date, x.Name, x.TableNumber, x.Time));
                }
                DisplayContent();
            }
            catch (ArgumentNullException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void sort_Name_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                sort_Table.IsChecked = false;
                sort_Time.IsChecked = false;
                sort_Date.IsChecked = false;
                List<Booking> sortByName = new List<Booking>(bookings.OrderBy(name => name.Name));

                bookings.Clear();

                foreach (var x in sortByName)
                {
                    bookings.Add(new Booking(x.Date, x.Name, x.TableNumber, x.Time));
                }
                DisplayContent();
            }
            catch (ArgumentNullException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void sort_Table_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                sort_Time.IsChecked = false;
                sort_Date.IsChecked = false;
                sort_Name.IsChecked = false;
                List<Booking> sortByTable = new List<Booking>(bookings.OrderBy(table => table.TableNumber));

                bookings.Clear();

                foreach (var x in sortByTable)
                {
                    bookings.Add(new Booking(x.Date, x.Name, x.TableNumber, x.Time));
                }
                DisplayContent();
            }
            catch (ArgumentNullException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void sort_Time_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                sort_Table.IsChecked = false;
                sort_Date.IsChecked = false;
                sort_Name.IsChecked = false;
                List<Booking> sortByTime = new List<Booking>(bookings.OrderBy(time => time.Time));

                bookings.Clear();

                foreach (var x in sortByTime)
                {
                    bookings.Add(new Booking(x.Date, x.Name, x.TableNumber, x.Time));
                }
                DisplayContent();
            }
            catch (ArgumentNullException ex)
            {
                System.Windows.MessageBox.Show("I'm afraid I can't do that, Dave.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }


}
