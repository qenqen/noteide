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

//using System.IO;
//using System.Text;
using Microsoft.VisualBasic;
namespace WindowsFormsApplication4
{

     
    public partial class Form1 : Form

    {
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int mciSendString(String command,
           StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
        ExChangeCombox2ToolStripComboBox SearchWord;
        debMessageLog Mess;
        public StgList stglst = new StgList();
        public delegate void TreeTagClickCallBack(TreeNode TN);
        bool listReading= false ;
        public PublicToolBox ptb;
        bool ListReaded{
            get{return listReading;}
            set{
                listReading = value;
                if(value){
                    MenuBtnOpenOtherStoryList.Enabled = false ;//.Visible = false ;
                    MenuBtnOpenOtherStoryList.Enabled   = true ;
                }else{
                    MenuBtnOpenOtherStoryList.Enabled = true ;//.Visible = false ;
                    MenuBtnOpenOtherStoryList.Enabled   =false  ;
                    
                }
            
            }
        }
        string caption;
        string Caption 
        {
            get { return caption  ; }
            set
            {
                caption = value;
                this.Text = "BigMouseEdita " +value ;

            }
        }
        //TabControl tabControl1;
        //TabPage myTabPage;
        //string mainstoryDirname;
       // ListMakeNote[] LMN=new ListMakeNote[1];
        
        
       Dictionary<string, string> safedata=new Dictionary<string,string>();
        private string  safefilelist=Properties.Settings.Default.ListFilename    ;
        string SafeFileList
        {
            get { return safefilelist; }
            set {
                if (safefilelist == value)
                {
                }
                else
                {
                    //SafeFileList = value;
                    safefilelist = value;
                    Properties.Settings.Default.ListFilename = value;
                    Properties.Settings.Default.Save();
                    RequestRenewListFileNameView();
                    
                }
            }

        }
      

       public struct safedatasElm
       {
          public string name;
          public string fname;
       }
       Dictionary<string, NotePageBase> LMNControl = new Dictionary<string, NotePageBase>();
       Dictionary<NotePageBase, string> LMNDelList = new Dictionary<NotePageBase, string>();
       Dictionary<TabPage, NotePageBase> ActiveLMNSearchlist = new Dictionary<TabPage, NotePageBase>();
       Dictionary<string, BookMarkElm> BookMark = new Dictionary<string, BookMarkElm>();
       SearchKeywords skw=new SearchKeywords();
       
       class BookMarkElm 
       {
           Form1   bs;
           TabPage tp;
           NotePageBase lmn;
           int SelectionStart;
           int SelectionLength;
           public BookMarkElm(Form1 frm, NotePageBase lst, int RetrunPos, int MarkLength, TabPage tabPage)
           {
                lmn=lst;
               SelectionStart = RetrunPos;
               SelectionLength = MarkLength;
               bs = frm;
               tp = tabPage;
           }
           public void Jump()
           {
                lmn.GetTxt().SelectionStart = SelectionStart ;
                lmn.GetTxt().SelectionLength = SelectionLength;
                bs.tabStory.SelectedTab = tp;
           }
           
       }
       struct deleteDataElm
       {
           public ListMakeNote lst;
           string dumy;
       }
//       List<safedata> sda;
       void ReNewStroyFileListFilenameView()
       {
           Caption = GetFileListFileName();
           ListFileNameView.Text = System.IO.Path.GetFileName(safefilelist);
       }

        private void ReNewTabControl(){
         //   tabControl1 = new TabControl();
    //.TabControl..void      tabControl1 = tabControl1org.();MemberwiseClone();
            //tabcnt1
        }
    
        private void  ValidateFileListName()
             
        {
                   
            if (Properties.Settings.Default.ListFilename !=System.IO.Path.GetFileName( safefilelist))
        
            {
           

    //            System.IO.Path.GetFileName( 
                Properties.Settings.Default.ListFilename = System.IO.Path.GetFileName(safefilelist);
                Properties.Settings.Default.Save();
        
            }
  
        }
       
        public ToolStripStatusLabel  GetToolStriplabel(){
            return toolStripStatusLabel0;
        }
        
        public string GetStroyDir()
        {
            return Properties.Settings.Default.MainDir;
        }
        
        public string GetFileListFileName(){
    //        safefilelist
            return Properties.Settings.Default.MainDir +"\\"+    System.IO.Path.GetFileName(safefilelist);
    

        }
        public string GetFileListTestName(string fname)
        {
            //        safefilelist
            return Properties.Settings.Default.MainDir + "\\" + System.IO.Path.GetFileName(fname );


        }
        public string GetDirTextFileListFileName(string TestMainDirName)
        {
            return TestMainDirName + "\\" + System.IO.Path.GetFileName(safefilelist);
        }

    public void loadReraxation(bool bVallidateForm=false ) {
        IntervalTimer_reraq.Enabled = false;
        Intervaltimer1.Interval = Properties.Settings.Default.ReraxationInterval * 60*1000;
        IntervalTimer_reraq.Interval = Properties.Settings.Default.ReraxationTime * 1000;
        Intervaltimer1.Enabled = Properties.Settings.Default.ReraxationEnable;
        if (bVallidateForm) {
            ReraxationIntarval.Value = Properties.Settings.Default.ReraxationInterval;
            ReraxationTime.Value = Properties.Settings.Default.ReraxationTime;
            btmEnableReraxation.Checked = Properties.Settings.Default.ReraxationEnable;
            lblFileName.Text = Properties.Settings.Default.ReraxationMp3 ; 
        }
    }
    public void SetReraxation(int interval_minutes, int time_second,bool enable ) {
        Properties.Settings.Default.ReraxationInterval = interval_minutes;
        Properties.Settings.Default.ReraxationTime = time_second;
        Properties.Settings.Default.ReraxationEnable = enable;
        loadReraxation();
    }
    //SetReraxation(ReraxationIntarval.Value, ReraxationTime.Value, btmEnableReraxation.CheckState  ); 

        public void InitSKW(){
            if (LMNControl.ContainsKey(".Persons") == false)
            {
                DebMess("Persons.non");
                return;
            }
            if (LMNControl.ContainsKey (".KeyWords") == false)
            {
                DebMess("KeyWords.non");
                return;
            }
            skw.make(LMNControl[".Persons"], LMNControl[".KeyWords"]);
        
        }

        public Form1(string [] args)
        {
            if (args.Length > 0) {
                if (args[0].Length > 0) { 
                    //
                
                }
            }
        
        }
        public Form1()
        {
//            btmTxt.Size.Height = 20;
  //          btmTxt.Size.Width  = 20;
          //  dic = new SerializableDictionary [];

            
            
            InitializeComponent();
            //InitMenuStrip();
            Mess = new debMessageLog(bodyMess );
            openFileDialog1.FileName =safeSettingfileDlg.FileName = "StoryList.lst";
            openFileDialog1.Filter = safeSettingfileDlg.Filter = Properties.Resources.FiletFILETYPE_LST; // "リストファイル(*.lst)|*.txt;*lst";
            
            // contextMenuStrip1
            // 

            loadReraxation(true  );

            if( Properties.Settings.Default.MainDir ==""){
                SelectMainDir();
                
            }
            SearchWord = new ExChangeCombox2ToolStripComboBox(SearchWordBox);
            LoadStroyList(false );//ここでは開ければ開く、開けなければそのまま。
        }
        int flagRequestRenewListFileNameView=0;
        int flagRequestRenewTreeView = 0;
        int flagRequestRenewStoryLsitBox = 0;

        public void RequestRenewListFileNameView(){
            flagRequestRenewListFileNameView++;
            DebMess(">>RequestRenewListFileNameView" + flagRequestRenewListFileNameView.ToString() );
        }
        public void RequestRenewTreeView()
        {
            flagRequestRenewTreeView++;
            DebMess(">>RequestRenewTreeView" + flagRequestRenewTreeView.ToString());

        }
        public void RequestRenewStoryLsitBox()
        {
            flagRequestRenewStoryLsitBox++;
            DebMess(">>RequestRenewStoryLsitBox" + flagRequestRenewListFileNameView.ToString());

        }
        public void opeRenewFormParts()
        {
            if (flagRequestRenewListFileNameView > 0)            ReNewStoryLsitBox();
            if (flagRequestRenewTreeView>0) ReNewTreeView();
            if (flagRequestRenewStoryLsitBox>0) ReNewStroyFileListFilenameView();
            flagRequestRenewListFileNameView = 0;
            flagRequestRenewTreeView = 0;
            flagRequestRenewStoryLsitBox = 0;
        
        }
        public void LoadStroyList(bool ChangeFileName = false ) {
            bool b;
            DebMess("LoadStroyList");
            b = CheckStoryListExisting();
            if (b)
            {
                //safefilelist=Properties.Settings.Default.ListFilename
                //    = (Properties.Settings.Default.MainDir == "") ?
                //  ;

                XmlRead(GetFileListFileName());
                //成功したら、パスのセーブ
                ValidateFileListName();
               // opeRenewFormParts();
                RequestRenewListFileNameView();
                RequestRenewTreeView();
                RequestRenewStoryLsitBox();
                //opeRenewFormParts
              //
                //Caption   = GetFileListFileName();
                 
            }
            else
            {
                //
               
            }
            DebMess("LoadStroyList end");
        }
        public bool CheckStoryListExisting() {
            string flname = GetFileListFileName();
//            lblStoryListFileName.Text = flname;
            bool b= System.IO.File.Exists(flname);
            //Check Log
            
            if (b)
            {
                if (flname == GetStroyDir()) b = false;
                DebMess("CheckStoryListExisting=>hit! " + flname);
            }
            else {
                DebMess("CheckStoryListExisting=>Not Exists" + flname);
            }
            return b;
        
        }
        class ViewOrComandBox : RichTextBox
        {


        }
        private NotePageBase      GetActiveLMN()
        {
            NotePageBase lmn = ActiveLMNSearchlist[tabStory.SelectedTab];
            return lmn;
        }
        private NotePageBase GetTabPage(string name_str)
        {
            NotePageBase lmn = ActiveLMNSearchlist[(TabPage )tabStory.Controls [name_str ]];
            return lmn;
        }

        public void enumKeyInControl(Control.ControlCollection scc)
        {
            DebMess(string.Format ("enumKeyInControl [count={0}]" ,scc.Count.ToString()) );
            foreach (Control tpx in scc)
            {
                DebMess(tpx.Name);
            }
        }
        public void SetSearchWordBox(string str ) {
            
            GetActiveLMN().Word.Text = str;
            GetActiveLMN().Word.Items.Add(str);

            this.SearchWordBox.Text = str;
            this.SearchWordBox.Items.Add(str);
        }
        public  void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip menu = (ContextMenuStrip)sender;
            //ContextMenuStripを表示しているコントロールを取得する
            Control source = menu.SourceControl;
            //  toolStripStatusLabel5.Text = "イベント" + source.Text + "-" + source.Name + "+" + e.ClickedItem.Text;
            //          

            if (source != null)
            {
                string str;
                //                TextBox 
                //RichTextBox tb = (RichTextBox)e.ClickedItem;
                int contkey = tabStory.SelectedTab.Controls.IndexOfKey("splitContainer1");
                DebMess("context contkey2=[" + contkey.ToString() + "/" + e.ClickedItem + ";" + tabStory.SelectedTab.Controls.Count.ToString() + "][" + e.ToString() + "]");
                SplitContainer  sc1 = (SplitContainer)tabStory.SelectedTab.Controls[contkey];
                int cont2key = sc1.Panel2.Controls.IndexOfKey("splitContainer2");
                enumKeyInControl(sc1.Panel2.Controls );
                SplitContainer sc2 = (SplitContainer)sc1.Panel2.Controls[cont2key];
                enumKeyInControl(sc2.Panel1.Controls  );
                int tbcntkey = sc2.Panel1.Controls.IndexOfKey("Txt");
                
//              
                DebMess("Name/Type=[" + sc2.Panel1.Controls[tbcntkey].Name + "/" + sc2.Panel1.Controls[tbcntkey].GetType());
               
                DebMess("context tabkey=[" + tbcntkey.ToString() + "/" + cont2key.ToString () + "]");
                if (tbcntkey >= 0)
                {
                    RichTextBox rb = (RichTextBox)sc2.Panel1.Controls[tbcntkey];
                    NotePageBase  lmn = GetActiveLMN();
                    TabControl tc = tabControl2;
                    //e.ClickedItem.
                    NotePageBase slmn = GetSettingPage();
                    str = (rb != null) ? rb.SelectedText : "";
                    DebMess("context rb.Text:["+str +"]["+e.ToString ()+"]" );
                    DebMess(e.ClickedItem.Text);
                    switch (e.ClickedItem.Text)
                    {
                        case "保存":
                            lmn.LMN.saveFile();
                            break;
                        case "切る":
                            if (rb != null)
                            {
                                ReplaceWordtxt.Text = rb.SelectedText;
                                rb.SelectedText = "";
                                //           
                            }

                            break;
                        case "貼る":
                            if (rb != null)
                            {
                                rb.SelectedText = ReplaceWordtxt.Text;//:"＜エラー＞RichTextが見つかりません";
                            }
                            break;
                        case "コピー":
                            //.text=
                            this.ReplaceWordtxt.Text = str;
                            break;
                        case "検索へ":
                            //.text=
                            //                    txtKeyWord.Text.Insert(0, textbs[0].SelectedText);
                            //                        おかしな動作をするの txtKeyWord.SelectedText は使わない。
                            
                            //MethodeReadSettingLine(slmn);
                            if (str == "")
                            {
                                str = GetSelectTextClickLine(rb);
                            }
                            SetSearchWordBox(str);
                            // /
                            break;
                        case "ZoomOut":
                            rb.ZoomFactor = rb.ZoomFactor * (float)0.7;
                            break;
                        case "ZoomIn":
                            rb.ZoomFactor = rb.ZoomFactor * (float )1.4; 
                            break;
                        case "キーワードへ":
                            //.text=
                            //                    txtKeyWord.Text.Insert(0, textbs[0].SelectedText);
                            //                        おかしな動作をするの txtKeyWord.SelectedText は使わない。
                            string kwts=".KeyWords";
                            // int tindex = tc.Controls.IndexOfKey(kwts);
                            //string rbkw = tc.Controls.ContainsKey (
                            NotePageBase kw = GetTabPage(kwts);
                                kw.GetTxt ().AppendText("\n"+str+"\n");
                            
                            // /
                            break;
                        case "人名へ":
                            //.text=
                            string pnts=".Persons";
                            NotePageBase pn = GetTabPage(pnts);
                                pn.GetTxt ().AppendText("\n"+str+"\n");
                            break;

                        case "色を解除":
                            if (rb != null)
                            {
                                lmn.LMN.RemoveSelectWords(rb);
                            }
                            break;

                        case "色をつける":
                            
                            if (rb != null)
                            {
                                if (rb.SelectionLength != 0)
                                {
                                    this.SearchWordBox.Text = str;
                                }
                            }
                            lmn.LMN.SeachWord();

                            break;
                        case "コピる（クリップボード）":
                            if (rb != null)
                            {
                                Clipboard.SetText(rb.SelectedText);
                            }
                            break;
                        case "貼る（クリップボード）":
                            if (rb != null)
                            {
                                rb.SelectedText = Clipboard.GetText();
                            }
                            break;
                        case "ぐぐる":
                            if (rb != null)
                            {
                                this.UrlList.Text = rb.SelectedText;

                                startwebbrowser();

                            }

                            break;
                        case "Google検索":
                            if (rb != null)
                            {
                                //this.UrlList.Text = rb.SelectedText;
                                string keyword= GetSelectTextClickLine(rb);
                                DebMess(">>"+keyword +"<<");
                                lmn.startwebbrowserFromKeyword(keyword);

                            }

                            break;
                        case "文章体裁確認":
                            if (rb != null )
                            {
                                //RichTextBox rtb;
                                //rtb = getActiveTextBox();
                                //          }
                                //SelectWordColoring(rtb, s, System.Drawing.Color.Yellow);
                                MethodeReadSettingLine(slmn);
                                int v = ((StgElmentBase<int>)stglst.dic["行文字数"]).Get();//(Int32)Decimal.ToInt32(40);
                                lmn.LMN.lineOverRunSelectWordColoring(rb, v);
                                lmn.LMN.InLineHANKAKUstringMarker(rb);
                                this.toolStripStatusLabel0.Text = "文字カウント" + v + "文字以上 " + rb.Font.ToString();
                                // public static bool isZenkaku(string str) {
                                // int num = sjisEnc.GetByteCount(str);
                                //  return num == str.Length * 2;

                            }

                            break;
                        case "現在時間を挿入する":
                            if (rb != null)
                            {
                                lmn.LMN.Txt.SelectedText = DateTime.Now.ToString();

                            }

                            break;
                        case "ダイアログ表示":
                            try
                            {
                                int ss = 0, sl = 0;
                                string propnameandval = GetSelectTextClickLine(rb, ref ss, ref sl);
                                stglst.ShowDialog(propnameandval);
                                rb.SelectionStart = ss;
                                rb.SelectionLength = sl;
                                rb.SelectedText = stglst.OutLine(propnameandval);


                            }
                            catch (Exception ex) { 
                                DebMess( "ダイアログ表示",ex);
                            }
                            break;
                        case "設定更新":
                            try
                            {
                                MethodeReadSettingLine(slmn);


                            }
                            catch (Exception ex)
                            {
                                DebMess("設定更新", ex);
                            }
                            break;
                        default:
#if false
                            if (e.ClickedItem.Text == Properties.Settings.Default.cmdMenuWord && rb != null)
                            {
                                bs.webBrowser1.Navigate(new Uri(Properties.Settings.Default.cmdMenuURL + UrlList.Text + rb.SelectedText));
                                AcvivePageWiki();
                            }
#endif
                            break;

                    }
                }else{
                    
                }
            }
        }
        
