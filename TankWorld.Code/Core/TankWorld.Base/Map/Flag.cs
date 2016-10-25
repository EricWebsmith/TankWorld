using System.Collections.Generic;
using System.Linq;
using TankWorld.Common;

namespace TankWorld.Core
{
    /// <summary>
    /// The flag or the headquarter of the player.
    /// </summary>
    public class Flag
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        /// <summary>
        /// Two-way bound.
        /// </summary>
        public IPlayer Owner { get; set; }

        public bool Captured { get; internal set; }

        public int[,] Distances { get; set; }

        private Map map;
        private Block myBlock;

        public Flag(int x, int y, Map map)
        {
            this.map = map;
            myBlock = map.Blocks[x, y];
            this.X = x;
            this.Y = y;

            Captured = false;
            FreshDistance();
        }

        private void FreshDistance()
        {
            Distances = new int[map.Width, map.Height];

            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    Distances[i, j] = int.MaxValue;
                }
            }
            Distances[X, Y] = 0;

            BlockNeighbours neighbourhood = map.GetNeighbours(myBlock);
            var allneighbours = neighbourhood.Neighbours;
            int currentDistance = 0;
            Block[] validNeighbours =(from b in allneighbours where b.Passable && Distances[b.X, b.Y]> (currentDistance+1) select b).ToArray();
            while(validNeighbours.Length>0)
            {
                List<Block> newNeighbours = new List<Block>();
                foreach(Block neighbour in validNeighbours)
                {
                    Distances[neighbour.X, neighbour.Y] = currentDistance+1;
                    //newNeighbours.AddRange();
                    foreach(Block newNeighbour in map.GetNeighbours(neighbour).Neighbours)
                    {
                        if(!newNeighbours.Contains(newNeighbour))
                        {
                            newNeighbours.Add(newNeighbour);
                        }
                    }
                }
                validNeighbours = newNeighbours.Where(b => Distances[b.X, b.Y] > currentDistance + 1 && b.Passable).ToArray();


                currentDistance++;
            }
        }

        public Block[] GetCloser(int x, int y)
        {
            var neighbourhood = map.GetNeighbours(x,y);
            var oldDistance = Distances[x, y];
            return neighbourhood.Neighbours.Where(b=>Distances[b.X,b.Y]== oldDistance-1).ToArray();
        }

        public bool GetIsRightWay(IXY xy1, IXY xy2)
        {
            if (xy1 == null) return false;
            if (xy2 == null) return false;
            return Distances[xy1.X, xy1.Y] - Distances[xy2.X, xy2.Y] == 1;
        }
    }
}