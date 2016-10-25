/* ***************************************
The block class
a map is consisted with several blocks.
a block is a square area.
the position of a block in a map is denoted as x and y.
*************************************** */
namespace TankWorld.Core
{
    public class Block: Common.IMapBlock
    {
        public int X { get;  set; }
        public int Y { get;  set; }
        /// <summary>
        /// Pattern can be clear or brick.
        /// in the future, other patterns can be added.
        /// Like water, grass and etc.
        /// </summary>
        public Pattern Pattern { get; internal set; }
        public bool IsHeadQuarder { get;  set; }
        public bool IsTankGate { get; set; }
        public bool Preserved { get { return (IsHeadQuarder || IsTankGate); } }

        public bool Passable
        {
            get
            {
                if (Pattern == Pattern.Clear) return true;
                return false;
            }
            set
            {
                
            }
        }

        public int Distance {  get; set; }

        public Block(int x,int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

}
