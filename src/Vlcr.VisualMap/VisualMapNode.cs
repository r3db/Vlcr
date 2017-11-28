using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using Vlcr.Core;
using Vlcr.Map;

namespace Vlcr.VisualMap
{
    [Serializable]
    public sealed class VisualMapNode
    {
        // Done!
        #region Internal Static Data

        private static readonly Vector OffSet1 = new Vector(10, -1);
        private static readonly Vector OffSet2 = new Vector(7, -3);
        private static readonly Vector OffSet3 = new Vector(20, 20);

        #endregion

        // Done!
        #region Automatic Properties

        public MapNode      ConcreteNode    { get; set; }
        public MapVisuals   Visuals         { get; set; }
        public bool         IsLocked        { get; set; }
        public bool         IsVisible       { get; set; }
        public bool         FillShape       { get; set; }
        public bool         FillShapePath   { get; set; }

        public Vector       Min             { get; set; }
        public Vector       Max             { get; set; }

        #endregion

        // Done!
        #region .Ctor

        // Done!
        public VisualMapNode()
        {
            this.CtorHelper();
        }

        // Done!
        private void CtorHelper()
        {
            this.ConcreteNode = new MapNode(NodeType.Geometry);
            this.Visuals = new MapVisuals();
            this.IsVisible = true;
            this.Min = Vector.Min;
            this.Max = Vector.Max;
        }

        #endregion

        // Done!
        #region Draw

        // Done!
        #region Helpers

        // Done!
        private static string DetermineCoordinateFormat(WorkAreaState was, Vector v)
        {
            Vector s = was.Normalize ? v.Normalize(was.Width, was.Height, Math.Max(was.Width, was.Height)) : v;
            CultureInfo ci = CultureInfo.InvariantCulture;
            if (was.Normalize)
            {
                return was.ShowZCoordinate
                           ? string.Format(ci, "({0:0.000}|{1:0.000}|{2:0.000})", s.X, s.Y, s.Z)
                           : string.Format(ci, "({0:0.000}|{1:0.000})", s.X, s.Y);
            }
            return was.ShowZCoordinate
                       ? string.Format(ci, "({0}|{1}|{2})", s.X, s.Y, s.Z)
                       : string.Format(ci, "({0}|{1})", s.X, s.Y);
        }

        // Done!
        private static void DrawGeneralGeometry(Graphics g, VisualMapNode vmn, WorkAreaState was, GraphicsPath gp1, GraphicsPath gp2, bool active)
        {
            var node = vmn.ConcreteNode;

            if (node.Geometry == null || node.Geometry.Count == 0)
            {
                return;
            }

            gp1.Reset();
            gp2.Reset();

            // Create Geometry!
            for (int i = 0; i < node.Geometry.Count - 1; ++i)
            {
                Point c = Vector.Transform(node.Geometry[i + 0], was.Scale, was.DeltaX, was.DeltaY);
                Point n = Vector.Transform(node.Geometry[i + 1], was.Scale, was.DeltaX, was.DeltaY);
                gp1.AddLine(c.X, c.Y, n.X, n.Y);
            }

            // Close Geometry!
            if (was.CloseGeometry && node.Geometry.Count > 2)
            {
                Vector c = Vector.Transform(node.Geometry[node.Geometry.Count - 1], was.Scale, was.DeltaX, was.DeltaY);
                Vector n = Vector.Transform(node.Geometry[0], was.Scale, was.DeltaX, was.DeltaY);
                gp1.AddLine(c.X, c.Y, n.X, n.Y);
            }

            // Show Name
            if (was.ShowShapeName == true && string.IsNullOrEmpty(node.Name) == false)
            {
                var center = Vector.Transform(Vector.Average(node.Geometry), was.Scale, was.DeltaX, was.DeltaY);
                var ci = CultureInfo.InvariantCulture;
                var f = vmn.Visuals.AnchorFont;
                string info = GenerateInfo(node, ci, was.ShowMST);
                gp2.AddString(info, new FontFamily(f.Name), (int)f.Style, f.Size, (PointF)(center + OffSet1), StringFormat.GenericDefault);
            }

            if (vmn.FillShape == true)
            {
                g.FillPath(vmn.Visuals.GeometryFill, gp1);
            }

            if (vmn.FillShapePath == true)
            {
                g.FillPath(vmn.Visuals.GeometryPathFill, gp1);
            }

            g.DrawPath(active ? vmn.Visuals.ActiveGeometry : vmn.Visuals.Geometry, gp1);
            if(active == false)
            {
                g.DrawPath(vmn.Visuals.NameLabel, gp2);
            }
        }

