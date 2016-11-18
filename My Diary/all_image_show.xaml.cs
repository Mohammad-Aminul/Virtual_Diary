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
using System.IO;
using System.Data;
namespace My_Diary
{
    /// <summary>
    /// Interaction logic for all_image_show.xaml
    /// </summary>
    public partial class all_image_show : Window
    {
        string Connection_string = @"Data source=database.db; version=3";
        history hs = new history();

        
        public all_image_show()
        {
            InitializeComponent();
            get_id_into_cb();
        }
        void get_id_into_cb()
        {
            try
            {
               
                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                string query = "SELECT Id from tbl_diary";
                SQLiteCommand sqlite_comm = new SQLiteCommand(query, sqlitecon);
                sqlite_comm.ExecuteNonQuery();
                SQLiteDataAdapter sda = new SQLiteDataAdapter();
                SQLiteDataReader data = sqlite_comm.ExecuteReader();
                while (data.Read())
                {
                    cb_Search_ID.Items.Add(data.GetInt16(0));
                }
                
                
                sqlitecon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " In search");
            }
        }
        void get_image1()
        {

            try
            {
                DataSet ds = new DataSet();
                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                SQLiteDataAdapter sqa = new SQLiteDataAdapter("Select image from tbl_diary where Id='" + this.cb_Search_ID.Text + "'", sqlitecon);
                sqa.Fill(ds);
                sqlitecon.Close();

                byte[] data = (byte[])ds.Tables[0].Rows[0][0];
                MemoryStream strm = new MemoryStream(data);
                strm.Write(data, 0, data.Length);
                strm.Position = 0;
                System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                image_box1.Source = bi;
            }
            catch (Exception)
            {
            }
         
        }
        void get_image2()
        {

            try
            {
                DataSet ds = new DataSet();
                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                SQLiteDataAdapter sqa = new SQLiteDataAdapter("Select image2 from tbl_diary where Id='" + this.cb_Search_ID.Text + "'", sqlitecon);
                sqa.Fill(ds);
                sqlitecon.Close();

                byte[] data = (byte[])ds.Tables[0].Rows[0][0];
                MemoryStream strm = new MemoryStream(data);
                strm.Write(data, 0, data.Length);
                strm.Position = 0;
                System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                image_box2.Source = bi;
            }
            catch (Exception)
            {
            }
        }
        void get_image3()
        {

            try
            {
                DataSet ds = new DataSet();
                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                SQLiteDataAdapter sqa = new SQLiteDataAdapter("Select image3 from tbl_diary where Id='" + this.cb_Search_ID.Text + "'", sqlitecon);
                sqa.Fill(ds);
                sqlitecon.Close();

                byte[] data = (byte[])ds.Tables[0].Rows[0][0];
                MemoryStream strm = new MemoryStream(data);
                strm.Write(data, 0, data.Length);
                strm.Position = 0;
                System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                image_box3.Source = bi;
            }
            catch (Exception)
            {
            }

        }
        void get_image4()
        {

            try
            {
                DataSet ds = new DataSet();
                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                SQLiteDataAdapter sqa = new SQLiteDataAdapter("Select image4 from tbl_diary where Id='" + this.cb_Search_ID.Text + "'", sqlitecon);
                sqa.Fill(ds);
                sqlitecon.Close();

                byte[] data = (byte[])ds.Tables[0].Rows[0][0];
                MemoryStream strm = new MemoryStream(data);
                strm.Write(data, 0, data.Length);
                strm.Position = 0;
                System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                image_box4.Source = bi;
            }
            catch (Exception)
            {
            }
        }




        private void cb_Search_ID_DropDownClosed(object sender, EventArgs e)
        {
            get_image1();
            get_image2();
            get_image3();
            get_image4();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cb_Search_ID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
