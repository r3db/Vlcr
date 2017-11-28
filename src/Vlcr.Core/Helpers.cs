using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Vlcr.Core
{
    public static class Helpers
    {
        // Done!
        public static byte[] ToByteArray<T>(T data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, data);
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        // Done!
        public static T FromByteArray<T>(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(ms);
            }
        }

        // Done!
        public static T Clone<T>(T data) where T : class
        {
            if(data == null) { return null; }
            using (MemoryStream ms = new MemoryStream(ToByteArray(data)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(ms);
            }
        }

        // Done!
        public static string Clone(string data)
        {
            if(data == null) { return null; }
            if(data == string.Empty) { return string.Empty; }
            return string.Copy(data);
        }

        // Done!
        public static bool StringCompare(string s1, string s2)
        {
            return string.Compare(s1, s2, StringComparison.Ordinal) == 0;
        }

        // Done!
        public static float ToDegrees(float a)
        {
            return (float)(a * 180 / Math.PI);
        }

        // Done!
        public static void SetHighQualityMode(Graphics g)
        {
            g.CompositingMode = CompositingMode.SourceOver;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        }

        // Done!
        public static bool IsInsideRectangle(Point p, RectangleF r)
        {
            return (p.X >= r.X && p.Y >= r.Y && p.X <= (r.X + r.Width) && p.Y <= (r.Y + r.Height));
        }

        // Done!
        public static Color GenerateColor(Random r)
        {
            if (r == null)
            {
                return Color.Empty;
            }

            var colors = Enum.GetNames(typeof(KnownColor));
            return Color.FromName(colors[r.Next(0, colors.Length)]);
        }

        // Done!
        public static Pen GeneratePen(Random r)
        {
            return new Pen(GenerateColor(r), r.Next(2, 8));
        }

        // Done!
        public static OpenFileDialog GetOpenFileDialog(string title, string filter)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                AddExtension = true,
                AutoUpgradeEnabled = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DereferenceLinks = true,
                Multiselect = true,
                ShowHelp = false,
                ShowReadOnly = true,
                ValidateNames = true,
                Title = title,
                Filter = filter
            };

            return ofd;
        }

        // Done!
        public static SaveFileDialog GetSaveFileDialog(string title, string filter)
        {
            SaveFileDialog ofd = new SaveFileDialog
            {
                AddExtension = true,
                AutoUpgradeEnabled = true,
                CheckFileExists = false,
                CheckPathExists = true,
                DereferenceLinks = true,
                ShowHelp = false,
                ValidateNames = true,
                Title = title,
                Filter = filter
            };

            return ofd;
        }

        // Done!
        public static int GetWeeks(TimeSpan span)
        {
            var t = (int)(span.TotalDays / 4.0f);
            if (t == 0)
            {
                t = 1;
            }
            return t;
        }

        // Done!
        public static Bitmap LoadImageFromBitmap(Bitmap source, float scale, Color c, float xoffset, float yoffset, int width, int height)
        {
            var temp1 = new Bitmap(source, width, height);
            using (Graphics g = Graphics.FromImage(temp1))
            {
                g.Clear(c);
                g.ScaleTransform(scale, scale);
                g.DrawImage(source, xoffset / scale, yoffset / scale);
                g.Save();
            }

            return temp1;
        }

        // Done!
        public static Bitmap LoadImageFromFile(string path, float scale, Color c, float xoffset, float yoffset, int width, int height)
        {
            using (Bitmap source = new Bitmap(path))
            {
                return LoadImageFromBitmap(source, scale, c, xoffset, yoffset, source.Width, source.Height);
            }
        }

        public static void SafeInvoke<TEventArgs>(EventHandler<TEventArgs> eh, object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            if(eh != null)
            {
                eh.Invoke(sender, e);
            }
        }

        public static void SafeInvoke(EventHandler eh, object sender)
        {
            if (eh != null)
            {
                eh.Invoke(sender, EventArgs.Empty);
            }
        }

    }
}
