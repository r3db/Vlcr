using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Vlcr.Core;
using Vlcr.Creator.Controls;
using Vlcr.IO;
using Vlcr.Map;
using Vlcr.VisualMap;

namespace Vlcr.Creator
{
    internal sealed partial class MainForm : Form
    {
        // Done!
        #region Events

        internal event EventHandler SelectionChanged;
        internal event EventHandler StateChanged;
        internal event EventHandler NodeAdded;
        
        #endregion

        // Done!
        #region Internal Instance Data

        private RectangleF drawArea = new RectangleF(0, 0, 0, 0);
        private NodeControl nodeController;
        private FinderControl finderControl;
        private XnaWorkArea xnaWorkArea;
        private int nodeCounter;
        private Bitmap imageGuide;
        private RectangleF shapeSelection = new RectangleF(float.MinValue, float.MinValue, float.MaxValue, float.MaxValue);
        private RectangleF moveSelection = new RectangleF(float.MinValue, float.MinValue, float.MaxValue, float.MaxValue);
        private readonly RectangleF defaultAgentView = new RectangleF(60, 60, 400, 400);

        #endregion

        // Done!
        #region Automatic Properties

        internal ConcreteVisualMap    VisualMap       { get; private set; }
        internal WorkAreaState        WorkAreaState   { get; private set; }
        internal Bitmap               LastImage       { get; private set; }

        #endregion

        // Done!
        #region .Ctor

        // Done!
        internal MainForm()
        {
            this.InitializeComponent();
            this.InitializeComponentHelper();
            this.CtorHelper();
        }

        // Done!
        private void InitializeComponentHelper()
        {
            this.VisualMap = new ConcreteVisualMap();
            this.WorkAreaState = WorkAreaState.Default;
            
            this.nodeController = new NodeControl(this)   { Dock = DockStyle.Left  };
            this.finderControl  = new FinderControl(this) { Dock = DockStyle.Right, Visible = false };
            this.xnaWorkArea    = new XnaWorkArea(this)   { Dock = DockStyle.Fill,  Visible = false, BackColor = Color.LightYellow };

            this.Controls.Remove(this.mainMenuStrip);
            this.Controls.Remove(this.mainMenuStrip);
            this.Controls.Add(this.nodeController);
            this.Controls.Add(this.finderControl);
            this.Controls.Add(this.xnaWorkArea);
            this.Controls.Add(this.mainMenuStrip);
            
        }

        // Done!
        private void CtorHelper()
        {
            if(VisualMap.Count > 0)
            {
                this.nodeController.Enabled = true;
            }

            this.workArea.BackColor = this.WorkAreaState.BackgroundColor;

            this.OnResize(null);
            
            this.workArea.Paint += WorkAreaPaint;
            this.workArea.MouseLeave += WorkAreaMouseLeave;
            this.workArea.MouseDown += WorkAreaMouseDown;
            this.workArea.MouseUp += WorkAreaMouseUp;
            this.workArea.MouseMove += WorkAreaMouseMove;

            //if (Debugger.IsAttached == true)
            //{
            //    OpenRawFile(@"C:\Users\redb\Desktop\Vlcr\Resources\Map.rxmf");
            //}
        }

        //// Done!
        //[Conditional(Constants.ConditionalDebug)]
        //private void GenerateData()
        //{
        //    Random r = new Random();

        //    int count1 = r.Next(2, 6);
        //    const int offset = 0;

        //    for (int i = 0; i < count1; ++i)
        //    {
        //        // Generate Node and VisualMapNode
        //        var node = new MapNode(Name = i.ToString(), NodeType.Geometry) { Level = 0 };
        //        var vmn = new VisualMapNode() { Visuals = new MapVisuals { Geometry = Helpers.GeneratePen(r) }, ConcreteNode = node };

        //        // Generate Geometry
        //        int count2 = r.Next(2, 6);
        //        for (int k = 0; k < count2; ++k)
        //        {
        //            node.Geometry.Add(new Vector(r.Next(offset, this.nodeController.Width), r.Next(offset, this.nodeController.Height)));
        //        }

        //        if (VisualMap.Count > 0)
        //        {
        //            // Generate Exits
        //            int count3 = r.Next(2, 6);
        //            for (int k = 0; k < count3; ++k)
        //            {
        //                var e = new MapNode(NodeType.Exit)
        //                {
        //                    Level = 0,
        //                    Parent = node,
        //                    Location = new Vector(r.Next(offset, this.nodeController.Width), r.Next(offset, this.nodeController.Height)),

        //                };
        //                e.Exits.Add(VisualMap[r.Next(0, VisualMap.Count - 1)].ConcreteNode);
        //                node.Exits.Add(e);
        //            }
        //        }

        //        VisualMap.Add(vmn);
        //    }
        //}

        #endregion

        // Done!
        #region Events

        // Done!
        #region Other

        // Done!
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.drawArea = new RectangleF(0, 0, this.workArea.Width, this.workArea.Height);
            if (this.WorkAreaState != null)
            {
                if (this.workArea.Width != 0 && this.workArea.Height != 0)
                {
                    this.WorkAreaState.Width = this.workArea.Width;
                    this.WorkAreaState.Height = this.workArea.Height;
                }
                this.WorkAreaState.IsDirty = true;
                this.ComposeImageGuide();
            }

            if(this.WorkAreaState != null && this.WorkAreaState.HighQuality == true)
            {
                this.WorkAreaState = WorkAreaState.LowQuality(this.WorkAreaState);
                this.ForceUpdate();
                return;
            }

