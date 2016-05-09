using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
namespace WindowsFormsApplication4
{
    public partial class NotePageBase : Form
    {
        ListMakeNote lmn;
        Form1 bs; 
        public ListMakeNote LMN
        {
            set { lmn = value; }
            get { return this.lmn; }
        }
        //現在時間を挿入する
        private System.Windows.Forms.ToolStripMenuItem SettingShowDialogMenuItem=new ToolStripMenuItem () ;
        private System.Windows.Forms.ToolStripMenuItem SettingValidateMenuItem = new ToolStripMenuItem();
        public NotePageBase()
        {
            InitializeComponent();
        }
        public NotePageBase(Form1 baseForm, TabControl mzr, string title, string fname)
        {
            InitializeComponent();
            bs = baseForm;
            lmn = new ListMakeNote(baseForm, mzr, title, fname,this );
            RichMemoLoad();
            //テキストの背景色やらは、ＮＧ
            if (title == ".Settings")
            {
                contextMenuStripTxt.Items.Add(SettingShowDialogMenuItem);
                SettingShowDialogMenuItem.Name = "SettingShowDialogMenuItem";
                SettingShowDialogMenuItem.Size = new System.Drawing.Size(220, 22);
                SettingShowDialogMenuItem.Text = "ダイアログ表示";
                //設定更新
                contextMenuStripTxt.Items.Add(SettingValidateMenuItem);
                SettingValidateMenuItem.Name = "SettingValidateMenuItem";
                SettingValidateMenuItem.Size = new System.Drawing.Size(220, 22);
                SettingValidateMenuItem.Text = "設定更新";
            }
        }
        public void PageRemove()
        {
            tabControl1.Controls.Remove(tabPage2 );
        
        }
        public RichTextBox  GetTxt()
        {
            
            return _Txt;

        }
        public RichTextBox GetLst()
        {
            return Lst;

        }
        public TabPage GetTabPage()
        {
            return tabPage2;
        }
        public void ReadyClose() {
            RichMemoSave();
            lmn.ReadyClose ();
        }
        public ToolStripComboBox GetWord()
        {

            return Word;
        
        }
        
        public void FileNameReNew(string newFilename) {
            ViewFilename(newFilename);
            
             
        }
        public void ViewFilename(string fname,string  name = ""){
            FileNameViewAREA.Text = "ファイル名："+ fname;
            if (fname == "")
            {
                bool bNext=false ;
                int count=0;
                //PublicToolBox tb = new PublicToolBox();
                do
                {

                    fname = PublicToolBox.RemoveInhibitChar(name) + bs.GetNowTimeFileName() + String.Format("{0:00}", count++) + ".txt";
                  //  bNext=(lmn.openFileDlg!=null)? false : File.Exists(lmn.GetTestNewFullPath(fname));
                } while (bNext );
                //新規作成
                //                Filename=fname;
            }
            //NewFileNameBox4SaveAs.Text = fname;

            //NewFileNameBox.Text = fname;
        }
        public void ViewStoryName(string title)
        {
            TitleViewAREA .Text = "題名：" + title;
            NewTitleNameBox.Text = title;
        }

       

        private void menuBtnDestroyStroy_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void ExeChangeFileName_Click(object sender, EventArgs e)
        {
           // NewFileNameBox
            //lmn.saveFile();
            //lmn.ReqestChangeNewFile(false , NewFileNameBox.Text);
            
        }

        private void MenuBtnWebGo_Click(object sender, EventArgs e)
        {
            string url;
            if (TsComboWebUri.Text.IndexOf("http") != 0)
            {
                url = "https://www.google.co.jp/?q=" + TsComboWebUri.Text;
            }
            else {
                url =  TsComboWebUri.Text;
            }
            webBrowser1.Navigate(url);
  
        }

