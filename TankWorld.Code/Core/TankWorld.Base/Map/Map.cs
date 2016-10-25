using System;
using TankWorld.Common;
using TankWorld.UnionFind;

namespace TankWorld.Core
{
    /// <summary>
    /// the abstract map, no for drawing.
    /// The map contains m*n squares.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// The squares in the map. It is a m by n matrix.
        /// </summary>
        public Block[,] Blocks { get; set; }
        public Block RedFlagBlock { get; internal set; }
        public Block BlueFlagBlock { get; internal set; }
        public Block BlockForRedTankA { get; internal set; }
        public Block BlockForRedTankB { get; internal set; }
        public Block BlockForBlueTankA { get; internal set; }
        public Block BlockForBlueTankB { get; internal set; }
        public int Width { get; internal set; }

        /// <summary>
        /// The neighbours of a block
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public BlockNeighbours GetNeighbours(int x, int y)
        {
            return GetNeighbours(Blocks[x, y]);
        }

        /// <summary>
        /// The neighbours of a block
        /// </summary>
        /// <param name="currentBlock"></param>
        /// <returns></returns>
        public BlockNeighbours GetNeighbours(Block currentBlock)
        {
            BlockNeighbours neighbours = new BlockNeighbours();
            if(currentBlock==null)
            {
                return neighbours;
            }
            if (currentBlock.Y != 0)
            {
                neighbours.Up = Blocks[currentBlock.X, currentBlock.Y - 1];
            }

            if (currentBlock.X != Width - 1)
            {
                neighbours.Right = Blocks[currentBlock.X + 1, currentBlock.Y];
            }

            if (currentBlock.Y != Height - 1)
            {
                neighbours.Down = Blocks[currentBlock.X, currentBlock.Y + 1];
            }

            if (currentBlock.X != 0)
            {
                neighbours.Left = Blocks[currentBlock.X - 1, currentBlock.Y];
            }
            return neighbours;
        }

        public int Height { get; internal set; }

        /// <summary>
        /// The unin-find algorithm, used to check it the map is passable.
        /// </summary>
        public Percolation mapPercolation;

        public Map(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            CreateFlagAndTanks();
            CreateRandomBricks();
            while (!IsConnected(RedFlagBlock, BlueFlagBlock))
            {
                CreateFlagAndTanks();
                CreateRandomBricks();
            }
        }

        public void CreateRandomBricks()
        {
            mapPercolation = new Percolation(Width, Height);
            Random r = new Random();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {

                    if (Blocks[i, j].Preserved)
                    {
                        mapPercolation.Open(i, j);
                        continue;
                    }
                    Blocks[i, j].Pattern = Pattern.Clear;
                    int randomNumber = r.Next(1, 100);
                    if (randomNumber % 3 == 0)
                    {
                        Blocks[i, j].Pattern = Pattern.Brick;
                    }
                    else
                    {
                        mapPercolation.Open(i, j);
                    }
                }
            }
        }

        public bool IsConnected(Block blockA, Block blockB)
        {
            return mapPercolation.Connected(blockA.X, blockA.Y, blockB.X, blockB.Y);
        }

        public bool IsOpen(int x, int y)
        {
            return mapPercolation.isOpen(x, y);
        }

        internal void CreateFlagAndTanks()
        {
            Blocks = new Block[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Blocks[i, j] = new Block(i, j);
                }
            }

            RedFlagBlock = Blocks[0, Height / 2];
            RedFlagBlock.IsHeadQuarder = true;

            BlockForRedTankA = Blocks[0, Height / 2 - 1];
            BlockForRedTankA.IsTankGate = true;
            BlockForRedTankB = Blocks[0, Height / 2 + 1];
            BlockForRedTankB.IsTankGate = true;

            BlueFlagBlock = Blocks[Width - 1, Height / 2];
            BlueFlagBlock.IsHeadQuarder = true;

            BlockForBlueTankA = Blocks[Width - 1, Height / 2 - 1];
            BlockForBlueTankA.IsTankGate = true;
            BlockForBlueTankB = Blocks[Width - 1, Height / 2 + 1];
            BlockForBlueTankB.IsTankGate = true;

        }

        public Block GetFrontBlock(int x, int y, Direction direction)
        {
            //if the front block is out of the map
            if ((x == 0 && direction == Direction.Left)
                || (x == Width - 1 && direction == Direction.Right)
                || (y == 0 && direction == Direction.Up)
                || (y == Height - 1 && direction == Direction.Down))
            {
                throw new Exception("The location is not in the map.");
            }

            switch (direction)
            {
                case Direction.Up:
                    return Blocks[x, y - 1];
                case Direction.Right:
                    return Blocks[x + 1, y];
                case Direction.Down:
                    return Blocks[x, y + 1];
                case Direction.Left:
                    return Blocks[x - 1, y];
                default:
                    throw new NotImplementedException();
            }
        }

        public bool GetIsPassable(int x, int y)
        {
            if (!GetIsValidate(x, y))
            {
                return false;
            }
            return Blocks[x, y].Passable;
        }

        public bool GetIsValidate(int x, int y)
        {
            if (x < 0 || x >= Width) { return false; }
            if (y < 0 || y >= Height) { return false; }
            return true;
        }

        public bool GetIsLineClear(int x1, int y1, int x2, int y2)
        {
            //vertical line
            if (x1 == x2)
            {
                int yMin = Math.Min(y1, y2);
                int yMax = Math.Max(y1, y2);
                for (int y = yMin; y <= yMax; y++)
                {
                    if (!Blocks[x1, y].Passable)
                    {
                        return false;
                    }
                }
            }

            //horizonal line
            if (y1 == y2)
            {
                int xMin = Math.Min(x1, x2);
                int xMax = Math.Max(x1, x2);
                for (int x = xMin; x <= xMax; x++)
                {
                    if (!Blocks[x, y1].Passable)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
