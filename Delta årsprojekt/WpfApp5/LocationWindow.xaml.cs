using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration;
using System.Windows.Navigation;

namespace WpfApp5
{
    public partial class Afdeling : Window
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["post"].ConnectionString;

        public Afdeling(string afdelingID)
        {
            InitializeComponent();
            AfdelingNameLabel.Content = $"Afdeling: {afdelingID}";
            LoadAfdelingData(afdelingID);
        }

        private void LoadAfdelingData(string afdelingID)
        {
            try
            {
                if (!int.TryParse(afdelingID, out int afdelingIDInt))
                {
                    MessageBox.Show($"Invalid afdelingID format: '{afdelingID}'. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"
                        SELECT h.*, o.*
                        FROM [Delta_projekt].[dbo].[afd.højhuset] h
                        JOIN [Delta_projekt].[dbo].[Opgaver] o ON h.[afdelingID] = o.[afdelingID]
                        WHERE h.[afdelingID] = @afdelingID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@afdelingID", SqlDbType.Int)).Value = afdelingIDInt;

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        AfdelingDataGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }

            //Fejl beskeder
            catch (FormatException formatEx)
            {
                MessageBox.Show($"Format error: {formatEx.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL error: {sqlEx.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpretOpgave_Click(object sender, RoutedEventArgs e)
        {
            Window opgaveWindow = new Window
            {
                Title = "Opgave",
                Content = new Opgave(), 
                Height = 500,
                Width = 500
            };

            opgaveWindow.ShowDialog();
        }
        private void Tilbage_Click(object sender, RoutedEventArgs e)
        {
            
            Page1 page1 = new Page1();
            NavigationWindow navigationWindow = new NavigationWindow
            {
                Content = page1,
                Title = "Page1",
                Height = 450,
                Width = 800
            };
            navigationWindow.Show();
            this.Close(); 
        }

    }
}


















