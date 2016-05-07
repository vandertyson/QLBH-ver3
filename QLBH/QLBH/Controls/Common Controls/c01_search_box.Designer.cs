namespace QLBH.Controls
{
    partial class c01_search_box
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_btn_search = new DevExpress.XtraEditors.SimpleButton();
            this.txtKeyWord = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_btn_search
            // 
            this.m_btn_search.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.m_btn_search.Appearance.Options.UseBackColor = true;
            this.m_btn_search.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.m_btn_search.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btn_search.Image = global::QLBH.Properties.Resources.Google_Web_Search_Filled_50;
            this.m_btn_search.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.m_btn_search.Location = new System.Drawing.Point(189, 0);
            this.m_btn_search.Name = "m_btn_search";
            this.m_btn_search.Size = new System.Drawing.Size(57, 40);
            this.m_btn_search.TabIndex = 2;
            this.m_btn_search.Click += new System.EventHandler(this.m_btn_search_Click);
            // 
            // txtKeyWord
            // 
            this.txtKeyWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(67)))));
            this.txtKeyWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtKeyWord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKeyWord.Font = new System.Drawing.Font("Segoe UI Semilight", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeyWord.ForeColor = System.Drawing.Color.White;
            this.txtKeyWord.Location = new System.Drawing.Point(0, 0);
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Size = new System.Drawing.Size(189, 32);
            this.txtKeyWord.TabIndex = 3;
            this.txtKeyWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKeyWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyWord_KeyDown);
            // 
            // c01_search_box
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(67)))));
            this.Controls.Add(this.txtKeyWord);
            this.Controls.Add(this.m_btn_search);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            this.Name = "c01_search_box";
            this.Size = new System.Drawing.Size(246, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton m_btn_search;
        private System.Windows.Forms.TextBox txtKeyWord;
    }
}
