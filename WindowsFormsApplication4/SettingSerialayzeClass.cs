using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Drawing;
namespace WindowsFormsApplication4
{
    interface IStgElmentBase {

        DialogResult ShowDialog(); 
    }
    public class StgElmentBase<T> : IStgElmentBase
    
    {
       // T value ;
       // string sampleword;
        Object obj;
        string format;
      //  bool bIsColor=false ;
     //   bool bIsFont=false ;
      //  bool IsFont(){return bIsFont ;}
       // bool IsColor(){return bIsColor;}
        interface  ISettingElm{
            // String name;
             void ConvertMethode(String valstr);
        }
        
        public StgElmentBase(string value) { 
            Set(value);
        }
        
        public  DialogResult ShowDialogFont()
        {

            FontDialog fd = new FontDialog();
            fd.Font = (Font)obj;
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                obj  = (object )fd.Font;
            }
            return dr;
        }
        public DialogResult ShowDialogColor()
        {
            ColorDialog fd = new ColorDialog();
            fd.Color = (Color )obj ;
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                obj =(object ) fd.Color;
            }
            return dr;
        }

        public DialogResult ShowDialog() {
            if(typeof(T)== typeof(Font)) {
                return ShowDialogFont();
            }
            else if (typeof(T) == typeof(Color))
            {
                return ShowDialogColor();
            }
            return DialogResult.Abort ;
            
        }
         public DialogResult ShowDialog(int dummy){
            Font font=(Font)Obj ;// = new System.Drawing.Font ();
            Color color=(Color )Obj ;
            DialogResult dr;
            if (IsFont())
            {
                dr = ShowDialogFont();


            }
            else if (IsColor ())
            {
                dr = ShowDialogColor(  );
                if(dr ==  DialogResult.OK ){
                    obj = (object )color ;
                    return dr ;
                }
            
            }
            return DialogResult.Abort ;
         }
        
         public void WriteSampleWord(RichTextBox rtb)
         {
             rtb.SelectionStart = rtb.TextLength ;
             
             rtb.Text  += obj.ToString () + "#"+String.Format (format);
             if(IsFont ()){
                
             }
             return;
         }
         public bool IsFont()
         {
             return obj.GetType().Equals(typeof(Font));
         }
         public bool IsColor()
         {
             return obj.GetType().Equals(typeof(Color));
         }
         public T Get() {
             return (T)obj;
         }
         public new  String ToString()
         {
             return ((T)obj).ToString ();
         }
         public new String ToArgb()
         {
             return ((int)((Color)obj).ToArgb ()).ToString ("X6");
         }
         public void Set(Font fnt,FontStyle newStyle) {
            
            obj =   new Font (fnt, newStyle);
         }
         public void Set(string fntName,float newSize) {
            Font fnt = new Font( fntName,newSize);
            obj =  fnt;
         }
         public void Set(string val) {
             if (typeof(T) == typeof(Font))
             {
                 string[] str=                val.Split(',');
                 float sz =(float ) Convert.ToDouble(str[1]);
                 Set(str[0], sz);
             }else
                 if (typeof(T) == typeof(Color))
                 {
                     int i =
                               Convert.ToInt32(val, 16);
                     int b = (i & 0xff);
                     int g = (i >> 8) & 0xff;
                     int r = i >> 16 & 0xff;
                     obj = Color.FromArgb(i);

                 }
                 else if (typeof(T) == typeof(int))
                 {
                     int i =
                               Convert.ToInt32(val, 10);
                     obj = i;
                     
                 }
         
         }
        
        public String SampleWord;
        public String FormatString{  get;set;        }
        public Object  Obj{  get{return obj;} set{obj = value ;} }       
       
        
       
        public bool IndexOfLine(string linestr){
            if (linestr.IndexOf (SampleWord)>=0){
                return true ;
            }
            return false ;
        }
        public new Type GetType()
        {
            return typeof(T);
        }

    }//StgElmentBase
    public class StgList{
        public  Dictionary <string,object> dic= new Dictionary<string,object> ();
        
        public StgList(){
            dic.Add("検索キーワード色", new StgElmentBase<Color>("0000FF"));
            dic.Add("テキスト背景色", new StgElmentBase<Color>("0F0F0F"));
            dic.Add("行文字数オーバー文字マーカー色", new StgElmentBase<Color>("5FFF3F"));
            dic.Add("行文字数", new StgElmentBase<int >("40"));
            dic.Add("半角文字マーカー色", new StgElmentBase<Color>("0xF03f3f"));
            
            //
            //MS UI Gothic, 9pt
        //    dic.Add("", new StgElmentBase<Color>("検索キーワード色", "0000FF"));
        }
        public DialogResult ShowDialog(string name_value) {
            if (name_value.IndexOf(':') <= 0)
            {
                Console.Write(".");
                return DialogResult.Abort;
            }

            string[] nv = name_value.Split(':');
            nv[0].Trim();
            if (dic.ContainsKey(nv[0]) == false)
            {
                Console.Write(",");
                return DialogResult.Abort;
            };
            Object o2 = dic[nv[0]];
            Type tp = dic[nv[0]].GetType();
            if (o2.GetType() == typeof(StgElmentBase<Font>))
            {

                StgElmentBase<Font> o = (StgElmentBase<Font>)o2;
                
                Console.WriteLine(o2.ToString() + " " + nv[1] + "=" + o.Get());
                return o.ShowDialog(); ;
            }
            if (o2.GetType() == typeof(StgElmentBase<Color>))
            {
                StgElmentBase<Color> o = (StgElmentBase<Color>)o2;
                o.Set(nv[1]);
                Console.WriteLine(o2.ToString() + " " + nv[1] + "=" + o.Get());
                return o.ShowDialog();
            }
            if (o2.GetType() == typeof(StgElmentBase<int>))
            {
                StgElmentBase<int> o = (StgElmentBase<int>)o2;
             
                Console.WriteLine(o2.ToString() + " " + nv[1] + "=" + o.Get());
                return o.ShowDialog(); ;
            }
            {
                Console.WriteLine(o2.GetType().ToString() + "_");


            }
            return DialogResult.Abort;
        }
        public bool ReadLine(string name_str)
        {
            if (name_str.IndexOf(':') <= 0)
            {
                Console.Write(".");
                return false;
            }
            
            string[] nv = name_str.Split(':');
            nv[0].Trim ();
            if(dic.ContainsKey (nv[0])==false ){
                Console.Write(",");
                return false;            
            };
            Object o2 = dic[nv[0]];
            Console.WriteLine(">>>"+o2.GetType().ToString());
            if (o2.GetType() == typeof(StgElmentBase<Font >))
            {

                StgElmentBase<Font> o = (StgElmentBase<Font>)o2;
                    o.Set(nv[1]);
                    Console.WriteLine(o2.ToString() + " " + nv[1] + "=" + o.Get());
                    return true;
            }
            if (o2.GetType() == typeof(StgElmentBase<Color>))
                {
                    StgElmentBase<Color> o = (StgElmentBase<Color>)o2;
                    o.Set(nv[1]);
                    Console.WriteLine(o2.ToString() + " " + nv[1]+"="+o.Get());
                    return true;
            }
            if (o2.GetType() == typeof(StgElmentBase<int>))
                {
                    StgElmentBase<int> o = (StgElmentBase<int>)o2;
                    o.Set(nv[1]);
                    Console.WriteLine(o2.ToString() + " " + nv[1] + "=" + o.Get());
                    return true;
            }
            {
                Console.WriteLine(o2.GetType().ToString() + "_");


            }
            

            return false;
            
        }
        public string  OutLine(string name_str)
        {
            if (name_str.IndexOf(':') <= 0)
            {
                Console.Write(".");
                return "";
            }

            string[] nv = name_str.Split(':');
            nv[0].Trim();
            if (dic.ContainsKey(nv[0]) == false)
            {
//                Console.Write;
                return (",");
            };
            Object o2 = dic[nv[0]];
            Console.WriteLine(">>>" + o2.GetType().ToString());
            if (o2.GetType() == typeof(StgElmentBase<Font>))
            {

                StgElmentBase<Font> o = (StgElmentBase<Font>)o2;
               
                //  Console.WriteLine(o2.ToString() + " " + nv[1] + "=" + o.Get());
                return nv[0]+":"+o.ToString ();
            }
            if (o2.GetType() == typeof(StgElmentBase<Color>))
            {
                StgElmentBase<Color> o = (StgElmentBase<Color>)o2;
                
                Console.WriteLine(o2.ToString() + " " + nv[1] + "=" + o.Get());
                return nv[0] + ":" + o.ToArgb();
            }
            if (o2.GetType() == typeof(StgElmentBase<int>))
            {
                StgElmentBase<int> o = (StgElmentBase<int>)o2;
                
                Console.WriteLine(o2.ToString() + " " + nv[1] + "=" + o.Get());
                return nv[0] + ":" + o.ToString () ;
            }
            {
                Console.WriteLine(o2.GetType().ToString() + "_");


            }


            return nv[0] + ":" + "[non]";

        }
        public bool  ReadLine2(string name_str){
            foreach (KeyValuePair<string, object> pair in dic) {

                if (name_str.IndexOf(':') <= 0)
                {
                    Console.Write( ".");
                }
                else if (name_str.Trim().IndexOf(pair.Key) == 0)
                {
                    //v =  StgElmentBase<Color>(pair .Value ;

                    string[] nv = name_str.Split(':');
                    if (nv[0].Trim() != pair.Key)
                    {
                        Console.WriteLine(nv[0] + " <> " + nv[1]);
                        continue;
                    }
                    else if (pair.Value.GetType() == typeof(Font))
                    {
                        StgElmentBase<Font> o = (StgElmentBase<Font>)pair.Value;
                        o.Set(nv[1]);
                        Console.WriteLine(pair.Value.ToString() + " " + nv[1]);
                        return true;
                    }
                    else if (pair.Value.GetType() == typeof(Color))
                    {
                        StgElmentBase<Color> o = (StgElmentBase<Color>)pair.Value;
                        o.Set(nv[1]);
                        Console.WriteLine(pair.Value.ToString() + " " + nv[1]);
                        return true;
                    }
                    else if (pair.Value.GetType() == typeof(int))
                    {
                        StgElmentBase<int> o = (StgElmentBase<int>)pair.Value;
                        o.Set(nv[1]);
                        Console.WriteLine(pair.Value.ToString() + " " + nv[1]);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine(pair.Value.GetType().ToString() + " ");


                    }
                }
                else {
                    Console.Write(",");
                } 
            }
           return false;
        }
    }
   
    #if false 

    public class  StgElment<T>:StgElmentBase
        where T:new(){
          public StgElment(T t,string n,string setFormat,string semothing) {
              Obj = (Object)t;
              SampleWord= semothing ;
              FormatString = setFormat;
        }
   
       
    }
    public class StgElmentDic<U>{
            Dictionary<string, StgElmentBase> dic = new Dictionary<string, StgElmentBase>();//     
//        StgElmentDic
            Dictionary<int, Type> typedic;
            public void SetAddTypeDic(Type type)
            {
                typedic.Add(   type.MetadataToken,type);
            }
            public StgElmentDic() {
                SetAddTypeDic(typeof(Font));
                SetAddTypeDic(typeof(Color));
            }
            public void Add(string name,U obj,string sampleword)
            {
                string format;
                Type  typemt=typedic[typeof(U).MetadataToken];
                if(typemt==typeof(Font)){            format = "";
                }
                else if (typemt == typeof(Color)) { format = "";
                }            
                
                   
               
                StgElment<U> sg =new StgElment<U>(obj , name ,format,sampleword) ; dic.Add(name, sg);
            }
            //public void Add(string name)
            //{
            //    dic.Add(name, (new StgElment<Color>(color, "フォント色", "例")));
            //}
    }
    [System.Serializable]
    public class SettingSerialayzeClass
    {
        public SettingSerialayzeClass(){
//            StgElment<Font>
            StgElmentBase SeB=new StgElmentBase ();
            
           // Color color = new Color ();
        
        }
        //Color,Size,
        //StgElment <T>
    }
#endif
}
