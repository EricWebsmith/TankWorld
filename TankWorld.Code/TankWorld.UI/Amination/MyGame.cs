using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using TankWorld.Core;
using TankWorld.GameManager;

namespace TankWorld.UI.Amination
{
    class MyGame : GameWindow
    {
        Texture2D texture;
        Texture2D tank;
        Texture2D redFlag;
        Texture2D blueFlag;

        AnimatedTank redTank1;
        AnimatedTank redTank2;
        AnimatedTank blueTank1;
        AnimatedTank blueTank2;
        GameManager.GameManager gm;

        public MyGame(int width, int height, IPlayer player1, IPlayer player2) : base(width, height)
        {
            GL.Enable(EnableCap.Texture2D);
            MyInput.Initialise(this);
            gm = new GameManager.GameManager(player1, player2, 24, 9);
            gm.PlayerPlayed += Gm_PlayerPlayed;
            redTank1 = new AnimatedTank(gm.redTank1, gm.map, 0, TankColor.Red);
            Tanks.Add(redTank1);
            redTank2 = new AnimatedTank(gm.redTank2, gm.map, 0, TankColor.Red);
            Tanks.Add(redTank2);
            blueTank1 = new AnimatedTank(gm.blueTank1, gm.map, 180, TankColor.Blue);
            Tanks.Add(blueTank1);
            blueTank2 = new AnimatedTank(gm.blueTank2, gm.map, 180, TankColor.Blue);
            Tanks.Add(blueTank2);
            //gm.Play();
        }

        private void Gm_PlayerPlayed(object sender, PlayerPlayedEventArgs e)
        {
            AnimatedTank at = Tanks.GetTank(e.Tank);
            if (at != null)
            {
                at.SetAction(e.PlayerAction);
            }

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            texture = ContentPipe.LoadTexture(@"Images\Brick.bmp");
            tank = ContentPipe.LoadTexture(@"Images\Tank.png");
            redFlag = ContentPipe.LoadTexture(@"Images\Flag_Red.png");
            blueFlag = ContentPipe.LoadTexture(@"Images\Flag_Blue.png");

        }

        //private void Mouse_ButtonDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
        //{
        //    Console.WriteLine("Down" + e.Position.X.ToString() + "," + e.Position.Y.ToString());
        //    Vector2 pos = new Vector2(e.Position.X, e.Position.Y);
        //    pos -= new Vector2(this.Width, this.Height) / 2f;
        //    pos = view.ToWorld(pos);

        //    view.SetPosition(pos, TweenType.Linear, 60);
        //}
        int round = 0;
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (!redTank1.IsAnimating)
            {
                gm.PlayRound(round++);
            }

            redTank1.Update();
            blueTank1.Update();
            redTank2.Update();
            
            blueTank2.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            //view.Update();
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.ClearColor(Color.White);

            Spritebatch.Begin(this.Width, this.Height);
            Vector2 upLeft = new Vector2(-this.Width / 2, -this.Height / 2);
            Vector2 zero = new Vector2(0, 0);
            for (int i = 0; i < gm.map.Blocks.GetLength(0); i++)
            {
                for (int j = 0; j < gm.map.Blocks.GetLength(1); j++)
                {
                    if (gm.map.Blocks[i, j].Pattern == Core.Pattern.Brick)
                    {
                        Spritebatch.Draw(texture, new Vector2(i * 50, j * 50), new Vector2(1f, 1f), Color.DarkRed, upLeft);
                    }
                }
            }
            Spritebatch.Draw(redFlag, new Vector2(gm.map.RedFlagBlock.X * 50, gm.map.RedFlagBlock.Y * 50), new Vector2(1f, 1f), Color.Transparent, upLeft);
            Spritebatch.Draw(blueFlag, new Vector2(gm.map.BlueFlagBlock.X * 50, gm.map.BlueFlagBlock.Y * 50), new Vector2(1f, 1f), Color.Transparent, upLeft);
            //Red Tank 1

            DrawRotatedTank(redTank1, Color.Purple);
            //Blue Tank 1
            DrawRotatedTank(blueTank1, Color.GreenYellow);
            //Red Tank 2
            DrawRotatedTank(redTank2, Color.Red);

            //Blue Tank 2
            DrawRotatedTank(blueTank2, Color.GreenYellow);

            GL.LoadIdentity();

            //Spritebatch.Draw(tank, new Vector2(tankY / 2 + 100, tankX / 2 - 100), new Vector2(1f, 1f), Color.White, view.Position);
            //GL.Rotate(rotate, Vector3d.UnitZ);
            //Vector2 d = new Vector2(100, 100);

            ////Vector2 newD= new Vector2()
            //Spritebatch.Draw(tank, d.Rotate(-rotate) + new Vector2(-20, -20), new Vector2(1f, 1f), Color.Yellow, view.Position);
            //GL.LoadIdentity();
            //Spritebatch.Begin(this.Width, this.Height);
            //GL.Rotate(-rotate, Vector3d.UnitZ);
            //Vector2 d2 = new Vector2(0, 0);
            //Spritebatch.Draw(tank, (d2).Rotate(rotate) + new Vector2(-20, -20), new Vector2(1f, 1f), Color.Pink, view.Position);
            this.SwapBuffers();
        }

        private void DrawRotatedTank(AnimatedTank aTank, Color color)
        {
            if (!aTank.IsAlive)
            {
                return;
            }
            GL.LoadIdentity();
            Spritebatch.Begin(this.Width, this.Height);
            GL.Rotate(aTank.Angle, Vector3d.UnitZ);
            Spritebatch.Draw(tank, (aTank.PhisicalPosition - new Vector2(Width / 2, Height / 2) + new Vector2(25, 25)).Rotate(-aTank.Angle) + new Vector2(-25, -25), new Vector2(1f, 1f), color, new Vector2(0, 0));

        }
    }
}
