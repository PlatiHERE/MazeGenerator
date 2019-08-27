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
        Bitmap originalMazeBitmap;


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
                thread.Abort();
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

            Action<bool> updateExportButton = new Action<bool>((value) => button2.Enabled = value);       //Turn off Export Button
            Action<bool> updateGeneratorionButton = new Action<bool>((value) => button1.Enabled = value);
            button2.Invoke(updateExportButton, false);
            button1.Invoke(updateGeneratorionButton, false);
            while (true)
            {
                while (true)
                {
                    if (generator.GenerateNextStep())
                    {
                        if (checkBox1.Checked)
                        {
                            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                            originalMazeBitmap = generator.Draw();
                            pictureBox1.Image = new Bitmap(originalMazeBitmap, 500, 500);
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
            if(pictureBox1.Image != null)pictureBox1.Image.Dispose();
            originalMazeBitmap = generator.Draw();
            pictureBox1.Image = new Bitmap(originalMazeBitmap, 500, 500);
            button2.Invoke(updateExportButton, true);                                                 //Turn on Exort Button
            button1.Invoke(updateGeneratorionButton, true);
            generator = null;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                originalMazeBitmap.Save(saveFileDialog1.FileName);
            }
        }
    }
}
