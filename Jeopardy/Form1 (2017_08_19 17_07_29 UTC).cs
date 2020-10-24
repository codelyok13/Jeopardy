using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace Jeopardy
{
    public partial class Form1 : Form
    {
        public struct test
        {
            public int points;
            public string line; //the title or qs for a qs;
            public string ans;
        }
        List<string> bArray = new List<string>();
        List<test> tArray = new List<test>();
        int findTest;


        public Form1()
        {
            InitializeComponent();
            setUp();
        }

        private void setUp() // fills the labels and buttons with the appropiate names
        {
            openFileDialog1.ShowDialog();
            string fileName = openFileDialog1.FileName;
            if (!fileName.Equals("openFileDialog1"))
            {
                string[] file = File.ReadAllLines(fileName);

                getTestData(file);

                fillTable();
            }
        }
        private void getTestData(string[] file)
        {
            for (int i = 0; i < file.Length; i++)
            {
                test check;
                check.points = int.Parse(file[i]);
                i++;
                check.line = file[i];
                i++;
                check.ans = file[i];
                tArray.Add(check);
            }
        }
        private void fillTable()
        {
            for (int i = 0; i < 30; i++)
            {
                int representColumn = (i / 6) + 1; //this varaible tells me where my columns are
                int representRow = i - (representColumn - 1) * 6;
                /*
                    i = 6 *(column) + row //use this formula to make sure that i is doing its job
                    How represent* works
                    ----------------------
                    I want to get column 4 row 5 whis is at i = 23
                    representColumn: ((23/6) = 3) + 1 = 4 >>>>> this tells control to go to column("title4")
                    representRow: 23 - (representColumn - 1 = 3) * 6) = 23 - 3*6 = 23 -18 = 5;
                    I will find t45 in the else part of the statement using controll
                */
                if ((i % 6) == 0)
                {
                    (Controls["title" + representColumn] as Label).Text = tArray[i].line;

                }
                else
                {
                    (Controls["t" + representColumn + "" + representRow] as Button).Text = "" + tArray[i].points;
                }
            }
        }
     
        private void t11_Click(object sender, System.EventArgs e)
        {
            giveQs(((Button)sender).Name);
            bArray.Add(((Button)sender).Name);
        }
        public void giveQs(string a)
        {
            for (int i = 0; i < 30; i++)
            {
                int representColumn = (i / 6) + 1; //this varaible tells me where my columns are
                int representRow = i - (representColumn - 1) * 6;
                if ((i % 6) != 0)
                {
                    (Controls["t" + representColumn + "" + representRow] as Button).Enabled = false;
                }
            }
            int column = int.Parse(a[1].ToString());
            int row = int.Parse(a[2].ToString());
            findTest = 6 * (column - 1) + row; //this find the column and rows the qs
            textBox1.Text = tArray[findTest].line;
        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < 30; i++)
            {
                int representColumn = (i / 6) + 1; //this varaible tells me where my columns are
                int representRow = i - (representColumn - 1) * 6;
                if ((i % 6) != 0)
                {
                    (Controls["t" + representColumn + "" + representRow] as Button).Enabled = true;
                }
                textBox1.Clear();
            }
            foreach(string a in bArray)
            {
                (Controls[a] as Button).Enabled = false;
            }
            button2.Text = "Show Answer";
        }
        private void button2_Click(object sender, System.EventArgs e)
        {
            button2.Text = tArray[findTest].ans;
        }
    }
}
