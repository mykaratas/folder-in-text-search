using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        public static ListBox lb;
        public static string uzanti;
        string dosyaadi = "";
        List<string> dosyalar = null;
        Dictionary<string, string> degisecekkelimeler = null;
        Thread t = null;
        void BulveDegistir()
        {
            try
            {
                dosyalar = Directory.GetFiles(textBox1.Text, "*.*", SearchOption.AllDirectories).ToList();
                // btnBulveDegistir.Enabled = false;
                //  btnGirisleriTemizle.Enabled = false;
                for (int i = 0; i < dosyalar.Count; i++)
                {
                    dosyaadi = dosyalar[i];


                    if (checkedListBox2.Items.Count > 0)
                    {


                        for (int j = 0; j < checkedListBox2.Items.Count; j++)
                        {
                            if (new FileInfo(dosyaadi).Name.Equals(checkedListBox2.Items[j].ToString()))
                            {
                                DosyaDegistir(dosyaadi, degisecekkelimeler);
                               // listBox2.Text = (i + 1).ToString() + " / " + dosyalar.Count + " \"" + dosyaadi + "\" güncellendi.";
                            }

                        }


                    }
                    else
                    {
                        DosyaDegistir(dosyaadi, degisecekkelimeler);
                       // listBox2.Text = (i + 1).ToString() + " / " + dosyalar.Count + " \"" + dosyaadi + "\" güncellendi.";
                    }

                }
               // listBox2.Text = "Güncelleme işlemleri başarıyla tamamlandı.";


                // btnGirisleriTemizle.Enabled = true;
                


            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            t = null;


        }
        void DosyaDegistir(string dosyayolu, Dictionary<string, string> degisecekler)
        {
            string icerik = "";

            icerik = File.ReadAllText(dosyayolu, Encoding.Default);
            foreach (var i in degisecekler)
            {
                icerik = icerik.Replace(i.Key, i.Value);
            }
            File.WriteAllText(dosyayolu, icerik, Encoding.Default);


        }
        void AddDegisecekKelime()
        {
            try
            {
                if (textBox3.Text != "" && textBox2.Text != "")
                {


                    degisecekkelimeler.Add(textBox3.Text, textBox2.Text);
                   // listBox2.Items.Add(textBox3.Text + " > " + textBox4.Text);


                    textBox3.Text = "";
                    textBox2.Text = "";
                    textBox3.Focus();


                }
                else
                {
                    MessageBox.Show("Gerekli alanları doldurun!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);


            }
        }
        

        public Form1()
        {
            InitializeComponent();
        }
        public static string bulveri = "sadas";
        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            listBox1.MeasureItem += lst_MeasureItem;
            listBox1.DrawItem += lst_DrawItem;

            checkedListBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            checkedListBox2.MeasureItem += chk_MeasureItem;
            checkedListBox2.DrawItem += chk_DrawItem;

            
           
        }
        

private void lst_MeasureItem(object sender, MeasureItemEventArgs e)
{
    e.ItemHeight = (int)e.Graphics.MeasureString(listBox1.Items[e.Index].ToString(), listBox1.Font, listBox1.Width).Height;
}

private void lst_DrawItem(object sender, DrawItemEventArgs e)
{
    e.DrawBackground();
    e.DrawFocusRectangle();
    e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
}

private void chk_MeasureItem(object sender, MeasureItemEventArgs e)
{

    e.ItemHeight = (int)e.Graphics.MeasureString(checkedListBox2.Items[e.Index].ToString(), checkedListBox2.Font, checkedListBox2.Width).Height;
}

private void chk_DrawItem(object sender, DrawItemEventArgs e)
{
    e.DrawBackground();
    e.DrawFocusRectangle();
    e.Graphics.DrawString(checkedListBox2.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
}
        public void ReadDirectory()
        {
            string uzanti = Form1.uzanti.ToString();
            //  listBox1.Items.Clear();
            string ad = Form1.bulveri.ToString();

            string[] dosyalar = Directory.GetFiles(bulveri.ToString(), uzanti, SearchOption.AllDirectories);
            foreach (var item in dosyalar)
            {
                checkedListBox2.Items.Add(item);
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

        private void button1_Click(object sender, EventArgs e)
        {


            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                fbd.SelectedPath = fbd.SelectedPath;
                textBox1.Text = fbd.SelectedPath.ToString();
                button3.Enabled = true;
                button6.Enabled = true;

            }
            bulveri = textBox1.Text;


            ReadDirectory();


        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Emin misiniz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                t = new Thread(new ThreadStart(BulveDegistir));
                t.Start();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            labelbilgi();
           
            //string[] pathary = new string[listBox1.Items.Count];
            //for (int i = 0; i < listBox1.Items.Count; i++)
            //{
            //    pathary[i] = "@" + "\"" + listBox1.Items[i].ToString() + "\"";
                
            //}
            

            //for (int j = 0; j < listBox1.Items.Count; j++)
            //{


            //    FileStream inFile = new FileStream(listBox1.Items[j].ToString(), FileMode.Open, FileAccess.Read);
            //    StreamReader reader = new StreamReader(inFile);
                
            //   int av= inFile.Name.LastIndexOf('\\');
            //    string dizin = inFile.Name.Substring(0, av);
            //    string dosyaadi = inFile.Name.Substring(av+1);
            //    string temp_file_name = dizin + "\\_temp_" + dosyaadi;
            //    StreamWriter wr = new StreamWriter(File.Create(temp_file_name));


            //    string output=textBox3.Text;
            //    int record;
            //    string input;
            //    int say = 0;
                
            //    input = textBox2.Text;
                
            //   // MessageBox.Show(input);
            //    try
            //    {
            //        //the program reads the record and displays it on the screen


            //        int TEMP = 0;
            //        char[] tempdizi = new char[input.Length];

            //        while ((record = reader.Read()) != -1)
            //        {
            //            if (record == input[TEMP])
            //            {
            //                tempdizi[TEMP] = (char)record;
            //                TEMP++;
            //                if (TEMP == input.Length)
            //                {
            //                    wr.Write(output);
            //                    TEMP = 0;
            //                    say++;
            //                }
            //                else
            //                {
            //                    continue;
            //                }
            //            }
            //            else
            //            {
            //                if (TEMP == 0)
            //                {
            //                    wr.Write((char)record);
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < TEMP; i++)
            //                    {
            //                        wr.Write(tempdizi[i]);
            //                    }
            //                    wr.Write((char)record);

            //                }
            //                TEMP = 0;
            //            }


            //        }
            //    }

            //    //    string ad = reader.ReadLine();

            //    //    while (record != null)
            //    //    {
            //    //        if (record.Contains(input))
            //    //        {
            //    //            say++;
            //    //            MessageBox.Show(input);


            //    //        }
            //    //        record = reader.ReadLine();

            //    //    }
            //    //}

            //    finally
            //    {
            //        //after the record is done being read, the progam closes
            //        reader.Close();
            //        inFile.Close();
            //        wr.Close();
            //        File.Delete(inFile.Name);
            //        File.Move( temp_file_name,inFile.Name);
            //        File.Delete(temp_file_name);

            //    }
            //    if (say > 0)
            //    {
            //        label5.Text +=listBox1.Items[j].ToString()+ "  "+textBox2.Text + " " + say + "" + " Adet Bulundu  \n";
            //         textBox4.Text = textBox2.Text;
            //    }
            //    // Console.ReadLine();
            

        }

        public void arabuldegistir()
        {


            string[] pathary = new string[checkedListBox2.Items.Count];
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                pathary[i] = "@" + "\"" + checkedListBox2.Items[i].ToString() + "\"";
                
            }


            for (int j = 0; j < checkedListBox2.Items.Count; j++)
            {


                FileStream inFile = new FileStream(checkedListBox2.Items[j].ToString(), FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);
                
               int av= inFile.Name.LastIndexOf('\\');
                string dizin = inFile.Name.Substring(0, av);
                string dosyaadi = inFile.Name.Substring(av+1);
                string temp_file_name = dizin + "\\_temp_" + dosyaadi;
                StreamWriter wr = new StreamWriter(File.Create(temp_file_name));


                string output=textBox3.Text;
                int record;
                string input;
                int say = 0;
                
                input = textBox2.Text;
                
               // MessageBox.Show(input);
                try
                {
                    //the program reads the record and displays it on the screen


                    int TEMP = 0;
                    char[] tempdizi = new char[input.Length];

                    while ((record = reader.Read()) != -1)
                    {
                        if (record == input[TEMP])
                        {
                            tempdizi[TEMP] = (char)record;
                            TEMP++;
                            if (TEMP == input.Length)
                            {
                                wr.Write(output);
                                TEMP = 0;
                                say++;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (TEMP == 0)
                            {
                                wr.Write((char)record);
                            }
                            else
                            {
                                for (int i = 0; i < TEMP; i++)
                                {
                                    wr.Write(tempdizi[i]);
                                }
                                wr.Write((char)record);

                            }
                            TEMP = 0;
                        }


                    }
                }

                //    string ad = reader.ReadLine();

                //    while (record != null)
                //    {
                //        if (record.Contains(input))
                //        {
                //            say++;
                //            MessageBox.Show(input);


                //        }
                //        record = reader.ReadLine();

                //    }
                //}

                finally
                {
                    //after the record is done being read, the progam closes
                    reader.Close();
                    inFile.Close();
                    wr.Close();
                    File.Delete(inFile.Name);
                    File.Move( temp_file_name,inFile.Name);
                    File.Delete(temp_file_name);

                }
                
            }
        

        }
        public int maxprogressbar()
        {
            int say = 0; int TEMP = 0;
            for (int j = 0; j < checkedListBox2.CheckedItems.Count; j++)
            {
                FileStream inFile = new FileStream(checkedListBox2.CheckedItems[j].ToString(), FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);
                int record;
                string input;
                
                input = textBox4.Text;


                
                char[] tempdizi = new char[input.Length];

                while ((record = reader.Read()) != -1)
                {
                    say++;
                }
                
            }
            MessageBox.Show(say.ToString());
                return say;

        }
        public void labelbilgi()
        {
            if (textBox4.Text!="")
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = checkedListBox2.CheckedItems.Count;
                progressBar1.Step = 1;

                progressBar1.Value = 0;
                int prb = 0;

                for (int j = 0; j < checkedListBox2.CheckedItems.Count; j++)
                {


                    FileStream inFile = new FileStream(checkedListBox2.CheckedItems[j].ToString(), FileMode.Open, FileAccess.Read);
                    StreamReader reader = new StreamReader(inFile);

                    int av = inFile.Name.LastIndexOf('\\');
                    string dizin = inFile.Name.Substring(0, av);
                    string dosyaadi = inFile.Name.Substring(av + 1);
                    // string temp_file_name = dizin + "\\_temp_" + dosyaadi;
                    // StreamWriter wr = new StreamWriter(File.Create(temp_file_name));

                    prb++;
                    progressBar1.Value = prb;

                    // string output = textBox3.Text;
                    int record;
                    string input;
                    int say = 0;

                    input = textBox4.Text;


                    int TEMP = 0;
                    char[] tempdizi = new char[input.Length];

                    while ((record = reader.Read()) != -1)
                    {
                        if (record == input[TEMP])
                        {
                            tempdizi[TEMP] = (char)record;
                            TEMP++;
                            if (TEMP == input.Length)
                            {

                                TEMP = 0;
                                say++;


                            }
                            else
                            {

                                continue;
                            }
                        }
                        else
                        {
                            if (TEMP == 0)
                            {

                            }
                            else
                            {


                            }
                            TEMP = 0;
                        }


                    }

                    reader.Close();
                    inFile.Close();

                    //  File.Delete(inFile.Name);
                    // File.Move(temp_file_name, inFile.Name);
                    // File.Delete(temp_file_name);
                    if (say > 0)
                    {

                        // listBox3.Items.Add(listBox1.Items[j].ToString());
                        listBox1.Items.Add(checkedListBox2.CheckedItems[j].ToString() + " -Dosyasından '" + textBox2.Text + "' Karakterleri Toplam " + say + " Adet Bulundu.");
                        textBox2.Text = textBox4.Text;
                        button4.Enabled = true;
                    }
                    if (say == 0)
                    {
                        MessageBox.Show("Aranılan Kelime Bulunamadı...");
                    }
                    ListBox listbbb = new ListBox();




                } 
            }
            else
            {
                MessageBox.Show("LüLfen Bir Kelime Giriniz...","UYARI...");
            }

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                //  if (listBox1.SelectedItem != null) if (listBox1.SelectedItem.ToString().Length != 0) listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox3.Text!="")
            {
                string saat = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                string icerik = "";
                string yol = "";
                // degisecekler.Add("aaaa","fff");
                string ad = textBox1.Text + @"\Backup" + saat;
                // ////"Z:\BELGELERIM\STJ40SGB\My Documents\Desktop\New folder (4)\backup"
                // Directory.CreateDirectory(@ad);
                if (!System.IO.Directory.Exists(@ad))
                {
                    System.IO.Directory.CreateDirectory(@ad);
                }
                DirectoryInfo dir = new DirectoryInfo(@ad);

                FileInfo[] files = dir.GetFiles();

                List<FileInfo> fff = new List<FileInfo>();
                for (int m = 0; m < checkedListBox2.CheckedItems.Count; m++)
                {
                    FileInfo fff1 = new FileInfo(checkedListBox2.CheckedItems[m].ToString());
                    fff.Add(fff1);
                }

                progressBar1.Minimum = 0;
                progressBar1.Maximum = fff.Count;
                progressBar1.Step = 1;

                progressBar1.Value = 0;
                int prb = 0;
                foreach (FileInfo file in fff)
                {
                    prb++;
                    //  MessageBox.Show(file.FullName);

                    string temppath = Path.Combine(@ad, file.Name);
                    file.CopyTo(temppath, true);
                    //FileInfo yeldaCopy = new FileInfo(@checkedListBox2.CheckedItems[j].ToString());
                    //yeldaCopy.CopyTo(yeldaCopy.Name);
                    // File.Copy(@checkedListBox2.Items[j].ToString(), @checkedListBox2.Items[j].ToString(), true);
                    icerik = File.ReadAllText(file.FullName, Encoding.Default);
                    icerik = icerik.Replace(textBox2.Text, textBox3.Text);
                    File.WriteAllText(file.FullName, icerik, Encoding.Default);
                    progressBar1.Value = prb;

                }
                MessageBox.Show("Tamamlandı...");

                
            }
            else
            {
                MessageBox.Show("Lütfen Değişecek Kelimeyi Giriniz");
                
            }



            
            

        }
        void yelda_copy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Kaynak dizin bulunamadı: " + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                if (File.Exists(temppath))
                {
                    File.Delete(temppath);
                }

                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    yelda_copy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
           
            string ad =   textBox1.Text + @"\Copy" ;
            Directory.CreateDirectory(@ad);
            MessageBox.Show(ad);
          //  string Metin = BulveDegistir("asdasdasdsad", txtArananMetin.Text, txtYeniMetin.Text);


        }
        public string BulveDegistir(string Metin, string arananMetin, string yeniMetin)
        {


            Metin = Metin.Insert(Metin.IndexOf(arananMetin), yeniMetin);
            Metin = Metin.Remove(Metin.IndexOf(arananMetin), arananMetin.Length);
            return Metin;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // uzanti = "*.txt";
            if (comboBox1.Text=="Seçiniz")
            {
                MessageBox.Show("Lütfen Bir Dosya Uzantı Türü Seçiniz...");
            }
            else
            {
                uzanti = comboBox1.Text;
                button1.Enabled = true;
                
            }
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                DataRowView view = checkedListBox2.Items[i] as DataRowView;
                checkedListBox2.SetItemChecked(i, true);

            }
            button2.Enabled = false;
            button8.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                DataRowView view = checkedListBox2.Items[i] as DataRowView;
                checkedListBox2.SetItemChecked(i, false);

            }
            button8.Enabled = false;
            button2.Enabled = true;
        }

        private void button3_Click_2(object sender, EventArgs e)
        {

        }

        private void button3_Click_3(object sender, EventArgs e)
        {

            if (Directory.Exists(@textBox1.Text))
            {
                System.Diagnostics.Process.Start(@textBox1.Text);
            }
            else
            {
                MessageBox.Show("Lütfen Klasör Seçiniz");
            }
        
            
        }
    }
}
    
    