        // Done!
        private static string GenerateInfo(MapNode node, CultureInfo ci, bool showMST)
        {
            return showMST
                       ? "N: " + node.Name +
                         "\nM: " + node.Constraints.Memory.Value.ToString(ci) +
                         "\nS: " + node.Constraints.Speed.Value.ToString(ci) +
                         "\nT: " + node.Constraints.Transitable.Value.ToString(ci)
                       : node.Name;
        }

        // Done!
        private static float GetFullAngle(Vector v1, Vector v2, float s, out int q)
        {
            var tetta = Helpers.ToDegrees((float)Math.Atan(s));
            q = 0;
            if (tetta >= -90 && tetta <= 0)
            {
                if (v2.X <= v1.X)
                {
                    q = 1;
                    tetta = Math.Abs(tetta);
                }
                else if (v2.X >= v1.X)
                {
                    q = 3;
                    tetta = 180 + Math.Abs(tetta);
                }
            }
            else if (tetta <= 90 && tetta >= 0)
            {
                if (v1.X <= v2.X)
                {
                    q = 2;
                    tetta = 180 - tetta;
                }
                else if (v2.X <= v1.X)
                {
                    q = 4;
                    tetta = 360 - tetta;
                }
            }
            return tetta;
        }

        #endregion

        // Done!
        #region Instance Methods

        // Done!
        public void DrawGeometry(Graphics g, WorkAreaState was)
        {
            DrawGeneralGeometry(g, this, was, this.Visuals.GeometryPath, this.Visuals.GeometryNamePath, false);
        }

        // Done!
        public void DrawAnchors(Graphics g, WorkAreaState wae)
        {
            if (ConcreteNode.Geometry.Count == 0)
            {
                return;
            }

            var gp = this.Visuals.AnchorPath;
            gp.Reset();
            
            var size = new Size(Visuals.AnchorSize, Visuals.AnchorSize);

            for (int i = 0; i < ConcreteNode.Geometry.Count; ++i)
            {
                gp.AddRectangle(new Rectangle(Vector.Transform(ConcreteNode.Geometry[i], wae.Scale, wae.DeltaX, wae.DeltaY), size));
            }

            g.FillPath(Visuals.AnchorFill, gp);
            g.DrawPath(Visuals.AnchorBorder, gp);
        }

        // Done!
        public void DrawExits(Graphics g, WorkAreaState was)
        {
            if (ConcreteNode.Exits.Count == 0)
            {
                return;
            }

            var gpe = this.Visuals.ExitPath;
            //var gpc = this.Visuals.ExitCrossPath;

            gpe.Reset();
            //gpc.Reset();

            var size = new Size(Visuals.ExitSize, Visuals.ExitSize);

            for (int i = 0; i < ConcreteNode.Exits.Count; ++i)
            {
                Point v = Vector.Transform(ConcreteNode.Exits[i + 0].Location, was.Scale, was.DeltaX, was.DeltaY);
                gpe.AddRectangle(new Rectangle(v, size));
                if (ConcreteNode.Exits[i] != null)
                {
                    //gpc.StartFigure();
                    //gpc.AddLine(v.X, v.Y, v.X + size.Width, v.Y + size.Height);
                    //gpc.AddLine(v.X + size.Width, v.Y, v.X, v.Y + size.Height);
                    //gpc.CloseFigure();
                }
            }

            g.FillPath(Visuals.ExitFill, gpe);
            g.DrawPath(Visuals.ExitBorder, gpe);
            //g.DrawPath(Visuals.ExitCross, gpc);

        }

