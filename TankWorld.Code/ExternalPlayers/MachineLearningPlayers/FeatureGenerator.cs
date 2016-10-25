using System;
using TankWorld.Common;
using TankWorld.Core;
using TankWorld.MachineLearning;

namespace TankWorld.MachineLearningPlayers
{
    class BlockProperties
    {
        public bool Passable { get; set; }
        public bool InDanger { get; set; }
        public bool UnderFire { get; set; }
        public bool GoesToFlag { get; set; }

        public void Assign(ref double[] arr, ref int i)
        {
            arr[i++] = Passable.ToInt();
            arr[i++] = InDanger.ToInt();
            arr[i++] = UnderFire.ToInt();
            arr[i++] = GoesToFlag.ToInt();
        }
    }

    public static class FeatureGenerator
    {
        public static int FeatureCount = 40;

        public static double[] CollectFeatures(Tank tank, Map map, IPlayer opponent)
        {
            double[] x = new double[FeatureCount];
            int i = 0;
            //Enemy in front
            x[i++] = TankHelper.EnemyInFront(tank, opponent.Tank1, opponent.Tank2, map).ToInt();

            //If a tank face one direction, it never turns to that direction.
            //Direction of the tank
            //A tank cannot turn to its direction.
            x[i++] = (tank.Direction == Direction.Up).ToInt();
            x[i++] = (tank.Direction == Direction.Right).ToInt();
            x[i++] = (tank.Direction == Direction.Down).ToInt();
            x[i++] = (tank.Direction == Direction.Left).ToInt();
            //Danger from and underfire

            {
                Tuple<bool, bool> dangerousAndUnderFire_up = TankHelper.GetIsInDangerAndUnderFire(tank.X, tank.Y, Common.Direction.Up, map, opponent.Tank1, opponent.Tank2);
                x[i++] = dangerousAndUnderFire_up.Item1.ToInt();
                x[i++] = dangerousAndUnderFire_up.Item2.ToInt();
            }
            {
                Tuple<bool, bool> dangerousAndUnderFire_right = TankHelper.GetIsInDangerAndUnderFire(tank.X, tank.Y, Common.Direction.Right, map, opponent.Tank1, opponent.Tank2);
                x[i++] = dangerousAndUnderFire_right.Item1.ToInt();
                x[i++] = dangerousAndUnderFire_right.Item2.ToInt();
            }
            {
                Tuple<bool, bool> dangerousAndUnderFire_down = TankHelper.GetIsInDangerAndUnderFire(tank.X, tank.Y, Common.Direction.Down, map, opponent.Tank1, opponent.Tank2);
                x[i++] = dangerousAndUnderFire_down.Item1.ToInt();
                x[i++] = dangerousAndUnderFire_down.Item2.ToInt();
            }
            {
                Tuple<bool, bool> dangerousAndUnderFire_left = TankHelper.GetIsInDangerAndUnderFire(tank.X, tank.Y, Common.Direction.Left, map, opponent.Tank1, opponent.Tank2);
                x[i++] = dangerousAndUnderFire_left.Item1.ToInt();
                x[i++] = dangerousAndUnderFire_left.Item2.ToInt();
            }

            //Neighbouring blocks properties
            //For turning
            var neighbourhood = map.GetNeighbours(tank.X, tank.Y);
            BlockProperties up_properties = GetProperties(tank, neighbourhood.Up, opponent, map);
            BlockProperties right_properties = GetProperties(tank, neighbourhood.Right, opponent, map);
            BlockProperties down_properties = GetProperties(tank, neighbourhood.Down, opponent, map);
            BlockProperties left_properties = GetProperties(tank, neighbourhood.Left, opponent, map);
            up_properties.Assign(ref x, ref i);
            right_properties.Assign(ref x, ref i);
            down_properties.Assign(ref x, ref i);
            left_properties.Assign(ref x, ref i);

            //Get Forward Block Properties
            //It is important, Indicates going forward or not.
            BlockProperties front_properties;// = GetNeighbourProperties(tank, neighbourhood.GetNeighbour(tank.Direction), opponent, map);
            switch (tank.Direction)
            {
                case Direction.Up:
                    front_properties = up_properties;
                    break;
                case Direction.Right:
                    front_properties = right_properties;
                    break;
                case Direction.Down:
                    front_properties = down_properties;
                    break;
                case Direction.Left:
                default:
                    front_properties = left_properties;
                    break;
            }
            front_properties.Assign(ref x, ref i);

            Block frontBlock = neighbourhood.GetNeighbour(tank.Direction);
            var frontNeighbourhood = map.GetNeighbours(frontBlock);
            BlockProperties front_front_properties = GetProperties(tank, neighbourhood.GetNeighbour(tank.Direction), opponent, map);
            front_front_properties.Assign(ref x, ref i);
            bool rush = front_properties.Passable && front_properties.InDanger && !front_properties.UnderFire
                && front_front_properties.Passable && !front_front_properties.InDanger;
            x[i++] = rush.ToInt();
            return x;
        }

        private static BlockProperties GetProperties(Tank tank, Block neighour, IPlayer opponent, Map map)
        {
            BlockProperties ps = new BlockProperties();
            if (neighour != null && neighour.Passable)
            {
                ps.Passable = true;
                Tuple<bool, bool> dangerousAndUnderFire = TankHelper.GetIsInDangerAndUnderFire(neighour, map, opponent.Tank1, opponent.Tank2);
                ps.InDanger = dangerousAndUnderFire.Item1;
                ps.UnderFire = dangerousAndUnderFire.Item2;
            }
            ps.GoesToFlag = opponent.Flag.GetIsRightWay(tank, neighour);
            return ps;
        }
    }
}
