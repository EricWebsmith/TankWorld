using TankWorld.Common;
using TankWorld.Core;

namespace TankWorld.GameManager
{
    [Player(DisplayName ="Flag Remover")]
    public class FlagRemovingPlayer : BasePlayer
    {
        //private Flag opponentFlag;
        //private List<Tank> opponentTanks;

        public override void Prepare()
        {
            base.Prepare();
            //opponentFlag = Opponent.Flag;
        }

        public override PlayerAction Play(Tank tank)
        {
            if (TankHelper.EnemyInFront(tank, Opponent.Tank1, Opponent.Tank2, map))
            {
                return PlayerActionHelper.GetAttackAction(tank);
            }

            PlayerAction pAction = new PlayerAction();
            Block currentBlock = map.Blocks[tank.X, tank.Y];
            

            BlockNeighbours neighbours = map.GetNeighbours(currentBlock);
            Block frontBlock = neighbours.GetNeighbour(tank.Direction);
            Block[] nextDistinations = Opponent.Flag.GetCloser(tank.X, tank.Y);

            //If moving forward makes the tank closer to enemy flag, move forward.
            if (frontBlock!=null && frontBlock.Pattern == Pattern.Clear)
            {
                foreach (Block b in nextDistinations)
                {
                    if (b == frontBlock)
                    {
                        pAction.PlayerActionType = PlayerActionType.Forward;
                        pAction.Direction = tank.Direction;
                        return pAction;
                    }
                }
            }

            if (Opponent.Flag.Captured)
            {
                pAction.PlayerActionType = PlayerActionType.Turn;
                return pAction;
            }

            //Turn toward one of the next distinations.
            pAction.Direction = currentBlock.GetDirection(nextDistinations[0]);
            pAction.PlayerActionType = PlayerActionType.Turn;
            return pAction;
        }
    }
}