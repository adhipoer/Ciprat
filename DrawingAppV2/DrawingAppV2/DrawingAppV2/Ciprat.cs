using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingAppV2
{
    public partial class Ciprat : Form
    {
        bool startDrawing;
        bool asd = false;
        Graphics graphic;
        int? initX = null;
        int? initY = null;
        float brushSize;
        float recentBrushSize;
        Color color;
        int? a, b;
        int c, d;
        Stack<int?> stackA = new Stack<int?>();
        Stack<int?> stackB = new Stack<int?>();
        Stack<int> stackC = new Stack<int>();
        Stack<int> stackD = new Stack<int>();

        public Ciprat()
        {
            InitializeComponent();
            graphic = panel1.CreateGraphics();
            startDrawing = false;
            brushSize = 1f;
            color = Color.Black;
            panel1.AutoScroll = true;
        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            startDrawing = true;
            if (!asd)
            {
                stackA.Push(-2);
                stackB.Push(-2);
                stackC.Push(-2);
                stackD.Push(-2);
            }
            else
            {
                asd = true;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(startDrawing)
            {
                Pen p = new Pen(color, brushSize);
                //Drawing the line.  
                graphic.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                
                //a = initX;
                //b = initY;
                //c = e.X;
                //d = e.Y;

                stackA.Push(initX);
                stackB.Push(initY);
                stackC.Push(e.X);
                stackD.Push(e.Y);
                recentBrushSize = brushSize;
                
                initX = e.X;
                initY = e.Y;  
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            startDrawing = false;
            initX = null;
            initY = null;
            asd = false;
            //stackA.Push(-1);
           // stackB.Push(-1);
            //stackC.Push(-1);
            //stackD.Push(-1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            brushSize = 10f;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                color = colorDialog1.Color;
                panelRecentColor.BackColor = colorDialog1.Color;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pen p = new Pen(Color.White, recentBrushSize);

            bool isStop = false;
            while(!isStop && stackD.Count > 0)
            {
                int tmp = stackD.Pop();
                if(tmp != -2)
                {
                    int? aa = stackA.Pop();
                    int? bb = stackB.Pop();
                    int cc = stackC.Pop();
                    int dd = tmp;
                    graphic.DrawLine(p, new Point(aa ?? cc, bb ?? dd), new Point(cc,dd));
                    Console.WriteLine(a + "  " + bb +" " + cc + " " + dd);
                }
                else
                {
                    isStop = true;
                    int? aa = stackA.Pop();
                    int? bb = stackB.Pop();
                    int cc = stackC.Pop();
                }
            }
            //Drawing the line.  
            //graphic.DrawLine(p, new Point(a ?? c, b ?? d), new Point(c,d));
            //Point wr = new Point();

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if(ActiveControl is TextBox)
              //  ((TextBox)ActiveControl).Copy();
            SendKeys.Send("^C");
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (ActiveControl is TextBox)
              //  ((TextBox)ActiveControl).Cut();
            SendKeys.Send("^X");
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (ActiveControl is TextBox)
             //   ((TextBox)ActiveControl).Paste();
            SendKeys.Send("^V");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Pen p = new Pen(Color.White, recentBrushSize);

            bool isStop = false;
            while (!isStop && stackD.Count > 0)
            {
                int tmp = stackD.Pop();
                if (tmp != -2)
                {
                    int? aa = stackA.Pop();
                    int? bb = stackB.Pop();
                    int cc = stackC.Pop();
                    int dd = tmp;
                    graphic.DrawLine(p, new Point(aa ?? cc, bb ?? dd), new Point(cc, dd));
                    Console.WriteLine(a + "  " + bb + " " + cc + " " + dd);
                }
                else
                {
                    isStop = true;
                    int? aa = stackA.Pop();
                    int? bb = stackB.Pop();
                    int cc = stackC.Pop();
                }
            }
        }

        
    }
}
