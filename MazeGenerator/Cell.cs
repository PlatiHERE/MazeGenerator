using System.Drawing;

namespace MazeGenerator
{
    public class Cell
    {
        public bool[] walls = { true, true, true, true };   // top, right, bottom, left
        public bool visited = false;
        public bool secondTry = false;          // goes true after setting seeker at this cell again
        public Cell() {

        }

        public void Draw(Pen pen, Graphics graphics, Point position, int cellPxSize) {
            SolidBrush brush = new SolidBrush(Color.White);
            if (visited) graphics.FillRectangle(brush, ((position.X + 1) * cellPxSize), ((position.Y + 1) * cellPxSize), cellPxSize, cellPxSize);

            if (walls[0]) graphics.DrawLine(pen, ((position.X + 1) * cellPxSize),                  //     LEFT X
                                                 ((position.Y + 1) * cellPxSize),                  //     TOP Y
                                                 ((position.X + 1) * cellPxSize) + cellPxSize,     //     RIGHT X
                                                 ((position.Y + 1) * cellPxSize));                 //     TOP Y

            if (walls[1]) graphics.DrawLine(pen, ((position.X + 1) * cellPxSize) + cellPxSize,     //     RIGHT X
                                                 ((position.Y + 1) * cellPxSize),                  //     TOP Y
                                                 ((position.X + 1) * cellPxSize) + cellPxSize,     //     RIGHT X
                                                 ((position.Y + 1) * cellPxSize) + cellPxSize);    //     BOTTOM Y

            if (walls[2]) graphics.DrawLine(pen, ((position.X + 1) * cellPxSize) + cellPxSize,     //     RIGHT X
                                                 ((position.Y + 1) * cellPxSize) + cellPxSize,     //     BOTTOM Y
                                                 ((position.X + 1) * cellPxSize),                  //     LEFT X
                                                 ((position.Y + 1) * cellPxSize) + cellPxSize);    //     BOTTOM Y

            if (walls[3]) graphics.DrawLine(pen, ((position.X + 1) * cellPxSize),                  //     LEFT X
                                                 ((position.Y + 1) * cellPxSize),                  //     TOP Y
                                                 ((position.X + 1) * cellPxSize),                  //     LEFT X
                                                 ((position.Y + 1) * cellPxSize) + cellPxSize);    //     BOTTOM Y
            
            //MessageBox.Show((position.X + 1 * cellPxSize) - pen.Width + " : " + ((position.Y + 1 * cellPxSize) - pen.Width));
        }

    }

}
