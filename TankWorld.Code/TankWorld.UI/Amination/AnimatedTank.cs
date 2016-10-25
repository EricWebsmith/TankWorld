using OpenTK;
using System.Collections.Generic;
using TankWorld.Core;
using TankWorld.Common;

namespace TankWorld.UI.Amination
{
    static class Tanks
    {
        static List<AnimatedTank> tanks = new List<AnimatedTank>();
        public static AnimatedTank GetTank(Tank tank)
        {
            foreach (AnimatedTank at in tanks)
            {
                if (at.IsMe(tank))
                {
                    return at;
                }
            }
            return null;
        }

        public static void Add(AnimatedTank aTank)
        {
            tanks.Add(aTank);
        }
    }

    class AnimatedTank
    {
        //public int BlockX { get; set; }
        //public int BlockY { get; set; }

        //public Vector2 OldPhisicalPosition { get; set; }
        //public Vector2 NewPhisicalPosition { get; set; }
        public Vector2 PhisicalPosition { get; set; }

        public int Angle { get; set; }

        private int oldAngle, newAngle;
        private int oldX, oldY;
        private int newX, newY;

        public int Frames { get; set; }
        private int frameIndex = int.MaxValue;
        private PlayerAction playerAction;

        public TankColor Color { get; private set; }

        public bool IsAnimating { get; private set; }

        public bool IsAlive
        {
            get
            {
                return tank.Alive;
            }
        }

        private Block currentBlock;
        private Block newBlock;

        Map map;
        Tank tank;
        public AnimatedTank(Tank tank, Map map,int angle, TankColor color)
        {
            Frames = 15;
            this.tank = tank;
            this.Color = color;
            currentBlock = map.Blocks[tank.X, tank.Y];
            this.Angle = angle;
            oldAngle = angle;
            newAngle = angle;
            oldX = tank.X * 50;
            oldY = tank.Y * 50;
            PhisicalPosition = new Vector2(oldX, oldY);
            this.map = map;
        }

        internal bool IsMe(Tank tank)
        {
            return this.tank == tank;
        }

        public void SetAction(PlayerAction playerAction)
        {
            IsAnimating = true;
            frameIndex = 0;
            this.playerAction = playerAction;
            switch(playerAction.PlayerActionType)
            {
                case PlayerActionType.Forward:
                    newBlock = map.GetFrontBlock(currentBlock.X, currentBlock.Y, playerAction.Direction);
                    newX = newBlock.X * 50;
                    newY = newBlock.Y * 50;
                    break;
                case PlayerActionType.Turn:
                    oldAngle = Angle;
                    newAngle = playerAction.Direction.ToAngle();
                    newBlock = currentBlock;
                    break;
                default:
                    newBlock = currentBlock;
                    break;
            }
        }

        public void Update()
        {
            //Animation
            if (frameIndex <= Frames)
            {
                switch(playerAction.PlayerActionType)
                {
                    case PlayerActionType.Forward:
                        PhisicalPosition = new Vector2(oldX + (newX - oldX) * frameIndex / Frames, oldY + (newY - oldY) * frameIndex / Frames);

                        break;
                    case PlayerActionType.Turn:
                        if (newAngle - oldAngle <= 180)
                        {
                            Angle = oldAngle + (newAngle - oldAngle) * frameIndex / Frames;
                        }
                        else {
                            Angle = oldAngle + (newAngle - oldAngle-360) * frameIndex / Frames;
                        }
                        break;
                }

                frameIndex++;

            }

            //Animation ends
            if (frameIndex == Frames)
            {
                IsAnimating = false;
                frameIndex = int.MaxValue;
                oldX = newX;
                oldY = newY;
                oldAngle = newAngle;
                Angle = newAngle;

                currentBlock = newBlock;
            }
        }
    }
}
