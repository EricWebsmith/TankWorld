using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TankWorld.Core;
using TankWorld.GameManager;
using TankWorld.UI.Amination;

namespace TankWorld.UI
{
    public partial class BatchMatchForm : Form
    {
        public BatchMatchForm()
        {
            InitializeComponent();
        }

        private async void startButton_Click(object sender, EventArgs e)
        {
            List<MatchResult> infos = new List<MatchResult>();
            int total = int.Parse(timesTextBox.Text);
            batchStartButton.Enabled = false;
            for(int i=0;i< total; i++)
            {
                var info = await Task.Run(() => OneMatch());
                infos.Add(info);
                UpdateDisplay(infos);
            }
            batchStartButton.Enabled = true;
        }

        private void UpdateDisplay(IEnumerable<MatchResult> infos)
        {
            int firstPlayerWon = infos.Where(c => c == MatchResult.Player1Won).Count();
            int secondPlayerWon = infos.Where(c => c == MatchResult.Player2Won).Count();
            int tieCount = infos.Where(c => c == MatchResult.Tie).Count();
            int total = (int)int.Parse(timesTextBox.Text);
            player1ScoreLabel.Text = firstPlayerWon.ToString();
            player2ScoreLabel.Text = secondPlayerWon.ToString();
            tieLabel1.Text = tieCount.ToString();
            totalLabel.Text = infos.Count().ToString();

            firstPlayerScoreBarLabel.Width =  (int)(firstPlayerWon * 280 / total);
            secondPlayerScoreBarLabel.Width =  (int)(secondPlayerWon * 280 / total);
            tieCountBarLabel.Width =  (int)(tieCount * 280 / total);
            totalBarLabel.Width = infos.Count()* 280 / total;
        }

        private async Task<MatchResult> MatchAsync()
        {
            Task<MatchResult> t = new Task<MatchResult>(() =>
            {
                return OneMatch();
            });
            t.Start();
            return await t;
        }

        private MatchResult OneMatch()
        {
            IPlayer player1 = PlayerFactory.Create(firstPlayerComboBox.Text);
            IPlayer player2 = PlayerFactory.Create(secondPlayerComboBox.Text);
            GameManager.GameManager gm = new GameManager.GameManager(player1, player2, 9, 9);
            return gm.Play();
        }

        private void BatchMatchForm_Load(object sender, EventArgs e)
        {
            firstPlayerComboBox.Items.Clear();
            secondPlayerComboBox.Items.Clear();
            foreach (string name in PlayerFactory.PlayerRoster)
            {
                firstPlayerComboBox.Items.Add(name);
                secondPlayerComboBox.Items.Add(name);
            }
            
            firstPlayerComboBox.SelectedIndex = 0;
            secondPlayerComboBox.SelectedIndex = 0;
        }

        private void singleStartButton_Click(object sender, EventArgs e)
        {
            IPlayer player1 = PlayerFactory.Create(firstPlayerComboBox.Text);
            IPlayer player2 = PlayerFactory.Create(secondPlayerComboBox.Text);
            MyGame myGame = new MyGame(1200, 650, player1, player2);
            myGame.Run();
        }
    }
}
