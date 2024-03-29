﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace MazeGenerator
{
    public class Generator
    {
        bool working;
        int seekerPosX;
        int seekerPosY;
        public int size;
        public Cell[,] cell;
        Random random;
        public List<Point> visited = new List<Point>();

        public Generator(int size, bool live) {
            random = new Random();
            this.size = size;
            seekerPosX = 0;
            seekerPosY = 0;
            cell = new Cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cell[j, i] = new Cell();
                }
            }
            visited.Add(new Point(seekerPosX, seekerPosY));
            cell[seekerPosX, seekerPosY].visited = true;
            cell[seekerPosX, seekerPosY].walls[0] = false;
            //Direction();
        }
        ~Generator() {
            cell = null;
        }

        public void SetSeeker(Point point) {
            seekerPosX = point.X;
            seekerPosY = point.Y;
        }

        public bool GenerateNextStep() {
            List<Point> list = CheckNeighbors();
            if (list.Count > 0)
            {
                int val = random.Next(list.Count);
                RemoveWalls(new Point(seekerPosX, seekerPosY), list[val]);
                seekerPosX = list[val].X;
                seekerPosY = list[val].Y;
                cell[seekerPosX, seekerPosY].visited = true;
                visited.Add(new Point(seekerPosX, seekerPosY));
                return true;
            }
            else return false;
        }
        void RemoveWalls(Point origin, Point direction) {
            int moveX = direction.X - origin.X;
            int moveY = direction.Y - origin.Y;
            //MessageBox.Show(moveX + ":" + moveY);


            if (moveX > 0) {
                cell[origin.X, origin.Y].walls[1] = false;
                cell[direction.X, direction.Y].walls[3] = false;
            }
            else if (moveX < 0)
            {
                cell[origin.X, origin.Y].walls[3] = false;
                cell[direction.X, direction.Y].walls[1] = false;
            }
            else if (moveY > 0)
            {
                cell[origin.X, origin.Y].walls[2] = false;
                cell[direction.X, direction.Y].walls[0] = false;
            }
            else if (moveY < 0)
            {
                cell[origin.X, origin.Y].walls[0] = false;
                cell[direction.X, direction.Y].walls[2] = false;
            }
        }

        List<Point> CheckNeighbors() {

            List<Point> list = new List<Point>();
            if (seekerPosY > 0        &&    !cell[seekerPosX,     seekerPosY - 1].visited) list.Add(new Point(seekerPosX,     seekerPosY - 1));    //UP
            if (seekerPosX < size - 1 &&    !cell[seekerPosX + 1, seekerPosY    ].visited) list.Add(new Point(seekerPosX + 1, seekerPosY));        //RIGHT
            if (seekerPosY < size - 1 &&    !cell[seekerPosX,     seekerPosY + 1].visited) list.Add(new Point(seekerPosX,     seekerPosY + 1));    //DOWN
            if (seekerPosX > 0        &&    !cell[seekerPosX - 1, seekerPosY    ].visited) list.Add(new Point(seekerPosX - 1, seekerPosY));        //LEFT
            //MessageBox.Show(list.Count + "");
            return list;

        }

        public Bitmap Draw() {

            Bitmap bmp = new Bitmap((size + 2) * 10 + (size + 2) * 2, (size + 2) * 10 + (size + 2) * 2);     //Create Bitmap. 10px per cell. 2px per space between cells.
            Graphics graphics = Graphics.FromImage(bmp);
            Pen pen = new Pen(Color.Black, 1);
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, (size + 2) * 10 + (size + 2) * 2, (size + 2) * 10 + (size + 2) * 2); 
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cell[j, i].Draw(pen, graphics, new Point(j, i), 12);
                }
            }
            return bmp;
        }
    }

}
