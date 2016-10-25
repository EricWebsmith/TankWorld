using System.Collections.Generic;
using System.Linq;
using TankWorld.Common;
using TankWorld.Core;

namespace TankWorld.GameManager
{
    [Player(DisplayName = "Wandering Attacher")]
    public class WanderingPlayer : BasePlayer
    {

        class MyWanderingTank
        {

            Tank Tank { get; set; }

            public bool Turned { get; set; }
            public int Number { get; set; }

            public MyWanderingTank(Tank tank, int number)
            {
                Tank = tank;
                Number = number;
                tanks.Add(this);
            }

            static List<MyWanderingTank> tanks = new List<MyWanderingTank>();
            public static MyWanderingTank GetMyTank(Tank tank)
            {
                return tanks.Where(t => t.Tank == tank).First();
            }
        }

        public override void Prepare()
        {
            base.Prepare();
            MyWanderingTank myTank1 = new MyWanderingTank(Tank1, 1);
            MyWanderingTank myTank2 = new MyWanderingTank(Tank2, 2);
        }

        public override PlayerAction Play(Tank tank)
        {
            if (TankHelper.EnemyInFront(tank, Opponent.Tank1, Opponent.Tank2, map))
            {
                return PlayerActionHelper.GetAttackAction(tank);
            }

            MyWanderingTank myTank = MyWanderingTank.GetMyTank(tank);
            PlayerAction pAction = new PlayerAction();
            Block currentBlock = map.Blocks[tank.X, tank.Y];
            BlockNeighbours neighbours = map.GetNeighbours(currentBlock);
            Block frontBlock = neighbours.GetNeighbour(tank.Direction);

            if (myTank.Turned)
            {
                pAction.PlayerActionType = PlayerActionType.Forward;
                pAction.Direction = tank.Direction;
                myTank.Turned = false;
                return pAction;
            }

            if (MathHelper.GetRandomNumber(0, 3) != 0 && frontBlock != null && frontBlock.Pattern == Pattern.Clear)
            {
                pAction.PlayerActionType = PlayerActionType.Forward;
                pAction.Direction = tank.Direction;
                myTank.Turned = false;
                return pAction;
            }

            //Turn
            int randomNumber = MathHelper.GetRandomNumber(0, 100) + myTank.Number;// % 4;
            var myNeighbours = map.GetNeighbours(currentBlock);
            var passableBlocks = myNeighbours.Neighbours.Where(c => c.Passable && c != frontBlock).ToList();

            myTank.Turned = true;
            var passableBlocksCount = passableBlocks.Count();
            if (passableBlocksCount == 0)
            {
                pAction.PlayerActionType = PlayerActionType.Forward;
                pAction.Direction = tank.Direction;
                myTank.Turned = false;
                return pAction;
            }
            var index = randomNumber % passableBlocksCount;
            var nextBlock = passableBlocks[index];
            pAction.Direction = DirectionHelper.GetDirection(currentBlock.X, currentBlock.Y, nextBlock.X, nextBlock.Y);
            pAction.PlayerActionType = PlayerActionType.Turn;
            return pAction;
        }
    }
}