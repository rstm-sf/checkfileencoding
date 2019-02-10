using System;
using System.Windows.Forms;
using UCharDetSharp;

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

        private void Button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            var encoding = GetFileEncodingUchardet(filename);
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
                    encoding = det.Complete().EncodingName;
                }
            }
 
            return encoding;
        }

        private string GetFileEncodingUchardet(string filename)
        {
            if (!System.IO.File.Exists(filename))
                return "файла не существует";

            var encoding = "пустой файл";
            using (var stream = new System.IO.FileStream(filename, System.IO.FileMode.Open))
            {
                if (stream.Length > 0)
                {
                    var bufferSize = 65536;
                    var buffer = new byte[bufferSize];
                    var det = new UCharDet();
                    long numBytesToRead = 0;
                    var retval = 0;
                    while (numBytesToRead < stream.Length)
                    {
                        var sz = stream.Read(buffer, 0, bufferSize);
                        retval = det.HandleData(buffer, (uint)bufferSize);
                        if (retval != 0) break;
                        numBytesToRead += sz;
                    }
                    if (retval == 0)
                    {
                        det.DataEnd();
                        var encodingName = det.GetCharset();
                        encoding = System.Text.Encoding.GetEncoding(encodingName).EncodingName;
                    }
                }
            }

            return encoding;
        }
    }
}
