using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using WpfApp5;

namespace LoginWindow
{
    public partial class MainWindow : Window
    {
       
        private string strConnect = ConfigurationManager.ConnectionStrings["post"].ConnectionString;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            
            using (SqlConnection con = new SqlConnection(strConnect))
            {
                con.Open();
                //Tjekker imod data i database
                using (SqlCommand cmd = new SqlCommand("SELECT Adgangskode FROM Ansatte WHERE Navn=@UN", con))
                {
                    cmd.Parameters.AddWithValue("@UN", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            string storedPassword = reader["Adgangskode"].ToString();

                            // Tjek password matcher
                            if (password == storedPassword)
                            {
                                //MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                                
                                Page1 page1 = new Page1();
                                App.Current.MainWindow.Content = page1;

                                return;
                            }
                        }
                    }
                }
            }

            // Fejl besked
            MessageBox.Show("Invalid username or password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}





