using System.Collections.Generic;
using TankWorld.Core;
using TankWorld.MachineLearning.Classification;
using TankWorld.MachineLearningPlayers;
using TankWorld.ProgrammedPlayers;

namespace TankWorld.MachineLearning.Trainers
{
    /// <summary>
    /// this class observes other players and
    /// collect data.
    /// </summary>
    public class SampleCollector
    {
        public List<MultiClassificatioinSample> samples = new List<MultiClassificatioinSample>();

        public void Collect(int times=1000)
        {
            for (int i = 0; i < times; i++)
            {
                ProgrammedAttackingPlayer p1 = new ProgrammedAttackingPlayer();
                ProgrammedAttackingPlayer p2 = new ProgrammedAttackingPlayer();
                GameManager.GameManager gm = new GameManager.GameManager(p1, p2);
                gm.PlayerPlayed += Gm_PlayerPlayed;
                gm.Play();
            }
        }

        /// <summary>
        /// Each time a player plays, collect the data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gm_PlayerPlayed(object sender, GameManager.PlayerPlayedEventArgs e)
        {
            GameManager.GameManager gm = (GameManager.GameManager)sender;
            double[] x =FeatureGenerator.CollectFeatures(e.Tank, gm.map, e.Player.Opponent);
            double[] y = new double[6];
            if (e.PlayerAction.PlayerActionType == PlayerActionType.Attack)
            {
                y[0] = 1;
            }
            else if (e.PlayerAction.PlayerActionType == PlayerActionType.Forward)
            {
                y[1] = 1;
            }
            else if (e.PlayerAction.PlayerActionType == PlayerActionType.Turn)
            {
                y[2 + (int)e.PlayerAction.Direction] = 1;
            }
            MultiClassificatioinSample sample = new MultiClassificatioinSample(x, y);
            samples.Add(sample);
        }
    }
}
