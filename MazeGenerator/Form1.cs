using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            Generator generator = new Generator((int)numericUpDown1.Value);
            pictureBox1.Image = generator.Draw();
        }
    }

    public class Generator {

        int seekerPosX;
        int seekerPosY;
        int size;
        Cell[,] cell;

        public Generator(int size) {
            this.size = size;
            seekerPosX = size / 2;
            seekerPosY = 0;
            cell = new Cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cell[j, i] = new Cell();
                }
            }
            
        }

        void Direction() {
            Random random = new Random();
            List<Point> list = CheckNeighbors();
            if (list.Count > 0)
            {
                int val = random.Next(list.Count - 1);
                seekerPosX = list[val].X;
                seekerPosY = list[val].Y;
            }
        }

        List<Point> CheckNeighbors() {
            List<Point> list = new List<Point>();
            if (seekerPosY > 0    &&    !cell[seekerPosX,     seekerPosY - 1].visited) list.Add(new Point(seekerPosX,     seekerPosY - 1));    //UP
            if (seekerPosX < size &&    !cell[seekerPosX + 1, seekerPosY    ].visited) list.Add(new Point(seekerPosX + 1, seekerPosY));        //RIGHT
            if (seekerPosY < size &&    !cell[seekerPosX,     seekerPosY + 1].visited) list.Add(new Point(seekerPosX,     seekerPosY + 1));    //DOWN
            if (seekerPosX > 0    &&    !cell[seekerPosX - 1, seekerPosY    ].visited) list.Add(new Point(seekerPosX - 1, seekerPosY));        //LEFT
            return list;
        }

        public Bitmap Draw() {
            Bitmap bmp = new Bitmap((size + 2) * 10 + (size + 2) * 2, (size + 2) * 10 + (size + 2) * 2);     //Create Bitmap. 10px per cell. 2px per space between cells.
            Graphics graphics = Graphics.FromImage(bmp);
            Pen pen = new Pen(Color.Black, 1);
            //for (int i = 0; i < size; i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        cell[j, i].Draw(pen, graphics, new Point(j, i), 10);
            //    }
            //}
            cell[0, 0].Draw(pen, graphics, new Point(0, 0), 10);
            return bmp;
        }
    }
    public class Cell {
        public bool[] walls = { true, true, true, true };   // top, right, bottom, left
        public bool visited = false;

        public Cell() {

        }

        public void Draw(Pen pen, Graphics graphics, Point position, int cellPxSize) {

            if (walls[0]) graphics.DrawLine(pen, (position.X + 1 * cellPxSize) - pen.Width,                  //     LEFT X
                                                 (position.Y + 1 * cellPxSize) - pen.Width,                  //     TOP Y
                                                 (position.X + 1 * cellPxSize) + cellPxSize + pen.Width,     //     RIGHT X
                                                 (position.Y + 1 * cellPxSize) - pen.Width);                 //     TOP Y

            if (walls[1]) graphics.DrawLine(pen, (position.X + 1 * cellPxSize) + cellPxSize + pen.Width,     //     RIGHT X
                                                 (position.Y + 1 * cellPxSize) - pen.Width,                  //     TOP Y
                                                 (position.X + 1 * cellPxSize) + cellPxSize + pen.Width,     //     RIGHT X
                                                 (position.Y + 1 * cellPxSize) + cellPxSize + pen.Width);    //     BOTTOM Y

            if (walls[1]) graphics.DrawLine(pen, (position.X + 1 * cellPxSize) + cellPxSize + pen.Width,     //     RIGHT X
                                                 (position.Y + 1 * cellPxSize) + cellPxSize + pen.Width,     //     BOTTOM Y
                                                 (position.X + 1 * cellPxSize) - pen.Width,                  //     LEFT X
                                                 (position.Y + 1 * cellPxSize) + cellPxSize + pen.Width);    //     BOTTOM Y

            if (walls[1]) graphics.DrawLine(pen, (position.X + 1 * cellPxSize) - pen.Width,                  //     LEFT X
                                                 (position.Y + 1 * cellPxSize) - pen.Width,                  //     TOP Y
                                                 (position.X + 1 * cellPxSize) - pen.Width,                  //     LEFT X
                                                 (position.Y + 1 * cellPxSize) + cellPxSize + pen.Width);    //     BOTTOM Y
        }

    }

}
