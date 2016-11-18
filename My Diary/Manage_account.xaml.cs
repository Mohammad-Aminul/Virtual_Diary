using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Data.SQLite;
namespace My_Diary
{
    /// <summary>
    /// Interaction logic for Manage_account.xaml
    /// </summary>
    public partial class Manage_account : Window
    {
        string ConnectionString = @"Data source=database.db; version=3";
        public Manage_account()
        {
            InitializeComponent();
            Timer();
        }

        void Timer()
        {
            System.Windows.Threading.DispatcherTimer dispatchtimer = new System.Windows.Threading.DispatcherTimer();
            dispatchtimer.Tick += new EventHandler(timer_tick);
            dispatchtimer.Interval = new TimeSpan(0, 0, 1);
            dispatchtimer.Start();
        }

        void timer_tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_reenter_new_password.Password))
            {
                if (this.txt_new_password.Password == this.txt_reenter_new_password.Password)
                {
                    
                    lbl_match.Foreground = Brushes.Green;
                    lbl_match.Content = "match";
                }
                else
                {
                    lbl_match.Foreground = new SolidColorBrush(Colors.Red);
                    lbl_match.Content = "Not match";
                }
            }
        }

        public void insert_into_tbl_admin()
        {
            SQLiteConnection mysqlcon = new SQLiteConnection(ConnectionString);
            mysqlcon.Open();
            string query = "insert into tbl_admin values('" + this.txt_new_username.Text + "','" + this.txt_new_password.Password + "')";
            SQLiteCommand mysqlcommd = new SQLiteCommand(query, mysqlcon);
            mysqlcommd.ExecuteNonQuery();
            mysqlcon.Close();
            MessageBox.Show("Your username and password has been changed successfully.");
            this.Close();
        }

        public void delete_from_tbl_admin()
        {
            SQLiteConnection mysqlcon = new SQLiteConnection(ConnectionString);
            mysqlcon.Open();
            string query = "DELETE FROM tbl_admin WHERE username='" + this.txt_current_username.Text + "'";
            SQLiteCommand mysqlcommd = new SQLiteCommand(query, mysqlcon);
            mysqlcommd.ExecuteNonQuery();
            mysqlcon.Close();
        }

        private void btn_submit_click2(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = 0;
                SQLiteConnection sqlitecon = new SQLiteConnection(ConnectionString);
                sqlitecon.Open();
                string query = "Select * from tbl_admin where username='" + this.txt_current_username.Text + "' and password='" + this.txt_current_password.Password + "'";
                SQLiteCommand sqlitecomm = new SQLiteCommand(query, sqlitecon);
                sqlitecomm.ExecuteNonQuery();
                SQLiteDataReader data = sqlitecomm.ExecuteReader();
                while (data.Read())
                {
                    count++;
                }
                if (count == 1)
                {
                    delete_from_tbl_admin();
                    insert_into_tbl_admin();
                }
                else
                    MessageBox.Show("Current Username and Password is not correct");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void Btn_clear_Click_1(object sender, RoutedEventArgs e)
        {
            txt_current_username.Clear();
            txt_new_username.Clear();
            txt_current_password.Clear();
            txt_new_password.Clear();
            txt_reenter_new_password.Clear();
        }
    }
}
