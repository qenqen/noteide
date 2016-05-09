using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

namespace WindowsFormsApplication4
{
    public class debMessageLog
    {
        public debMessageLog(RichTextBox txtbox)
        {
            txt = txtbox;
        }
        private RichTextBox txt;
        public string Text  // read-write instance property
        {
            get
            {
                return txt.Text;
            }
            set
            {


                txt.Text = value + "\n";
            }
        }
        public static string operator +(debMessageLog d, string t)
        {
            return (d.txt.Text + t);
        }
    } 
}
