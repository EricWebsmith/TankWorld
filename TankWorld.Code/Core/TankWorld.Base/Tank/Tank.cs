using System.Diagnostics;
using TankWorld.Common;

namespace TankWorld.Core
{
    [DebuggerDisplay("{Name} {X} {Y} Direction={Direction} alive={Alive}")]
    public class Tank:IXY
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public Direction Direction { get; internal set; }
        public bool Alive { get; internal set; }
        public IPlayer Owner { get; internal set; }

        /// <summary>
        /// Mainly for debug purpose.
        /// </summary>
        public string Name{get;internal set;}

        public Tank(string name,int x, int y, Direction direction, IPlayer owner)
        {
            this.Name = name;
            this.X = x;
            this.Y = y;
            this.Direction = direction;
            this.Alive = true;
            this.Owner = owner;
        }
    }
}
