using System.Drawing;
using TankWorld.Core;

namespace TankWorld
{
    public static class DrawingHelper
    {
        public static int BlockLength = 50;

        public static Point GetCenter(this Block theBlock)
        {
            return new Point(
                BlockLength * theBlock.X + BlockLength / 2,
                BlockLength * theBlock.Y + BlockLength / 2
                );
        }
    }
}
