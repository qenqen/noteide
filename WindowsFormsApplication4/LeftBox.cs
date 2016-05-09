using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class LeftBox : Form
    {
        /// <summary>
        /// 廃棄予定
        /// </summary>
        public LeftBox()
        {
            InitializeComponent();
        }
        private System.Windows.Forms.ToolStripMenuItem 人名へToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem キーワードへToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 埋込へToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 色を解除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 貼るクリップボードToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem コピルクリップボードToolStripMenuItem;
       // private System.Windows.Forms.ToolStripMenuItem 本文検索ToolStripMenuItem;
       // private System.Windows.Forms.ToolStripMenuItem google検索ToolStripMenuItem;
       // private System.Windows.Forms.ToolStripMenuItem wiki参照ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightclickcut;
        private System.Windows.Forms.ToolStripMenuItem rightclickcpy;
        private System.Windows.Forms.ToolStripMenuItem rightclickpast;

        private System.Windows.Forms.ToolStripMenuItem menuAddColor;

        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ぐぐるToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem googleSelfToolStripMenuItem;
        //現在時間を挿入する
        private System.Windows.Forms.ToolStripMenuItem TimeStampMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private void LeftBox_Load(object sender, EventArgs e)
        {

        }
        public ContextMenuStrip GetContextMenuStrip()
        {
            return contextMenuStrip1;
        }
        public void InitMenuStrip()
        {
            try
            {

                this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);

                this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.rightclickcut = new System.Windows.Forms.ToolStripMenuItem();
                this.rightclickcpy = new System.Windows.Forms.ToolStripMenuItem();
                this.rightclickpast = new System.Windows.Forms.ToolStripMenuItem();
                this.埋込へToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.人名へToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.キーワードへToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.menuAddColor = new System.Windows.Forms.ToolStripMenuItem();
                this.色を解除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.貼るクリップボードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.コピルクリップボードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.ぐぐるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.TimeStampMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.googleSelfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                //this.tabPage1 = new System.Windows.Forms.TabPage();
                //this.statusStrip1 = new System.Windows.Forms.StatusStrip();
                //this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
                this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.保存ToolStripMenuItem,
        this.rightclickcut,
        this.rightclickcpy,
        this.rightclickpast,
        this.埋込へToolStripMenuItem,
        this.人名へToolStripMenuItem,
        this.キーワードへToolStripMenuItem,
        this.menuAddColor,
        this.色を解除ToolStripMenuItem,
        this.貼るクリップボードToolStripMenuItem,
        this.コピルクリップボードToolStripMenuItem,
        this.googleSelfToolStripMenuItem,
        this.ぐぐるToolStripMenuItem,
        this.TimeStampMenuItem,
        this.testToolStripMenuItem});
                this.contextMenuStrip1.Name = "contextMenuStrip1";
                this.contextMenuStrip1.Size = new System.Drawing.Size(221, 290);
//                this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
                // 
                // 保存ToolStripMenuItem
                // 

                this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
                this.保存ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
                this.保存ToolStripMenuItem.Text = "保存";
                // 
                // rightclickcut
                // 
                this.rightclickcut.Name = "rightclickcut";
                this.rightclickcut.Size = new System.Drawing.Size(220, 22);
                this.rightclickcut.Text = "切る";
                // 
                // rightclickcpy
                // 
                this.rightclickcpy.Name = "rightclickcpy";
                this.rightclickcpy.Size = new System.Drawing.Size(220, 22);
                this.rightclickcpy.Text = "コピー";
                // 
                // rightclickpast
                // 
                this.rightclickpast.Name = "rightclickpast";
                this.rightclickpast.Size = new System.Drawing.Size(220, 22);
                this.rightclickpast.Text = "貼る";
                // 
                // 埋込へToolStripMenuItem
                // 
                this.埋込へToolStripMenuItem.Name = "埋込へToolStripMenuItem";
                this.埋込へToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
                this.埋込へToolStripMenuItem.Text = "検索へ";
                // 
                // 人名へToolStripMenuItem
                // 
                this.人名へToolStripMenuItem.Name = "人名へToolStripMenuItem";
                this.人名へToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
                this.人名へToolStripMenuItem.Text = "人名へ";
                // 
                // キーワードへToolStripMenuItem
                // 
                this.キーワードへToolStripMenuItem.Name = "キーワードへToolStripMenuItem";
                this.キーワードへToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
                this.キーワードへToolStripMenuItem.Text = "キーワードへ";
                // 
                // menuAddColor
                // 
                this.menuAddColor.Name = "menuAddColor";
                this.menuAddColor.Size = new System.Drawing.Size(220, 22);
                this.menuAddColor.Text = "色をつける";
                // 
                // 色を解除ToolStripMenuItem
                // 
                this.色を解除ToolStripMenuItem.Name = "色を解除ToolStripMenuItem";
                this.色を解除ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
                this.色を解除ToolStripMenuItem.Text = "色を解除";
                // 
                // 貼るクリップボードToolStripMenuItem
                // 
                this.貼るクリップボードToolStripMenuItem.Name = "貼るクリップボードToolStripMenuItem";
                this.貼るクリップボードToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
                this.貼るクリップボードToolStripMenuItem.Text = "貼る（クリップボード）";
                // 
                // コピルクリップボードToolStripMenuItem
                // 
                this.コピルクリップボードToolStripMenuItem.Name = "コピルクリップボードToolStripMenuItem";
                this.コピルクリップボードToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
                this.コピルクリップボードToolStripMenuItem.Text = "コピる（クリップボード）";
                // 
                // ぐぐるToolStripMenuItem
                // 
                this.ぐぐるToolStripMenuItem.Name = "ぐぐるToolStripMenuItem";
                this.ぐぐるToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
                this.ぐぐるToolStripMenuItem.Text = "ぐぐる";

                googleSelfToolStripMenuItem.Name = "googleSelfToolStripMenuItem";
                googleSelfToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
                googleSelfToolStripMenuItem.Text = "Google検索";

                TimeStampMenuItem.Name = "TimeStampMenuItem";
                TimeStampMenuItem.Size = new System.Drawing.Size(220, 22);
                TimeStampMenuItem.Text = "現在時間を挿入する";
            }
            catch (Exception ex)
            {
                //DebMess(InitMenuStrip,ex);
            }
        }
    }
}
