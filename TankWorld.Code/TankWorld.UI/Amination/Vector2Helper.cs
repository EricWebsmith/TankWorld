using OpenTK;
using System;

namespace TankWorld.UI.Amination
{
    public static class Vector2Helper
    {
        /// <summary>
        /// x' = x \cos \theta - y \sin \theta\,,
        /// y' = x \sin \theta + y \cos \theta\,.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector2 Rotate(this Vector2 v, double angle)
        {
            double theta = angle * Math.PI / 180;
            float newX = (float) v.X * (float)Math.Cos(theta) - v.Y * (float)Math.Sin(theta);
            float newY = (float)v.X * (float)Math.Sin(theta) + v.Y * (float)Math.Cos(theta);
            var returnValue= new Vector2(newX, newY);
            return returnValue;
        }
    }
}
