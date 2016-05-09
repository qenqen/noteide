using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace WindowsFormsApplication4
{
    public interface ISettingIF
    {
        // string GetName();
         DialogResult ShowDialog();
         void AppendDrawLine(RichTextBox OutPutTarget);
         bool IsInLine(string line);
         object GetObject();
    }
    public class TextTree{
        
    }
    public class xFontPack :BasePack,ISettingIF{ 
        public FontDialog fd = new FontDialog ();
//        public Font obj;
        public xFontPack(string propName,string init,string format,string sample,bool bIsNext) {
            Name = propName;
            strInit = sample;
            strFormat = format;
            strSample = sample;
        }
        public object GetObject(){
            return fd.Font ;
        }
        public new void  AppendDrawLine(RichTextBox OutPutTarget) {
          
            //Font 
            Font FontBackup = OutPutTarget.Font;
            OutPutTarget.SelectionStart = OutPutTarget.TextLength;
            OutPutTarget.Text += String.Format("[{0}]#", Name);
            OutPutTarget.Text += String.Format("[{0}]#",fd.Font.ToString());
            //書式変更
            OutPutTarget.Font = fd.Font;
            OutPutTarget.Text += String.Format(strFormat , strSample );
            OutPutTarget.Font = FontBackup;
            return;
        
        }
        public new DialogResult ShowDialog()
        {
            DialogResult dr = fd.ShowDialog();
            return dr;
        }
    }
    public class xColorPack :BasePack, ISettingIF
    { 
        public ColorDialog  cd =new ColorDialog ();
        public Color obj;
        public xColorPack(string propName, string init, string format, string sample, bool bIsNext)
        {
            Name = propName;
            strInit = sample;
            strFormat = format;
            strSample = sample;
        }
        public void  AppendDrawLine(RichTextBox OutPutTarget) {
          
            //Color
            Color ColorBackup = OutPutTarget.ForeColor  ;
            OutPutTarget.SelectionStart = OutPutTarget.TextLength;
            OutPutTarget.Text += String.Format("[{0}]#", Name);
            OutPutTarget.Text += String.Format("[{0}]#",cd.Color .ToString());
            //書式変更
            OutPutTarget.ForeColor = cd.Color ;
            OutPutTarget.Text += String.Format(strFormat , strSample );
            OutPutTarget.ForeColor = ColorBackup;
            return;
        
        }
        
        public object GetObject(){

            return cd.Color ;
        }
        public new DialogResult ShowDialog()
        {
            //ColorDialog fd = new ColorDialog();
            cd.Color = (Color )obj ;
            DialogResult dr = cd.ShowDialog();
            if (dr == DialogResult.OK) {
                obj = cd.Color;
            }
            return dr ;
        }
    
    }
    public class BasePack{
        public string Name;
        public string strInit;
        public string strFormat;
        public string strSample;

    
        public string GetName(){return Name;}
        public bool IsInLine(string line) {
            return ((line.IndexOf (Name ) >= 0)? true :false );
        }
        public DialogResult ShowDialog()
        {
            return MessageBox.Show("error");
        }
        public void AppendDrawLine(RichTextBox OutPutTarget)
        {

        }
    }
    public class xBackColorPack :BasePack, ISettingIF
    {
        public ColorDialog cd = new ColorDialog();
        public Color obj;
        
              
        public void  AppendDrawLine(RichTextBox OutPutTarget) {
          
            //Color
            Color ColorBackup = OutPutTarget.BackColor  ;
            OutPutTarget.SelectionStart = OutPutTarget.TextLength;
            OutPutTarget.Text += String.Format("[{0}]#", Name);
            OutPutTarget.Text += String.Format("[{0}]#", cd.Color.ToString());
            //書式変更
            OutPutTarget.BackColor = cd.Color ;
            OutPutTarget.Text += String.Format(strFormat , strSample );
            OutPutTarget.BackColor  = ColorBackup;
            return;
        
        }

        public object GetObject()
        {
            return cd.Color;
        }
       
        public new DialogResult ShowDialog()
        {
            //ColorDialog fd = new ColorDialog();
            cd.Color = (Color )obj ;
            DialogResult dr = cd.ShowDialog();
            if (dr == DialogResult.OK) {
                obj = cd.Color;
            }
            return dr ;
        }

    }
    public class xTimekSpanPack : BasePack, ISettingIF
    {
        //public string Name;
        //public  cd = new ColorDialog();
        //        public Color obj;
        public object GetObject()
        {
            return null;//cd.Color;
        }


    }


    public class xStringPack : BasePack, ISettingIF
    {
        //public string Name;
        //public  cd = new ColorDialog();
        //        public Color obj;
        public object GetObject()
        {
            return null;//cd.Color;
        }


    }
    public class xBasePack :BasePack, ISettingIF
    {
        //public string Name;
        //public  cd = new ColorDialog();
        //        public Color obj;
        public object GetObject()
        {
            return null;//cd.Color;
        }
       
    }
    public class propControl 
    {
        public enum enPropType{
        str,integer,font,color,time
        }
        public Dictionary<string, object> dic;
        //public Dictionary<string, object> obj2dic;//ＯＢＪがわかれば、名前がわかる
        public object AddElement(enPropType typ, string name, string init, string format, string sample)
        {
            if (dic.ContainsKey(name )) {
                MessageBox.Show ("エラー：オブジェクト名の重複");
                return null;
            }
            object obj=null ;
            switch (typ) {
                case enPropType.str :
                   // obj = new xStringPack (name ,init,format ,sample,false );

                    break;
                case enPropType .color:

                    break;
                case enPropType.font:

                    break;
                case enPropType.integer:

                    break;
                default :
                    obj = null;
                    break;
            }
            dic.Add  (name,obj);
            return obj;

        }
        public propControl()
        {
            
        }
        public DialogResult  ShowDialog(string name){
            xBasePack If= (xBasePack)dic[name];
            return If.ShowDialog ();
        }
    
    }
}