        public void startwebbrowser()
        {
            if (this.UrlList.Text == null || this.UrlList.Text.Length == 0)
            {
                return;
            }

            //! 指定されたURLのWebページを読み込む.
            try
            {
                this.webBrowser1.Navigate(new Uri("https://www.google.co.jp/search?q=" + this.UrlList.Text + "&ie=UTF-8"));
            }
            catch (Exception ex)//(System.UriFormatException)
            {
                this.DebMess(ex.Message);
                //                    debMessageLog]
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("button push b3"); 
//            string title = "button3_Click " + (tabControl1.TabCount + 1).ToString();
  //           myTabPage2 = new TabPage(title);
    //         tabControl1.TabPages.Add(myTabPage);
  //          tabControl1.TabPages.Add(myTabPage);
//            tabPage1.Controls.Add(new Button());
            //MouseClick = button3_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            // NameValueCollection confg= (NameValueCollection)System.Configuration.ConfigurationSettings.   ConfigurationSettings.GetConfig("file_ki_name");//System.Configuration.NameValueSectionHandler[];
            //            string value[2] = ["file_ki_name"];
#if false 
            System.Configuration.Configuration config =
    System.Configuration.ConfigurationManager.AppSetting
            OpenExeConfiguration(
    System.Configuration.ConfigurationUserLevel.None);

            config.AppSettings.Settings.Add(
    "Application Description", "MyApplicationDescription");
            config.AppSettings.Settings["Application Name"].Value = "MyNewApplication";
            string value = System.Configuration.ConfigurationSettings.AppSettings["file_ki_name"];
    //       System.Configuration.SettingsProperty[];  
#endif
            //  Configuration  cfig = System. 
            //        System.Configuration...ConfigurationManager;
            //
           // string userID = Properties.Settings.Default.userID;
          //  string value = userID; //System.Configuration.ConfigurationManager.AppSetting["file_ki_name"];
          //  MessageBox.Show(value);
          //  Properties.Settings.Default.userID = "NewUser";
            Properties.Settings.Default.Save();
            //System.Configuration.ConfigurationManager.AppSettings["file_ki_name"]);
        }
        

        private void ReplaceWordtxt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBodyBox_TextChanged(object sender, EventArgs e)
        {

        }

        
        
        public void SelectMainDir(){
        
            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.MainDir;
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "小説グループのメインフォルダを設定してください";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (ListReaded　&& File.Exists(GetDirTextFileListFileName(folderBrowserDialog1.SelectedPath)))
                {
                    //string btnText=
 //                   if( MessageBox.Show ("指定されたフォルダ上に現在のリストと同じ名前のファイルが存在します。\nフォルダ変更の前に現在のフォルダに保存し\n読み直しますか？","メインフォルダの設定",MessageBoxButtons.YesNo )==DialogResult .OK ){
                      if( MessageBox.Show ("指定されたフォルダ上に現在のリストと同じ名前のファイルが存在します。","フォルダの変更" ,MessageBoxButtons.OK  )== DialogResult.OK ){
                          
                      }
                          //\nフォルダ変更の前に現在のフォルダに保存し\n読み直しますか？","メインフォルダの設定",MessageBoxButtons.YesNo )==DialogResult .OK ){
                    
  //                  }
                    string[] Commands =  { 
                        "中止します",
                   
                    };
#if false
                    SelectOderForm sof = new SelectOderForm();
                    sof.ButtonTexts = Commands;
                    if(DialogResult.OK == sof.ShowDialog () ){
                        switch (sof.ResultText) { 
                            case "":
                                break;
                            case "":
                                break;
                            case "":
                                break;
                            case "":
                                break;
                            case "":
                                break;

                        
                        }
                    
                    }
#endif
                }
                if (folderBrowserDialog1.SelectedPath != "")
                {
                    Properties.Settings.Default.MainDir = folderBrowserDialog1.SelectedPath;
                    Properties.Settings.Default.Save();
                }
                
            }
        }
        public bool CheckUsableNewName(string newname) {
            if (LMNControl.ContainsKey(newname)) return false;
            if (safedata.ContainsKey(newname)) return false;
            return true;
        }
        //Use TreeTagClickCallBack
        void TreeViewGoStroyListFile(TreeNode TN) 
        {
           // MessageBox.Show ("List");
            
            string newListFileName = TN.Text;
            //TreeNode tn = tv.SelectedNode;
            //string tmp = (TN.Tag != null) ? tv.SelectedNode.Tag.GetType().ToString() : "Not Tag";
            //DebMess(tv.ToString() + " < tv.SelectedNode .FullPath" + tv.SelectedNode.FullPath + " >" + tmp + " >" + tn.ToString());
            //                LoadStroyList(true);
            //                
            //                   string now = SafeFileList;
            if (File.Exists(GetFileListTestName(newListFileName)))
            {
                ListClose();
                SetDestroyAll(true );
                SafeFileList = (newListFileName);

                XmlRead(GetFileListFileName());
                //ReNewStoryLsitBox();
                RequestRenewStoryLsitBox();
            }
        
        }
        //Use TreeTagClickCallBack
        void TreeViewGoStroyPageAccess(TreeNode TN)
        {
          
            //  opeListClick(caractorBox, txtPersonNameList, cmbJumpName);
            string str = TN.Text ;
            //int i = tabControl1.TabPages.IndexOfKey(str.Trim());の使い方確認
            int i = tabStory.TabPages.IndexOfKey(str.Trim());
            //[str.Trim];
            if (i < 0)
            {

            }
            else
            {
                //txtBodyBox.Text = i.ToString();
                //tabControl1.TabPages[i].();  
                tabStory.SelectedIndex = i;
            }
        


        }

