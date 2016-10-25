/////////////////////////////////////////////////
//This class controls and judges the game
//For each match, we create a new GameManager.
/////////////////////////////////////////////////
using System;
using TankWorld.Core;
using TankWorld.Common;

namespace TankWorld.GameManager
{
    public class PlayerPlayedEventArgs : EventArgs
    {
        public IPlayer Player { get; set; }
        public Tank Tank { get; set; }
        public PlayerAction PlayerAction { get; set; }
    }

    public class RoundPlayedEventArgs : EventArgs
    {
        public int Round { get; set; }
        public bool Wait { get; set; }
    }

    public class GameManager
    {
        public Map map { get; set; }
        public Tank redTank1 { get; set; }
        public Tank redTank2 { get; set; }
        public Tank blueTank1 { get; set; }
        public Tank blueTank2 { get; set; }
        public Tank[] AllTanks { get; private set; }
        Flag redFlag;
        Flag blueFlag;
        IPlayer redPlayer;
        IPlayer bluePlayer;

        public event EventHandler<PlayerPlayedEventArgs> PlayerPlayed;
        public event EventHandler<RoundPlayedEventArgs> RoundPlayed;

        public GameManager(IPlayer player1, IPlayer player2, int width = 15, int height = 15)
        {
            map = new Map(width, height);
            map.CreateFlagAndTanks();
            map.CreateRandomBricks();

            while (!map.IsConnected(map.RedFlagBlock, map.BlueFlagBlock))
            {
                map = new Map(width, height);
                map.CreateFlagAndTanks();
                map.CreateRandomBricks();
            }

            redPlayer = player1;
            bluePlayer = player2;

            redTank1 = new Tank("Player 1 Tank 1", map.BlockForRedTankA.X, map.BlockForRedTankA.Y, Direction.Right, null);
            redTank2 = new Tank("Player 1 Tank 2", map.BlockForRedTankB.X, map.BlockForRedTankB.Y, Direction.Right, null);

            redFlag = new Flag(map.RedFlagBlock.X, map.RedFlagBlock.Y, map);
            //redPlayer = new FlagRemovingPlayer();
            redPlayer.ShowMap(map);
            redPlayer.Assign(redFlag, redTank1, redTank2);

            blueTank1 = new Tank("Player 2 Tank 1", map.BlockForBlueTankA.X, map.BlockForBlueTankA.Y, Direction.Left, null);
            blueTank2 = new Tank("Player 2 Tank 2", map.BlockForBlueTankB.X, map.BlockForBlueTankB.Y, Direction.Left, null);
            blueFlag = new Flag(map.BlueFlagBlock.X, map.BlueFlagBlock.Y, map);
            //bluePlayer = new WonderingPlayer();
            bluePlayer.ShowMap(map);
            bluePlayer.Assign(blueFlag, blueTank1, blueTank2);

            redPlayer.HandShake(bluePlayer);
            bluePlayer.HandShake(redPlayer);

            redPlayer.Prepare();
            bluePlayer.Prepare();
            AllTanks = new Tank[] { redTank1, redTank2, blueTank1, blueTank2 };
        }

        /// <summary>
        /// The main function
        /// </summary>
        /// <returns></returns>
        public MatchResult Play()
        {
            for (int roundIndex = 1; roundIndex <= 2000; roundIndex++)
            {
                int stepIndex = 1;
                Step(roundIndex, stepIndex++, redTank1, redPlayer);
                Step(roundIndex, stepIndex++, blueTank1, bluePlayer);
                Step(roundIndex, stepIndex++, redTank2, redPlayer);
                Step(roundIndex, stepIndex++, blueTank2, bluePlayer);
                MatchResult result = UpdateResult();
                if (result != MatchResult.UnGoing)
                {
                    return result;
                }
            }
            return MatchResult.Tie;
        }

