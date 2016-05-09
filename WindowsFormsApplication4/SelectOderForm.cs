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
    public partial class SelectOderForm : Form
    {
        string resulttext;
        //string[] requests;
        List<RadioButton> proposala = new List<RadioButton>();
        public SelectOderForm()
        {
            InitializeComponent();
        }

        private void OderlistBox1SavenameListExistNewFolder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)(sender);
            resulttext = btn.Text;
        }
        public string ResultText
        {
            set { resulttext = value; }
            get { return resulttext; }
        }
        //public NewButton
        public string [] ButtonTexts
        {
            set
            {
               
                Size sz = btnA1.Size;
                Point  lc = new Point( btnA1.Left,btnA1.Top  );
                AnchorStyles anchor;
                int DistY=btnA2 .Top - btnA1.Top ;
                 
                anchor = btnA1.Anchor;
                foreach (string str in value)
                {
                    if (str == "")
                    {

                    }
                    else {

                        RadioButton nb = new RadioButton();
                        nb.Checked = false;
                        nb.Click +=  radioButton1_CheckedChanged;
                        nb.Text = str;
                        btnA1.Size = sz;
                        btnA1.Location = lc;
                        proposala.Add(nb);
                        lc.Y += DistY;
                    
                    }

                }
//                requests = value; 

            //ボタンを作成する
            
            }
        }
        
    }
}
