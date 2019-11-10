using AdvancedEncryptionStandard;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FormApp
{
    public partial class ScramblerForm : Form
    {
        private byte[] FileValue { get; set; }

        public ScramblerForm()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtFileName.Text = OpenFile.FileName.Replace(@"\\", @"\");
                    FileValue = File.ReadAllBytes(OpenFile.FileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERRO!");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(new string('*', 10));
                }
            }
        }

        private void btnEncryptedFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (var folderBrowser = new FolderBrowserDialog())
                {
                    DialogResult result = folderBrowser.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                    {
                        txtEncryptedFile.Text = folderBrowser.SelectedPath.Replace(@"\\", @"\") + "\\encrypted";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO!");
                Console.WriteLine(ex.Message);
                Console.WriteLine(new string('*', 10));
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                var result = new Scrambler().WithKey(txtKey.Text, 4).Encrypt(FileValue);
                Console.WriteLine(string.Join(string.Empty, result.Select(b => b.ToHexByte())).ToUpper());
                File.WriteAllBytes(txtEncryptedFile.Text, result);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO!");
                Console.WriteLine(ex.Message);
                Console.WriteLine(new string('*', 10));
            }
        }
    }
}
