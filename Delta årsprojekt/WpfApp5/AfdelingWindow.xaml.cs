using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp5
{
    public partial class DepartmentDataWindow : Window
    {
        public DepartmentDataWindow(List<string> departmentData)
        {
            InitializeComponent();
            DisplayDepartmentData(departmentData);
        }

        private void DisplayDepartmentData(List<string> departmentData)
        {
            double topMargin = 10;

            foreach (var departmentName in departmentData.Select((name, index) => new { Name = name, Index = index + 1 }))
            {
                //laver knapperne for stuerne
                Button departmentButton = new Button
                {
                    Content = departmentName.Name,
                    Width = 200,
                    Height = 30,
                    Tag = departmentName.Index 
                };

                
                Canvas.SetTop(departmentButton, topMargin);
                Canvas.SetLeft(departmentButton, 10);

                
                DepartmentCanvas.Children.Add(departmentButton);

                
                departmentButton.Click += DepartmentButton_Click;

                
                topMargin += 40;
            }
        }

        private void DepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // afdelingID fra knappen
                string afdelingID = button.Tag.ToString();

                // Laver en vindue som matcher afdelingID fra knappen
                try
                {
                    Afdeling afdelingWindow = new Afdeling(afdelingID);
                    afdelingWindow.Show();

                    // Lukker tidligere vinduer
                    foreach (Window window in Application.Current.Windows.Cast<Window>().ToList())
                    {
                        if (window != afdelingWindow && window != Application.Current.MainWindow)
                        {
                            window.Close();
                        }
                    }
                }

                //fejl besked
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening Afdeling window for afdelingID: {afdelingID}\nError: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}







//private void DepartmentButton_Click(object sender, RoutedEventArgs e)
//{
//    if (sender is Button button)
//    {
//        string afdelingID = button.Tag.ToString();

//        string validAfdelingID = "3"; // This should be a valid numeric string
//        Afdeling afdelingWindow = new Afdeling(validAfdelingID);
//        afdelingWindow.Show();

//        // Open the new Afdeling window
//        //Afdeling detailWindow = new Afdeling(afdelingID);
//        //detailWindow.Show();

//        // Close all open windows except the new Afdeling window and the main window
//        foreach (Window window in Application.Current.Windows.Cast<Window>().ToList())
//        {
//            if (window != afdelingWindow && window != Application.Current.MainWindow)
//            {
//                window.Close();
//            }
//        }
//    }
//}