        // Done!
        public void DrawAnchorLabels(Graphics g, WorkAreaState was)
        {
            if (ConcreteNode.Geometry.Count == 0)
            {
                return;
            }

            var gp = this.Visuals.AnchorLabelPath;
            gp.Reset();

            for (int i = 0; i < ConcreteNode.Geometry.Count; ++i)
            {
                var f = this.Visuals.AnchorFont;
                var v = (Point)Vector.Transform(ConcreteNode.Geometry[i], was.Scale, was.DeltaX, was.DeltaY, was.DeltaZ);
                var coords = DetermineCoordinateFormat(was, ConcreteNode.Geometry[i]);
                gp.AddString(coords, new FontFamily(f.Name), (int)f.Style, f.Size, (PointF)(v + OffSet2), StringFormat.GenericDefault);
            }

            g.DrawPath(this.Visuals.AnchorLabel, gp);
        }

        // Done!
        public void DrawExitLabels(Graphics g, WorkAreaState was)
        {
            if (ConcreteNode.Exits.Count == 0)
            {
                return;
            }

            var gp = this.Visuals.ExitLabelPath;
            gp.Reset();
            
            for (int i = 0; i < ConcreteNode.Exits.Count; ++i)
            {
                var f = this.Visuals.AnchorFont;
                var v = (Point)Vector.Transform(ConcreteNode.Exits[i].Location, was.Scale, was.DeltaX, was.DeltaY, was.DeltaZ);
                var coords = DetermineCoordinateFormat(was, ConcreteNode.Exits[i].Location);
                gp.AddString(coords, new FontFamily(f.Name), (int)f.Style, f.Size, (PointF)(v + OffSet2), StringFormat.GenericDefault);
            }

            g.DrawPath(this.Visuals.ExitLabel, gp);
        }

        // Todo: Review!
        public void DrawExitConnections(Graphics g, WorkAreaState was)
        {
            if (ConcreteNode.Exits.Count == 0)
            {
                return;
            }

            var gp1 = this.Visuals.ExitSourceConnectorsPath;
            //var gp2 = this.Visuals.ExitConnectorPath;

            gp1.Reset();
            //gp2.Reset();

            var c = Vector.Transform(Vector.Average(ConcreteNode.Geometry), was.Scale, was.DeltaX, was.DeltaY);
            for (int i = 0; i < ConcreteNode.Exits.Count; ++i)
            {
                var e = ConcreteNode.Exits[i];
                Point p = Vector.Transform(e.Location, was.Scale, was.DeltaX, was.DeltaY);
                if (was.ShowExitSources == true)
                {
                    gp1.StartFigure();
                    gp1.AddLine(p.X, p.Y, c.X, c.Y);
                    gp1.CloseFigure();
                }
                if (was.ShowExitConnectors == true && ConcreteNode.Exits[i].Exits.Count > 0 && ConcreteNode.Exits[i].Exits[0] != null)
                {
                    var gp2 = this.Visuals.ExitConnectorPath;
                    gp2.Reset();
                    Point n = Vector.Transform(Vector.Average(ConcreteNode.Exits[i].Exits[0].Geometry), was.Scale, was.DeltaX, was.DeltaY);
                    gp2.StartFigure();
                    gp2.AddLine(p.X, p.Y, n.X, n.Y);
                    gp2.CloseFigure();

                    if(was.ShowConnectorColors == true)
                    {
                        const float factor = 3;
                        const float factor2 = 100000;
                        var constraint = e.Exits[0].Constraints;
                        var width = Math.Abs(constraint.Memory.Value) * factor + constraint.Speed.Value * factor + constraint.Transitable.Value * factor + 1;
                        var color = Color.FromArgb(0xff, Color.FromArgb((int)(Math.Abs(constraint.Memory.Value) * factor2 + constraint.Speed.Value * factor2 + constraint.Transitable.Value * factor2 + 1)));
                        g.DrawPath(new Pen(color, width), gp2);
                    }
                    else
                    {
                        g.DrawPath(Visuals.ExitConnectors, gp2);
                    }
                }
            }

            g.DrawPath(Visuals.ExitSourceConnectors, gp1);
            //g.DrawPath(Visuals.ExitConnectors, gp2);
        }

        #endregion

        // Done!
        #region Static Methods

