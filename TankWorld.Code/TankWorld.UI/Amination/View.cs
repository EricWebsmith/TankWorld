using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace TankWorld.UI.Amination
{
    public enum TweenType
    {
        Instant,
        Linear,
        QuadraticInOut,
        CubicInOut,
        QuarticOut
    }

    class View
    {
        public Vector2 Position { get; private set; }
        public Vector2 PositionGoTO { get; private set; }
        public Vector2 PositionFrom { get; private set; }
        public double Rotation { get; set; }
        public double Zoom { get; set; }
        
        private TweenType tweenType;
        private int currentStep, tweenSteps;

        public View(Vector2 startPosition, double startZoom= 1.0, double startRotation =0.0)
        {
            this.Position = startPosition;
            this.Zoom = startZoom;
            this.Rotation = startRotation;
        }

        public Vector2 ToWorld(Vector2 input)
        {
            input /= (float)Zoom;
            Vector2 dX = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            Vector2 dY = new Vector2((float)Math.Cos(Rotation+ MathHelper.PiOver2), (float)Math.Sin(Rotation) + MathHelper.PiOver2);
            return (this.Position + dX * input.X + dY * input.Y);
        }

        public void Update()
        {
            if(currentStep<tweenSteps)
            {
                currentStep++;
                switch (tweenType)
                {
                    case TweenType.Linear:
                        Position = PositionFrom + (PositionGoTO-PositionFrom) * GetLinear((float)currentStep/tweenSteps);
                        break;
                    case TweenType.QuadraticInOut:
                        Position = PositionFrom + (PositionGoTO - PositionFrom) * GetQuadraticInOut((float)currentStep / tweenSteps);
                        break;
                    case TweenType.CubicInOut:
                        Position = PositionFrom + (PositionGoTO - PositionFrom) * GetCubicInOut((float)currentStep / tweenSteps);
                        break;
                    case TweenType.QuarticOut:
                        Position = PositionFrom + (PositionGoTO - PositionFrom) * GetQuarticOut((float)currentStep / tweenSteps);
                        break;
                }
            }
            else
            {
                Position = PositionGoTO;
            }
        }

        public void SetPosition(Vector2 newPosition)
        {
            this.Position = newPosition;
            this.PositionFrom = newPosition;
            this.PositionGoTO = newPosition;
            tweenType = TweenType.Instant;
            currentStep = 0;
            tweenSteps = 0;
        }

        public void SetPosition(Vector2 newPosition, TweenType type, int numSteps)
        {
            this.PositionFrom = Position;
            //this.Position = newPosition;
            //this.positionFrom = Position;
            this.PositionGoTO = newPosition;
            tweenType = type;
            currentStep = 0;
            tweenSteps = numSteps;
        }

        public void ApplyTransform()
        {
            Matrix4 transform = Matrix4.Identity;

            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-Position.X, -Position.Y, 0));

            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ(-(float)Rotation));

            transform = Matrix4.Mult(transform, Matrix4.CreateScale((float)Zoom, (float)Zoom, 1.0f));

            GL.MultMatrix(ref transform);


        }

        public float GetLinear(float t)
        {
            return t;
        }

        public float GetQuadraticInOut(float t)
        {
            return (t * t) / ((2 * t * t) - (2 * t) + 1);
        }

        public float GetCubicInOut(float t)
        {
            return (t * t * t) / ((3 * t * t) - (3 * t) + 1);
        }

        public float GetQuarticOut(float t)
        {
            return -((t - 1) * (t - 1) * (t - 1) * (t - 1)) + 1;
        }
    }
}
