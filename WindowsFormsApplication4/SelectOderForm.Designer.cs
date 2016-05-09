namespace WindowsFormsApplication4
{
    partial class SelectOderForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnA1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.BtnNo = new System.Windows.Forms.Button();
            this.btnA2 = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(729, 515);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.BtnNo);
            this.tabPage1.Controls.Add(this.buttonOK);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnA2);
            this.tabPage1.Controls.Add(this.btnA1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(721, 489);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "フォルダの変更";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnA1
            // 
            this.btnA1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnA1.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnA1.AutoEllipsis = true;
            this.btnA1.ForeColor = System.Drawing.Color.Black;
            this.btnA1.Location = new System.Drawing.Point(43, 73);
            this.btnA1.Name = "btnA1";
            this.btnA1.Size = new System.Drawing.Size(646, 22);
            this.btnA1.TabIndex = 0;
            this.btnA1.Text = "中止します。";
            this.btnA1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnA1.UseVisualStyleBackColor = true;
            this.btnA1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(43, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(355, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "新ファイルに同名のリストファイルが存在します。";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(43, 436);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // BtnNo
            // 
            this.BtnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnNo.Location = new System.Drawing.Point(139, 436);
            this.BtnNo.Name = "BtnNo";
            this.BtnNo.Size = new System.Drawing.Size(75, 23);
            this.BtnNo.TabIndex = 5;
            this.BtnNo.Text = "キャンセル";
            this.BtnNo.UseVisualStyleBackColor = true;
            // 
            // btnA2
            // 
            this.btnA2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnA2.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnA2.AutoEllipsis = true;
            this.btnA2.ForeColor = System.Drawing.Color.Black;
            this.btnA2.Location = new System.Drawing.Point(43, 101);
            this.btnA2.Name = "btnA2";
            this.btnA2.Size = new System.Drawing.Size(646, 22);
            this.btnA2.TabIndex = 0;
            this.btnA2.Text = "ファイルを指定し直します";
            this.btnA2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnA2.UseVisualStyleBackColor = true;
            this.btnA2.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // SelectOderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 515);
            this.Controls.Add(this.tabControl1);
            this.Name = "SelectOderForm";
            this.Text = "SelectOderForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton btnA1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button BtnNo;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.RadioButton btnA2;
    }
}