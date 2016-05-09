using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public class FileNameControl
    {
        //このクラスは、生成時にファイル名を渡された後、他からファイル名を渡されることを許さない。

        private OpenFileDialog ofd = new OpenFileDialog();
        private SaveFileDialog sfd = new SaveFileDialog();
        string MainDir;
        string fname;
        string EXT;
        bool reading;//他のクラスで設定する
        public ValidateFilenameCallBack validatefilenamefCallBackMethod;

        //インターフェース
        public string Filename
        {
           // set { fname = value; }
            get { return this.fname; }
        }
        public string FullPath
        {
            get { return MainDir + "\\" + fname; }
        }


        public bool ReadOnly
        {
            set { ofd.ReadOnlyChecked = value; }
            get { return ofd.ReadOnlyChecked; }
        }
        public bool ReadingFlag
        {
            set { reading = value; }
            get { return reading; }
        }
        public bool IsExits
        {
            get { return File.Exists(FullPath); }

        }

        public DialogResult InputFilename(string newFilename, string newDir = "", bool bSave = false)
        {

            return ShowDialog(bSave, newDir, true, newFilename);
        }
        public string GetTestNewFileName(string newfullpath) { 
            if( newfullpath.IndexOf(MainDir )!=0){
                return "*パス異常*"+newfullpath;
            }
            if( newfullpath.Length == MainDir .Length ){
                return "*ファイル名が有りません*"+newfullpath;
            
            }
            return newfullpath.Substring(MainDir.Length + 1);
        }
        public  string GetTestNewFullPath(string nfn)
        {
            return MainDir + "\\" + nfn;
        }
       
        public DialogResult ShowDialog(bool bSave = false, string newDir = "", bool bInputed = false, string newfilename = "")
        {


            int dirstartpos=0;
            bool bFileExits;
            
            string TEXTCAPTION = (bSave) ? "ファイルを保存" : "ファイルを開く";
            //ofd.Title = TEXTCAPTION;
            if (newfilename == "")
            {
                newfilename = Filename;//未入力は補う
            }
            else {

                //PublicToolBox tb=new PublicToolBox ();
                newfilename = PublicToolBox.RemoveInhibitChar(newfilename);
                
            
            }

            sfd.FileName = ofd.FileName = newfilename;

           
         
            if (newDir == "")
            {
                sfd.InitialDirectory = ofd.InitialDirectory = MainDir;
                string tmp=GetTestNewFullPath( newfilename);;
                sfd.FileName = ofd.FileName = tmp;
            }
            else
            {
                if (File.Exists(newDir) == false)
                {
                    return DialogResult.Abort;
                }
                else
                {
                    sfd.InitialDirectory = ofd.InitialDirectory = newDir;
                    sfd.FileName = ofd.FileName = GetTestNewFullPath(newfilename);
                }
                //
            }
            bFileExits = false;
            bool bRetry = false;

            do
            {
                bRetry = false;
                if (bInputed == false)
                {
                    if (bSave)
                    {
                        if (sfd.ShowDialog() == DialogResult.OK) {
                            //newfilename = GetTestNewFileName(sfd.FileName );//GetTestNewFileName()
                            //newSafeName = sfd.sa;
                            bInputed = true;
                            newfilename = GetTestNewFileName(sfd.FileName);//GetTestNewFileName()
                            
                            dirstartpos = sfd.FileName.IndexOf(MainDir);
                        } 
                    }
                    else
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            newfilename = GetTestNewFileName(ofd.FileName);//GetTestNewFileName()
                            
                            dirstartpos = ofd.FileName.IndexOf(MainDir);
                            bInputed = true;
                        }
                        else {
                            return DialogResult.Abort;
                        }
                    }
                }
                //ファイルが存在すること
                bFileExits = File.Exists(GetTestNewFullPath(newfilename));
                if (!bInputed){
                    return DialogResult.Abort;
                }
                //ファイルダイアログが正しい値を返し、
                if (dirstartpos > 0)
                {
                    //ファイル名が絶対パスで帰っていないか、ディレクトリの指定に間違いがある。
                    MessageBox.Show(string.Format("ファイルシステム異常が発生しました。\nメインフォルダに間違いがある可能性があります。\nメインフォルダ{0}\n指定パス{1}"
                        , MainDir, newfilename), TEXTCAPTION, MessageBoxButtons.YesNoCancel);
                    return DialogResult.Abort;


                }
                if (dirstartpos < 0)
                {
                    //メインフォルダと全一致するパスが無い＝＞配下に無い
                    if( bSave ){
                    //保存の時はありえない。
                        bInputed = false ;
                        break;
                    }
                    
                    DialogResult dr =
                        MessageBox.Show(string.Format
                            (
                            "メインフォルダとは別のフォルダにあるファイルを指定されました。\n"
                            +
                            "指定ファイルをメインフォルダにコピーして編集を開始しますか。\n"
                            +
                            "メインフォルダ{0}\n指定パス{1}"
                                , MainDir
                                , newfilename
                                )
                                , TEXTCAPTION, MessageBoxButtons.YesNoCancel);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            //次のステップへ
                         
                        // bInputed = false;
                            break;
                        case DialogResult.No:
                            //じゃあ、如何しろって言うの？
                            bInputed = false ;
                            bRetry = true;
                            break;
                        case DialogResult.Cancel:
                            //中止
                            return DialogResult.Abort;
                    }

                }
                if (bFileExits == false ){
                    if (!bSave)
                    {
                        //ディレクトリの配下であるが、
                        //パスは存在せず。
                        //オープン時は困る
                        MessageBox.Show(string.Format
                            (
                                "ファイルが見つかりません\n"
                                +
                                "メインフォルダ:{0}\n指定パス:{1}\n{2}"
                                , MainDir
                                , newfilename
                                , GetTestNewFullPath(newfilename)
                                )
                                , TEXTCAPTION, MessageBoxButtons.OK);
                        return DialogResult.Abort;
                    }
                    else
                    {

                        //（保存の時）
                        //ディレクトリの配下であるが、
                        //パスが存在していない
                        DialogResult dr =
                        MessageBox.Show("ファイルが存在していません。新規に作成しますか?\n新しいパス:" + sfd.FileName  , TEXTCAPTION, MessageBoxButtons.YesNoCancel);
                        switch (dr)
                        {
                            case DialogResult.Yes:
                                //次のステップへ
                                bRetry = false;
                                break;
                            case DialogResult.No:
                                //じゃあ、如何しろって言うの？
                                bInputed = false;
                                bRetry = true;
                                break;
                            case DialogResult.Cancel:
                                //中止
                                return DialogResult.Abort;
                        }
                    }
                }//存在していればGood!
            } while (bRetry);
            //ファイル名のチェックは終わった。
            //dirstartpos = MainDir以下であることが担保されている。
            //
            
            TryNewFilename(bFileExits, newfilename);

            return DialogResult.OK;
        }
        private bool TryNewFilename(bool bFileExits, string newfilename)
        {
            try
            {
                int dirnamelen = MainDir.Length;
                string newFileName = newfilename;//.Substring(dirnamelen + 1);
                if (File.Exists(GetTestNewFullPath(newFileName)) == bFileExits)
                {
                    //ファイルパスは先のチェックと同じ結果になった。
                    fname = newFileName;
                    if (validatefilenamefCallBackMethod != null)
                    {
                        validatefilenamefCallBackMethod(fname);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TryNewFilename:"+ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace + "\n");
                
                return false;
            }
            return true;
        }
        public  bool ClearFileName()
        {
            try
            {
                int dirnamelen = MainDir.Length;
                string newFileName = "";//.Substring(dirnamelen + 1);

                ReadingFlag = false;
                fname = newFileName;
                ofd.FileName = sfd.FileName = fname;
                if (validatefilenamefCallBackMethod != null)
                {
                    validatefilenamefCallBackMethod(fname);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace + "\n");
                return false;
            }
            return true;
        }
        public FileNameControl(string defaultfilename, string maindir, string extension, string FileTypeTxt, string dialogtitle)
        {
            MainDir = maindir;
            fname = defaultfilename;
            sfd.DefaultExt = ofd.DefaultExt = extension;
            EXT = extension;
            sfd.Filter=ofd.Filter = FileTypeTxt + string.Format("(*{0})|*{0}", extension);


        }

    }
}
