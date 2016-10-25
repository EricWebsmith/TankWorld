using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using TankWorld.Core;
using TankWorld.GameManager;
using TankWorld.MachineLearningPlayers;
using TankWorld.ProgrammedPlayers;

namespace TankWorld.Test
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void GameSmockTest()
        {
            GameManager.GameManager gm = new GameManager.GameManager(new WanderingPlayer(), new WanderingPlayer());
            gm.Play();
        }

        private BatchMatchResult PlayGame(IPlayer player1, IPlayer player2, int playTimes = 100)
        {
            BatchMatchResult bResult = new BatchMatchResult();
            bResult.Total = playTimes;
            for (int i = 0; i < playTimes; i = i + 2)
            {
                GameManager.GameManager gm1 = new GameManager.GameManager(player1, player2, 9, 9);
                var result1 = gm1.Play();
                //Switch Positions
                GameManager.GameManager gm2 = new GameManager.GameManager(player2, player1, 9, 9);
                var result2 = gm2.Play();

                switch (result1)
                {
                    case Core.MatchResult.Player1Won:
                        bResult.Player1Won++;
                        break;
                    case Core.MatchResult.Player2Won:
                        bResult.Player2Won++;
                        break;
                    case Core.MatchResult.Tie:
                        bResult.Tie++;
                        break;
                }

                switch (result2)
                {
                    case Core.MatchResult.Player1Won:
                        bResult.Player2Won++;
                        break;
                    case Core.MatchResult.Player2Won:
                        bResult.Player1Won++;
                        break;
                    case Core.MatchResult.Tie:
                        bResult.Tie++;
                        break;
                }
            }
            Debug.Print("Result:");
            Debug.Print("Player 1 Red Won:" + bResult.Player1Won.ToString());
            Debug.Print("Player 2 Blue Won:" + bResult.Player2Won.ToString());
            Debug.Print("Tie:" + bResult.Tie.ToString());
            return bResult;
        }

        #region Wandering
        [TestMethod]
        public void FlagWanderingPlayerTest()
        {
            PlayGame(new WanderingPlayer(), new WanderingPlayer());
        }
        #endregion

        #region FlagRemovingPlayer
        [TestMethod]
        public void FlagRemoverTest()
        {
            PlayGame(new FlagRemovingPlayer(), new FlagRemovingPlayer());
        }

        [TestMethod]
        public void FlagRemoverVsWandererTest()
        {
            var result = PlayGame(new FlagRemovingPlayer(), new WanderingPlayer());
            Assert.IsTrue(result.Player1Won > result.Player2Won);
        }
        #endregion

        #region ProgrammedPlayer
        [TestMethod]
        public void ProgrammedPlayerTest()
        {
            var result = PlayGame(new ProgrammedAttackingPlayer(), new ProgrammedAttackingPlayer());
        }

        [TestMethod]
        public void ProgrammedPlayerVsWanderingPlayerTest()
        {
            var result = PlayGame(new ProgrammedAttackingPlayer(), new WanderingPlayer());
            Assert.IsTrue(result.Player1Won > result.Player2Won);
        }

        [TestMethod]
        public void ProgrammedPlayerVsFlagRemoverTest()
        {
            var result = PlayGame(new ProgrammedAttackingPlayer(), new FlagRemovingPlayer());
            Assert.IsTrue(result.Player1Won > result.Player2Won);
        }
        #endregion

        #region Machine Learning
        [TestMethod]
        public void MachineLearningPlayerVsWandererTest()
        {
            var result = PlayGame(new LogisticRegressionPlayer(), new WanderingPlayer());
            Assert.IsTrue(result.Player1Won > result.Player2Won);
        }

        [TestMethod]
        public void MachineLearningPlayerVsFlagRemoverTest()
        {
            var result = PlayGame(new LogisticRegressionPlayer(), new FlagRemovingPlayer());
            Assert.IsTrue(result.Player1Won > result.Player2Won);
        }

        [TestMethod]
        public void MachineLearningPlayerVsProgrammedAttackingPlayerTest()
        {
            var result = PlayGame(new LogisticRegressionPlayer(), new ProgrammedAttackingPlayer());
        }

        [TestMethod]
        public void ML_vs_ML_test()
        {
            var result = PlayGame(new LogisticRegressionPlayer(), new LogisticRegressionPlayer());
        }
        #endregion

        #region NeuralNetwork
        [TestMethod]
        public void NN_vs_NN_test()
        {
            var result = PlayGame(new NeuralNetworkPlayer(), new NeuralNetworkPlayer());
        }

        [TestMethod]
        public void NN_vs_Logistic_test()
        {
            var result = PlayGame(new NeuralNetworkPlayer(), new LogisticRegressionPlayer());
        }

        [TestMethod]
        public void NN_vs_Programmed_test()
        {
            var result = PlayGame(new NeuralNetworkPlayer(), new ProgrammedAttackingPlayer());
        }

        [TestMethod]
        public void NN_vs_FlagRemover_test()
        {
            var result = PlayGame(new NeuralNetworkPlayer(), new FlagRemovingPlayer());
        }

        [TestMethod]
        public void NN_vs_Wandering_test()
        {
            var result = PlayGame(new NeuralNetworkPlayer(), new WanderingPlayer());
        }
        #endregion
    }
}
