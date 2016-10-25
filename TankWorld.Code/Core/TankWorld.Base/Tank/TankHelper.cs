using System;
using System.Collections.Generic;
using TankWorld.Common;

namespace TankWorld.Core
{
    public static class TankHelper
    {
        /// <summary>
        /// We assume the two tanks are in each other's sight.
        /// </summary>
        /// <param name="tank"></param>
        /// <param name="enemyTank1"></param>
        /// <returns></returns>
        public static bool GetIsUnderFire(Tank tank, Tank enemyTank)
        {
            return DirectionHelper.GetDirection(enemyTank.X, enemyTank.Y, tank.X, tank.Y) == enemyTank.Direction;
        }

        /// <summary>
        /// We assume the two tanks are in each other's sight.
        /// </summary>
        /// <param name="tank"></param>
        /// <param name="enemyTanks"></param>
        /// <returns></returns>
        public static bool GetIsUnderFire(int x, int y, Tank[] enemyTanks)
        {
            foreach (Tank enemyTank in enemyTanks)
            {
                if (DirectionHelper.GetDirection(enemyTank.X, enemyTank.Y, x, y) == enemyTank.Direction)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// We assume the two tanks are in each other's sight.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="enemyTank"></param>
        /// <returns></returns>
        public static bool GetIsUnderFire(int x, int y, Tank enemyTank)
        {
            return DirectionHelper.GetDirection(enemyTank.X, enemyTank.Y, x, y) == enemyTank.Direction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="d"></param>
        /// <param name="map"></param>
        /// <param name="enemy1"></param>
        /// <param name="enemy2"></param>
        /// <returns>Item1=Indanger, Item2=Underfire</returns>
        public static Tuple<bool, bool> GetIsInDangerAndUnderFire(int x, int y, Direction d, Map map, Tank enemy1, Tank enemy2)
        {
            if ((x != enemy1.X && x != enemy2.X) && (d==Direction.Up || d==Direction.Down))
            {
                return new Tuple<bool, bool>(false, false);
            }

            if ( (y != enemy1.Y && y != enemy2.Y) && (d == Direction.Right || d == Direction.Left))
            {
                return new Tuple<bool, bool>(false, false);
            }

            bool inDanger = false;
            bool underFire = false;
            var steppingFunc = DirectionHelper.GetSteppingFunc(d);
            int newX = x;
            int newY = y;
            while (map.GetIsPassable(newX, newY))
            {
                if (enemy1.X == newX && enemy1.Y == newY)
                {
                    inDanger = true;
                    underFire = d.GetIsOpposite(enemy1.Direction);
                }

                if (enemy2.X == newX && enemy2.Y == newY)
                {
                    inDanger = true;
                    underFire = d.GetIsOpposite(enemy1.Direction);
                }
                var newXY = steppingFunc(newX, newY);
                newX = newXY.X;
                newY = newXY.Y;
            }

            return new Tuple<bool, bool>(inDanger, underFire);
        }

        public static Tuple<bool, bool> GetIsInDangerAndUnderFire(IXY location, Map map, Tank enemy1, Tank enemy2)
        {
            return GetIsInDangerAndUnderFire(location.X, location.Y, map, enemy1, enemy2);

        }

        public static Tuple<bool, bool> GetIsInDangerAndUnderFire(int x, int y, Map map, Tank enemy1, Tank enemy2)
        {
            var up = GetIsInDangerAndUnderFire(x, y, Direction.Up, map, enemy1, enemy2);
            if (up.Item2) return new Tuple<bool, bool>(true, true);
            var right = GetIsInDangerAndUnderFire(x, y, Direction.Right, map, enemy1, enemy2);
            if (up.Item2) return new Tuple<bool, bool>(true, true);
            var down = GetIsInDangerAndUnderFire(x, y, Direction.Down, map, enemy1, enemy2);
            if (up.Item2) return new Tuple<bool, bool>(true, true);
            var left = GetIsInDangerAndUnderFire(x, y, Direction.Left, map, enemy1, enemy2);
            if (up.Item2) return new Tuple<bool, bool>(true, true);
            return new Tuple<bool, bool>(
                up.Item1 || right.Item1 || down.Item1 || left.Item1,
                up.Item2 || right.Item2 || down.Item2 || left.Item2
                );
        }

        public static Tank[] GetEnemyTanksInSight(Tank tank, Tank tank1, Tank tank2, Map map)
        {
            List<Tank> tanks = new List<Tank>();
            if (IsInSight(tank, tank1, map))
            {
                tanks.Add(tank1);
            }

            if (IsInSight(tank, tank2, map))
            {
                tanks.Add(tank2);
            }
            return tanks.ToArray();
        }

        public static Tank[] GetEnemyTanksInSight(int x, int y, Tank tank1, Tank tank2, Map map)
        {
            List<Tank> tanks = new List<Tank>();
            if (IsInSight(x, y, tank1, map))
            {
                tanks.Add(tank1);
            }

            if (IsInSight(x, y, tank2, map))
            {
                tanks.Add(tank2);
            }
            return tanks.ToArray();
        }

        public static bool IsInSight(Tank tank1, Tank tank2, Map map)
        {
            if (tank1.X == tank2.X || tank1.Y == tank2.Y)
            {
                return map.GetIsLineClear(tank1.X, tank1.Y, tank2.X, tank2.Y);
            }
            return false;
        }

        public static bool IsInSight(int x, int y, Tank tank2, Map map)
        {
            if (x == tank2.X || y == tank2.Y)
            {
                return map.GetIsLineClear(x, y, tank2.X, tank2.Y);
            }
            return false;
        }

        public static bool EnemyInFront(Tank tank, Tank enemyTank1, Tank enemyTank2, Map map)
        {
            return EnemyInFront(tank, enemyTank1, map) || EnemyInFront(tank, enemyTank2, map);
        }

        public static bool EnemyInFront(Tank tank, Tank enemyTank, Map map)
        {
            if (tank.Direction == Direction.Up && tank.X == enemyTank.X && tank.Y > enemyTank.Y)
            {
                return map.GetIsLineClear(tank.X, tank.Y, enemyTank.X, enemyTank.Y);
            }

            if (tank.Direction == Direction.Right && tank.X < enemyTank.X && tank.Y == enemyTank.Y)
            {
                return map.GetIsLineClear(tank.X, tank.Y, enemyTank.X, enemyTank.Y);
            }

            if (tank.Direction == Direction.Down && tank.X == enemyTank.X && tank.Y < enemyTank.Y)
            {
                return map.GetIsLineClear(tank.X, tank.Y, enemyTank.X, enemyTank.Y);
            }

            if (tank.Direction == Direction.Left && tank.X > enemyTank.X && tank.Y == enemyTank.Y)
            {
                return map.GetIsLineClear(tank.X, tank.Y, enemyTank.X, enemyTank.Y);
            }
            return false;
        }


        /// <summary>
        /// Suppose they are on each other's sight.
        /// </summary>
        /// <param name="tank"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool GetIsPointedBy(Tank t1, Tank t2)
        {
            return t2.Direction == DirectionHelper.GetDirection(t2.X, t2.Y, t1.X, t1.Y);
        }
    }
}
