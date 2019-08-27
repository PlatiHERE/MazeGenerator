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

        Thread thread;

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
            thread = new Thread(Live);
            if (thread.ThreadState == ThreadState.Running)
            {
                checkBox1.Checked = false;
                generator = null;
            }
            else
            {
                generator = new Generator((int)numericUpDown1.Value, checkBox1.Checked);
                //thread = new Thread(Live);
                thread.Start();
            }
        }
        private void Live() {

            Action<bool> updateAction = new Action<bool>((value) => button2.Enabled = value);       //Turn off Export Button
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
                //MessageBox.Show("ilosc w liscie:" + generator.visited.Count + "\n pozycja:");
                generator.visited.RemoveAt(0);
                if (generator.visited.Count == 0) break;   
            }
            generator.cell[(int)numericUpDown1.Value-1, (int)numericUpDown1.Value-1].walls[2] = false;
            pictureBox1.Image = generator.Draw();
            button2.Invoke(updateAction, true);                                                 //Turn on Exort Button
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {

                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }
    }
}
