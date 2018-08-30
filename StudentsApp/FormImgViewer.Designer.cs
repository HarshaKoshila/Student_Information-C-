namespace StudentsApp
{
    partial class FormImgViewer
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
            this.picBoxOL = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOL)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxOL
            // 
            this.picBoxOL.Location = new System.Drawing.Point(1, 1);
            this.picBoxOL.Name = "picBoxOL";
            this.picBoxOL.Size = new System.Drawing.Size(100, 50);
            this.picBoxOL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBoxOL.TabIndex = 0;
            this.picBoxOL.TabStop = false;
            // 
            // FormImgViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.picBoxOL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormImgViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Viewer";
            this.Load += new System.EventHandler(this.FormOLimg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxOL;
    }
}