        private void ReNewTreeView() {
            DebMess("ReNewTreeView");

            TreeNode tn,tf,ts=null ;
            treeView1.Nodes.Clear();
            string[] files = System.IO.Directory.GetFiles(
                GetStroyDir(), "*.lst", System.IO.SearchOption.AllDirectories);
            tf = treeView1.Nodes.Add(GetStroyDir());
            foreach (string fullpath in files)
            {
                string fname;
                fname = fullpath.Substring(GetStroyDir().Length + 1);
                tn = tf.Nodes.Add(fname);
                tn.Tag = (Object)new TreeTagClickCallBack(TreeViewGoStroyListFile);
                if (fname == SafeFileList)
                {
                    ts = tn;
                }
                else {
                    ts = null ;
                }
                Dictionary <string , string > dic=new Dictionary<string,string> ();
                XmlRead(fullpath, ref dic);
                {
                    
                    foreach (KeyValuePair<string, string> pair in dic )
                    {
                        TreeNode filenode;
                        filenode=tn.Nodes.Add(pair.Key);
                        filenode.Tag = (object)new TreeTagClickCallBack(TreeViewGoStroyPageAccess);
                    }
                    //tn.Expand();
                    //tn.TreeView.
                }
                
            }
            if (ts != null) {
                ts.Expand();
            }
            if (tf != null)
            {
                tf.Expand();
            }
            DebMess("ReNewTreeView end");
        }
        private void ReNewStoryLsitBox(){
            txtListBox.Text = "";
            foreach (KeyValuePair  <string ,string > pair in safedata)
            {
                txtListBox.Text += pair.Key  + "\n";

            }

            txtListBox.Modified = false;
        }
        public bool ChangeLMN_Name(string name, string newname) {
            //check
            if (LMNControl.ContainsKey(newname)) return false;
            if (safedata.ContainsKey(newname)) return false;
            //stock
            string stockfilename = safedata[name];
            NotePageBase lst = LMNControl[name];
            //remove
            LMNControl.Remove(name);
            safedata.Remove(name);
            //add
            LMNControl.Add(newname, lst);
            safedata.Add(newname, stockfilename);
            //Call NameChange
            lst.LMN.ReName(newname);
            //
            //XmlWrite2();
            //txtListBox.Text = "";
            // opeRenewFormParts();
            RequestRenewTreeView();
            RequestRenewStoryLsitBox();
            //opeRenewFormParts            ReNewStoryLsitBox();
            //opeRenewFormParts            ReNewTreeView();
            BookMark.Clear();
            BookMarkList.ComboBox.Controls.Clear();

            return true;
        }
        
