using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace Vlcr.Core
{
    public static class SerializationHelper
    {
        // Done!
        #region Byte[]

        // Done!
        public static void StoreByteArray(SerializationInfo info, byte[] data, string name)
        {
            info.AddValue(name + ":[]ByteValue", data);
        }

        // Done!
        public static byte[] RetrieveByteArray(SerializationInfo info, string name)
        {
            return (byte[])info.GetValue(name + ":[]ByteValue", typeof(byte[]));
        }

        #endregion

        // Done!
        #region Int

        // Done!
        public static void StoreInt(SerializationInfo info, int value, string name)
        {
            info.AddValue(name + ":IntValue", value);
        }

        // Done!
        public static int RetrieveInt(SerializationInfo info, string name)
        {
            return (int)info.GetValue(name + ":IntValue", typeof(int));
        }

        #endregion

        // Done!
        #region Float

        // Done!
        public static void StoreFloat(SerializationInfo info, float value, string name)
        {
            info.AddValue(name + ":FloatValue", value);
        }

        // Done!
        public static float RetrieveFloat(SerializationInfo info, string name)
        {
            return (float)info.GetValue(name + ":FloatValue", typeof(float));
        }

        #endregion

        // Done!
        #region String

        // Done!
        public static void StoreString(SerializationInfo info, string value, string name)
        {
            info.AddValue(name + ":StringValue", value);
        }

        // Done!
        public static string RetrieveString(SerializationInfo info, string name)
        {
            return (string)info.GetValue(name + ":StringValue", typeof(string));
        }

        #endregion

        // Done!
        #region Color

        // Done!
        public static void StoreColor(SerializationInfo info, Color color, string name)
        {
            StoreString(info, color.Name, name);
        }

        // Done!
        public static Color RetrieveColor(SerializationInfo info, string name)
        {
            return Color.FromName(RetrieveString(info, name));
        }

        #endregion

        // Done!
        #region SolidBrush

        // Done!
        public static void StoreSolidBrush(SerializationInfo info, SolidBrush brush, string name)
        {
            StoreColor(info, brush.Color, name);
        }

        // Done!
        public static SolidBrush RetrieveSolidBrush(SerializationInfo info, string name)
        {
            return new SolidBrush(RetrieveColor(info, name));
        }

        #endregion

        // Done!
        #region Pen

        // Done!
        public static void StorePen(SerializationInfo info, Pen pen, string name)
        {
            StoreColor(info, pen.Color, name);
            StoreFloat(info, pen.Width, name);
            StoreInt(info, (int)pen.DashStyle, name + "1");
        }

        // Done!
        public static Pen RetrievePen(SerializationInfo info, string name)
        {
            var color = RetrieveColor(info, name);
            var width = RetrieveFloat(info, name);
            var dash = (DashStyle)RetrieveInt(info, name + "1");
            return new Pen(color, width) {DashStyle = dash};
        }

        #endregion

        // Done!
        #region Font

        // Done!
        public static void StoreFont(SerializationInfo info, Font font, string name)
        {
            StoreString(info, font.Name, name);
            StoreFloat(info, font.Size, name);
        }

        // Done!
        public static Font RetrieveFont(SerializationInfo info, string name)
        {
            var font = RetrieveString(info, name);
            var width = RetrieveFloat(info, name);
            return new Font(font, width);
        }

        #endregion

        // Done!
        #region GraphicsPath

        // Done!
        public static void StoreGraphicsPath(SerializationInfo info, GraphicsPath gp, string name)
        {
            info.AddValue(name + ":[]FloatValue", gp.PointCount > 0 ? gp.PathPoints : new PointF[0]);
            info.AddValue(name + ":[]ByteValue",  gp.PointCount > 0 ? gp.PathTypes  : new byte[0]);
        }

        // Done!
        public static GraphicsPath RetrieveGraphicsPath(SerializationInfo info, string name)
        {
            var points = (PointF[])info.GetValue(name + ":[]FloatValue", typeof(PointF[]));
            var types = (byte[])info.GetValue(name + ":[]ByteValue", typeof(byte[]));
            return (points.Length == 0) ? new GraphicsPath() : new GraphicsPath(points, types);
        }

        #endregion

    }
}