            this.ForceUpdate();
        }

        // Done!
        public void ForceResizeEvent()
        {
            this.OnResize(null);
        }

        // Done!
        private static bool IsValidSelection(RectangleF r)
        {
            return (r.X >= -1000 && r.Y >= -1000 && r.Width > 0 && r.Height > 0);
        }

        // Done!
        private void WorkAreaPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            if (this.WorkAreaState.TweakAgentView == true && IsValidSelection(drawArea))
            {
                this.drawArea = defaultAgentView;
                g.FillRectangle(new SolidBrush(Color.FromArgb(0x44, Color.ForestGreen)), drawArea);
            }

            if (this.WorkAreaState.ShowShapeSelection == true && IsValidSelection(shapeSelection))
            {
                g.FillRectangle(Brushes.RosyBrown, shapeSelection);
            }

            if (this.WorkAreaState.ShowMoveSelection == true && IsValidSelection(moveSelection))
            {
                g.FillRectangle(Brushes.RoyalBlue, moveSelection);
            }

            if(this.WorkAreaState.HighQuality == true)
            {
                Helpers.SetHighQualityMode(g);
            }

            var start = Environment.TickCount;
            UpdateTitle(start.ToString());
            if(this.WorkAreaState.IsDirty == true)
            {
                var temp = new Bitmap(e.ClipRectangle.Width, e.ClipRectangle.Height);
                Draw(Graphics.FromImage(temp), false);
                g.DrawImage(temp, 0, 0);
                this.LastImage = temp;
                this.WorkAreaState.IsDirty = false;
                MeasureTime("Paint 1", start);
            }
            else
            {
                MeasureTime("Paint 2", start);
                g.DrawImage(this.LastImage, 0, 0);
            }

            // We draw this section apart because we don't want it in the final export picture!
            if (WorkAreaState.ShowOnlyConnectors == false)
            {
                DrawSelections(g);
                if (WorkAreaState.ShowExtraDetails)
                {
                    DrawSlopes(g);
                }
            }