        public bool ChangeLMN_FileName(string name, string newfname)
        {
            //名前が変らないので、LMNControlはいじらない。
            safedata.Remove(name);
            safedata.Add(name, newfname);
            // opeRenewFormParts();
            RequestRenewListFileNameView();

            return true;


        }
       
        public Color SearchWordColor;// = new SettingColor(Color.Blue);
        public Color GetSearchWordColor()
        {
            Color defo;
            defo = Color.Blue;
            try
            {
             //   Color defo;
                NotePageBase lst;
                lst = LMNControl[".Settings"];

                

                return GetSearchWordColor(defo, lst);
            }
            catch (Exception ex) {
                return defo;
            }
        }
        public NotePageBase GetSettingPage() {
            try
            {
                if (LMNControl.ContainsKey(".Settings") != false)
                {
                    return LMNControl[".Settings"];
                }
                else {
                    return null ;
                }
            }catch(Exception ex){
                DebMess ("GetSettingPage",ex );
                return null;
            }
        }
        //リストから、テキストを読み出す関数と、そこから設定の読み出しを行う関数がごっちゃになっている。
        //MethodeReadSettingLineは与えられた頁から設定構文を介錯する
        //しかし、常に行う必要は無い。
        public void MethodeReadSettingLine(NotePageBase npb)
        {
            //StgList
            if(npb == null ){
                DebMess("NO .Setting page");
                return;
            }
            DebMess("MethodeReadSettingLine<" + npb.LMN.Name);
            if (npb.GetTxt().Modified == true)
            {
                foreach (string str in npb.GetTxt().Lines)
                {
                    stglst.ReadLine(str);
                    DebMess(str);
                }
                npb.LMN.saveFile();
                npb.GetTxt().Modified = false;
            }
        }
        public Color GetSearchWordColor(Color defo,NotePageBase lst  ) { 
                 
            foreach (string str in lst.GetTxt().Lines)
            {
                int si;
                if ((si = str.IndexOf(':')) > 0)
                {
                    if (str.Trim().IndexOf("検索キーワード色") == 0)
                    {
                        //Color.Red.
                        int i =
                            Convert.ToInt32(str.Substring(si + 1), 16);
                        SearchWordColor = Color.FromArgb(i);
                        DebMess(str);
                        break;
                    }
                    
                    //StgList                     dic.Add("",new StgElmentBase<Color>("検索キーワード色","0000FF"));

                }
            }
                return SearchWordColor;      
  
        }
        private void SetNewLMN(string newtitle)
        {


            try
            {
                NotePageBase lst = new NotePageBase(
                    this,
                    tabStory,
                    newtitle,
                    safedata[newtitle]
                    //               , inputText
                    //             , ""
                    );
               // LMN[LMN.Length - 1] = lst;

               LMNControl.Add(newtitle, lst);
               ActiveLMNSearchlist.Add(lst.GetTabPage(),lst  );
               if( lst.LMN.Name == ".Settings"){
                  // GetSearchWordColor(Color.Blue ,lst);
                   MethodeReadSettingLine(lst);
               }
            }catch(Exception ex ){
                DebMess("[SetNewLMN]" + ex.Message);
            
            }
            
        }

       
        // private void 
        private void OpenNewTab(string newtitle = "" ) {

            //string newtitle;

            
            if (newtitle == "")
            {
                newtitle = Interaction.InputBox(
                "新章題", "ページ作成", "章" + safedata.Count.ToString(), 200, 100);
                newtitle.Trim();
                if (newtitle== "")
                {
                    return;
                }
                //return;
            };
           

            //Array.Resize(ref LMN, LMN.Length + 1);
            // safedata ts = new safedata(inputText, "");
            // sda.Add(ts );
          
            if (safedata.ContainsKey(newtitle) == false) {
                safedata.Add(newtitle, "");

            
            };
            SetNewLMN(newtitle);
            //
            // opeRenewFormParts();
            RequestRenewListFileNameView();
            RequestRenewTreeView();
            RequestRenewStoryLsitBox();
            //opeRenewFormParts ReNewStoryLsitBox();
            //opeRenewFormParts ReNewTreeView();
        
        }
        


        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

       
        public void XmlWrite2()
        {

           // openFileDialog1.Title = "章リストを保存する";
            Console.WriteLine("\n[XmlWrite2 file=" + GetFileListFileName() + "]\n");
            //safefilelist
            if (System.IO.Path.GetFileName(safefilelist) == "") {
                DebMess("XmlWrite2-NoFilename (safefilelist)");
                return;
            }
            try
            {

                List<safedatasElm> lst = new List<safedatasElm>();
                foreach (KeyValuePair<string, string> pair in safedata)
                {
                    safedatasElm a = new safedatasElm();
                    a.name = pair.Key;
                    a.fname = pair.Value;

                    lst.Add(a);
                    //txtBodyBox.Text += pair.ToString() + "\n";
                }

                Type[] et = new Type[] { typeof(safedatasElm) };
                XmlSerializer serializer =
                     new XmlSerializer(typeof(List<safedatasElm>), et);
//                openFileDialog1.InitialDirectory = Properties.Settings.Default.MainDir;
  //              openFileDialog1.CheckFileExists = false;
    //            openFileDialog1.ShowDialog();
                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("shift_jis");//shift-jisでのエンコード
                StreamWriter sw =
                    new StreamWriter(GetFileListFileName()  , false , encoding);//, new UTF8Encoding(false));
                
                serializer.Serialize(sw, lst);
                sw.Close();
            }
            catch (Exception ex)
            {
  //              txtBodyBox.Text = ex.Message + "\n" + ex.InnerException;
//                bodyMess.Text += "\n[XmlWrite2]\n" + ex.Message;
                
                DebMess ("\n[XmlWrite2]\n" + ex.Message); 
            }
            //txtBodyBox.Text += "" + openFileDialog1.FileName + "!!\n";



        }

