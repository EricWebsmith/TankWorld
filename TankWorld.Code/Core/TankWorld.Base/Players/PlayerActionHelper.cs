using TankWorld.Common;

namespace TankWorld.Core
{
    /// <summary>
    /// Generates actions
    /// </summary>
    public static class PlayerActionHelper
    {
        public static PlayerAction GetTurnToAction(Tank tank, Block aBlock)
        {
            return GetTurnToAction(tank, aBlock.X, aBlock.Y);
        }

        public static PlayerAction GetTurnToAction(Tank tank, Tank enemyTank)
        {
            return GetTurnToAction(tank, enemyTank.X, enemyTank.Y);
        }

        public static PlayerAction GetTurnToAction(Tank tank, int x, int y)
        {
            PlayerAction action = new PlayerAction();
            action.Direction = DirectionHelper.GetDirection(tank.X, tank.Y, x, y);
            action.PlayerActionType = PlayerActionType.Turn;
            return action;
        }

        public static PlayerAction GetTurnToAction(Tank tank, Direction d)
        {
            PlayerAction action = new PlayerAction();
            action.Direction = d;
            action.PlayerActionType = PlayerActionType.Turn;
            return action;
        }

        public static PlayerAction GetForwardAction(Tank tank)
        {
            PlayerAction forwardAction = new PlayerAction();
            forwardAction.Direction = tank.Direction;
            forwardAction.PlayerActionType = PlayerActionType.Forward;
            return forwardAction;
        }

        public static PlayerAction GetAttackAction(Tank tank)
        {
            PlayerAction attackAction = new PlayerAction();
            attackAction.Direction = tank.Direction;
            attackAction.PlayerActionType = PlayerActionType.Attack;
            return attackAction;
        }
    }
}
