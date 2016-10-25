using TankWorld.Common;
using TankWorld.Core;
using TankWorld.MachineLearning;
using TankWorld.MachineLearning.NeuralNetwork;


namespace TankWorld.MachineLearningPlayers
{
    [Player(DisplayName = "Neural Network Player")]
    public class NeuralNetworkPlayer : BasePlayer
    {
        
        Matrix theta1 = Matlab.LoadMatrix("Plugins/nn_theta1.txt");
        Matrix theta2 = Matlab.LoadMatrix("Plugins/nn_theta2.txt");

        public override PlayerAction Play(Tank tank)
        {
            Matrix[] theta = new Matrix[2] { theta1, theta2};
            NeuralNetworkClassifier mc = new NeuralNetworkClassifier(theta);
            mc.Theta = theta;
            var x = FeatureGenerator.CollectFeatures(tank, map, Opponent);
            var resultIndex = (int)mc.Predict(x);

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