        //XmlRead用パーツ
        private void StartSetData(){
            safedata.Clear ();
            LMNControl.Clear ();
            ActiveLMNSearchlist.Clear();
            LMNDelList.Clear();
            BookMark.Clear();
        }
        private void SetSafeData(safedatasElm pair,ref Dictionary<string,string > LNMFilenameList )
        {
            try
            {
                LNMFilenameList.Add(pair.name, pair.fname);
            }
            catch (Exception ex)
            {
                // foreach (KeyValuePair <string ,string>p in  safedata){
                //     DebMess(p.Key +"<>"+p.Value );
                // }
                DebMess(string.Format("[SetSafeData]count{0}/{1}/{2}...\n{3}{4}{5}", safedata.Count.ToString(), pair.name, pair.fname, ex.Message, ex.StackTrace, ex.Source));
            }
            try
            {
                if (safedata == LNMFilenameList)
                {
                    SetNewLMN(pair.name);
                }
            }
            catch (Exception ex)
            {
                DebMess(string.Format("[SetSafeData]count{0}/{1}/{2}...\n{3}{4}{5}", safedata.Count.ToString(), pair.name, pair.fname, ex.Message, ex.StackTrace, ex.Source));
            }
        }
        private void SetSafeData(safedatasElm pair){

            SetSafeData(pair,ref  safedata);
        }

        //XmlRead(string file)
        //fileから、連想配列safedataに名前とファイル名のセットを得る。
        //リスト→連想配列
        //テキスト編集タブは全て解除されている事が前提
        public void XmlRead(string file)        {

            XmlRead(file,ref safedata);
        }
 
        public void XmlRead(string file, ref Dictionary<string, string> LNMFilenameList)
        {
            try
            {
                LNMFilenameList.Clear();
                DebMess("XmlRead(file)-->>"+file +">>");
                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("shift_jis");
                Type[] et = new Type[] { typeof(safedatasElm) };
                List<safedatasElm> lst = new List<safedatasElm>();
                XmlSerializer serializer =
                     new XmlSerializer(typeof(List<safedatasElm>), et);
                StreamReader sr =
                    new StreamReader(file, encoding);

                lst = (List<safedatasElm>)serializer.Deserialize(sr);
                sr.Close();
                if (LNMFilenameList == safedata)
                {
                    StartSetData();
                }
                
                foreach (safedatasElm pair in lst)
                {

                    //txtListBox.Text +="\n"+ safedata.Count .ToString() +":";
                    SetSafeData(pair,ref LNMFilenameList);
                    //txtListBox.Text += pair.name+"\n";

                }
                if (LNMFilenameList == safedata)
                {
                    ListReaded = true;
                }
            }
            catch (Exception ex)
            {
                DebMess(ex.Message + "\n||" + ex.StackTrace + "\n||" + ex.ToString());
               
            }

            DebMess("<<--XmlRead(file)");
        
        }
        public void XmlRead()
        {
            DebMess("XmlRead()-->>");
            if ( !CheckStoryListExisting() || safedata.Count < 1)
            {
                openFileDialog1.Title = "XmlRead";
                openFileDialog1.InitialDirectory = Properties.Settings.Default.MainDir;
                if (DialogResult.OK == openFileDialog1.ShowDialog())
                {

                    SafeFileList = System.IO.Path.GetFileName(openFileDialog1.FileName);
                    XmlRead(GetFileListFileName());
                }

            }
            else {
                XmlRead(GetFileListFileName() );
             
            
            }

            //成功したら、パスのセーブ
            // ReNewStroyFileListView();
            ValidateFileListName();
            DebMess("<<--XmlRead(safefilelist)" + GetFileListFileName() + ">>");
            ListReaded = true;
               
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

           // List<KeyAndValue<TKey, TValue>> lst =
 //           new List<KeyAndValue<TKey, TValue>>();
           // List <safedatasElm > 
            //MessageBox.Show ("xxx"+lst.Count.ToString ()  ); 
            //XmlWriter xmlw = new XmlWriter();
           SaveAll(sender ,e);
            XmlWrite2();
        }

       
        private string  SelectTxtListBoxLine() {
            string str = txtListBox.Text;
            //カレットの位置を取得
            int selectPos = txtListBox.SelectionStart;
            int selectLinehome = 0;
            //カレットの位置までの行を数える
            int row = 1, startPos = 0, endPos;
            for (endPos = 0;
                (endPos = str.IndexOf('\n', startPos)) < selectPos && endPos > -1;
                row++)
            {

                startPos = endPos + 1;
                selectLinehome = startPos;
            }

            txtListBox.SelectionStart = selectLinehome;
            //selectLinehomeにはカレットから一つ前の'\n'の次の位置
            //endPosにはカレットから一つ目の'\n'の位置が入っている。
            int len = endPos - selectLinehome;
            if (len > 0)
            {
                //  if (colorchange != false) RemoveSelectWords(txtBody);
                txtListBox.SelectionLength = len;
                str = txtListBox.SelectedText;
                DebMess("\n[SelectTxtListBoxLine]\n");
                return str;
                //  cmb.Text = str.Trim();
                //Jump2MarkUp(txtBody, cmb);
                //  if (colorchange != false) txtBody.SelectionBackColor = Color.SeaShell;
            }
            DebMess("\n[SelectTxtListBoxLine]\n" +len.ToString ()+","+row.ToString ());
            return "";
        }
        private void txtListBox_MouseClick(object sender, MouseEventArgs e)
        {
//            if( txtListBox.Enabled
            if (txtListBox.ReadOnly == false) {
                return;
            }
            //  opeListClick(caractorBox, txtPersonNameList, cmbJumpName);
            string str = SelectTxtListBoxLine();
            //int i = tabControl1.TabPages.IndexOfKey(str.Trim());の使い方確認
            int i = tabStory.TabPages.IndexOfKey(str.Trim());
            //[str.Trim];
            if (i < 0)
            {

            }
            else
            {
                //txtBodyBox.Text = i.ToString();
                //tabControl1.TabPages[i].();  
                tabStory.SelectedIndex  = i ;
            }
        
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void safeSettingfileDlg_FileOk(object sender, CancelEventArgs e)
        {
            SafeFileList = System.IO.Path.GetFileName(safeSettingfileDlg.FileName);
            XmlRead(GetFileListFileName()  );
            // opeRenewFormParts();
            RequestRenewListFileNameView();
            RequestRenewTreeView();
            RequestRenewStoryLsitBox();
            //opeRenewFormParts ReNewStoryLsitBox();
            //opeRenewFormParts ReNewTreeView();
            //opeRenewFormParts ReNewStroyFileListView();
            ValidateFileListName();
            DebMess ("\nsafeSettingfileDlg_FileOk=" + GetFileListFileName());  
           // txtBody.Text += "\n\nXREAD\n\n"; 

        }
        

        private void txtBodyBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtBody.SelectionLength == 0) {
//             int
                string posstr = Interaction.InputBox( 
                  
             "移動位置", "Line,Culmn", "," + safedata.Count.ToString(), 200, 100);
                string[] sa = posstr.Split(',');

                int l;
                int c;
                try
                {
                    l = int.Parse(sa[0]);
                    c = int.Parse(sa[1]);
                   // string str =
                   // txtBody.Lines[l].ToString();
                   // txtBody.Text += "<" + str + ">";
                }
                catch(Exception ex ) {
                    //txtBody.Text += "<" + ex.Message  + ">";
                    DebMess("[txtBodyBox_MouseClick]\n"+ex.Message  );
                }
                txtBodyBox.Text +=  typeof(ListMakeNote ).ToString() +"\n"+typeof (NotePageBase ).ToString ();
            }  
        }
        private void opeDestroyLMN() {
            //deleteDataElm delelement 
            try
            {
                foreach (KeyValuePair<NotePageBase, string> pair in LMNDelList)
                {
                    DestrotyTimer.Enabled = false;

                    if (pair.Key != null)
                    {
                        //削除する。
                        NotePageBase lmn = pair.Key;
                        string delname = lmn.LMN.Name;
                        //LMNControl[delname]; 
                        TabPage tp = lmn.GetTabPage();
                        ActiveLMNSearchlist.Remove(tp);
                        tabStory.TabPages.Remove(tp);
                        //DebMess("Try delete Timer file=" + delname);
                        if (LMNControl.Keys.Contains(delname) != false)
                        {
                            LMNControl[delname].ReadyClose();
                            LMNControl.Remove(delname);
                            safedata.Remove(delname);
                            // DebMess("delete Timer file=" + delname);
                        }
                        else
                        {
                            //  DebMess("Ng delete Timer file=" + delname);

                        };
                        //LMNControl 

                    }
                }
                LMNDelList.Clear();//一発削除する
                // opeRenewFormParts();
                RequestRenewListFileNameView();
                RequestRenewTreeView();
                RequestRenewStoryLsitBox();
            }
            catch (Exception ex) {
                DebMess("opeDestroyLMN", ex);
            }
            DebMess("opeDestroyLMN end\n");
            //opeRenewFormParts            ReNewStoryLsitBox();
            //ReNewTreeView();

        }
        private void DestrotyTimer_Tick(object sender, EventArgs e)
        {
            opeDestroyLMN();
            opeRenewFormParts();
        }
        public string GetSearchWordBoxString()
        {
            return SearchWordBox.Text.ToString();
        }
        public string SetSafeData_filename(string keyName, string filename)
        {
            return safedata[keyName] = filename;

        }
        public void SetDeleteLMN(NotePageBase  le,bool UseTimer=true   )
        {
            le.GetTabPage ().SuspendLayout();
            le.GetTabPage().Hide();
            //新：タイマーで削除
            //旧：メッセージの発生を抑制するため、タブページは初めに消す。
            //非アクティブにしたので、大丈夫。
//            ListMakeNote 
            if (le != null)
            {
                LMNDelList.Add(le, "delete");
            }
            //タイマーを設定する。
            DestrotyTimer.Enabled = UseTimer;
            //          

        }
        public string GetSelectTextClickLine(RichTextBox txtBody) {

            int ss=0, sl=0;
            return GetSelectTextClickLine(txtBody,ref ss,ref  sl);
        
        }
        public string GetSelectTextClickLine(RichTextBox txtBody, ref int start, ref int slen)
        {

            string str = txtBody.Text;
            //カレットの位置を取得
            int ss = txtBody.SelectionStart;
            int sl = txtBody.SelectionLength;
            int selectPos = ss;
            int selectLinehome = 0;
            //カレットの位置までの行を数える
            int row = 1, startPos = 0, endPos;
            for (endPos = 0;
                (endPos = str.IndexOf('\n', startPos)) < selectPos && endPos > -1;
                row++)
            {

                startPos = endPos + 1;
                selectLinehome = startPos;
            }
            if (endPos < 0 && row > 1)
            {
                    //一度は\nを見つけた。
                endPos = str.Length;

            }
            txtBody.SelectionStart = selectLinehome;
            int len = endPos - selectLinehome;
            if (len > 0)
            {
                start = selectLinehome;
                slen = len;

                txtBody.SelectionLength = len;
                str = txtBody.SelectedText;
                //   cmb.Text = str.Trim();
                //   Jump2MarkUp(txtBody, cmb);
                //   if (colorchange != false) txtBody.SelectionBackColor = Color.SeaShell;
                txtBody.SelectionStart = ss;
                txtBody.SelectionLength = sl;
                return str.Trim();
            }
            else {
                txtBody.SelectionStart = ss;
                txtBody.SelectionLength = sl;
                return "";
            }
        }
        private void EditStroyList_Click(object sender, EventArgs e)
        {
            //
        }