            if(this.WorkAreaState.Redraw == true)
            {
                this.WorkAreaState = this.WorkAreaState.Backup;
                this.ForceUpdate();
            }
        }

        // Done!
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            if (this.WorkAreaState.IsSaved == false)
            {
                var result = MessageBox.Show(@"Do you want to close?", @"Map Not Saved", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion

        // Done!
        #region Mouse Events

        // Done!
        #region Helpers

        // Done!
        private static bool IsRightUp(MouseEventArgs e)
        {
            return e.Clicks == 1 && e.Button == MouseButtons.Right;
        }

        // Done!
        private static bool IsLeftUp(MouseEventArgs e)
        {
            return e.Clicks == 1 && e.Button == MouseButtons.Left;
        }

        // Done!
        private void UpdateMouseLocation(Point p)
        {
            this.WorkAreaState.LastMousePosition = p;
        }

        // Done!
        private void MoveAll(Point p, WorkAreaState was)
        {
            float dx = was.LastMousePosition.X - p.X;
            float dy = was.LastMousePosition.Y - p.Y;

            was.DeltaX -= dx;
            was.DeltaY -= dy;
            was.DeltaZ -= 0;

            this.ComposeImageGuide();
            this.ForceUpdate();
        }

        // Todo: Make more efficient
        private void MoveArtifacts(Point p)
        {
            ClearSlopeEvents();

            var was = this.WorkAreaState;
            var artifact = GetDetectedArtifact(this.WorkAreaState);
            switch (artifact)
            {
                case Artifact.Anchor:
                {
                    DetermineSlopes();
                    TranslateAnchor(p, this.WorkAreaState);
                    was.LastSelectedShape.DetermineBounds();
                    was.IsSaved = false;
                    was.IsDirty = true;
                    was.ShowPath = false;
                    break;
                }
                case Artifact.Exit:
                {
                    TranslateExit(p, this.WorkAreaState);
                    was.LastSelectedShape.DetermineBounds();
                    was.IsSaved = false;
                    was.IsDirty = true;
                    was.ShowPath = false;
                    break;
                }
                case Artifact.Geometry:
                {
                    TranslateShape(p, this.WorkAreaState);
                    was.LastSelectedShape.DetermineBounds();
                    was.IsSaved = false;
                    was.IsDirty = true;
                    was.ShowPath = false;
                    break;

                }
                case Artifact.None: return;
            }

            this.ForceUpdate();
        }

        // Done!
        internal void ClearSlopeEvents()
        {
            this.WorkAreaState.SlopeVector0 = null;
            this.WorkAreaState.SlopeVector1 = null;
        }

        // Todo: Improve this technique!
        private void DetermineSlopes()
        {
            if (this.WorkAreaState.ShowSlopes == false)
            {
                return;
            }

            var v = this.WorkAreaState.SelectedAnchor;
            var s = this.WorkAreaState.SelectedShape;

            this.WorkAreaState.SlopeVector0 = FindAnchorBefore(s, v);
            this.WorkAreaState.SlopeVector1 = FindAnchorAfter(s, v);
        }

        // Done!
        private static Vector FindAnchorBefore(VisualMapNode vmn, Vector vector)
        {
            var mn = vmn.ConcreteNode.Geometry;
            for (int i = 0; i < mn.Count; ++i)
            {
                if (mn[i] == vector)
                {
                    return (i == 0) ? mn[mn.Count - 1] : mn[i - 1];
                }
            }
            return null;
        }

        // Done!
        private static Vector FindAnchorAfter(VisualMapNode vmn, Vector vector)
        {
            var mn = vmn.ConcreteNode.Geometry;
            for (int i = 0; i < mn.Count; ++i)
            {
                if (mn[i] == vector)
                {
                    return (i == mn.Count - 1) ? mn[0] : mn[i + 1];
                }
            }
            return null;
        }

        // Done!
        private void AddVectorToShape(Point p)
        {
            var was = this.WorkAreaState;

            if (was.LastSelectedShape == null)
            {
                MessageBox.Show(this.VisualMap.Count == 0 ? @"Please create a new shape first" : @"Please create or select a new shape first");
                return;
            }

            if (was.LastSelectedShape.IsLocked == true)
            {
                MessageBox.Show(@"That shape is locked");
                return;
            }

            if (was.LastSelectedShape.IsVisible == false)
            {
                MessageBox.Show(@"That shape is not visible");
                return;
            }

            float scale = was.Scale;

            Vector v = new Vector((p.X - was.DeltaX) / scale, (p.Y - was.DeltaY) / scale);
            var vmp = was.LastSelectedShape;
            if (was.Action == WorkAreaAction.AddAnchor)
            {
                vmp.AddAnchor(v);
            }
            else
            {
                vmp.AddExit(new MapNode(NodeType.Exit) { Location = v });
            }
            was.IsSaved = false;
            was.IsDirty = true;
            was.ShowPath = false;
            SafeInvoker(this.StateChanged, this.WorkAreaState);
            SafeInvoker(this.NodeAdded, this.WorkAreaState);
            this.ForceUpdate();
        }

        #endregion

        // Done!
        #region Mouse

        // Done!
        private void WorkAreaMouseLeave(object sender, EventArgs e)
        {
            this.WorkAreaState.IsMouseDown = false;
            this.Cursor = Cursors.Default;
        }

        // Done!
        private void WorkAreaMouseDown(object sender, MouseEventArgs e)
        {
            this.WorkAreaState.IsMouseDown = true;
        }

        // Done!
        private void WorkAreaMouseUp(object sender, MouseEventArgs e)
        {
            if (this.WorkAreaState.IsMouseDown == false || this.WorkAreaState.Action == WorkAreaAction.Blocked)
            {
                return;
            }

            this.WorkAreaState.IsMouseDown = false;

            if (this.WorkAreaState.Action == WorkAreaAction.Blocked)
            {
                return;
            }
            if (this.WorkAreaState.Action == WorkAreaAction.Move)
            {
                return;
            }

            var artifact = GetDetectedArtifact(this.WorkAreaState);

            if (IsRightUp(e))
            {
                if (artifact == Artifact.Anchor)
                {
                    this.contextMenuStripAnchor.Show(this.workArea, e.Location);
                }
                else if (artifact == Artifact.Geometry)
                {
                    this.contextMenuStripShape.Show(this.workArea, e.Location);
                }
                else if (artifact == Artifact.Exit)
                {
                    this.contextMenuStripExit.Show(this.workArea, e.Location);
                }
                else if (artifact == Artifact.None)
                {
                    this.contextMenuStripWorkspace.Show(this.workArea, e.Location);
                }
            }
            else if (IsLeftUp(e))
            {
                if (artifact == Artifact.None)
                {
                    AddVectorToShape(e.Location);
                    this.ForceUpdate();
                }
            }
        }

        // Done!
        private void WorkAreaMouseMove(object sender, MouseEventArgs e)
        {
            var start = Environment.TickCount;
            
            if (WorkAreaState.Action == WorkAreaAction.Blocked)
            {
                UpdateMouseLocation(e.Location);
                MeasureTime("Move 1", start);
                return;
            }

            if (WorkAreaState.Action == WorkAreaAction.Move)
            {
                Cursor = Cursors.Cross;
                if (e.Button == MouseButtons.Left)
                {
                    if (WorkAreaState.Action == WorkAreaAction.Move)
                    {
                        MoveAll(e.Location, WorkAreaState);
                        UpdateMouseLocation(e.Location);
                        this.WorkAreaState.IsSaved = false;
                        this.WorkAreaState.IsDirty = true;
                        return;
                    }
                }
                UpdateMouseLocation(e.Location);
                MeasureTime("Move 2", start);
                return;
            }

            Artifact lastArtifact = WorkAreaState.SelectedArtifact;

            if (WorkAreaState.IsMouseDown)
            {
                if (e.Button == MouseButtons.Left)
                {
                    // We are probably working on something!
                    MoveArtifacts(e.Location);
                }
            }
            else
            {
                // Detect Current Artifact!
                Artifact artifact = this.DetectArtifact(e.Location, VisualMap);
                
                if (artifact == Artifact.None)
                {
                    Cursor = Cursors.Default;
                    if (lastArtifact != artifact)
                    {
                        if ((lastArtifact == Artifact.Anchor    && WorkAreaState.PersistAnchor == false) ||
                            (lastArtifact == Artifact.Exit      && WorkAreaState.PersistExit   == false) ||
                            (lastArtifact == Artifact.Geometry  && WorkAreaState.PersistShape  == false))
                        {
                            ForceUpdate();
                        }
                    }
                    UpdateMouseLocation(e.Location);
                    MeasureTime("Move 3", start);
                    return;
                }

                DetermineSlopes();

                Cursor = Cursors.Hand;
                ForceUpdate();
                GeneralInvoker(artifact, lastArtifact);
            }

            UpdateMouseLocation(e.Location);
            MeasureTime("Move 4", start);
        }

        #endregion

        #endregion

        #endregion

        // Done!
        #region Helpers

        // Done!
        private void UpdateTitle(string s = "")
        {
            this.Text = Constants.ApplicationTitle + " Scale: " + this.WorkAreaState.Scale + " | " + s;
        }

        // Done!
        [Conditional(Constants.ConditionalDebug)]
        private static void MeasureTime(string msg, int start)
        {
            var end = Environment.TickCount;
            Console.WriteLine(msg + ": " + (end - start) + "ms");
            Console.WriteLine(Environment.TickCount);
            Console.WriteLine("-------------------------------");
        }

        // Done!
        private static void SafeInvoker(EventHandler eh, WorkAreaState was)
        {
            if(eh != null)
            {
                eh.Invoke(was, null);
            }
        }

        // Done!
        internal void InvokeStateChangedEvent()
        {
            SafeInvoker(this.StateChanged, this.WorkAreaState);
        }

        // Done!
        internal void InvokeNodeAddedEvent()
        {
            SafeInvoker(this.NodeAdded, this.WorkAreaState);
        }

        // Done!
        private void GeneralInvoker(Artifact artifact, Artifact lastArtifact)
        {
            if (lastArtifact == artifact)
            {
                switch (artifact)
                {
                    case Artifact.Anchor:
                    {
                        if (this.WorkAreaState.LastSelectedAnchor != this.WorkAreaState.SelectedAnchor)
                        {
                            SafeInvoker(SelectionChanged, this.WorkAreaState);
                        }
                    }
                    break;
                    case Artifact.Exit:
                    {
                        if (this.WorkAreaState.LastSelectedExit != this.WorkAreaState.SelectedExit)
                        {
                            SafeInvoker(SelectionChanged, this.WorkAreaState);
                        }
                    }
                    break;
                    case Artifact.Geometry:
                    {
                        if (this.WorkAreaState.LastSelectedShape != this.WorkAreaState.SelectedShape)
                        {
                            SafeInvoker(SelectionChanged, this.WorkAreaState);
                        }
                    }
                    break;
                }
            }
            else
            {
                SafeInvoker(SelectionChanged, this.WorkAreaState);
            }
        }

        // Done!
        // Todo: Account for areas that overcome paint area!
        private bool IsInsideWorkArea(VisualMapNode vmn)
        {
            if (this.WorkAreaState.TweakAgentView == true)
            {
                drawArea = this.defaultAgentView;
            }

            var was = this.WorkAreaState;
            var p1 = Vector.Transform(new Vector(vmn.Min.X, vmn.Min.Y), was.Scale, was.DeltaX, was.DeltaY);
            var p2 = Vector.Transform(new Vector(vmn.Max.X, vmn.Min.Y), was.Scale, was.DeltaX, was.DeltaY);
            var p3 = Vector.Transform(new Vector(vmn.Min.X, vmn.Max.Y), was.Scale, was.DeltaX, was.DeltaY);
            var p4 = Vector.Transform(new Vector(vmn.Max.X, vmn.Max.Y), was.Scale, was.DeltaX, was.DeltaY);

            return Helpers.IsInsideRectangle(p1, drawArea) ||
                Helpers.IsInsideRectangle(p2, drawArea) ||
                Helpers.IsInsideRectangle(p3, drawArea) ||
                Helpers.IsInsideRectangle(p4, drawArea);
        }

        // Done!
        internal void ResetWorkSpace()
        {
            var was = this.WorkAreaState;
            was.Scale = 1;
            was.DeltaX = 0;
            was.DeltaY = 0;
            was.DeltaZ = 0;
        }

        #endregion

        // Done!
        #region Paint

        // Done!
        internal void ForceUpdate()
        {
            this.workArea.Invalidate(false);
        }

        // Done!
        private void ForceUpdate(RectangleF r)
        {
            this.workArea.Invalidate(Rectangle.Round(r), false);
        }

        // Done!
        private bool ShouldDraw(VisualMapNode vmn, bool toImage)
        {
            return vmn.IsVisible == false || (toImage == false && IsInsideWorkArea(vmn) == false);
        }

        // Done!
        private void Draw(Graphics g, bool toImage)
        {
            if (this.WorkAreaState.HighQuality == true)
            {
                Helpers.SetHighQualityMode(g);
            }

            if (WorkAreaState.ShowOnlyConnectors == false)
            {
                if (WorkAreaState.IsAnchorOnTop)
                {
                    DrawGeometry(g, toImage);
                    DrawAnchorSet(g, toImage);
                }
                else
                {
                    DrawAnchorSet(g, toImage);
                    DrawGeometry(g, toImage);
                }
            }

            DrawExitSet(g, toImage);
            VisualMapNode.DrawPath(g, this.WorkAreaState);
        }

        // Done!
        private void DrawGeometry(Graphics g, bool toImage)
        {
            for (int i = 0; i < this.VisualMap.Count; ++i)
            {
                var vmn = this.VisualMap[i];
                if (ShouldDraw(vmn, toImage)) { continue; }
                vmn.DrawGeometry(g, this.WorkAreaState);
            }
        }

        // Done!
        private void DrawAnchors(Graphics g, bool toImage)
        {
            for (int i = 0; i < this.VisualMap.Count; ++i)
            {
                var vmn = this.VisualMap[i];
                if (ShouldDraw(vmn, toImage)) { continue; }
                vmn.DrawAnchors(g, this.WorkAreaState);
            }
        }

        // Done!
        private void DrawAnchorLabels(Graphics g, bool toImage)
        {
            
            for (int i = 0; i < this.VisualMap.Count; ++i)
            {
                var vmn = this.VisualMap[i];
                if (ShouldDraw(vmn, toImage)) { continue; }
                vmn.DrawAnchorLabels(g, this.WorkAreaState);
            }
        }

        // Done!
        private void DrawAnchorSet(Graphics g, bool toImage)
        {
            if (this.WorkAreaState.ShowAnchors == false)
            {
                return;
            }
            DrawAnchors(g, toImage);
            if (this.WorkAreaState.ShowAnchorLabels == true)
            {
                DrawAnchorLabels(g, toImage);
            }
        }

        // Done!
        private void DrawExits(Graphics g, bool toImage)
        {
            for (int i = 0; i < this.VisualMap.Count; ++i)
            {
                var vmn = this.VisualMap[i];
                if (ShouldDraw(vmn, toImage)) { continue; }
                vmn.DrawExits(g, this.WorkAreaState);
            }
        }

        // Done!
        private void DrawExitsLabels(Graphics g, bool toImage)
        {
            for (int i = 0; i < this.VisualMap.Count; ++i)
            {
                var vmn = this.VisualMap[i];
                if (ShouldDraw(vmn, toImage)) { continue; }
                vmn.DrawExitLabels(g, this.WorkAreaState);
            }
        }

        // Done!
        private void DrawExitSet(Graphics g, bool toImage)
        {
            if (WorkAreaState.ShowOnlyConnectors == false)
            {
                if (WorkAreaState.ShowExits == false)
                {
                    return;
                }
                DrawExits(g, toImage);
                if (WorkAreaState.ShowExitLabels)
                {
                    DrawExitsLabels(g, toImage);
                }
            }
            DrawExitsConnections(g, toImage);
        }

        // Done!
        private void DrawExitsConnections(Graphics g, bool toImage)
        {
            for (int i = 0; i < this.VisualMap.Count; ++i)
            {
                var vmn = this.VisualMap[i];
                if (ShouldDraw(vmn, toImage)) { continue; }
                vmn.DrawExitConnections(g, this.WorkAreaState);
            }
        }

        // Done!
        private void DrawSelections(Graphics g)
        {
            var artifact = GetDetectedArtifact(this.WorkAreaState);
            switch (artifact)
            {
                case Artifact.Anchor:
                {
                    VisualMapNode.DrawSelectedAnchor(g, this.WorkAreaState);
                }
                break;
                case Artifact.Exit:
                {
                    VisualMapNode.DrawSelectedExit(g, this.WorkAreaState);
                }
                break;
                case Artifact.Geometry:
                {
                    VisualMapNode.DrawSelectedGeometry(g, this.WorkAreaState);
                    if (WorkAreaState.ShowBounds)
                    {
                        VisualMapNode.DrawBounds(g, this.WorkAreaState);
                    }
                }
                break;
            }
        }

        // Done!
        private void DrawSlopes(Graphics g)
        {
            var was = this.WorkAreaState;

            if (was.SlopeVector0 == null || was.SlopeVector1 == null || was.LastSelectedAnchor == null || was.ShowSlopes == false)
            {
                return;
            }

            Vector vc = Vector.Transform(was.LastSelectedAnchor,    was.Scale, was.DeltaX, was.DeltaY);
            Vector v0 = Vector.Transform(was.SlopeVector0,          was.Scale, was.DeltaX, was.DeltaY);
            Vector v1 = Vector.Transform(was.SlopeVector1,          was.Scale, was.DeltaX, was.DeltaY);

            if (Vector.Slope(vc, v0) != Vector.Slope(v1, vc) == false)
            {
                VisualMapNode.DrawSlope(g, v0, vc, -1);
            }
            else
            {
                VisualMapNode.DrawSlope(g, v0, vc, 0);
                VisualMapNode.DrawSlope(g, v1, vc, 1);
                if (was.ShowAngles == true)
                {
                    VisualMapNode.DrawAngle(g, v0, v1, vc);
                }
            }

        }

        // Done!
        internal void ComposeImageGuide()
        {
            var was = this.WorkAreaState;
            if (this.imageGuide == null || was.ShowImageGuide == false)
            {
                this.workArea.BackgroundImage = null;
            }
            else if (was.ShowImageGuide == true)
            {
                this.workArea.BackgroundImage = Helpers.LoadImageFromBitmap(this.imageGuide, was.Scale, was.BackgroundColor, was.DeltaX, was.DeltaY, (int)(drawArea.Width + drawArea.X), (int)(drawArea.Height + drawArea.Y));
                //this.workArea.BackgroundImage = Helpers.LoadImageFromBitmap(this.imageGuide, was.Scale, was.BackgroundColor, was.DeltaX, was.DeltaY, was.Width, was.Height);
            }
        }

        #endregion

        // Done!
        #region Artifacts

        // Done!
        #region Helpers

        // Done!
        private bool IsArtifactDetectable(Point location, VisualMapNode vmn)
        {
            var was = this.WorkAreaState;
            var s = vmn.Visuals.AnchorSize;
            var p1 = Vector.Transform(new Vector(vmn.Min.X, vmn.Min.Y), was.Scale, was.DeltaX, was.DeltaY);
            var p2 = Vector.Transform(new Vector(vmn.Max.X, vmn.Max.Y), was.Scale, was.DeltaX, was.DeltaY);
            shapeSelection = new RectangleF(p1.X, p1.Y, p2.X - p1.X + s, p2.Y - p1.Y + s);
            return Helpers.IsInsideRectangle(location, shapeSelection);
        }

        // Done!
        internal void ClearArtifactEvents()
        {
            var was = this.WorkAreaState;
            was.LastMousePosition = null;
            was.SelectedAnchor = null;
            was.SelectedArtifact = Artifact.None;
            was.SelectedExit = null;
            was.SelectedShape = null;
        }

        // Done!
        private static Artifact SetState(WorkAreaState was, Artifact a)
        {
            if (a == Artifact.None)
            {
                return was.SelectedArtifact = a;
            }
            return was.SelectedArtifact = was.LastSelectedArtifact = a;
        }

        #endregion

        // Done!
        private Artifact DetectArtifact(Point location, ConcreteVisualMap cvm)
        {
            var was = this.WorkAreaState;

            this.ClearArtifactEvents();

            if (cvm.Count == 0 || location.X < 0 || location.Y < 0)
            {
                return SetState(was, Artifact.None);
            }

            Artifact a;

            if (was.IsAnchorOnTop == true)
            {
                a = SetState(was, DetectAnchors(location, cvm));
                if(a == Artifact.None)
                {
                    a = SetState(was, DetectShapes(location, cvm));
                }
                if(a != Artifact.None)
                {
                    return a;
                }
            }
            else
            {
                a = SetState(was, DetectShapes(location, cvm));
                if (a == Artifact.None)
                {
                    a = SetState(was, DetectAnchors(location, cvm));
                }
                if (a != Artifact.None)
                {
                    return a;
                }
            }

            return SetState(was, DetectExits(location, cvm));

        }

        // Done!
        private Artifact DetectExits(Point location, ConcreteVisualMap cvm)
        {
            var was = this.WorkAreaState;

            if (was.ShowExits == false || was.ShowOnlyConnectors == true)
            {
                return Artifact.None;
            }

            // Lets iterate each Shape in reverse order!
            for (int i = cvm.Count - 1; i >= 0; --i)
            {
                var vmn = cvm[i];
                if (vmn.IsLocked == true || vmn.IsVisible == false)
                {
                    continue;
                }
                
                if (IsArtifactDetectable(location, vmn) == false)
                {
                    continue;
                }
                var node = vmn.ConcreteNode;
                int size = vmn.Visuals.ExitSize;
                // Lets iterate each Exit!
                for (int k = 0; k < node.Exits.Count; ++k)
                {
                    var point = Vector.Transform(node.Exits[k].Location, was.Scale, was.DeltaX, was.DeltaY);
                    var isInside = Helpers.IsInsideRectangle(location, new RectangleF(point.X, point.Y, size, size));
                    if (isInside == true)
                    {
                        was.LastSelectedExit = was.SelectedExit = node.Exits[k];
                        was.LastSelectedShape = was.SelectedShape = vmn;
                        return Artifact.Exit;
                    }
                }
            }

            return Artifact.None;
        }

        // Done!
        private Artifact DetectAnchors(Point location, ConcreteVisualMap cvm)
        {
            var was = this.WorkAreaState;

            if (was.ShowAnchors == false || was.ShowOnlyConnectors == true)
            {
                return Artifact.None;
            }

            for (int i = cvm.Count - 1; i >= 0; --i)
            {
                var vmn = cvm[i];
                if (vmn.IsLocked == true || vmn.IsVisible == false)
                {
                    continue;
                }
                if (IsArtifactDetectable(location, vmn) == false)
                {
                    continue;
                }
                int size = vmn.Visuals.AnchorSize;
                var node = vmn.ConcreteNode;
                for (int k = 0; k < node.Geometry.Count; ++k)
                {
                    // Lets iterate each Exit!
                    var point = Vector.Transform(node.Geometry[k], was.Scale, was.DeltaX, was.DeltaY);
                    var isInside = Helpers.IsInsideRectangle(location, new RectangleF(point.X, point.Y, size, size));
                    if (isInside == true)
                    {
                        was.LastSelectedAnchor = was.SelectedAnchor = node.Geometry[k];
                        was.LastSelectedShape = was.SelectedShape = vmn;
                        return Artifact.Anchor;
                    }
                }
            }

            return Artifact.None;
    
        }

        // Done!
        private Artifact DetectShapes(Point location, ConcreteVisualMap cvm)
        {
            var was = this.WorkAreaState;
            // Lets iterate each Shape in reverse order!
            for (int i = cvm.Count - 1; i >= 0; --i)
            {
                var vmn = cvm[i];
                if (vmn.IsLocked == true || vmn.IsVisible == false)
                {
                    continue;
                }
                if (IsArtifactDetectable(location, vmn) == false)
                {
                    continue;
                }
                var isVisible = vmn.Visuals.GeometryPath.IsOutlineVisible(location, vmn.Visuals.Geometry);
                if (isVisible == true)
                {
                    was.LastSelectedShape = was.SelectedShape = vmn;
                    return Artifact.Geometry;
                }
            }
            return Artifact.None;
        }

        // Done!
        private static Artifact GetDetectedArtifact(WorkAreaState was)
        {
            if (was.SelectedExit != null)
            {
                return Artifact.Exit;
            }
            if (was.SelectedAnchor != null)
            {
                return Artifact.Anchor;
            }
            return was.SelectedShape != null ? Artifact.Geometry : Artifact.None;
        }

        #endregion

        // Done!
        #region Translate

        // Done!
        private static void TranslateAnchor(Point location, WorkAreaState was)
        {
            TranslateVector(location, was.SelectedAnchor, was);
        }

        // Done!
        private static void TranslateExit(Point location, WorkAreaState was)
        {
            TranslateVector(location, was.SelectedExit.Location, was);
        }

        // Done!
        private static void TranslateVector(Point location, Vector v, WorkAreaState was)
        {
            float dx = was.LastMousePosition.X - location.X;
            float dy = was.LastMousePosition.Y - location.Y;
            v.Translate(dx, dy, 0, was.Scale);
        }

        // Done!
        private static void TranslateShape(Point location, WorkAreaState was)
        {
            float dx = was.LastMousePosition.X - location.X;
            float dy = was.LastMousePosition.Y - location.Y;
            was.SelectedShape.ConcreteNode.Translate(dx, dy, 0, was.Scale);
        }

        #endregion

        // Done!
        #region Other

        // Done!
        internal void FakeSelection(VisualMapNode vmn)
        {
            var was = this.WorkAreaState;
            was.SelectedArtifact = was.LastSelectedArtifact = Artifact.Geometry;
            was.LastSelectedShape = was.SelectedShape = vmn;
            this.ForceUpdate();
            SafeInvoker(SelectionChanged, was);
        }

        #endregion

        // Done!
        #region Menu Controls

        // Done!
        #region Helpers

        // Done!
        private void AddNewShape()
        {
            if (this.VisualMap.Count == 0)
            {
                this.nodeController.Enabled = true;
            }

            var vmn = new VisualMapNode { IsLocked = false, IsVisible = true, ConcreteNode = new MapNode(string.Format("<NoName:{0}>", nodeCounter++), NodeType.Geometry) };
            this.VisualMap.Add(vmn);
            this.WorkAreaState.LastSelectedShape = this.WorkAreaState.SelectedShape = vmn;

            SafeInvoker(this.StateChanged, this.WorkAreaState);
            SafeInvoker(this.NodeAdded, this.WorkAreaState);
            this.ForceUpdate();
        }

        // Done!
        private Rectangle GetRepresentationBounds()
        {
            if (this.VisualMap == null || this.VisualMap.Count == 0)
            {
                return Rectangle.Empty;
            }

            float minX = int.MaxValue;
            float minY = int.MaxValue;
            float maxX = int.MinValue;
            float maxY = int.MinValue;

            for (int i = 0; i < VisualMap.Count; ++i)
            {
                var shape = VisualMap[i];
                for (int k = 0; k < shape.ConcreteNode.Geometry.Count; ++k)
                {
                    var g = shape.ConcreteNode.Geometry[k];
                    if (g.X > maxX)
                    {
                        maxX = g.X;
                    }
                    if (g.Y > maxY)
                    {
                        maxY = g.Y;
                    }

                    if (g.X < minX)
                    {
                        minX = g.X;
                    }
                    if (g.Y < minY)
                    {
                        minY = g.Y;
                    }
                }
            }

            if (this.imageGuide != null)
            {
                maxX = Math.Max(this.imageGuide.Width, maxX);
                maxY = Math.Max(this.imageGuide.Height, maxY);
            }

            maxX += Math.Abs(minX);
            maxY += Math.Abs(minY);

            return Rectangle.Round(new RectangleF(minX, minY, maxX, maxY));
        }

        #endregion

        // Done!
        private void OpenRawXmlFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = Helpers.GetOpenFileDialog(@"Open Map", Constants.XmlRawMapFilter);

            DialogResult d = ofd.ShowDialog();

            if (d != DialogResult.OK)
            {
                return;
            }

            try
            {
                OpenRawFile(ofd.FileName);
            }
            catch
            {
                MessageBox.Show(@"Could not open map");
            }
        }

        // Done!
        private void SaveRawXmlFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = Helpers.GetSaveFileDialog(@"Save Map", Constants.XmlRawMapFilter);

            DialogResult d = sfd.ShowDialog();

            if (d != DialogResult.OK)
            {
                return;
            }

            try
            {
                MapFile.XmlRawSave(sfd.FileName, this.VisualMap.ToConcreteMap());
            }
            catch
            {
                MessageBox.Show(@"Could not save map");
            }
        }

        // Done!
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Done!
        private void ResetWorkSpace_Click(object sender, EventArgs e)
        {
            this.ResetWorkSpace();
            this.ForceUpdate();
        }

        // Done!
        private void ToogleFinder_Click(object sender, EventArgs e)
        {
            this.finderControl.Visible = pathFinderToolStripMenuItem.Checked = !pathFinderToolStripMenuItem.Checked;
        }

        // Done!
        private void NewShapeMainMenu(object sender, EventArgs e)
        {
            AddNewShape();
        }

        // Done!
        private void NewShapeSubMenu(object sender, EventArgs e)
        {
            AddNewShape();
        }

        // Done!
        private void ResetWorkSpaceSubMenu_Click(object sender, EventArgs e)
        {
            this.ResetWorkSpace();
            this.ForceUpdate();
        }

        // Done!
        private void RemoveExit_Click(object sender, EventArgs e)
        {
            var was = this.WorkAreaState;
            var las = was.LastSelectedShape;
            var lae = was.SelectedExit;

            if (las == null || lae == null)
            {
                return;
            }

            las.ConcreteNode.Exits.Remove(lae);
            was.IsDirty = true;
            was.IsSaved = false;
            this.ForceUpdate();
        }

        // Done!
        private void RemoveAnchor_Click(object sender, EventArgs e)
        {
            var was = this.WorkAreaState;
            was.SelectedShape.ConcreteNode.Geometry.Remove(was.SelectedAnchor);
            was.SelectedAnchor = null;
            this.ClearSlopeEvents();
            was.IsDirty = true;
            was.IsSaved = false;
            this.ForceUpdate();
        }

        // Done!
        private void RemoveShape_Click(object sender, EventArgs e)
        {
            this.RemoveSelectedShape();
            SafeInvoker(this.StateChanged, this.WorkAreaState);
            SafeInvoker(this.NodeAdded, this.WorkAreaState);
        }

        // Done!
        private void RemoveSelectedShape()
        {
            var was = this.WorkAreaState;
            var las = was.LastSelectedShape;
            this.VisualMap.Remove(las);
            this.ClearArtifactEvents();

            if (las != null)
            {
                for (int i = 0; i < this.VisualMap.Count; ++i)
                {
                    var shape = this.VisualMap[i];
                    if (las == shape || shape.ConcreteNode.Exits == null)
                    {
                        continue;
                    }
                    for (int k = 0; k < shape.ConcreteNode.Exits.Count; ++k)
                    {
                        var exit = shape.ConcreteNode.Exits[k];
                        if (exit.Exits[0] == las.ConcreteNode)
                        {
                            exit.Exits[0] = null;
                        }
                    }
                }

            }

            this.ClearSlopeEvents();
            was.IsDirty = true;
            was.IsSaved = false;
            this.ForceUpdate();
        }

        // Done!
        private void RemoveAllExits_Click(object sender, EventArgs e)
        {
            var was = this.WorkAreaState;
            var a = was.LastSelectedShape;

            if (a == null)
            {
                return;
            }

            a.ConcreteNode.Exits.Clear();
            was.IsDirty = true;
            was.IsSaved = false;
            this.ForceUpdate();
        }

        // Done!
        private void SaveAsImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = Helpers.GetSaveFileDialog(@"Save Map Representation", Constants.PictureFilter);

            DialogResult d = sfd.ShowDialog();

            if (d != DialogResult.OK)
            {
                return;
            }

            try
            {
                var was = this.WorkAreaState;
                const int offset = 80;
                var s = GetRepresentationBounds();
                var temp = new Bitmap(s.Width + offset, s.Height + offset);
                var tempDx = was.DeltaX;
                var tempDy = was.DeltaY;
                was.DeltaX += Math.Abs(s.X);
                was.DeltaY += Math.Abs(s.Y);
                var g = Graphics.FromImage(temp);
                if (this.imageGuide != null)
                {
                    g.FillRectangle(new SolidBrush(was.BackgroundColor), new Rectangle(0, 0, temp.Width, temp.Height));
                    g.DrawImage(Helpers.LoadImageFromBitmap(this.imageGuide, 1, was.BackgroundColor, 0, 0, temp.Width, temp.Height), 0, 0);
                }
                Draw(g, true);
                was.DeltaX = tempDx;
                was.DeltaY = tempDy;
                temp.Save(sfd.FileName);
            }
            catch
            {
                MessageBox.Show(@"Could not save map representation");
            }
        }

        // Done!
        private void XnaWorkAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.xnaWorkArea.Visible = xnaWorkAreaToolStripMenuItem.Checked = !xnaWorkAreaToolStripMenuItem.Checked;
            this.workArea.Visible = !this.xnaWorkArea.Visible;
        }

        #endregion

        // Done!
        #region Open/Save Helpers

        // Done!
        private void OpenRawFile(string path)
        {
            this.VisualMap.Clear();
            this.VisualMap = ConcreteVisualMap.LoadFrom(MapFile.XmlRawOpen(path));
            var was = this.WorkAreaState;
            was.IsDirty = true;
            was.IsSaved = true;
            this.ForceUpdate();
            SafeInvoker(StateChanged, this.WorkAreaState);
            SafeInvoker(NodeAdded, this.WorkAreaState);
        }

        #endregion

        // Done!
        #region Load/Clear Image

        // Done!
        internal void LoadImageFromFile(string path)
        {
            var was = this.WorkAreaState;
            this.imageGuide = Helpers.LoadImageFromFile(path, 1, was.BackgroundColor, 0, 0, was.Width, was.Height);
            this.ComposeImageGuide();
        }

        // Done!
        internal void LoadImageFromFile()
        {
            OpenFileDialog ofd = Helpers.GetOpenFileDialog(@"Load Image from File", Constants.PictureFilter);
            ofd.Multiselect = false;

            DialogResult d = ofd.ShowDialog();

            if (d != DialogResult.OK)
            {
                return;
            }

            try
            {
                this.LoadImageFromFile(ofd.FileName);
            }
            catch
            {
                MessageBox.Show(@"Could not open picture");
            }
        }

        // Done!
        internal void ClearImage()
        {
            this.imageGuide = null;
            this.ForceUpdate();
        }

        #endregion

    }
}