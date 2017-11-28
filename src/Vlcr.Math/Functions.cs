using System;

namespace Vlcr.Math
{
    public static class Functions
    {
        // Done!
        public static Func<float, float> Sigmoid(float slope, float deltaX = 0, float deltaY = 0)
        {
            return x => (float)(System.Math.Pow((1f + System.Math.Exp(-slope*x + deltaX)), -1) + deltaY);
        }

        // Done!
        public static float Angle(float radians)
        {
            return (float)(radians%(System.Math.PI));
        }
    }
}