        private void AddNewStroy_Click(object sender, EventArgs e)
        {
            OpenNewTab();
            opeRenewFormParts();
        }
        private void EditStroyListChangeOparation() {
            //CheckBox chkBox = (CheckBox)sender;
            txtListBox.ReadOnly = !EditStroyList.Checked;
            if (EditStroyList.Checked)
            {
                /*LIST編集モードへ*/
                EditStroyList.Text = Properties.Resources.lblStoryListActive;
                //判らないが、１回目に色が戻らないので、
                //一度プロパティを別の色にして、再度白にする。（デフォルト色なので?）
                //                txtListBox.BackColor = SystemColors.Window;
                txtListBox.BackColor = SystemColors.Control;
                txtListBox.BackColor = SystemColors.Window;
                //txtListBox.Invalidate();
                //                txtListBox.
                // txtListBox.Visible=true ;

            }
            else
            {
                /*LIST活用モードへ*/
                if (txtListBox.Modified == true)
                {
                    RenewSafeData();
                    // opeRenewFormParts();
                    // RequestRenewListFileNameView();
                    RequestRenewTreeView();
                    RequestRenewStoryLsitBox();
                    //opeRenewFormParts ReNewStoryLsitBox();
                    XmlWrite2();
                    //ReNewTreeView();
                }
                /*リストにあるファイルリストのみ有効にする*/
                /*ボタン表示有効にする→編集する*/
                EditStroyList.Text = Properties.Resources.lblStoryListEdit;
                txtListBox.BackColor = SystemColors.Control;//System.Windows.Forms.    
            }

        
        }
        private void EditStroyList_CheckedChanged(object sender, EventArgs e)
        {
            EditStroyListChangeOparation();
            opeRenewFormParts();
        }
        public  void DebMess(string mess,int deep =0){
            StackFrame CallStack = new StackFrame(1 + deep, true);
            StackFrame CallStack2 = new StackFrame(2 + deep, true);

            string debcome = String.Format("\n{0}\n File:{1}({2})@File:{3}({4})@{5}", 
                CallStack.GetMethod().ToString () ,
                System.IO.Path.GetFileName(CallStack.GetFileName()),
                CallStack.GetFileLineNumber(),
                System.IO.Path.GetFileName(CallStack2.GetFileName()),
                CallStack2.GetFileLineNumber(),
                System.DateTime.Now.ToString () 
                );
//            int SourceLine =;
            //("Error: " + Message + " - File: " + SourceFile + "Line: " + SourceLine.ToString());
            string outline = mess +"{" +debcome+"}";
            Console.WriteLine(outline);

            Mess.Text += "\n." + outline;
        }
        public void DebMess(string mess, Exception ex, int deep = 0)
        {
            StackFrame CallStack = new StackFrame(1, true);
            StackFrame CallStack2 = new StackFrame(2, true);

            string debcome = String.Format("\n{0}\n File:{1}({2})@File:{3}({4})", CallStack.GetMethod().ToString(), System.IO.Path.GetFileName(CallStack.GetFileName()), CallStack.GetFileLineNumber(), System.IO.Path.GetFileName(CallStack2.GetFileName()), CallStack2.GetFileLineNumber());
            //            int SourceLine =;
            string erromsg= String.Format ("ErrorMessage:{0}\nStackTrace{1}\n****\n{2}",ex.Message,ex.StackTrace,ex.ToString () );
            //("Error: " + Message + " - File: " + SourceFile + "Line: " + SourceLine.ToString());
            string outline = mess + "{" + debcome + "}\n*******\n" + erromsg;
            Console.WriteLine(outline);

            Mess.Text += "\n." + outline;
        }
        //Exception ex
        /// <summary>
        /// 
        /// </summary>
#if false
         private void StartSetData()
         
