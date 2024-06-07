using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp5
{
    public partial class Page1 : Page
    {
        private string strConnect = ConfigurationManager.ConnectionStrings["post"].ConnectionString;

        public Page1()
        {
            InitializeComponent();
        }

        private void HøjhusetButton_Click(object sender, RoutedEventArgs e)
        {
            FetchAndDisplayDepartmentData("Højhuset");
        }

        private void MedicinerhusetButton_Click(object sender, RoutedEventArgs e)
        {
            FetchAndDisplayDepartmentData("Medicinerhuset");
        }

        private void SkadestuenButton_Click(object sender, RoutedEventArgs e)
        {
            FetchAndDisplayDepartmentData("Skadestuen");
        }

        private void FetchAndDisplayDepartmentData(string department)
        {
            List<string> departmentNames = new List<string>();

            try
            {
                using (SqlConnection con = new SqlConnection(strConnect))
                {
                    con.Open();
                    string query = $"SELECT TOP (10) [AfdelingsNavn] FROM [Delta_projekt].[dbo].[afd.{department}]";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string departmentName = reader["AfdelingsNavn"].ToString();
                                    departmentNames.Add(departmentName);
                                }
                            }
                            else
                            {
                                MessageBox.Show($"No data found for department: {department}", "No Data", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                }

                if (departmentNames.Count > 0)
                {
                    // Lav og vis afdelingWindow 
                    DepartmentDataWindow departmentDataWindow = new DepartmentDataWindow(departmentNames);
                    departmentDataWindow.Show();

                    
                    CloseParentWindow();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching data for department: {department}\nError: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseParentWindow()
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }
    }
}













