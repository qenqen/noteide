using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace WindowsFormsApplication4
{
    class ExChangeCombox2ToolStripComboBox : ComboBox
    {
        ToolStripComboBox tscb;
        public ExChangeCombox2ToolStripComboBox(ToolStripComboBox tcb)
        {
            tscb = tcb;

        }

        public new string Text
        {
            set { tscb.Text = value; }
            get { return this.tscb.Text; }
        }
        public  string  Add{
            set{
                tscb.Text = value;
                tscb.Items.Add(value);
                //tscb.AutoCompleteCustomSource.Add(value ) ;
                //tscb.AutoCompleteCustomSource.Add(value+"tetx");
                //tscb.AutoCompleteCustomSource.Add(

            }
        }
        //string Txt;
        //ComboBox Word;
    }
    public delegate void ValidateFilenameCallBack(string newFileName);
    

    //何故継承と言う形をやめたのか記憶に無い。
    //仮説：デザイナーからはあくまでもNotePageBaseへのコード編集を要求されるため、
    //　　　イベントメソッド編集には親クラスから子クラスのプロパティアクセスとなり、
    //　　　体裁が悪い

    public class ListMakeNote //: NotePageBase
    {
        //Form2 fm;
       
        public TabPage myTabPage;

        //            Form1.safedata 
        //Dictionary<string,string >sd;// = new Form1.safedata();

        //btm3ope btmMakeMarkList;//小題のリスト　*
        //btm3ope btmMakeKeyWord;//キーワーﾄﾞ使用位置
        //btm3ope btmMakeFreeWordList;//FreeWord検索
        
        public System.Windows.Forms.SplitContainer splitContainer1;
        public RichTextBox TXT
        {
           // set { tscb.Text = value; }
            get { return this.PageForm.GetTxt () ; }
        }
//        RichTextBox TXT;
        RichTextBox LST;
        
        
       // ToolStripComboBox TsWord;
        
        ExChangeCombox2ToolStripComboBox Word;
        //public delegate void EventHandler(object sender, EventArgs e);

     //   btm3ope btmLfT;//Lsit From Text
     //   btm3ope btmLfW;//Lsit From Word
      //  btm3ope btmJtT;//Jump To Text
     //   btm3ope btmDestroy;
        //ファイル名の変更、ファイル変更などの項目
      //  btm3ope btmFileChange;
        int MyID;
        bool dirtyFlag = false;

       
        FileNameControl openFileDlg;// = new FileNameControl();

 
        Form1 bs;
        string name;
        
        public bool  DirtyFlag 
        {
            set { this.dirtyFlag = value; }
            get { return this.dirtyFlag; }
        }
        public bool ReadOnly
        {
            set { openFileDlg.ReadOnly = value; }
            get { return openFileDlg.ReadOnly; }
        }
        public bool ReadingFlag
        {
            set { openFileDlg.ReadingFlag = value; }
            get { return openFileDlg.ReadingFlag; }
        }
        public RichTextBox Txt
        {
          //  set { this.TXT = value; }
            get { return this.TXT; }
        }
        public RichTextBox Lst
        {
            set { this.LST = value; }
            get { return this.LST; }
        }
        public string Name
        {
           // set { this.name = value; }
            get { return name; }
        }
        //===ファイル名管理プロパティ
        //OpenFileDlgも含めた一括にしたほうが良いのかも
        public  string Filename
        {
           // set { this.edhitfilename = value; }
            get { return openFileDlg.Filename ; }
        }
        public string FullFilePath
        {
            get { return openFileDlg.FullPath ; }
        }
        
        int TabCount = 1;
        private int autoTabIndex()
        {
            TabCount++;
            return TabCount;
        }

        NotePageBase PageForm;
        ToolStripStatusLabel toolStripStatusLabel0;
        public void ClearFileName(){
            openFileDlg.ClearFileName();
            DirtyFlag = false;
        }
        private void FileNameReNew(string newFileName){
            PageForm.FileNameReNew(newFileName);
            bs.ChangeLMN_FileName(Name, newFileName);
        }
        public string GetTestNewFullPath(string fname){
            return openFileDlg.GetTestNewFullPath(fname);      
        }
        public  bool ReName(string newname) {
            name = newname;
            if (PageForm != null) {
                PageForm.ViewStoryName(name );
            } 
            return true;
        }
        public bool CheckUsableNewName(string newname) {
            return bs.CheckUsableNewName(newname);
        
        }
        public ListMakeNote(Form1 baseClass, TabControl mzr, string title, string fname, NotePageBase npb)
        ///         public ListMakeNote(Form1 baseClass, TabControl mzr, Dictionary<string,string > sf)
        {
            try
            {
                name = title;
                PageForm = npb;
                bs = baseClass;
            }
            catch (Exception ex)
            {
                bs.DebMess(ex.Message + "<>" + ex.Source.ToString() + ex.TargetSite.ToString());
            }

            try
            {
                openFileDlg = new FileNameControl(fname, bs.GetStroyDir (), "TXT", "テキストファイル", GetOpenDlgTitle());
                openFileDlg.validatefilenamefCallBackMethod =new ValidateFilenameCallBack(FileNameReNew );  
            }
            catch (Exception ex)
            {
                bs.DebMess(ex.Message + "<>" + ex.Source.ToString() + ex.TargetSite.ToString  ());
            }
            try
            {
           // fm = new NotePageBase();
                bs.DebMess("LMN" + name);
              //  TXT = PageForm.GetTxt();
                TXT.Name = "Txt";
                //Txt.ContextMenuStrip ;//bs.GetContextMenuStrip();
                LST = PageForm.GetLst();
                Word = new ExChangeCombox2ToolStripComboBox(PageForm.GetWord());
                //Word = GetWord();
            }
            catch (Exception ex)
            {
                bs.DebMess(ex.Message + "<>" + ex.Source.ToString());
            }
            try

            {
                /*タブ頁の作成*/
                npb.PageRemove();
                myTabPage = npb.GetTabPage();
                //  myTabPage = new TabPage(title);
                myTabPage.Name = title;
                myTabPage.Text  = title;
                PageForm.ViewFilename(Filename,name);
                PageForm.ViewStoryName(title);
                //  mzr.TabPages.Add();//contextMenuStrip1_ItemClicked
                mzr.TabPages.Add(myTabPage);
                mzr.SelectedIndex = mzr.TabPages.Count - 1;
                MyID = mzr.SelectedIndex;
                toolStripStatusLabel0 = bs.GetToolStriplabel();
            }
            catch (Exception ex){
                bs.DebMess(ex.Message +"<>"+ ex.Source.ToString ()  ); 
            }
           // myTabPage.Controls.Add(fm.GetTabPage());
            
           
           
#if false 
            try
            {
                openFileDlg = new OpenFileDialog();
                openFileDlg.Filter = Properties.Resources.FiletFILETYPE_TXT;//"テキストファイル(*.txt)|*.txt";
                openFileDlg.InitialDirectory = Properties.Settings.Default.MainDir;
                openFileDlg.DefaultExt = "";// "txt";/* ？親切な用で親切で無い仕組みは作らない */
                openFileDlg.Title = Properties.Resources.openFileDlgTitle;//*章を開く*//
            }
            catch (Exception ex)
            {
                bs.DebMess(ex.Message + "<>" + ex.Source.ToString());
            }
#endif
            //sd = sf;
            /*
             コンテナアイテムの作成。
                 
           */
            try
            {
                fileOpenOrNew();
            }
            catch (Exception ex)
            {
                DebMess(ex.Message + "<" + Filename+"," + Name + ">" , ex);
            }
        }

        private void fileOpenOrNew()
        {
            if (Filename  == "")
            {
                //仮に入れる
               
                Txt.Text = "☆\n" + Name;
            }
            else
            {
                openFile();
            }

        }
        private string GetOpenDlgTitle()
        {
            return string.Format("{1}[{0}]", Name, Properties.Resources.openFileDlgTitle);
        }
        public void SetReadOnly()
        {
            ReadingFlag  = true;
            Txt.ReadOnly = true;
        }
        private void openFile(bool showDlg = false)
        {
            //              openFileDlg. +=
            
            if (showDlg)
            {
               // openFileDlg.Title = 
                if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    openFileOk();
                }
            }
            else
            {

                openFileOk();

            }
            // openFileDlg.= filename;

        }
        /*****
         * ファイルの変更
         * ファイル名を変更し、文章を継続する
         * 現在の文章は別名保存し、
         * 
         * 
         * 
         * *******************************/
        public void RenameFilename(string newFilename)
        {
            //newFilename
            openFileDlg.ShowDialog(true, "", true, newFilename);
        }
        public DialogResult  ReqestChangeNewFile(bool bInputed=false ,string newfilename = "") {
            DialogResult dr= openFileDlg.ShowDialog(true , "", bInputed, newfilename);
            if (DialogResult.OK == dr) {
               
                if(File.Exists (GetSavePath ())){
                    fileOpenOrNew ();
                }
            }
            return dr;
        }
        public DialogResult  ReqestChangeFile(bool bInputed=false ,string newfilename = "") {
            return openFileDlg.ShowDialog(true , "", bInputed, newfilename);
        }
        public DialogResult ReqestChangeFileExist(bool bInputed = false, string newfilename = "")
        {
            //既存のファイルを指定する
            return openFileDlg.ShowDialog(false, "", bInputed, newfilename);
        }
        public void openFileOk()
        {
            const string TITLE_EXTN_ReadOnly = " (読み取り専用)";
            const string MSGBOX_TITLE = "ファイル オープン";
            // this.FileNameView.Text = TITLE_EXTN_ReadOnly + filename;
            // SetReadOnly();
            //選択されたファイルのパスを取得;;
            try
            {
                DebMess("openFileOk[" + FullFilePath + "@" + bs.GetFileListFileName() + "]");
            }
            catch (Exception ex) {
                DebMess("openFileOk-NG[" + FullFilePath + "@" + bs.GetFileListFileName() + "]",ex);
            
            }
                //Filename = openFileDlg.SafeFileName;
            //FullFilePath = openFileDlg.FileName;
            if (CheckFilePath(FullFilePath) == false)
            {
                bs.DebMess("ディレクトリ配下に無い.." + FullFilePath);
                return;
            }
            bs.DebMess("[openFileOk..middle");
            try
            {
                bs.SetSafeData_filename(Name,Filename);
            }
            catch (Exception ex)
            {
                //ファイルの読み込みでエラーが発生した場合に Exception の内容を表示

                bs.DebMess("[openFileOk:NG"+ex.Message+"]" ,ex );
            }
            bs.DebMess("[openFileOk..middle1.5");
            try
            {
                //ファイルダイアログで読み取り専用が選択されたかどうかの値を取得
                
                //filename = openFileDlg.SafeFileName;
                //読み取り専用で開いた場合にタイトル(ファイル名)に "読み取り専用" の文字をつける
                PageForm.ViewFilename((ReadOnly) ? Filename + TITLE_EXTN_ReadOnly : Filename);
            }
            catch (Exception ex)
            {
                //ファイルの読み込みでエラーが発生した場合に Exception の内容を表示
             //   MessageBox.Show(null, ex.Message, MSGBOX_TITLE);
                //File.SetCreationTime(editFilePath)
                // Mess.Text += "\n[startwebbrowser]\n" + ex.Message;
                DebMess("[openFileOk:NG" ,ex );
            }
            DebMess("[openFileOk..middle2");
            //ダーティーフラグのリセット
            DirtyFlag=false;
            int lines;
            try
            {
                //テキストファイルの内容をテキストボックスにロード
                // textBox1.Text = File.ReadAllText(editFilePath, Encoding.Default);
                if(File.Exists (FullFilePath)==false ){
                DebMess("ReadLines--><--");
                
                }
                lines = 0;
                Txt.Text = "";
                DebMess("ReadLines-->>");
                foreach (string line in File.ReadLines(FullFilePath, Encoding.Default))
                {
                    lines++;
                    Txt.Text += line + "\r\n";
                }
                DebMess("<<--ReadLines" + lines.ToString () );
                //ShowReadLines(lines);
                ReadingFlag = true;
                DirtyFlag = false;

                

            }
            catch (System.ArgumentNullException  ex)
            {
                //FileNotFoundExceptionをキャッチした時
                DebMess("openFileOk NG。",ex);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                //FileNotFoundExceptionをキャッチした時
                DebMess("ファイルが見つかりませんでした。", ex);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                //UnauthorizedAccessExceptionをキャッチした時
                DebMess("必要なアクセス許可がありません。", ex);
            }
            catch (Exception ex)
            {
                //ファイルの読み込みでエラーが発生した場合に Exception の内容を表示
                DebMess("openFileOk NG", ex);
                //File.SetCreationTime(editFilePath)
                // Mess.Text += "\n[startwebbrowser]\n" + ex.Message;
            }

            ReadingFlag = true;
            bs.DebMess("openFileOk success");
            if (Txt.Text != "")
            {
                Word.Text = Txt.Lines[0];
                Word.Add=Word.Text;
            }
        }
        public int Setcnt(int c)
        {
            MyID = c;
            return MyID;
        }
        
        public bool confirmDestructionText(string msgboxTitle)
        {
            const string MSG_BOX_STRING = "ファイルは保存されていません。\n\n編集中のテキストは破棄されます。\n\nよろしいですか?";
            if (!DirtyFlag) return true;
            return (MessageBox.Show(MSG_BOX_STRING, msgboxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
        }
        public void RemoveSelectWords(RichTextBox target)
        {
            int s, l;
            if (target != null)
            {
                s = target.SelectionStart;
                l = target.SelectionLength;
                target.SelectionStart = 0;
                target.SelectionLength = target.TextLength;

                target.SelectionBackColor = System.Drawing.SystemColors.Window;// Control.DefaultForeColor  ;//;DefaultBackColor;
                target.SelectionStart = s;
                target.SelectionLength = l;
            }
        }
        public void SelectWordColoring(RichTextBox target, String keyword, Color color)
        {
            // found 変数では検索を行う文字位置の一つ手前を示します。最初は - 1 となります。
            int found = -1;
            RemoveSelectWords(target);
            // キーワードが見つからなくなるまで繰り返します。
            do
            {
                // 対象の RichTextBox から、キーワードが見つかる位置を探します。検索開始位置は、前回見つかった場所の次からとします。
                found = target.Find(keyword, found + 1, RichTextBoxFinds.MatchCase);

                // キーワードが見つかった場合は、その色を変更します。
                if (found >= 0)
                {
                    target.SelectionStart = found;
                    target.SelectionLength = keyword.Length;
                    //
                    //target.SelectionBackColor = color;
                    // target.SelectionColor = color;
                    found += keyword.Length;
                }
                else
                {
                    // キーワードが見つからなかった場合は、繰り返し処理を終了します。
                    break;
                }
                // キーワードが見つかった場合は、その色を変更します。

            }
            while (true);
        }
        public static void Jump2MarkUp(RichTextBox target, ComboBox cmb)
        {
            int found = -1;

            String keyword = (cmb.Text.ToString());
            keyword.Trim();
            do
            {
                // 対象の RichTextBox から、キーワードが見つかる位置を探します。検索開始位置は、前回見つかった場所の次からとします。
                found = target.Find(keyword, found + 1, RichTextBoxFinds.MatchCase);
                if (found >= 0)
                {
                    target.ScrollToCaret();
                }
                else
                {
                    // キーワードが見つからなかった場合は、繰り返し処理を終了します。
                    //140318ライフマイル、ｇｍｏとくとく登録解除
                    break;
                }
            }
            while (true);
        }
        public void OutLineMarkUp(RichTextBox target, RichTextBox txtList, ComboBox cmb)
        {
            OutLineMarkUp(target, txtList, cmb, Color.Black);
        }
        public void OutLineMarkUp(RichTextBox target, RichTextBox txtList, ComboBox cmb, Color color)
        {
            String keyword = (target.Text[0].ToString());
            opeOutLineMarkUp(target, txtList, cmb, Color.Black, keyword);

        }

        //キーワードを含む行をリストへ送る
        public void MarkUpKeyWords(System.Collections.Generic.IEnumerable<string> keywords,int keywrodsCount)
        {
            opeMarkUpKeyWords(Lst, keywords, keywrodsCount);
        }
        public void opeMarkUpKeyWords(RichTextBox txtList, System.Collections.Generic.IEnumerable<string> keywords,int keywrodsCount)
        {
            // found 変数では検索を行う文字位置の一つ手前を示します。最初は - 1 となります。
            int found = -1;
            int ret = 0;
            RichTextBox tgt = Txt;
            int charcount=0;
            txtList.Text = "";
            int processlength = tgt.Lines.Length  * keywrodsCount;
            foreach (string target in tgt.Lines)
            {
                
                
                int count = 0;
                //toolStripStatusLabel0.Text = keyword.ToString() + "の検索";

                
                foreach (string keyword in keywords )
                
                {
                   // DebMess(keyword + (processlength--).ToString () );
                    processlength--;
                    PageForm.Statuslabel1.Text = processlength.ToString();
                    found = target.IndexOf (keyword);
                    if (found >= 0)
                    {
                        string str = target;//.SelectedText;
                        //                        txtList.Text += str + "\n";
                        txtList.AppendText(str + "\n");
                        tgt.SelectionStart =charcount+found ;
                        tgt.SelectionLength = target.Length;
                        tgt.SelectionBackColor = Color.Bisque;
                        found = ret;
                        //
                        count++;
                    }
                    
                }
                charcount += target.Length+1;
                
            }
            //toolStripStatusLabel0.Text = toolStripStatusLabel0.Text + "結果：" + count.ToString() + "個の項目";
        }
        public void opeOutLineMarkUp(RichTextBox target, RichTextBox txtList, ComboBox cmb, Color color, String keyword)
        {
            // found 変数では検索を行う文字位置の一つ手前を示します。最初は - 1 となります。
            int found = -1;
            int ret = 0;
            int tl = target.TextLength;
            keyword.Trim();
            if (keyword.Length <= 0)
            {
                //  toolStripStatusLabel0.Text = keyword.ToString() + "マーカー文字指定なし";
                return;
            }
            int count = 0;
            //toolStripStatusLabel0.Text = keyword.ToString() + "の検索";
            cmb.Items.Clear();
            txtList.Text = "";

            //RemoveSelectWords(target);
            // キーワードが見つからなくなるまで繰り返します。
            do
            {
                // 対象の RichTextBox から、キーワードが見つかる位置を探します。検索開始位置は、前回見つかった場所の次からとします。
                found = target.Find(keyword, found + 1, RichTextBoxFinds.MatchCase);
                if (found >= 0)
                {
                    ret = target.Find("\r", found + 1, RichTextBoxFinds.MatchCase);
                    if (ret >= 0)
                    {
                        target.SelectionStart = found;
                        // トップマークが見つかったら、エンドの￥Rを探す。
                        target.SelectionLength = ret - found; //keyword.Length;
                        string str = target.SelectedText;
                        //                        txtList.Text += str + "\n";
                        txtList.AppendText(str + "\n");
                        cmb.Items.Add(str.ToString());//.SelectionBackColor = color;
                        found = ret;
                        target.SelectionColor = color;
                        count++;
                    }
                    else {
                        break;
                    }
                    //found += keyword.Length;
                }
                else
                {
                    // キーワードが見つからなかった場合は、繰り返し処理を終了します。
                    break;
                }
            }
            while (true);
            //toolStripStatusLabel0.Text = toolStripStatusLabel0.Text + "結果：" + count.ToString() + "個の項目";
        }
        public void lineOverRunSelectWordColoring(RichTextBox target, int nomallengs)
        {
            int count = 0;
            //toolStripStatusLabel0.Text = nomallengs + "文字以上の行の検索";

            // found 変数では検索を行う文字位置の一つ手前を示します。最初は - 1 となります。
            int found = -1;
            RemoveSelectWords(target);
            // キーワードが見つからなくなるまで繰り返します。
            int spos = 0;
            //        int spos2 = 0;
            int targetLength = target.Text.Length;
            //            int nomallengs = 40;// Decimal.ToInt32(textLengsMarker.value);  
            string keyword = "\r";
            int keywordlength = keyword.Length;
            //            int errorcnt=0;
            do
            {
                //              spos2 = spos;
                // 対象の RichTextBox から、キーワードが見つかる位置を探します。検索開始位置は、前回見つかった場所の次からとします。
                if (spos + 1 >= targetLength)
                {
                    //終端位置から検索を始めると、初めから検索しなおすバグ（仕様？）が発生する。
                    break;
                }
                found = target.Find(keyword, spos + 1, RichTextBoxFinds.MatchCase);
                //MessageBox.Show(keyword.Length.ToString()); 
                // nomallengs　以上の行を検索します。。
                if (found > 0)
                {
                    int newLineLength = found - spos;
                    int markStringLength = (newLineLength - nomallengs);
                    if (markStringLength > 0)//if((newLineLength) > nomallengs)
                    {
                        target.SelectionStart = spos + nomallengs;//;found - (newLineLength - nomallengs);
                        target.SelectionLength = markStringLength;
                        target.SelectionBackColor = ((System.Drawing.Color)((StgElmentBase<Color>) bs.stglst.dic["行文字数オーバー文字マーカー色"]).Get ()) ;
                        // target.SelectionColor = color;
                        count++;

#if false
                        if (spos2 != spos) {
                            errorcnt++;
                        }
                        if (spos > found)
                        {
                            errorcnt++;
                            //MessageBox.Show ("error");
                        }
#endif
                    }
                    spos = found + keywordlength;
                }
                else
                {
                    // キーワードが見つからなかった場合は、繰り返し処理を終了します。
                    break;
                }
#if false
                if( spos2 > spos){
                   errorcnt++;
                }
#endif
            }
            while (true);
            toolStripStatusLabel0.Text = toolStripStatusLabel0.Text + "結果：" + count.ToString() + "個の項目";
        }

        public void InLineHANKAKUstringMarker(RichTextBox target)
        {
            int count = 0;
            toolStripStatusLabel0.Text = "半角文字の検索";

            int found = 0;
            //          他のマーカーと平行に行う
            //            RemoveSelectWords(target);
            // キーワードが見つからなくなるまで繰り返します。
            // int spos = 0;
            //char[] ca = target.Text.ToCharArray;
            Encoding sjisEncoding = Encoding.GetEncoding("Shift_JIS");
            //return ( == strMoji.Length * 2 );
            //            int nomallengs = 40;// Decimal.ToInt32(textLengsMarker.value);  
            do
            {
                if (found + 1 > target.Text.Length) break;
                string s = target.Text.Substring(found, 1);

                //                found = target.Find(keyword, spos + 1, RichTextBoxFinds.MatchCase);
                //MessageBox.Show(keyword.Length.ToString()); 
                // キーワードが見つかった場合は、その色を変更します。
                int sz = sjisEncoding.GetByteCount(s);

                if (sz <= 1)
                {

                    target.SelectionStart = found;
                    target.SelectionLength = 1;
                    target.SelectionBackColor = ((StgElmentBase<Color>)bs.stglst.dic["半角文字マーカー色"]).Get(); ;//System.Drawing.Color.Cyan;

                    // keyword.Length; 
                }
                found++;
            }
            while (true);

           toolStripStatusLabel0.Text = toolStripStatusLabel0.Text + "結果：" + count.ToString() + "個の項目";
        }
        
        public void SeachWord()
        {


            string s;
            s = bs.GetSearchWordBoxString();
            //            if(s.Last() =='\n'){
            s.Trim();
            //          }
//            SelectWordColoring(Txt, s, System.Drawing.Color.Yellow);
          //  SelectWordColoring(Txt, s, bs.GetSearchWordColor());
            SelectWordColoring(Txt, s, ((StgElmentBase<Color> )bs.stglst.dic ["検索キーワード色"]).Get () );
            
            //lineOverRunSelectWordColoring

        }


        private void SeachWordBox_Enter(object sender, EventArgs e)
        {

            ///statusStrip1.Text = "seach";
            SeachWord();
        }

        private void Seachbtm_Click(object sender, EventArgs e)
        {

            SeachWord();
           

        }



        private void ShowSaveDateTime() { ;}


        private bool CheckFilePath(string path)
        {


            return (path.IndexOf(bs.GetStroyDir()) >= 0);
        }
        private string GetFileNameString(string filePath, char separateChar)
        {
            try
            {
                string[] strArray = filePath.Split(separateChar);
                return strArray[strArray.Length - 1];
            }
            catch
            { return ""; }
        }



        private void tabPage2_Click(object sender, EventArgs e)
        {

        }



       

        private void MainStory_ModifiedChanged_total(object sender, EventArgs e)
        {
            RichTextBox tb = (RichTextBox)sender;
            if (tb.Modified == false)
            {

            }
            else
            {
                DirtyFlag = true;
            }

        }


        private void txtMarklist_MouseClick(object sender, MouseEventArgs e)
        {
            opeListClick(Txt, Lst, Word);

        }
        private void btmMakeNameList_Click(object sender, EventArgs e)
        {
            OutLineMarkUp(Txt, Lst, Word, Color.YellowGreen);
        }


        private void btmNameGo_Click(object sender, EventArgs e)
        {
            Jump2MarkUp(Txt, Word);
        }

        private void txtPersonNameList_MouseClick(object sender, MouseEventArgs e)
        {
            opeListClick(Txt, Lst, Word);
        }

        private void btmMakeMemoList_Click(object sender, EventArgs e)
        {
            OutLineMarkUp(Txt, Lst, Word, Color.YellowGreen);
        }

        private void btmMemoGO_Click(object sender, EventArgs e)
        {
            Jump2MarkUp(Txt, Word);
        }
        private void txtMarklist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            opeListClick(Txt, Lst, Word, true);

        }
        private void txtMemo_MouseClick(object sender, MouseEventArgs e)
        {
            opeListClick(Txt, Lst, Word);

        }


        private void txtMemoList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            opeListClick(Txt, Lst, Word, true);
        }

        private void opeListClick(RichTextBox txtBody, RichTextBox txtList, ComboBox cmb, bool colorchange = false)
        {

            string str = txtList.Text;
            //カレットの位置を取得
            int selectPos = txtList.SelectionStart;
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

            txtList.SelectionStart = selectLinehome;
            int len = endPos - selectLinehome;
            if (len > 0)
            {
                if (colorchange != false) RemoveSelectWords(txtBody);
                txtList.SelectionLength = len;
                str = txtList.SelectedText;
                cmb.Text = str.Trim();
                Jump2MarkUp(txtBody, cmb);
                if (colorchange != false) txtBody.SelectionBackColor = Color.SeaShell;
            }
        }
        
        private void makeList(object sender, EventArgs e)
        {
            MessageBox.Show("button push..." + MyID.ToString());
            //       JumpListToTxtBox 
        }
        private void JumpListToTxtBox(object sender, EventArgs e)
        {
            MessageBox.Show("JumpListToTxtBox button push..." + MyID.ToString());
            //        
        }
        private void ListFromSelectWord(object sender, EventArgs e)
        {
            MessageBox.Show("ListFromSelectWord" + MyID.ToString());
            //        
        }
        public void SetDestroyMe() {
            bs.SetDeleteLMN(PageForm);
        }
        private void tabPageDestroy(object sender, EventArgs e)
        {
            // OpenFileDialog newFile=new OpenFileDialog ();
            //  newFile.Filter 



            SetDestroyMe();

            // bs.ClearLMN( this    );
            // MessageBox.Show("destroy push..." + MyID.ToString());
        }
        private void FileViewLablel_Clicked(object sender, EventArgs e)
        {
            MessageBox.Show("button push..." + MyID.ToString());
            //       JumpListToTxtBox 
        }
        //        FileChange
        private void FileChange(object sender, EventArgs e)
        {
            openFileDlg.ShowDialog();
            //if(openFileDlg.
            MessageBox.Show("工事中　ファイルを変更する" + MyID.ToString());
            //現状ファイルの保存＋新ファイルへの以降。か
            //現文章を別名で保存するか、現ファイルの削除ファイル名を変更するか、
            //現文章はとりあえずタイトル+保存し時間＋.txt
        }
        public void saveFile() {
            saveFile(FullFilePath);
        
        }
        public void saveFile(string filefullpath)
        {
            DebMess("in saveFile");
            // bs.mainstoryDirname + "\\" + fname + ".txt";
            try
            {
                //テキストボックスの内容をファイルに書き込み
                if ((ReadingFlag == false && !File.Exists(filefullpath)) ||
                    (ReadingFlag == true && DirtyFlag != false))
                {
                    DebMess("call WriteAllText");
                    //                    File.WriteAllText(FullFilePath, Txt.Text, Encoding.Default);
                    saveFileAllOK(filefullpath);
                    //hozon
                    
                }
                else
                {
                    DebMess(string.Format("savefile cancel..IsExits:{0} DirtyFlag:{1} ReadingFlag:{2}"

                        , openFileDlg.IsExits.ToString()
                        , DirtyFlag.ToString()
                        , ReadingFlag.ToString()));

                }
            }
            catch (Exception ex)
            {
                //ファイルの書き込みでエラーが発生した場合に Exception の内容を表示
                //        MessageBox.Show(bs, ex.Message, MSGBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                DebMess(ex.Message + "+" + ex.StackTrace + "+" + ex.TargetSite.ToString() + "\n" + ex.Source);


            }

        }
        public void saveFileAllOK(string filefullpath)
        {
            try
            {
                File.WriteAllLines(filefullpath, Txt.Lines, Encoding.Default);
                //ダーティーフラグをリセット
                DirtyFlag=false;
                ReadingFlag = true;
                Txt.Modified = false;
                DebMess("call saveFileAllOK ");

               
            }
            catch (Exception ex)
            {
                //ファイルの書き込みでエラーが発生した場合に Exception の内容を表示
                //        MessageBox.Show(bs, ex.Message, MSGBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                bs.DebMess(ex.Message + ":saveFileAllOK:" + filefullpath, ex);


            }
        }
        
        public string GetSavePath()
        {

            return Properties.Settings.Default.MainDir + "\\" + Filename;// +".txt";
        }
        //     public void saveFileASK(object sender, FormClosingEventArgs e)
        public void saveFileASK(object sender,ref FormClosingEventArgs e)
        {

            if (dirtyFlag)
            {
                //string pname = GetSavePath();
                DialogResult res = MessageBox.Show("文章:" + Name +" パス："+Filename  + "は保存されていません。保存しますか？（「はい」で保存）", "終了コマンド", MessageBoxButtons.YesNoCancel);

                if (DialogResult.Yes == res)
                {

                    saveAsk(ref e  );
                    //saveFile();
                }
                else if (res == DialogResult.Cancel)
                {
                    //   FormClosingEventArgs fe = (FormClosingEventArgs)e;
                    bs.DebMess("cansel");
                    e.Cancel = true;

                }
                else {
                
                }
            }
        }
        public void saveAsk( )
        {

            bool bSave = true;
            string newFilename = "";
            if (Filename == "")
            {
                //  ファイル名未指定
                newFilename = Name;
            }
            DebMess("[saveAsk]");
            if (ReadingFlag == true && openFileDlg.IsExits == true)
            {
                //読み込み済みで、ファイルが存在している場合。
            }
            else if (openFileDlg.ShowDialog(true, "", false, newFilename) == DialogResult.OK)
            {
                //bSave = false;
                DebMess("Dlg.OK");
            }
            else
            {
                bSave = false;
             
            };
            if (bSave)
            {
                DebMess("[Call saveFile]");

                saveFile();
            }
        }
        public void saveAsk(ref FormClosingEventArgs e )
        {
            bool bSave = true;
            DebMess("[saveAsk]");
            DialogResult dr;
            string newFilename = "";
            if (Filename == "")
            {
                //  ファイル名未指定
                newFilename = Name;
            }
            if (ReadingFlag == true && openFileDlg.IsExits == true)
            {
                //読み込み済みで、ファイルが存在している場合。
            }
            else if ((dr = openFileDlg.ShowDialog(true, "", false, newFilename)) == DialogResult.OK)
            {
                //bSave = false;
                DebMess("Dlg.OK");
            }
            else
            {
                bSave = false;
                e.Cancel = true;
                DebMess("Dlg.ret = "+ dr.ToString() );
                
            };
            if (bSave)
            {
                DebMess("[Call saveFile]");

                saveFile();
            }
        }
        public void ReadyClose()
        {
            if (dirtyFlag != false)
            {
                saveAsk();
            }

        }
        public void DebMess(string messagestr)
        {
            bs.DebMess("LMN("+Name+")>>"+messagestr,1); 
        
        }
        public void DebMess(string messagestr,Exception ex)
        {
            bs.DebMess("LMN(" + Name + ")>>" + messagestr, ex, 1);

        }



        



        //string title = "TabPagexxx " + (tabcnt1.TabCount + 1).ToString();


        //myTabPage = tabControl1.SelectedTab;
        //myTabPage.TabIndex =\
        //小タイトルボタン
        //            tabPage1.Controls.Add(new Button());
        //   this.Controls.Add(this.btmTxt);
        //this.ResumeLayout(false);



    }
    public class btm3ope : Button
    {
        //Button btn;
        public const int btnNomalWidth = 60;
        public btm3ope(TabControl tbc, string caption, int left, int top)
        {
            // btn = new Button();
            //  tbc.SelectedTab.Controls.Add(this );
            Left = left;
            Top = top;
            Height = 20;
            Width = btnNomalWidth;
            Text = caption.ToString();
            Name = caption;/*親クラスのプロパティ*/
        }

        public void SetEv(EventHandler ev)
        {
            Click += ev;

        }
        public int height
        {
            get { return Height; }

        }

    }
}
