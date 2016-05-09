using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
#if true 
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //ThreadExceptionイベントハンドラを追加
            Application.ThreadException +=
                new System.Threading.ThreadExceptionEventHandler(
                    Application_ThreadException);

            Application.Run(new Form1());
        }
        private static void Application_ThreadException(object sender,
    System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                //エラーメッセージを表示する
                //MessageBox.Show(e.Exception.Message, "エラー");
                //
                Console.WriteLine(e.Exception.Message.ToString() + "\n:" + e.Exception.Source.ToString() + "\n:"+e.Exception.StackTrace .ToString() );
            }
            finally
            {
                Console.WriteLine("filary");
                //アプリケーションを終了する

                Application.Exit();
            }
        }
#else
        static void Main(string[] args)
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
                //コマンドラインからの引数があった場合は、一番先頭の引数を Form1 のコンストラクタに渡す
            Application.Run(new Form1(args));
  
        }
#endif
    }
}
