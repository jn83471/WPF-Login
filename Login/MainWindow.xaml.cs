using MaterialDesignThemes.Wpf;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Configuration;
using System.Data.SqlClient;

namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        public MainWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            if (VerifyUser(txtUsername.Text, txtPassword.Password))
            {
                MessageBox.Show("Login Successfully", "Congrats", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Username or password is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool VerifyUser(string username, string password)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "select Status from Users where username='" + username + "' and password='" + password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToBoolean(dr["Status"]) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}