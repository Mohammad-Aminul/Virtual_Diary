namespace My_Diary
{
    partial class Show_image
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_show_image = new System.Windows.Forms.Button();
            this.txt_uid = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(104, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btn_show_image
            // 
            this.btn_show_image.Location = new System.Drawing.Point(825, 536);
            this.btn_show_image.Name = "btn_show_image";
            this.btn_show_image.Size = new System.Drawing.Size(97, 33);
            this.btn_show_image.TabIndex = 1;
            this.btn_show_image.Text = "Show Image";
            this.btn_show_image.UseVisualStyleBackColor = true;
            this.btn_show_image.Click += new System.EventHandler(this.btn_show_image_Click);
            // 
            // txt_uid
            // 
            this.txt_uid.Location = new System.Drawing.Point(720, 497);
            this.txt_uid.Name = "txt_uid";
            this.txt_uid.Size = new System.Drawing.Size(180, 20);
            this.txt_uid.TabIndex = 2;
            // 
            // Show_image
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 613);
            this.Controls.Add(this.txt_uid);
            this.Controls.Add(this.btn_show_image);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Show_image";
            this.Text = "Show Image";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_show_image;
        private System.Windows.Forms.TextBox txt_uid;
    }
}