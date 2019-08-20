using System;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MazeGenerator
{
    public partial class Form1 : Form
    {
        public Generator generator;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            generator = new Generator((int)numericUpDown1.Value, checkBox1.Checked);
            ThreadStart ts = new ThreadStart(Live);
            Thread thread = new Thread(ts);
            thread.Start();
        }
        private void Live() {

            Action<bool> updateAction = new Action<bool>((value) => button2.Enabled = value);       //Turn off Exort Button
            button2.Invoke(updateAction, false);

            while (true)
            {
                while (true)
                {
                    if (generator.GenerateNextStep())
                    {
                        if (checkBox1.Checked)
                        {
                            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                            pictureBox1.Image = generator.Draw();
                            Thread.Sleep(20);
                        }
                    }
                    else break;
                }

                generator.SetSeeker(generator.visited[0]);
                generator.visited.RemoveAt(0);
                if (generator.visited.Count == 0) break;   
            }
            generator.cell[(int)numericUpDown1.Value-1, (int)numericUpDown1.Value-1].walls[2] = false;
            pictureBox1.Image = generator.Draw();
            button2.Invoke(updateAction, true);                                                 //Turn on Exort Button
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("Maze", ImageFormat.Jpeg);
        }
    }
}