        private void SetSafeData(safedatasElm pair)
#endif
        private void SetDestroyAll(bool AllPageRemove=false ) {
            try
            {
                //バックアップと全削除
                DebMess("\n.FileterSafeData.\n" + txtListBox.Text + "count=" + txtListBox.Lines.Length);
                //削除リストの作成
                //残すもののバックアップ
                foreach (KeyValuePair<string, NotePageBase> pair in LMNControl)
                {
                    if (pair.Key != "")
                    {
                        //全部を削除するのはおかしいところ
                        //このあとで、タブの名前から、残すリストを作っている。
                        //AllPageRemove では全てのページを消す。
                        //　txtListBox.Lines.Contains(pair.Key)では、次に存在するページも消す？
                        if (AllPageRemove||txtListBox.Lines.Contains(pair.Key) == true)
                        {
                            SetDeleteLMN(pair.Value, false);
                            
                        }
                        //Timerを設定しているので、設定しないようにする必要がある。
                    }
                }
                opeDestroyLMN();
            }
            catch (Exception ex)
            {
                txtBodyBox.Text = ex.Message + "\r" + ex.InnerException;
                DebMess("[FileterSafeData]\n" + GetExMessage(ex));
            }
        
        } 
        private void RenewSafeData()
        {

            /*リストtxtListBoxにあるファイルリストのみ有効にする*/
           
                //古いリスト削除
                //
//                Dictionary <string ,ListMakeNote > 
                //LMNControl.Clear();
            Dictionary<string, string> backUpDic = new Dictionary<string, string>();
                //新しいリストのインスタンス。

            Dictionary<string, string> newDic = new Dictionary<string, string>();
            try{
                //新リスト（バックアップ）の作成
                foreach (string key in txtListBox.Lines)
                
                {
                   
                    if (key != "" )
                    {
                        
                        string fname="";
                        
                          //  if(backUpDic.ContainsKey (key)==true ){
                        if(safedata.ContainsKey(key )){
                               // DebMess(string.Format ( "<name,fname>={0}{1}",key,fname ));
                                fname = safedata[key ];   
                                
                                                              
                        }
                        backUpDic.Add (key,fname);
                    }else{
                       //空行は無視 SetNewLMN(key );
                       // DebMess("+"+lines.Length +"+");
                        //DebMess(string.Format ("重複発見({0})ファイル：{1}はリストに接続できません。" , key,safedata[key] ));    
                    }
                    

                }
                DebMess("[FileterSafeData ]new safedata length\n" +safedata.Count .ToString ());
                //safedata = newDic;//一先ずコピー
            }
            catch (Exception ex)
            {
                txtBodyBox.Text = ex.Message + "\n" + ex.InnerException;
                DebMess("[FileterSafeData]\n" + GetExMessage(ex));
            }
                //StartSetData();//旧safedata,LMNControlの削除
            SetDestroyAll();
//再構築
            
            //再構築
            try
            {
                safedata.Clear();
                ActiveLMNSearchlist.Clear();
                BookMark.Clear ();
                foreach (KeyValuePair<string,string >pair in backUpDic )
                {

                    if (pair.Key  != "")
                    {

                        string fname = "";
                        if (tabStory.TabPages.ContainsKey(pair.Key) == false)
                        {
                            if (backUpDic.ContainsKey(pair.Key) == true)
                            {
                                DebMess(string.Format("<name,fname>={0}{1}", pair.Key, fname));
                                fname = backUpDic[pair.Key];
                            }
                            //safedata.Add(key, fname);//SetNewLMNがsafedataを必要としている
                        }
                        else
                        {
                            //error
                            DebMess("key in list =" + pair.Key);

                        }
                        safedata.Add(pair.Key, fname);
                        SetNewLMN(pair.Key);
                    }
                    else
                    {
                        //空行は無視 SetNewLMN(key );
                        // DebMess("+"+lines.Length +"+");
                        //DebMess(string.Format ("重複発見({0})ファイル：{1}はリストに接続できません。" , key,safedata[key] ));    
                    }


                }
                DebMess("[FileterSafeData ]new safedata length\n" + safedata.Count.ToString());
                //safedata = newDic;//一先ずコピー
            }
            catch (Exception ex)
            {
                txtBodyBox.Text = ex.Message + "\n" + ex.InnerException;
                DebMess("[FileterSafeData]\n" + GetExMessage(ex));
            }


        }
        public string GetExMessage(Exception ex) {
            return ex.Message + "\n+" + ex.StackTrace + "\n+" + ex.TargetSite.ToString() + "\n+" + ex.Source;
        }

