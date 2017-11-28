using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace Vlcr.Core
{
    // Done!
    [Serializable]
    public sealed class Vector
    {
        // Done!
        #region Automatic Properties

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        #endregion

        // Done!
        #region .Ctor

        // Done!
        public Vector(float x, float y, float z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        #endregion

        // Done!
        #region Base Methods

        // Done!
        public override string ToString()
        {
            const string format = "({0:0,00};{1:0,00};{2:0,00})";
            return string.Format(CultureInfo.InvariantCulture, format, X, Y, Z);
        }

        // Done!
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        // Done!
        public override bool Equals(object obj)
        {
            var v = obj as Vector;

            if (v == null)
            {
                return false;
            }

            return this == v;
        }

        #endregion

        // Done!
        #region Static Properties

        // Done!
        public static Vector Empty
        {
            get { return new Vector(0, 0); }
        }

        // Done!
        public static Vector Max
        {
            get { return new Vector(float.MaxValue, float.MaxValue, float.MaxValue); }
        }

        // Done!
        public static Vector Min
        {
            get { return new Vector(float.MinValue, float.MinValue, float.MinValue); }
        }

        // Done!
        public static Vector Invalid
        {
            get { return new Vector(float.NaN, float.NaN, float.NaN); }
        }

        #endregion

        // Done!
        #region Static Methods

        // Done!
        public static Vector Transform(Vector p, float scale, float dx, float dy, float dz)
        {
            var nx = (p.X * scale) + dx;
            var ny = (p.Y * scale) + dy;
            var nz = (p.Z * scale) + dz;
            return new Vector(nx, ny, nz);
        }

        // Done!
        public static Point Transform(Vector p, float scale, float dx, float dy)
        {
            var nx = (p.X * scale) + dx;
            var ny = (p.Y * scale) + dy;
            return new Point((int)Math.Round(nx), (int)Math.Round(ny));
        }

        // Done!
        public static Vector Center(Vector v1, Vector v2)
        {
            return ((v2 - v1)/2.0f) + v1;
        }

        // Done!
        public static float Distance(Vector v1, Vector v2)
        {
            var dx = Math.Abs(v1.X - v2.X);
            var dy = Math.Abs(v1.Y - v2.Y);
            var dz = Math.Abs(v1.Z - v2.Z);
            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        // Done!
        public static float Slope(Vector v1, Vector v2)
        {
            var dx = v2.X - v1.X;
            var dy = v2.Y - v1.Y;
            return dy / dx;
        }

        // Done!
        public static Vector Average(IList<Vector> list)
        {
            float sx = 0;
            float sy = 0;
            float sz = 0;

            int count = list.Count;
            for (int i = 0; i < count; ++i)
            {
                sx += list[i].X;
                sy += list[i].Y;
                sz += list[i].Z;
            }
            return new Vector(sx / count, sy / count, sz / count);
        }

        // Done!
        public static float Angle(Vector v1, Vector v2, Vector v3)
        {
            var s1 = Slope(v1, v3);
            var s2 = Slope(v2, v3);
            return (float)Math.Atan((s1 - s2) / (1 + s1 * s2));
        }

        #endregion

        // Done!
        #region Operators

        // Done!
        public static implicit operator Vector(Point p)
        {
            return new Vector(p.X, p.Y);
        }

        // Done!
        public static explicit operator PointF(Vector v)
        {
            return new PointF(v.X, v.Y);
        }

        // Done!
        public static explicit operator Point(Vector v)
        {
            return Point.Round((PointF)v);
        }

        // Done!
        public static bool operator ==(Vector v1, Vector v2)
        {
            if (ReferenceEquals(v1, v2))
            {
                return true;
            }

            if (ReferenceEquals(v1, null) || ReferenceEquals(null, v2))
            {
                return false;
            }

            return (v1.X == v2.X) && (v1.Y == v2.Y) && (v1.Z == v2.Z);
        }

        // Done!
        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }

        // Done!
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        // Done!
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        // Done!
        public static Vector operator *(Vector v1, float v2)
        {
            return new Vector(v1.X * v2, v1.Y * v2, v1.Z * v2);
        }

        // Done!
        public static Vector operator /(Vector v1, float v2)
        {
            return new Vector(v1.X / v2, v1.Y / v2, v1.Z / v2);
        }

        #endregion

        // Done!
        #region Methods

        // Done!
        public Vector Normalize(float x, float y, float z)
        {
            return new Vector(this.X / x, this.Y / y, this.Z / z);
        }

        // Done!
        public bool IsInvalid()
        {
            return float.IsNaN(this.X) && float.IsNaN(this.Y) && float.IsNaN(this.Z);
        }

        // Done!
        public bool IsValid()
        {
            return !IsInvalid();
        }

        // Done!
        public void Translate(float dx, float dy, float dz, float scale)
        {
            this.X -= dx / scale;
            this.Y -= dy / scale;
            this.Z -= dz / scale;
        }

        #endregion
    }
}