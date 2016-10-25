using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TankWorld.Core;

namespace TankWorld
{
    public partial class GameForm : Form
    {
        Map theMap = new Map(24, 13);
        Flag blueFlag;
        IEnumerable<Common.MapPath> shortPaths;
        bool shortPathSearched = false;
        public GameForm()
        {
            InitializeComponent();

            theMap.CreateRandomBricks();
            blueFlag = new Flag(theMap.BlueFlagBlock.X, theMap.BlueFlagBlock.Y, theMap);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            //Red Headquarter
            e.Graphics.DrawImage(new Bitmap("Images/Flag_Red.png"), theMap.RedFlagBlock.X * 50, theMap.RedFlagBlock.Y * 50, 50, 50);
            //Blue Headquarter
            e.Graphics.DrawImage(new Bitmap("Images/Flag_Blue.png"), theMap.BlueFlagBlock.X * 50, theMap.BlueFlagBlock.Y * 50, 50, 50);

            //Tanks
            Bitmap redTankImage = new Bitmap("Images/Red_Tank.png");
            e.Graphics.DrawImage(redTankImage, theMap.BlockForRedTankA.X * 50, theMap.BlockForRedTankA.Y * 50, 50, 50);
            e.Graphics.DrawImage(redTankImage, theMap.BlockForRedTankB.X * 50, theMap.BlockForRedTankB.Y * 50, 50, 50);
            Bitmap tankImage = new Bitmap("Images/Tank.png");
            e.Graphics.DrawImage(tankImage, theMap.BlockForBlueTankA.X * 50, theMap.BlockForRedTankA.Y * 50, 50, 50);
            e.Graphics.DrawImage(tankImage, theMap.BlockForBlueTankB.X * 50, theMap.BlockForRedTankB.Y * 50, 50, 50);
            //Bricks
            Bitmap brickMap = new Bitmap("Images/Brick.bmp");
            for (int i = 0; i < theMap.Width; i++)
            {
                for (int j = 0; j < theMap.Height; j++)
                {
                    if (theMap.Blocks[i, j].Pattern == Pattern.Brick)
                    {
                        e.Graphics.DrawImage(brickMap, i * 50, j * 50, 50, 50);

                    }
                }
            }

            if (theMap.IsConnected(theMap.BlockForRedTankA, theMap.BlueFlagBlock))
            {
                e.Graphics.DrawString("connected", new Font("Times New Rome", 20, FontStyle.Bold), Brushes.White, 110, 110);
            }
            else
            {
                e.Graphics.DrawString("not connected", new Font("Times New Rome", 20, FontStyle.Bold), Brushes.White, 110, 110);
            }

            if (!shortPathSearched)
            {
                shortPaths = Common.ShortPathUtility.GetShortPaths(theMap.Blocks, theMap.BlockForRedTankA, theMap.BlueFlagBlock);
                shortPathSearched = true;
            }
            if (shortPaths.Count() > 0)
            {
                foreach (Common.MapPath path in shortPaths)
                {
                    var shortPathBlocks = path.Path.ToArray();
                    DrawPath(e.Graphics, shortPathBlocks);
                }
            }
            else
            {
                e.Graphics.DrawString("Sorry, it is not passable!", Font, Brushes.Red, 100, 100);
                e.Graphics.DrawString("Sorry, it is not passable!", new Font("Times New Rome", 20, FontStyle.Bold), Brushes.Green, 200, 100);
            }

            for (int i = 0; i < theMap.Width; i++)
            {
                for (int j = 0; j < theMap.Height; j++)
                {
                    if (blueFlag.Distances[i, j] != int.MaxValue)
                    {
                        e.Graphics.DrawString(blueFlag.Distances[i, j].ToString()+"", new Font("Times New Rome", 15, FontStyle.Bold), Brushes.Green, new Point(i * 50 + 5, j * 50 + 5));

                    }
                }
            }
        }

        private void DrawPath(Graphics e, params Common.IMapBlock[] blocks)
        {
            e.DrawLines(Pens.Black, blocks.Select(p => ((Block)p).GetCenter()).ToArray());
        }
    }
}
