using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace WindowsFormsApplication4
{
    public class SearchKeywords
    {
        List<string> skw = new List<string>();
        public void make(NotePageBase  persons,NotePageBase keywords)
        {
           // RichTextBox rtb;
            skw.Clear ();
            skw.AddRange(persons.SKWS ());
            skw.AddRange(keywords.SKWS());
        }
        public void Clear()
        {
            // RichTextBox rtb;
            skw.Clear();
        }
        public List<string> SKW() {
            return skw;

        }
        public List<string> MarkupSKW()
        {
            
            return skw;

        }

        public System.Collections.Generic.IEnumerable<string > SKWS()
        {
            foreach (string str in skw) {
                yield return str;
            }
        }
        public int GetLength(){
            return skw.Count;
        }

            
    }
}
