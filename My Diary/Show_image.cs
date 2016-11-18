using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Data.SQLite;
namespace My_Diary
{
    public partial class Show_image : Form
    {
        string Connection_string = @"Data source=database.db; version=3";
        public Show_image()
        {
            InitializeComponent();
        }

        private void btn_show_image_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteConnection sqlitecon = new SQLiteConnection(Connection_string);
                sqlitecon.Open();
                string query = "Select image2 from tbl_diary where Id='" + this.txt_uid.Text + "'";
                SQLiteCommand sqlitecomm = new SQLiteCommand(query, sqlitecon);
                sqlitecomm.ExecuteNonQuery();
                SQLiteDataReader data = sqlitecomm.ExecuteReader();
                while (data.Read())
                {
                    byte[] imgg = (byte[])(data["image2"]);
                    if (imgg == null)
                        pictureBox1 = null;
                    else
                    {
                        MemoryStream mstream = new MemoryStream(imgg);
                        pictureBox1.Image = System.Drawing.Image.FromStream(mstream);
                    }
                }
                sqlitecon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
