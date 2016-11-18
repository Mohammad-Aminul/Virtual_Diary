using System;
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

using System.Data.SQLite;
namespace My_Diary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ConnectionString = @"Data source=database.db; version=3; New=True;Compress=True";
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btn_login(object sender, RoutedEventArgs e)
        {
            try
            {


                SQLiteConnection sqlitecon = new SQLiteConnection(ConnectionString);
                sqlitecon.Open();
                string query = "Select * from tbl_admin where username='" + this.txt_username.Text + "' and password='" + this.txt_password.Password + "'";
                SQLiteCommand sqlitecomm = new SQLiteCommand(query, sqlitecon);
                sqlitecomm.ExecuteNonQuery();

                SQLiteDataReader data = sqlitecomm.ExecuteReader();

                if (data.HasRows)
                {
                    Input_history ih = new Input_history();
                    ih.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Please Enter Correct Username and Password", "Message", MessageBoxButton.OK);
                sqlitecon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