        private void MenuBtnSave_Click(object sender, EventArgs e)
        {
            if (File.Exists(lmn.FullFilePath) == false) { 
                //ファイルがあるのに存在していない
                //新規に作ってよい.
                _Txt.Modified = true;
                //保存のため、編集後を装う
            }
            lmn.saveAsk  ();
            RichMemoSave();
            
        }
        private void ChangeName(string newName) {

            string newname = newName ;
            if (newname == "")
            {
                //newname = lmn.Name;
                MessageBox.Show("無名には出来ません。\n題名の変更には新しい題名を指定してください");
                return;
                // MessageBox.Show(string.Format("題名の変更には新しい題名の指定してください"));
                //return;
            }
            if (lmn.CheckUsableNewName(newname) == false)
            {
                MessageBox.Show(string.Format("既にある題名です。（指定：{0}）題名の変更には新しい題名の指定してください", newname));
                return;
            }
            if (DialogResult.Yes
                == MessageBox.Show(
                string.Format("題名の変更をします。\n現在の題名：{0}\n新しい題名：{1}\n（「はい」で変更）", lmn.Name, newname
                ), "題名の変更", MessageBoxButtons.YesNo))
            {
                //処理
                bs.ChangeLMN_Name(lmn.Name, newname);
                //ViewStoryName(lmn.Name);

            };
        
        }
        private void MenuBtnSetNewTitle_Click(object sender, EventArgs e)
        {
            ChangeName(NewTitleNameBox.Text);

        }

