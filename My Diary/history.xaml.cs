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
using System.Data;
using Microsoft.Win32;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
namespace My_Diary
{
    /// <summary>
    /// Interaction logic for history.xaml
    /// </summary>
    public partial class history : Window
    {
        public int search_id;
        string Connection_string = @"Data source=database.db; version=3";
        public history()
        {
            InitializeComponent();
            get_value_in_data_grid();
        }
        void get_value_in_data_grid()
        {
            try
            {
                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                string query = "SELECT Id,Date,Place from tbl_diary";
                SQLiteCommand sqlite_comm = new SQLiteCommand(query, sqlitecon);
                sqlite_comm.ExecuteNonQuery();
                SQLiteDataAdapter sqlite_adpt = new SQLiteDataAdapter(sqlite_comm);
                DataTable data = new DataTable();
                sqlite_adpt.Fill(data);
                data_grid.ItemsSource = data.DefaultView;

                sqlitecon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void search_by_id()
        {
            try
            {
                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                string query = "SELECT Description from tbl_diary where Id ='" + search_id + "'";
                SQLiteCommand sqlite_comm = new SQLiteCommand(query, sqlitecon);
                sqlite_comm.ExecuteNonQuery();
                SQLiteDataReader data = sqlite_comm.ExecuteReader();
                if (data.Read())
                {
                    txt_show_selected_description.Text = data.GetString(0);
                }

                sqlitecon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " In search");
            }
        }



        /// <summary>
        /// when a row of dataGrid will be selected,
        /// Title , Description and image will be displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void data_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                object item = data_grid.SelectedItem;
                try
                { search_id = Convert.ToInt16((data_grid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text); }
                catch (FormatException)
                { 
                }
                lbl_title.Content = (data_grid.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;

                search_by_id();

                DataSet ds = new DataSet();
                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                SQLiteDataAdapter sqa = new SQLiteDataAdapter("Select image,image2,image3,image4 from tbl_diary where Id='" + search_id + "'", sqlitecon);
                sqa.Fill(ds);
                sqlitecon.Close();
                try
                {
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
                catch (IndexOutOfRangeException)
                {
                }
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
