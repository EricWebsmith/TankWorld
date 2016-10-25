using System.Collections.Generic;
using TankWorld.Common;

namespace TankWorld.Core
{
    public class BlockNeighbours
    {
        public Block Up { get; set; }
        public Block Right { get; set; }
        public Block Down { get; set; }
        public Block Left { get; set; }

        public List<Block> Neighbours
        {
            get
            {
                List<Block> returnValue = new List<Block>();
                if (Up != null) returnValue.Add(Up);
                if (Right != null) returnValue.Add(Right);
                if (Down != null) returnValue.Add(Down);
                if (Left != null) returnValue.Add(Left);

                return returnValue;
            }
        }

        public Block GetNeighbour(Direction direction)
        {
            switch(direction)
            {
                case Direction.Up:
                    return Up;
                case Direction.Right:
                    return Right;
                case Direction.Down:
                    return Down;
                case Direction.Left:
                    return Left;
            }
            return null;
        }
    }
}
