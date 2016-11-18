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
using System.Windows.Shapes;


using Microsoft.Win32;
using System.IO;
using System.Data.SQLite;
namespace My_Diary
{
    /// <summary>
    /// Interaction logic for Input_history.xaml
    /// </summary>
    public partial class Input_history : Window
    {
        public string current_date;
        int id = 100;
        string Connection_string = @"Data source=database.db; version=3";
        public Input_history()
        {
            InitializeComponent();
            date_picker.SelectedDate = DateTime.Today;
            get_id_into_cb();
        }
        /// <summary>
        /// getting search comboBox ID
        /// </summary>
        void get_id_into_cb()
        {

            try
            {
                cb_id.DataContext = -1;
                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                string query = "SELECT Id from tbl_diary";
                SQLiteCommand sqlite_comm = new SQLiteCommand(query, sqlitecon);
                sqlite_comm.ExecuteNonQuery();
                SQLiteDataAdapter sda = new SQLiteDataAdapter();
                SQLiteDataReader data = sqlite_comm.ExecuteReader();
                while (data.Read())
                {
                    cb_id.Items.Add(data.GetInt16(0));

                }
                sqlitecon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " In search");
            }
        }
        /// <summary>
        /// when you will select a date and close the calender the selected date will go to the vairable 'current_date'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void date_picker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            DateTime ida = Convert.ToDateTime(date_picker.SelectedDate);
            current_date = ida.ToShortDateString();

        }

        private void clc_logout_menu(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void clc_history_menu(object sender, RoutedEventArgs e)
        {
            history h = new history();
            h.Show();
        }
        private void clc_about_menu(object sender, RoutedEventArgs e)
        {
            about ab = new about();
            ab.Show();
        }
        private void clc_manage_account_mene(object sender, RoutedEventArgs e)
        {
            Manage_account ma = new Manage_account();
            ma.ShowDialog();
        }
        void func_txt_clear()
        {
            txt_description.Clear();
            txt_title.Clear();
            txt_image1.Clear();
            txt_image2.Clear();
            txt_image3.Clear();
            txt_image4.Clear();
            image_box.Source = null;
            cb_id.Text = "";
        }
        void create_id()
        {

            try
            {
                int uid = 0;
                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                string query = "SELECT Id from tbl_diary";
                SQLiteCommand sqlite_comm = new SQLiteCommand(query, sqlitecon);
                sqlite_comm.ExecuteNonQuery();
                SQLiteDataReader data = sqlite_comm.ExecuteReader();
                while (data.Read())
                {
                    uid = data.GetInt16(0);
                }
                id = uid + 1;
                sqlitecon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " In search");
            }
        }

        void insert_into_tbl_diary() //data inserting function
        {
            try
            {



                byte[] data1 = null;  //for first image convert to byte
                FileStream fs1 = new FileStream(this.txt_image1.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br1 = new BinaryReader(fs1);
                data1 = br1.ReadBytes((int)fs1.Length);//first 




                byte[] data2 = null; //for second image convert to byte
                FileStream fs2 = new FileStream(this.txt_image2.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br2 = new BinaryReader(fs2);
                data2 = br2.ReadBytes((int)fs2.Length);




                byte[] data3 = null; //for third image convert to byte
                FileStream fs3 = new FileStream(this.txt_image3.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br3 = new BinaryReader(fs3);
                data3 = br3.ReadBytes((int)fs3.Length);

                byte[] data4 = null; //for fourth image convert to byte
                FileStream fs4 = new FileStream(this.txt_image4.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br4 = new BinaryReader(fs4);
                data4 = br4.ReadBytes((int)fs4.Length);

                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                string query = "insert into tbl_diary values('" + current_date + "','" + this.txt_title.Text + "','" + this.txt_description.Text + "','" + id + "',@p1,@p2,@p3,@p4)";
                SQLiteCommand sqlcommand = new SQLiteCommand(query, sqlitecon);
                sqlcommand.Parameters.AddWithValue("@p1", data1);
                sqlcommand.Parameters.AddWithValue("@p2", data2);
                sqlcommand.Parameters.AddWithValue("@p3", data3);
                sqlcommand.Parameters.AddWithValue("@p4", data4);
                sqlcommand.ExecuteNonQuery();

                fs1.Close();
                fs2.Close();
                fs3.Close();
                fs4.Close();
               
                sqlitecon.Close();

                MessageBox.Show("Save successfully");
            }
            catch (Exception)
            {
                MessageBox.Show("Not save successfully");
            }
        }


        private void btn_save_history(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_title.Text) && !string.IsNullOrWhiteSpace(txt_description.Text))
            {
                if (string.IsNullOrEmpty((this.txt_image1.Text)))
                {

                    MessageBox.Show("Please Browse the first Image.");
                    goto last;
                }
                if (string.IsNullOrEmpty((this.txt_image2.Text)))
                {
                    MessageBox.Show("Please Browse the second Image.");
                    goto last;
                }
                if (string.IsNullOrEmpty((this.txt_image3.Text)))
                {
                    MessageBox.Show("Please Browse the Third Image.");
                    goto last;
                }
                if (string.IsNullOrEmpty((this.txt_image4.Text)))
                {
                    MessageBox.Show("Please Browse the Fourth Image.");
                    goto last;
                }
                if (string.IsNullOrEmpty((current_date)))
                {
                    MessageBox.Show("Pleas select Date.","Message",MessageBoxButton.OK);
                    goto last;
                }
                create_id();
                insert_into_tbl_diary();
                func_txt_clear();

            last:
                ;
            }
            else
                MessageBox.Show("Please Provide information in Heading  and Description.", "Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }


        private void btn_clear_click(object sender, RoutedEventArgs e)
        {
            func_txt_clear();
        }


        public string picpath;

        public void image_1()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            txt_image1.Text = dlg.FileName.ToString();
            ImageSourceConverter imgs = new ImageSourceConverter();
            image_box.SetValue(Image.SourceProperty, imgs.ConvertFromString(dlg.FileName.ToString()));


        }
        public void image_2()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            txt_image2.Text = dlg.FileName.ToString();
            ImageSourceConverter imgs = new ImageSourceConverter();
            image_box.SetValue(Image.SourceProperty, imgs.ConvertFromString(dlg.FileName.ToString()));

        }

        public void image_3()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            txt_image3.Text = dlg.FileName.ToString();
            ImageSourceConverter imgs = new ImageSourceConverter();
            image_box.SetValue(Image.SourceProperty, imgs.ConvertFromString(dlg.FileName.ToString()));

        }
        public void image_4()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            txt_image4.Text = dlg.FileName.ToString();
            ImageSourceConverter imgs = new ImageSourceConverter();
            image_box.SetValue(Image.SourceProperty, imgs.ConvertFromString(dlg.FileName.ToString()));

        }
        private void btn_image1_click(object sender, RoutedEventArgs e)  // load image no. 1
        {
            image_1();
        }
        private void Button_image2_Click(object sender, RoutedEventArgs e) // load image no. 2
        {
            image_2();
        }

        private void btn_image3_click(object sender, RoutedEventArgs e) // load image no. 3
        {
            image_3();
        }

        private void btn_image4_click(object sender, RoutedEventArgs e) // load image no. 4
        {
            image_4();
        }


        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txt_title.Text) && !string.IsNullOrWhiteSpace(this.cb_id.Text))
            {
                try
                {

                    SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                    sqlitecon.Open();
                    string query = "Update tbl_diary set Place='" + this.txt_title.Text + "', Date='" + current_date + "',Description='" + this.txt_description.Text + "' where Id='" + this.cb_id.Text + "'";
                    SQLiteCommand sqlite_comm = new SQLiteCommand(query, sqlitecon);
                    sqlite_comm.ExecuteNonQuery();
                    sqlitecon.Close();
                    func_txt_clear();
                    //get_id_into_cb();
                    MessageBox.Show("Update successfully");
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.Message);
                }
            }
            else
                MessageBox.Show("Please select an ID.","Messange",MessageBoxButton.OK,MessageBoxImage.Asterisk);
        }
        private void cb_id_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                string query = "SELECT Id,Place,Description,Date from tbl_diary where Id='" + this.cb_id.Text + "'";
                SQLiteCommand sqlite_comm = new SQLiteCommand(query, sqlitecon);
                sqlite_comm.ExecuteNonQuery();
                SQLiteDataReader data = sqlite_comm.ExecuteReader();
                if (data.Read())
                {

                    txt_title.Text = data.GetString(1);
                    txt_description.Text = data.GetString(2);
                    date_picker.SelectedDate = Convert.ToDateTime(data.GetString(3));
                    current_date = data.GetString(3);

                }
                sqlitecon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void vacuum_database()
        {
            try
            {

                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                string query = "vacuum database";
                SQLiteCommand sqlite_comm = new SQLiteCommand(query, sqlitecon);
                sqlite_comm.ExecuteNonQuery();
                sqlitecon.Close();



            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_delete_click(object sender, RoutedEventArgs e)
        {
            try
            {

                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                string query = "Delete from tbl_diary where Id='" + this.cb_id.Text + "'";
                SQLiteCommand sqlite_comm = new SQLiteCommand(query, sqlitecon);
                if (sqlite_comm.ExecuteNonQuery() != 0)
                {
                    MessageBox.Show("Delete Successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);

                    vacuum_database();
                    func_txt_clear();
                }
                else
                    MessageBox.Show("Select an ID.","Message",MessageBoxButton.OK,MessageBoxImage.Asterisk);
                sqlitecon.Close();



            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clc_images_menu(object sender, RoutedEventArgs e)
        {
            all_image_show all_im = new all_image_show();
            all_im.Show();
        }

    }
}