        private void txtNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            //なんだっけ？
            System.Diagnostics.Process.Start(e.LinkText);
//            opeRenewFormParts();

        }

        private void Intervaltimer1_Tick(object sender, EventArgs e)
        {
            //txtBodyBox.SelectionAlignment 
            //System.Media.SoundPlayer
            BackColor = Color.Cyan;
            IntervalTimer_reraq.Enabled = true;
            if (File.Exists(Properties.Settings.Default.ReraxationMp3) == true)
            {
              //  Intervaltimer1.Interval = 100000;
              //  mciSendString("play " +  Mp3fileReraOfd.FileName,null,0,IntPtr.Zero  );
                axWindowsMediaPlayer1.URL = Properties.Settings.Default.ReraxationMp3;
                axWindowsMediaPlayer1.settings.autoStart = false;
                
                axWindowsMediaPlayer1.Ctlcontrols.play(); 
                //axWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsPlaying;
            }
            //.Magenta  ;
          
        }

        private void Mp3PlayFileSelect_Click(object sender, EventArgs e)
        {
            if (Mp3fileReraOfd.ShowDialog() == DialogResult.OK)
            {
               // Properties.Resources.n83.   ;
                Properties.Settings.Default.ReraxationMp3 = Mp3fileReraOfd.FileName  ;// Properties.
                Properties.Settings.Default.Save();
                Intervaltimer1.Enabled = true;
            }

        }

        private void IntervalTimer_reraq_Tick(object sender, EventArgs e)
        {
            BackColor = SystemColors.Control;
            IntervalTimer_reraq.Enabled = false;
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
          
        }
        private void btmSave_Click(object sender, EventArgs e)
        {

            /*全てのファイルの保存*/
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        private void MenuBtnSave_Click(object sender, EventArgs e)
        {
            SaveAll(); 
        }
        public void SaveAll(object sender=null , FormClosingEventArgs e=null )
        {
            DebMess("SaveAll");
            try
            {
                foreach (KeyValuePair<string, NotePageBase> lmns in LMNControl)
                {
                    if (lmns.Key != "")
                    {
                        //lmns.Value.LMN.saveFileASK(sender, ref e);
                        lmns.Value.ReadyClose();
                    }
                    else
                    {
                        DebMess("LMNCntrol count="+LMNControl.Count .ToString ());
                    }

                }
            }catch(Exception ex){
                DebMess("saveall NG", ex);
            }
        }

        private void GoList_Click(object sender, EventArgs e)
        {
            //tabStory.SelectedIndex = 0; 
        }

        

        private void BookMarkList_Click(object sender, EventArgs e)
        {

        }

        private void MenubtmFind_Click(object sender, EventArgs e)
        {
            DebMess("検索クリック");
        }

        private void MenubtmFind_DoubleClick(object sender, EventArgs e)
        {
            DebMess("dbl検索クリック");
        }

        private void MenuBtnNextSeach_Click(object sender, EventArgs e)
        {
            NotePageBase  lmn = GetActiveLMN();
            AddBookMark();
            lmn.LMN.SeachWord();
        }
        private void AddBookMark()
        {
            int backupSelectionLength=-1;
            NotePageBase  lmn = GetActiveLMN();
            if (lmn.GetTxt().SelectedText.Length < 1)
            {
                backupSelectionLength = 
                    lmn.GetTxt().SelectionLength;
                lmn.GetTxt().SelectionLength = 7;
            }
            string indexkey = lmn.GetTxt().SelectedText;
            if (backupSelectionLength >= 0) {
                lmn.GetTxt().SelectionLength = backupSelectionLength;
            }
            //indexkey.Length
            //int index = 

            indexkey.Replace(Environment.NewLine, "");//.IndexOf ("\n");

            if (indexkey.Length > 7)
            {
                indexkey.Substring(0,7);
            }
            int pos = lmn.GetTxt().SelectionStart;
            int len = indexkey.Length;
            string bmKey = string.Format("{0}[{1}({2})]", indexkey, lmn.Name,pos);
            if (BookMarkList.Items.Contains (bmKey )==false ){
                BookMarkList.Items.Add(bmKey);
     
                BookMarkElm bml = new BookMarkElm(this,lmn,pos,len,tabStory.SelectedTab  );
                BookMark.Add(bmKey, bml);  
                    
            }else{
            }

            //         lmn.Txt.SelectedText  
            //           BookMark.Add (,); 
        }
        private void retrunBookMark() {
            string ss = BookMarkList.Text;
            if (BookMark.Keys.Contains(ss))
            {
                BookMark[ss].Jump();
            }
        }
        private void BookMarkList_TextChanged(object sender, EventArgs e)
        {
            retrunBookMark();
        }

        private void BookMarkList_DropDownClosed(object sender, EventArgs e)
        {
            retrunBookMark();
        }

        private void btmEnableReraxation_CheckedChanged(object sender, EventArgs e)
        {
            SetReraxation(Decimal.ToInt32(ReraxationIntarval.Value), Decimal.ToInt32(ReraxationTime.Value), btmEnableReraxation.Checked   ); 
        }

        private void ReraxationIntarval_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ReraxationTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void SearchWordBox_TextChanged(object sender, EventArgs e)
        {
            MenubtmFind.ToolTipText = "検索 ワード：" + SearchWordBox.Text;
        }

        private void SearchWordBox_TextUpdate(object sender, EventArgs e)
        {
           DebMess( MenubtmFind.ToolTipText = "検索ワード：" + SearchWordBox.Text);//キー入力で発生
        }

        private void SearchWordBox_Validated(object sender, EventArgs e)
        {
            //この書き方は少しおかしな挙動をするようなので注意
            DebMess(SearchWordBox.ToolTipText = "検索☆ワード：" + SearchWordBox.Text);//テキスト変更少し後
        }

        private void MenuBtnSeachJump_Click(object sender, EventArgs e)
        {

        }

      

        private void MenuBtnChangeDirectry_Click(object sender, EventArgs e)
        {
            SelectMainDir();// XmlWrite2();
        }

        private void MenuBtnNewStroyLists_Click(object sender, EventArgs e)
        {
//            SelectMainDir();
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void MenuBtnListDeload_Click(object sender, EventArgs e)
        {
            ListClose();
            SetDestroyAll(true);
            opeRenewFormParts();
        }
        public void ListClose()
        {
            XmlWrite2();
            SafeFileList = "";
            ListReaded = false;
            txtListBox.Text = "";
            RenewSafeData();
            //ReNewStoryLsitBox();
            //ReNewTreeView();
            // opeRenewFormParts();
            RequestRenewListFileNameView();
            RequestRenewTreeView();
            RequestRenewStoryLsitBox();
        }

        private void MenuBtnOpenOtherStoryList_Click(object sender, EventArgs e)
        {

        }

        private void MenuBtnOpenStoryList_Click(object sender, EventArgs e)
        {
            ClearEditingListBox();
            ListClose();
            SetDestroyAll(true);
            XmlRead();
            
            //ここではファイル名を変更することもあるので実施
            
            RequestRenewListFileNameView();
            RequestRenewTreeView();
            RequestRenewStoryLsitBox();
            opeRenewFormParts();
        }

        private void StColorListBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ColorDialog a=new ColorDialog ();
            DialogResult dr =  a.ShowDialog();
            if (dr == DialogResult.OK) {
                MessageBox.Show(a.Color.ToString());
            } 
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        private void ClearEditingListBox() {

            txtListBox.Modified = false;
            EditStroyList.Checked = false;
            EditStroyListChangeOparation();
            RequestRenewTreeView();        
        }
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            TreeView tv = (TreeView)sender;
            if (tv != null)
            {
               // MessageBox.Show();
                //&& tv.SelectedNode ==
                if (tv.Nodes.Count > 0 && tv.SelectedNode != null)
                {
                    //リスト編集中を警戒
                    //tv.SelectedNode. 
                    string newListFileName = tv.SelectedNode.Text.ToString();
                    TreeNode tn = tv.SelectedNode;
                    if (tn.Tag != null)
                    {
                        TreeTagClickCallBack ttccb = (TreeTagClickCallBack)tn.Tag;
                        ttccb(tn);
                        opeRenewFormParts();
                    }
                }
#if false
                if (tv.Nodes.Count > 0 && tv.SelectedNode != null  )
                {
                    //リスト編集中を警戒
                    //tv.SelectedNode. 
                    string newListFileName = tv.SelectedNode.Text.ToString();
                    TreeNode tn = tv.SelectedNode;
                    string tmp = (tv.SelectedNode.Tag != null) ? tv.SelectedNode.Tag.GetType().ToString() : "Not Tag";
                    DebMess(tv.ToString() + " < tv.SelectedNode .FullPath" + tv.SelectedNode.FullPath + " >" +tmp   +" >"+tn.ToString());
                    //                LoadStroyList(true);
                    //                
 //                   string now = SafeFileList;
                    if (File.Exists (GetFileListTestName(newListFileName)))
                    {
                        ListClose();
                        SetDestroyAll();
                        SafeFileList = (newListFileName);

                        XmlRead(GetFileListFileName());
                        ReNewStoryLsitBox();
                    }
                  //  ReNewTreeView();
                }
#endif
            }
        }

        private void MenuBtnTreeViewReNew_Click(object sender, EventArgs e)
        {
            ReNewTreeView();
        }
        private void txtListBoxAddNewDocument(string newdoc)
        {
            txtListBox.Text = txtListBox.Text.Trim() + "\n" + newdoc + "\n";
            // txtBodyBox.Modified = true;
            RenewSafeData();
            //ReNewStoryLsitBox();
            RequestRenewStoryLsitBox();
            XmlWrite2();
        }
        private void MenuBtnAddlogToday_Click(object sender, EventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            txtListBoxAddNewDocument(dtNow.ToString());
            opeRenewFormParts();
        }

        public string GetNowTimeFileName() {
            string fnameTime;
            DateTime dtNow = DateTime.Now;
            
            fnameTime = String.Format("{0:0000}{1:00}{2:00}" +
                "{3:00}{4:00}{5:00}", dtNow.Year.ToString(), dtNow.Month, dtNow.Day, dtNow.Hour, dtNow.Minute, dtNow.Second, dtNow);
            return fnameTime;
        }

        private void MenuBtnAddSettings_Click(object sender, EventArgs e)
        {
            txtListBoxAddNewDocument(".Settings");
            opeRenewFormParts();
        }

        private void MenuBtnAddPersons_Click(object sender, EventArgs e)
        {
            txtListBoxAddNewDocument(".Persons");
            opeRenewFormParts();
        }

        private void MenuBtnAddKeyWords_Click(object sender, EventArgs e)
        {
            txtListBoxAddNewDocument(".KeyWords");
            opeRenewFormParts();
        }

        private void MenuBtnSerchListMake_Click(object sender, EventArgs e)
        {
            //検索リストを作成する。

        }

        private void MenuBtnMakeAllWordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitSKW();
            GetActiveLMN().LMN.MarkUpKeyWords(skw.SKWS(), skw.GetLength()); 
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            opeRenewFormParts();
        }

        private void contextMenuStrip4StroyListBox_Opening(object sender, CancelEventArgs e)
        {

        }

        private void テキストボックスの比較ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox  TXT1= GetActiveLMN().GetTxt ();

            propertyGrid1.SelectedObject = TXT1;
        }

        private void MenuBtnCountCharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveLMN().Statuslabel1.Text  = GetActiveLMN().GetTxt().SelectionLength.ToString();
        }



   

  




    
        

  
    }

}
