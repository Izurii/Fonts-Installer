using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Fonts_Installer
{
    public partial class MainForm : Form
    {

        string currentDir = Directory.GetCurrentDirectory();
        string fontsPath = Path.GetPathRoot(Environment.SystemDirectory) + "Windows/Fonts/";
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            IEnumerable<string> files = Directory.EnumerateFiles(currentDir, "*.ttf");
            try
            {
                foreach (string file in files)
                {
                    registerFont(Path.GetFileName(file));
                }
                MessageBox.Show(null, "O processo foi finalizado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, "Epa, deu algum erro. Contate o autor e peça para ele arrumar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void registerFont(string font)
        {
            string[] description = font.Split('-');

            if(!File.Exists(fontsPath+font))
                File.Copy(font, (fontsPath + font));

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");
            key.SetValue(description[0] + " " + description[1], font);
            key.Close();

        }

    }
}
