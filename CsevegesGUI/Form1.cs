using System.Runtime.CompilerServices;

namespace CsevegesGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using StreamReader sr = new(@"..\..\..\bin\Debug\net6.0-windows\csevegesek.txt");
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string? sor = sr.ReadLine();
                string[] sordarabok = sor.Split(';');

                list.Add(sordarabok[2]);
                list2.Add(sordarabok[3]);
            }
            list.OrderBy(x => x).ToList();
            list2.OrderBy(x => x).ToList();
        }
    }
}