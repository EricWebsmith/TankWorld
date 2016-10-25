using System.Collections.Generic;
using System.Linq;
using TankWorld.Core;

namespace TankWorld.ProgrammedPlayers
{
    [Player(DisplayName = "Programmed Attacker")]
    public class ProgrammedAttackingPlayer : BasePlayer
    {
        //public Analysis analysis = new Analysis();

        private Tank tank = null;

        public override PlayerAction Play(Tank tank)
        {
            this.tank = tank;
            //if there is an enemy, fire.;
            if (TankHelper.EnemyInFront(tank, Opponent.Tank1, Opponent.Tank2, map))
            {
                return PlayerActionHelper.GetAttackAction(tank);
            }
            var neighbourhood = map.GetNeighbours(tank.X, tank.Y);
            Tank[] enemiesInSight = TankHelper.GetEnemyTanksInSight(tank, Opponent.Tank1, Opponent.Tank2, map);

            bool isUnderFire = false;
            if (enemiesInSight.Length > 0)
            {
                foreach (Tank t in enemiesInSight)
                {
                    if (TankHelper.GetIsUnderFire(tank, t))
                    {
                        isUnderFire = true;
                        if (TankHelper.GetIsPointedBy(tank, t))
                        {
                            return PlayerActionHelper.GetTurnToAction(tank, t);
                        }

                    }
                }
                //it is under fire but the enemy is not at back
                //then, go forward anyway, even if going forward leads to danger.
                if (isUnderFire)
                {
                    return PlayerActionHelper.GetForwardAction(tank);
                }

                //if enemy is in sight but not pointing at me, 
                //I turn and point at the enemy
                PlayerActionHelper.GetTurnToAction(tank, enemiesInSight.First());
            }

            //Get the neighbouring blocks leading to enemy headquarter
            //Make the distance = distance - 1
            
            var neighbours = neighbourhood.Neighbours;
            Block frontBlock = neighbourhood.GetNeighbour(tank.Direction);
            //distance = distance - 1
            List<Block> nextDistinations = Opponent.Flag.GetCloser(tank.X, tank.Y).ToList();
            //Go forward if frontblock makes the distance=distance-1
            if (nextDistinations.Contains(frontBlock))
            {
                var frontInsightTanks = TankHelper.GetEnemyTanksInSight(frontBlock.X, frontBlock.Y, Opponent.Tank1, Opponent.Tank2, map);
                if (frontInsightTanks.Length == 0)
                {
                    return PlayerActionHelper.GetForwardAction(tank);
                }


                //if the next block is insight 
                //and the next next block is clear and is not insight.
                //the tank can still get away from the enemy
                if (!TankHelper.GetIsUnderFire(frontBlock.X, frontBlock.Y, frontInsightTanks))
                {
                    var frontNeighbourhood = map.GetNeighbours(frontBlock);
                    var frontFrontBlock = frontNeighbourhood.GetNeighbour(tank.Direction);

                    if (frontFrontBlock != null && frontFrontBlock.Passable)
                    {
                        var frontFrontInsightTanks = TankHelper.GetEnemyTanksInSight(frontFrontBlock.X, frontFrontBlock.Y, Opponent.Tank1, Opponent.Tank2, map);
                        if (frontFrontInsightTanks.Length == 0)
                        {
                            return PlayerActionHelper.GetForwardAction(tank);
                        }
                    }
                }
                nextDistinations.Remove(frontBlock);
            }
            //long randomNumber = DateTime.Now.Ticks;
            if (nextDistinations.Count != 0)
            {
                Block selectedBlock = nextDistinations[0];
                return PlayerActionHelper.GetTurnToAction(tank, selectedBlock);
            }

            //if nextDistinations.Count==0
            Block selectedBlock2 = neighbourhood.Neighbours[0];
            return PlayerActionHelper.GetTurnToAction(tank, selectedBlock2);

        }
    }
}
