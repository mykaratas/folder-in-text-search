using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public static List<string> listdosya = new List<string>();
        private void DizinIceriginiListeyeEkle(string dizin)


        {

            string[] dizindekiKlasorler = Directory.GetDirectories(dizin);

            string[] dizindekiDosyalar = Directory.GetFiles(dizin);

            foreach (string klasor in dizindekiKlasorler)

            {


                DirectoryInfo dirInfo = new DirectoryInfo(klasor);

                string klasorAdi = dirInfo.Name;

                DateTime olsTarihi = dirInfo.CreationTime;



                ListViewItem item = new ListViewItem(klasorAdi);
                CheckedListBox ck1 = new CheckedListBox();

                item.SubItems.Add("Klasör");

                item.SubItems.Add("");

                item.SubItems.Add(olsTarihi.ToString("dd.MM.yyyy HH:mm"));
                checkedListBox1.Items.Add(klasorAdi);
                checkedListBox1.Items.Add(ck1);

                // listView1.Items.Add(item);

            }

            foreach (string dosya in dizindekiDosyalar)

            {

                FileInfo fileInfo = new FileInfo(dosya);



                string dosyaAdi = fileInfo.Name;

                long byteBoyut = fileInfo.Length;

                DateTime olsTarihi = fileInfo.CreationTime;



                ListViewItem item = new ListViewItem(dosyaAdi);

                item.SubItems.Add("Dosya");

                item.SubItems.Add(byteBoyut.ToString());

                item.SubItems.Add(olsTarihi.ToString("dd.MM.yyyy HH:mm"));
                // checkedListBox1.Items.Add(klasorAdi);

                //    listView1.Items.Add(item);

            }

        }
        public void ReadDirectory()

        {
            string uzanti=Form1.uzanti.ToString();
            //  listBox1.Items.Clear();
            string ad = Form1.bulveri.ToString();
            
            string[] dosyalar = Directory.GetFiles(@Form1.bulveri.ToString(), uzanti, SearchOption.AllDirectories);
            foreach (var item in dosyalar)
            {
                checkedListBox1.Items.Add(item);
            }


            //     DirectoryInfo di = new DirectoryInfo(Form1.bulveri.ToString());

            //   FileInfo rgfiles = new FileInfo("Z:\\BELGELERIM\\STJ40SGB\\My Documents\\Desktop\\New folder\\*txt");


            //  FileInfo[] rgFiles = di.GetFiles();

            // MessageBox.Show(rgfiles.Name);

            //foreach (FileInfo fi in rgFiles)

            //{

            //listBox1.Items.Add(fi.Name);

            //}

        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            //  DizinIceriginiListeyeEkle(Form1.bulveri.ToString());
            //  txtbul(Form1.bulveri.ToString());
            ReadDirectory();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                DataRowView view = checkedListBox1.Items[i] as DataRowView;
                checkedListBox1.SetItemChecked(i, true);

            }

        }

       public static List<string> listfrm2dosya = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                listdosya.Add(itemChecked.ToString());
                listfrm2dosya.Add(itemChecked.ToString());
                
            }
            
            MessageBox.Show("Başarılı...");

        }
       public ListBox lb = new ListBox();
       Form1 fff = new Form1();
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
           
            
            for (int i = 0; i < Form2.listdosya.Count; i++)
            {
               lb.Items.Add(Form2.listdosya[i].ToString());
            }
            
          
            
        }

        
    }
}