        public MatchResult PlayRound(int roundIndex)
        {
            int stepIndex = 1;
            Step(roundIndex, stepIndex++, redTank1, redPlayer);
            Step(roundIndex, stepIndex++, blueTank1, bluePlayer);
            Step(roundIndex, stepIndex++, redTank2, redPlayer);
            Step(roundIndex, stepIndex++, blueTank2, bluePlayer);
            if (RoundPlayed != null)
            {
                RoundPlayedEventArgs args = new RoundPlayedEventArgs();
                RoundPlayed(this, args);
            }
            return UpdateResult();
        }

        private void Step(int roundNumber, int stepNumber, Tank tank, IPlayer player)
        {
            if (tank.Alive)
            {
                PlayerAction tankAction = player.Play(tank);
                EventHandler<PlayerPlayedEventArgs> handler = PlayerPlayed;
                if (handler != null)
                {
                    PlayerPlayedEventArgs args = new PlayerPlayedEventArgs();
                    args.Player = player;
                    args.Tank = tank;
                    args.PlayerAction = tankAction;
                    handler(this, args);
                }
                UpdateAction(tank, tankAction);
                Log(roundNumber, stepNumber, player,tank, tankAction);
            }
        }

        private void Log(int roundNumber, int stepNumber,IPlayer player, Tank tank, PlayerAction tankAction)
        {
            //Log record = new Log();
            //record.Round = roundNumber;
            //record.Step = stepNumber;
            //record.Action = tankAction.PlayerActionType.ToString() + "-" + tankAction.Direction.ToString();
            //Ezfx.Csv.CsvContext.WriteFile<Log>("record."+DateTime.Now.ToString("yyyyMMddHHmmss")+".csv",)
        }

        private MatchResult UpdateResult()
        {
            //Tie
            if (!redPlayer.Tank1.Alive && !redPlayer.Tank2.Alive && !bluePlayer.Tank1.Alive && !bluePlayer.Tank2.Alive)
            {
                return MatchResult.Tie;
            }

            if (redPlayer.Flag.Captured && bluePlayer.Flag.Captured)
            {
                return MatchResult.Tie;
            }

            //Either side wins
            if (!redPlayer.Tank1.Alive && !redPlayer.Tank2.Alive)
            {
                return MatchResult.Player2Won;
            }

            if (!bluePlayer.Tank1.Alive && !bluePlayer.Tank2.Alive)
            {
                return MatchResult.Player1Won;
            }

            if (redPlayer.Flag.Captured)
            {
                return MatchResult.Player2Won;
            }

            if (bluePlayer.Flag.Captured)
            {
                return MatchResult.Player1Won;
            }

            return MatchResult.UnGoing;
        }

        private void UpdateAction(Tank theTank, PlayerAction theAction)
        {
            switch (theAction.PlayerActionType)
            {
                case PlayerActionType.Attack:
                    //Find the tank or brick which suffers the tank attack
                    UpdateAttack(theTank, theAction);
                    break;
                case PlayerActionType.Forward:
                    Block frontBlock = map.GetFrontBlock(theTank.X, theTank.Y, theTank.Direction);
                    if (frontBlock.Passable)
                    {
                        theTank.X = frontBlock.X;
                        theTank.Y = frontBlock.Y;
                    }
                    var opponentFlag = theTank.Owner.Opponent.Flag;
                    if (theTank.X == opponentFlag.X && theTank.Y == opponentFlag.Y)
                    {
                        opponentFlag.Captured = true;
                    }
                    break;
                case PlayerActionType.Turn:
                    theTank.Direction = theAction.Direction;
                    break;
            }
        }

        private void UpdateAttack(Tank theTank, PlayerAction theAction)
        {
            var steppingFunc = DirectionHelper.GetSteppingFunc(theTank.Direction);
            bool hit = false;
            var newXY1 = steppingFunc(theTank.X, theTank.Y);
            int x = newXY1.X;
            int y = newXY1.Y;
            while (map.GetIsPassable(x, y) && !hit)
            {
                foreach (Tank t in AllTanks)
                {
                    if (t.X == x && t.Y == y)
                    {
                        t.Alive = false;
                        t.X = -1;
                        t.Y = -1;
                        hit = true;
                    }
                }
                var newXY = steppingFunc(x, y);
                x = newXY.X;
                y = newXY.Y;
            }
        }
    }
}