        private void BrowFolderDlgShow_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == lmn.ReqestChangeFile(false ,Name)) { 
                
            
            };
        }

        private void Txt_ModifiedChanged(object sender, EventArgs e)
        {
            //ここで不具合
            if (lmn != null)
            {
                DebMess("Txt MOdefied Changed ");
                lmn.DirtyFlag = true;
            }
        }


        private void MenuBtnChangeFile_Click(object sender, EventArgs e)
        {
            //MenuBtnChangNewNameBox
//            TsCmbNameBox4ChangFile.Text )
        }

        private void MenuBtnDestroy_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("この章をリストから削除しますか？\n（ファイルは消えません）", "章の削除", MessageBoxButtons.YesNo))
            {

                lmn.SetDestroyMe();
            }
        }
        public void startwebbrowserFromKeyword(string Keyword)
        {
            if (Keyword == null || Keyword.Length  == 0)
            {
                return;
            }

            //! 指定されたURLのWebページを読み込む.
            try
            {
                webBrowser1.Navigate(new Uri("https://www.google.co.jp/search?q=" + Keyword + "&ie=UTF-8"));
                tabControl2.SelectedTab = tabPage3;
            }
            catch (Exception ex)//(System.UriFormatException)
            {
                DebMess(ex.Message);
                //                    debMessageLog]
                return;
            }
        }
        public void startwebbrowserFromKeyword2(string Keyword)
        {
            if (Keyword == null || Keyword.Length == 0)
            {
                return;
            }

            //! 指定されたURLのWebページを読み込む.
            try
            {
                webBrowser2.Navigate(new Uri("https://www.google.co.jp/search?q=" + Keyword + "&ie=UTF-8"));
               // tabControl2.SelectedTab = tabPage3;
            }
            catch (Exception ex)//(System.UriFormatException)
            {
                DebMess(ex.Message);
                //                    debMessageLog]
                return;
            }
        }
        public void DebMess(string messagestr)
        {
            if (lmn != null)
            {
                bs.DebMess("NPB>>" + messagestr +"@"+ lmn.Name ,1);

            }
            else
            {
                bs.DebMess("NPB>>" + messagestr,1);
            }
        }

        private void NotePageBase_FormClosed(object sender, FormClosedEventArgs e)
        {
        //SHOW して無いので呼ばれない。
        }

        private void NotePageBase_Deactivate(object sender, EventArgs e)
        {
           
        }
        public void RichMemoSave()
        {
            if (Memo.Modified == true)
            {
                string rtffname = lmn.FullFilePath + ".rtf";
               
               // if (File.Exists(rtffname) == true)
                {
                    Memo.SaveFile(rtffname);
                    DebMess("Memo Save");
                }
            }
            else
            {
                DebMess("Memo Save NG<Not Mdefiled>");
            }
        }
        public void RichMemoLoad()
        {
            string rtffname = lmn.FullFilePath + ".rtf";
            if (File.Exists(rtffname) == true)
            {
                Memo.LoadFile(rtffname);
            }
        }

        private void Memo_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void MenuBtnChangeFile_FromFolder_Click(object sender, EventArgs e)
        {
            if (_Txt.Modified == true)
            {
                if (DialogResult.Yes == MessageBox.Show("現在編集中のファイルを保存しますか？", "ファイル変更", MessageBoxButtons.YesNo))
                {
                    
                    lmn.saveAsk();
                }
            }
            else
            {

            }
            if (DialogResult.Yes == MessageBox.Show("この題名の内容のファイルを別のファイルに設定しますか？", "ファイル変更", MessageBoxButtons.YesNo))
            {
                if (DialogResult.OK == lmn.ReqestChangeFileExist(false,lmn.Name + ".txt"))
                {
                    //↑で「既に新たに作成しますか」と言う問いがあるので、ファイルが新規である場合はオープンするほうがおかしい。
                    if (File.Exists(lmn.FullFilePath) != false)
                    {
                        lmn.openFileOk();
                    }

                };

            }
            ViewFilename(lmn.Filename );
        }

        private void NotePageBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void tabControl1_VisibleChanged(object sender, EventArgs e)
        {
            //表示は呼ばれるが、頁が閉じられる際は呼ばれない。
            //MessageBox.Show("close");
        }

        private void MenuBtnMakeSearchList_Click(object sender, EventArgs e)
        {
//            lmn.opeOutLineMarkUp(Txt, Lst, cmbIndex, Color.Yellow, Word.Text);//lmn.
            lmn.opeOutLineMarkUp(_Txt, Lst, cmbIndex, bs.GetSearchWordColor(), Word.Text);//lmn.
        }

        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {
            lmn.SelectWordColoring(_Txt, Word.Text.Trim(), System.Drawing.Color.Blue );
        }

        private void MenuBtnSaveAs_Click(object sender, EventArgs e)
        {
            //lmn.CheckUsableNewName 
            //lmn.RenameFilename(NewFileNameBox4SaveAs.Text);
           // lmn.ReqestChangeFile(true ,NewFileNameBox4SaveAs.Text);
        }

        /* MenuBtnSaveAsNowDocument_Click は現存の文章を
         * 一時別のファイルに保存する。
         * （メインのファイルは変らない）
         * 
         * 
         * 
         * 
        */
        private void MenuBtnSaveAsNowDocument_Click(object sender, EventArgs e)
        {
//            lmn.saveFileASK();
            FileNameControl fnc = new FileNameControl(lmn.Name + bs.GetNowTimeFileName(), bs.GetStroyDir(), ".TXT", "テキストファイル", "一時保存");
            DialogResult dr = fnc.ShowDialog(true );
            if (dr == DialogResult.OK)
            {
                lmn.saveFileAllOK(fnc.FullPath);
            }
        }

        /* MenuBtnNewTextFile_Click は新規のファイルを作り、保存する。
         * 
         * 
         * 
         * 
         * 
        */
        private void MenuBtnNewTextFile_Click(object sender, EventArgs e)
        {
            lmn.saveFile();
            _Txt.Text  = "";
            lmn.ClearFileName();
        }

        private void DestroyDocument_Click(object sender, EventArgs e)
        {
            Memo.Text += _Txt.Text;
            _Txt.Text = "";
            lmn.ClearFileName();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ToolStripMenuItemChange_Click(object sender, EventArgs e)
        {

        }

        public System.Collections.Generic.IEnumerable<string> SKWS() {

            int count = 0;
            
            if(_Txt.Lines.Length > 0){
                string keywords;
                keywords= _Txt.Lines [0].ToString ();
                foreach (string elm in _Txt.Lines) {

                    if (count > 0) { 
                    //一行目以降が対象
                        if (elm.IndexOf(keywords) >= 0)
                        {
                            yield return elm.Substring(keywords .Length );
                        }
                    }else{
                    }
                    count++;
                }
            }else {
                yield break ;
            }
            
        
        }

        private void Lst_Click(object sender, EventArgs e)
        {
            if (Lst.TextLength < 1) return;
            //int line = Lst.GetFirstCharIndexOfCurrentLine();//
            int line = Lst.GetLineFromCharIndex(Lst.SelectionStart);
            if (Lst.Lines.Length <= line) return;
            string searchstr = "";
            if (line >= 0)
            {
                searchstr = Lst.Lines[line];

                Statuslabel1.Text = searchstr;
            }
            int pos;
            if ((pos = _Txt.Text.IndexOf(searchstr.Trim ())) >= 0)
            {
                _Txt.SelectionStart = pos;
                _Txt.Select(pos, searchstr.Length);
                _Txt.ScrollToCaret();

            }
            else {
                Statuslabel1.Text = "//"+searchstr;
            };
        }

        private void Memo_TextChanged(object sender, EventArgs e)
        {
            Memo.Modified = true;
        }

        private void contextMenuStripTxt_Opening(object sender, CancelEventArgs e)
        {
           
        }

        private void contextMenuStripTxt_Click(object sender, EventArgs e)
        {
           // bs.contextMenuStrip1_ItemClicked(sender, e);
        }

        private void contextMenuStripTxt_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            contextMenuStripTxt.Hide();
            bs.contextMenuStrip1_ItemClicked(sender, e);
        }

        private void zoomUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void Txt_MouseMove(object sender, MouseEventArgs e)
        {
            //if (_Txt.SelectionLength > 1)
            //{
                //変換不具合原因になる。
               // toolTip4Txt.SetToolTip(_Txt, "文字数:" + _Txt.SelectionLength.ToString());
            //}
        }

        private void NotePageBase_Load(object sender, EventArgs e)
        {

        }

        private void contextMenuStripTxt_Opening_1(object sender, CancelEventArgs e)
        {

        }

        private void trackBarZoom_Scroll(object sender, EventArgs e)
        {
            float sz;
            sz = (float )trackBarZoom.Value / 30f;
            if(sz < 63 && sz > 0.016)            _Txt.ZoomFactor = sz * sz;
        }
        private void btnX1_Click(object sender, EventArgs e)
        {
            trackBarZoom.Value = 30;
            _Txt.ZoomFactor = 1;

        }

     

        private void 文頭から検索ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int pos;
            pos = _Txt.Text.IndexOf(Word.Text);
            if (pos >= 0) {
                _Txt.SelectionStart = pos;
                _Txt.ScrollToCaret();
                Word.Items.Add(Word.Text);
            }
        }

        private void Lst_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void MenuBtnCountCharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetTxt().SelectionLength > 0)
            {
                Statuslabel1.Text = "選択文字数:"+GetTxt().SelectionLength.ToString();

            }
            else {
                Statuslabel1.Text = "全文字数:"+GetTxt().Text .Length.ToString () ;

            }
        }

        private void MenuBtnGoSearchBox_Click(object sender, EventArgs e)
        {
           // bs.SearchWordBox .Text = _Txt.SelectedText;
           bs.SetSearchWordBox( _Txt.SelectedText);

        }

        private void MenuBrnClip2WebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                //this.UrlList.Text = rb.SelectedText;
                //string keyword = GetSelectTextClickLine(rb);
                string keyword = Clipboard.GetText();
                DebMess(">>" + keyword + "<<");
                startwebbrowserFromKeyword2(keyword);

            }
        }

        private void btnWebPageBack_Click(object sender, EventArgs e)
        {
            if (webBrowser2.CanGoBack == true)
            {
                webBrowser2.GoBack();
            }
        }

        private void MenuBtnRetrun_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack == true)
            {
                webBrowser1.GoBack();
            }
        }

        

  

       



  
    }
}