        // Done!
        public static void DrawSlope(Graphics g, Vector v1, Vector v2, int index)
        {
            if (v1 == null || v2 == null)
            {
                return;
            }

            var s = Vector.Slope(v2, v1);
            if (s == float.MaxValue)
            {
                s = float.NaN;
            }
            Vector c = Vector.Center(v2, v1);
            g.DrawLine(Pens.Black, (PointF)(v1 + OffSet3), (PointF)(v2 + OffSet3));
            int q;
            float tetta = GetFullAngle(v1, v2, s, out q);
            var d = Vector.Distance(v2, v1);
            var ci = CultureInfo.InvariantCulture;
            g.DrawString(
                index != -1
                    ? string.Format(ci, "#{0} : S {1:0.00}\n#{0} : D {2:0.00}\n#{0} : A {3:0.00}°\n#{0} : Q {4}", index, s, d, tetta, q)
                    : string.Format(ci, "S {0:0.00}\nD {1:0.00}\nA {2:0.00}°\nQ {3}", s, d, tetta, q),
                    MapVisuals.ProgramFont, Brushes.Black, (PointF)c);
        }

        // Todo: Write/Fix
        public static void DrawAngle(Graphics g, Vector v1, Vector v2, Vector v3)
        {
            if (v1 == null || v2 == null || v3 == null)
            {
                return;
            }

            const int size = 100;

            var v4 = v3 + OffSet3;

            var s1 = Vector.Slope(v2, v3);
            int q1;
            float tetta1 = GetFullAngle(v2, v3, s1, out q1);

            var s2 = Vector.Slope(v1, v3);
            int q2;
            float tetta2 = GetFullAngle(v1, v3, s2, out q2);

            //const int max = 1000;

            //g.DrawLine(Pens.Black, v4.X, 0, v4.X, max); // y
            //g.DrawLine(Pens.Black, 0, v4.Y, max, v4.Y); // x

            var angle = Math.Abs(tetta1 - tetta2);
            g.DrawString(string.Format(CultureInfo.InvariantCulture, "θ : ({0:0.00}°)", angle), MapVisuals.ProgramFont, Brushes.Black, (PointF)v4);

            var angleR = (tetta1 - tetta2) < 0 ? angle : 360 - angle;

            //if (angleR >= 180)
            //{
            //    g.DrawArc(new Pen(Color.Blue, 2f), new Rectangle((int)v4.X - size / 2, (int)v4.Y - size / 2, size, size), -tetta1, angle > 180 ? (180 - tetta1) + angle : angle);
            //}
            //else
            //{
            //    g.DrawArc(Pens.Black, new Rectangle((int)v4.X - size / 2, (int)v4.Y - size / 2, size, size), -tetta2, angleR);
            //}

            g.DrawArc(Pens.Black, new Rectangle((int)v4.X - size / 2, (int)v4.Y - size / 2, size, size), -tetta2, angleR);
        }

        // Done!
        private static void DrawRectangle(Graphics g, Brush b, Pen p, Vector v, SizeF size)
        {
            var r = Rectangle.Round(new RectangleF(v.X, v.Y, size.Width, size.Height));
            g.FillRectangle(b, r);
            g.DrawRectangle(p, r);
        }

        // Done!
        private static void DrawSquare(Graphics g, Brush b, Pen p, Vector v, int size)
        {
            DrawRectangle(g, b, p, v, new SizeF(size, size));
        }

        // Done!
        public static void DrawSelectedAnchor(Graphics g, WorkAreaState was)
        {
            //var node = was.SelectedShape;
            //DrawSquare(g, node.Visuals.ExitFill, node.Visuals.ExitBorder, was.SelectedAnchor, node.Visuals.AnchorSize);
            var vmn = was.SelectedShape;
            var v = was.SelectedAnchor;
            var p1 = Vector.Transform(new Vector(v.X, v.Y), was.Scale, was.DeltaX, was.DeltaY);
            DrawSquare(g, Brushes.Green, Pens.RosyBrown, p1, vmn.Visuals.ExitSize);
        }

