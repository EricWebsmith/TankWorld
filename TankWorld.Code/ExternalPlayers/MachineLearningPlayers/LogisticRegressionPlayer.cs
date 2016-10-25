using System.Collections.Generic;
using TankWorld.Common;
using TankWorld.Core;
using TankWorld.MachineLearning;
using TankWorld.MachineLearning.LogisticRegression;

namespace TankWorld.MachineLearningPlayers
{
    [Player(DisplayName = "Logistic Regression Player")]
    public class LogisticRegressionPlayer : BasePlayer
    {
        List<double[]> theta= FeatureExtension.LoadMatrix("Plugins/theta.txt");

        public override PlayerAction Play(Tank tank)
        {
            //var theta = 
            MultiLogisticClassifier mc = new MultiLogisticClassifier(40, 6);
            mc.Theta = theta;
            var x = FeatureGenerator.CollectFeatures(tank, map, Opponent);
            var predict = mc.Predict(x);

            int resultIndex = 0;
            for (int i = 0; i < predict.Length; i++)
            {
                if (predict[i] == 1)
                {
                    resultIndex = i;
                    break;
                }
            }

            switch (resultIndex)
            {
                case 0:
                    return PlayerActionHelper.GetAttackAction(tank);
                case 1:
                    return PlayerActionHelper.GetForwardAction(tank);
                case 2:
                    return PlayerActionHelper.GetTurnToAction(tank, Direction.Up);
                case 3:
                    return PlayerActionHelper.GetTurnToAction(tank, Direction.Right);
                case 4:
                    return PlayerActionHelper.GetTurnToAction(tank, Direction.Down);
                case 5:
                    return PlayerActionHelper.GetTurnToAction(tank, Direction.Left);
                default:
                    return PlayerActionHelper.GetForwardAction(tank);
            }
            
        }

    }
}
