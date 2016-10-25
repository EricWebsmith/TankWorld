using TankWorld.Common;

namespace TankWorld.Core
{
    public class PlayerAction
    {
        public Direction Direction { get; set; }
        /// <summary>
        /// An action can be Forward, Turn or Attack
        /// </summary>
        public PlayerActionType PlayerActionType { get; set; }
    }
}