        // Done!
        public static void DrawSelectedExit(Graphics g, WorkAreaState was)
        {
            var vmn = was.SelectedShape;
            var v = was.SelectedExit.Location;
            var p1 = Vector.Transform(new Vector(v.X, v.Y), was.Scale, was.DeltaX, was.DeltaY);
            DrawSquare(g, Brushes.Green, Pens.RosyBrown, p1, vmn.Visuals.ExitSize);
        }

        // Done!
        public static void DrawSelectedGeometry(Graphics g, WorkAreaState was)
        {
            var ss = was.SelectedShape;
            DrawGeneralGeometry(g, was.SelectedShape, was, ss.Visuals.GeometryPath, ss.Visuals.GeometryNamePath, true);
        }

        // Done!
        public static void DrawBounds(Graphics g, WorkAreaState was)
        {
            var vmn = was.SelectedShape;
            var s = vmn.Visuals.AnchorSize;
            var p1 = Vector.Transform(new Vector(vmn.Min.X, vmn.Min.Y), was.Scale, was.DeltaX, was.DeltaY);
            var p2 = Vector.Transform(new Vector(vmn.Max.X, vmn.Max.Y), was.Scale, was.DeltaX, was.DeltaY);
            var r = new RectangleF(p1.X, p1.Y, p2.X - p1.X + s, p2.Y - p1.Y + s);
            DrawRectangle(g, Brushes.Transparent, Pens.RosyBrown, new Vector(r.X, r.Y), new SizeF(r.Width, r.Height));
        }

        // Done!
        public static void DrawPath(Graphics g, WorkAreaState was)
        {
            if(was.ShowPath == false && was.Path == null || was.Path.Count == 0)
            {
                return;
            }

            GraphicsPath gpd = new GraphicsPath();
            GraphicsPath gpl = new GraphicsPath();

            const int size = 10;
            const int os = size/2;
            for (int i = 0; i < was.Path.Count; ++i)
            {
                var c = Vector.Transform(was.Path[i+0].Location, was.Scale, was.DeltaX, was.DeltaY, was.DeltaZ);
                gpd.AddEllipse(new RectangleF(c.X - os, c.Y - os, size, size));
                if(i < was.Path.Count - 1)
                {
                    var n = Vector.Transform(was.Path[i+1].Location, was.Scale, was.DeltaX, was.DeltaY, was.DeltaZ);
                    gpl.AddLine((PointF)c, (PointF)n);
                }
            }

            g.DrawPath(new Pen(Color.Blue, 3f), gpl);
            g.FillPath(Brushes.Blue, gpd);
        }

        #endregion

        #endregion

        // Done!
        #region Base Methods

        // Done!
        public override string ToString()
        {
            return this.ConcreteNode.Name;
        }

        #endregion

        // Done!
        #region Methods

        // Done!
        public void AddAnchor(Vector v)
        {
            this.ConcreteNode.Geometry.Add(v);
            this.DetermineBounds();
        }

        // Done!
        public void AddExit(MapNode mn)
        {
            if(mn.NodeType != NodeType.Exit)
            {
                throw new ArgumentException("mn");
            }
            this.ConcreteNode.Exits.Add(mn);
            this.DetermineBounds();
        }

        // Done!
        public void DetermineBounds()
        {
            float hx = float.MaxValue;
            float hy = float.MaxValue;
            float lx = float.MinValue;
            float ly = float.MinValue;
            
            if (this.ConcreteNode.Geometry.Count > 0)
            {
                var g = this.ConcreteNode.Geometry;
                hx = g.Min(x => x.X);
                hy = g.Min(x => x.Y);
                lx = g.Max(x => x.X);
                ly = g.Max(x => x.Y);
            }
            if (this.ConcreteNode.Exits.Count > 0)
            {
                var e = this.ConcreteNode.Exits;
                hx = Math.Min(hx, e.Min(x => x.Location.X));
                hy = Math.Min(hy, e.Min(x => x.Location.Y));
                lx = Math.Max(lx, e.Max(x => x.Location.X));
                ly = Math.Max(ly, e.Max(x => x.Location.Y));
            }
            this.Min = new Vector(hx, hy);
            this.Max = new Vector(lx, ly);
        }

        #endregion

    }
}