using System;

namespace Vlcr.Core
{
    // Todo: Test
    public sealed class RandomGenerator
    {
        // Done!
        #region Internal Instance Data

        private readonly Random r = new Random();
      
        #endregion

        // Done!
        #region Methods

        // Done!
        #region Doubles

        // Done!
        public double NextDouble(double min, double max)
        {
            return (max - min)*this.r.NextDouble() + min;
        }

        // Done!
        public double NextDouble(double max)
        {
            return NextDouble(0, max);
        }

        // Done!
        public double NextDouble()
        {
            return this.r.NextDouble();
        }

        #endregion

        // Done!
        #region Floats

        // Done!
        public float NextSingle(float min, float max)
        {
            return (float) NextDouble(min, max);
        }

        // Done!
        public float NextSingle(float max)
        {
            return (float)NextDouble(max);
        }

        // Done!
        public double NextSingle()
        {
            return (float)NextDouble();
        }

        #endregion

        // Done!
        #region Integers

        // Done!
        public int NextInt(int min, int max)
        {
            return r.Next(min, max);
        }

        // Done!
        public int NextInt(int max)
        {
            return NextInt(0, max);
        }

        // Done!
        public int NextInt()
        {
            return r.Next();
        }

        #endregion

        // Done!
        #region Bool

        // Done!
        public bool NextBoolean()
        {
            return this.r.Next(0, 2) != 0;
        }

        #endregion

        // Done!
        #region Bytes

        // Done!
        public byte[] NextBytes(int count)
        {
            byte[] data = new byte[count];
            r.NextBytes(data);
            return data;
        }

        // Done!
        public byte[] NextBytes()
        {
            return NextBytes(NextInt(512));
        }

        #endregion

        // Done!
        #region Enums

        // Done!
        public T NextEnum<T>(Type enumeration)
        {
            T[] values = (T[])Enum.GetValues(enumeration);
            return values[NextInt(values.Length)];
        }

        #endregion

        #endregion
    }
}