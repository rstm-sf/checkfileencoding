using System;
using System.Windows.Forms;

namespace CheckFileEncoding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            var encoding = GetFileEncoding(filename);
            MessageBox.Show("Кодировка файла: " + encoding);
        }

        private string GetFileEncoding(string filename)
        {
            if (!System.IO.File.Exists(filename))
                return "файла не существует";

            string encoding;
            var det = new SimpleHelpers.FileEncoding();
            using (var stream = new System.IO.FileStream(filename, System.IO.FileMode.Open))
            {
                if (stream.Length == 0)
                {
                    encoding = "пустой файл";
                }
                else
                {
                    det.Detect(stream);
                    encoding = det.Complete().ToString();
                }
            }
 
            return encoding;
        }
    }
}
