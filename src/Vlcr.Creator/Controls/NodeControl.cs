using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vlcr.Map;
using Vlcr.VisualMap;

namespace Vlcr.Creator.Controls
{
    internal sealed partial class NodeControl : UserControl
    {
        // Done!
        #region Internal Instance Data

        private bool canFireArtifactEvent = true;

        #endregion

        // Done!
        #region Automatic Properties

        internal MainForm RealParent { get; private set; }
        
        #endregion

        // Done!
        #region Properties

        // Done!
        internal ConcreteVisualMap VisualMap
        {
            get
            {
                return this.RealParent.VisualMap;
            }
        }

        #endregion

        // Done!
        #region .Ctor

        // Done!
        internal NodeControl() : this(null)
        {
        }

        // Done!
        internal NodeControl(MainForm parent)
        {
            this.InitializeComponent();
            if(parent != null)
            {
                this.CtorHelper(parent);
            }
        }

        // Done!
        internal void CtorHelper(MainForm parent)
        {
            this.RealParent = parent;
            this.GenerateColors();

            this.RealParent.SelectionChanged += ArtifactSelectionChanged;
            this.RealParent.StateChanged += StateChanged;
        }

        // Done!
        private void GenerateColors()
        {
            var c = Enum.GetNames(typeof(KnownColor)).ToList();
            c.Sort();

            for (int i = 0; i < c.Count; ++i)
            {
                this.shapeLineColors.Items.Add(c[i]);
                this.shapeFillColors.Items.Add(c[i]);
            }
        }

        #endregion

        // Done!
        #region Events

        // Done!
        #region Helpers

        // Done!
        private void GenerateExitOptions(VisualMapNode vmp, MapNode e)
        {
            List<string> sort = new List<string>();
            for (int i = 0; i < this.VisualMap.Count; ++i)
            {
                if (vmp != this.VisualMap[i])
                {
                    sort.Add(this.VisualMap[i].ConcreteNode.Name);
                }
            }
            sort.Sort();

            var oc = this.listOfPossibleExits.Items;
            oc.Clear();
            oc.Add(string.Empty);
            for (int i = 0; i < sort.Count; ++i)
            {
                oc.Add(sort[i]);
            }

            this.listOfPossibleExits.Enabled = true;

            // Select an item if that's the case!
            if (e.Exits.Count > 0 &&  e.Exits[0] != null)
            {
                var i = oc.IndexOf(e.Exits[0].Name);
                this.listOfPossibleExits.SelectedIndex = i == -1 ? 0 : i;
            }
        }

        // Done!
        private void SetScale(WorkAreaState was)
        {
            var v = was.Scale;

            if (v == 1)
            {
                this.scale.Value = 0;
            }
            else if (v > 0)
            {
                this.scale.Value = (int)(was.Scale - 1);
            }
            else if (v < 0)
            {
                this.scale.Value = (int)-(Math.Pow(was.Scale, -1) + 1);
            }
        }

        // Done!
        private void BindShapesToShapeSelector()
        {
            this.shapeSelector.Items.Clear();
            this.shapeSelector.Enabled = false;

            if (this.VisualMap != null && this.VisualMap.Count > 0)
            {
                this.shapeSelector.Enabled = true;
                List<string> sort = new List<string>();
                for (int i = 0; i < this.VisualMap.Count; ++i)
                {
                    sort.Add(this.VisualMap[i].ConcreteNode.Name);
                }
                sort.Sort();
                for (int i = 0; i < sort.Count; ++i)
                {
                    this.shapeSelector.Items.Add(sort[i]);
                }
            }
        }

        #endregion

        // Done!
        private void ArtifactSelectionChanged(object sender, EventArgs e)
        {
            this.canFireArtifactEvent = false;

            var was = (WorkAreaState)sender;

            this.groupBoxLocal.Enabled = true;
            this.shapeName.Text = was.LastSelectedShape.ConcreteNode.Name;
            this.groupBoxArtifact.Name = "Artifact";
            this.artifactXCoord.Enabled = true;
            this.artifactYCoord.Enabled = true;
            this.artifactZCoord.Enabled = true;
            this.lineWidth.Enabled = true;
            this.artifactSize.Enabled = true;
            this.shapeLineColors.Enabled = false;
            this.shapeFillColors.Enabled = false;
            
            this.memory.Enabled = false;
            this.speed.Enabled = false;
            this.passable.Enabled = false;

            var shape = was.LastSelectedShape;

            switch (was.LastSelectedArtifact)
            {
                case Artifact.Anchor:
                {
                    var sa = was.LastSelectedAnchor;
                    this.groupBoxArtifact.Name = "Anchor";
                    // -----------------------------------------------------------
                    this.artifactXCoord.Value = (decimal)sa.X;
                    this.artifactYCoord.Value = (decimal)sa.Y;
                    this.artifactZCoord.Value = (decimal)sa.Z;
                    // -----------------------------------------------------------
                    this.lineWidth.Enabled = false;
                    this.artifactSize.Value = shape.Visuals.AnchorSize;
                }
                break;
                case Artifact.Exit:
                {
                    var se = was.LastSelectedExit;
                    this.groupBoxArtifact.Name = "Exit";
                    // -----------------------------------------------------------
                    this.artifactXCoord.Value = (decimal)se.Location.X;
                    this.artifactYCoord.Value = (decimal)se.Location.Y;
                    this.artifactZCoord.Value = (decimal)se.Location.Z;
                    // -----------------------------------------------------------
                    this.lineWidth.Enabled = false;
                    this.artifactSize.Value = shape.Visuals.ExitSize;
                    // -----------------------------------------------------------
                    this.GenerateExitOptions(shape, se);
                }
                break;
                case Artifact.Geometry:
                {
                    this.groupBoxArtifact.Name = "Geometry";
                    // -----------------------------------------------------------
                    this.artifactXCoord.Value = decimal.Zero;
                    this.artifactYCoord.Value = decimal.Zero;
                    this.artifactZCoord.Value = decimal.Zero;
                    // -----------------------------------------------------------
                    this.artifactXCoord.Enabled = false;
                    this.artifactYCoord.Enabled = false;
                    this.artifactZCoord.Enabled = false;
                    // -----------------------------------------------------------
                    this.lineWidth.Value = (decimal)shape.Visuals.Geometry.Width;
                    this.artifactSize.Enabled = false;
                    // -----------------------------------------------------------
                    this.shapeLineColors.Enabled = true;
                    this.shapeFillColors.Enabled = true;
                    this.shapeLineColors.SelectedIndex = this.shapeLineColors.Items.IndexOf(shape.Visuals.Geometry.Color.Name);
                    this.shapeFillColors.SelectedIndex = this.shapeFillColors.Items.IndexOf(shape.Visuals.GeometryFill.Color.Name);
                    // -----------------------------------------------------------
                    this.lockShape.Checked = shape.IsLocked;
                    this.visibleShape.Checked = shape.IsVisible;
                    this.fillShape.Checked = shape.FillShape;
                    // -----------------------------------------------------------
                    this.shapeSelector.SelectedIndex = this.shapeSelector.Items.IndexOf(shape.ConcreteNode.Name);
                    // -----------------------------------------------------------
                    this.memory.Enabled = true;
                    this.memory.Value = (decimal)shape.ConcreteNode.Constraints.Memory.Value;
                    // -----------------------------------------------------------
                    this.speed.Enabled = true;
                    this.speed.Value = (decimal)shape.ConcreteNode.Constraints.Speed.Value;
                    // -----------------------------------------------------------
                    this.passable.Enabled = true;
                    this.passable.Value = (decimal)shape.ConcreteNode.Constraints.Transitable.Value;
                }
                break;
            }

            this.canFireArtifactEvent = true;

        }

        // Done!
        private void StateChanged(object sender, EventArgs e)
        {
            var was = (WorkAreaState)sender;

            BindShapesToShapeSelector();

            // ----------------------------------------------------------------

            this.showAnchors.Checked = was.ShowAnchors;
            this.showAnchorLabels.Checked = was.ShowAnchorLabels;
            this.showExits.Checked = was.ShowExits;
            this.showExitLabels.Checked = was.ShowExitLabels;
            this.showExitConnectors.Checked = was.ShowExitConnectors;
            this.persistAnchors.Checked = was.PersistAnchor;
            this.persistExit.Checked = was.PersistExit;
            this.showName.Checked = was.ShowShapeName;
            this.showExitSources.Checked = was.ShowExitSources;
            this.showExtraDetails.Checked = was.ShowExtraDetails;
            this.showAngles.Checked = was.ShowAngles;
            this.showImageGuide.Checked = was.ShowImageGuide;
            this.showZCoordinate.Checked = was.ShowZCoordinate;
            this.persistShape.Checked = was.PersistShape;
            this.persistExtraInfo.Checked = was.PersistExtraInfo;
            this.showOnlyConnectors.Checked = was.ShowOnlyConnectors;
            this.showMST.Checked = was.ShowMST;
            this.shapeSelection.Checked = was.ShowShapeSelection;
            this.tweakAgentView.Checked = was.TweakAgentView;
            this.moveSelection.Checked = was.ShowMoveSelection;

            // ----------------------------------------------------------------

            this.normalizeCoordinates.Checked = was.Normalize;
            this.closeGeometry.Checked = was.CloseGeometry;
            this.isAnchorOnTop.Checked = was.IsAnchorOnTop;

            // ----------------------------------------------------------------

            switch (was.Action)
            {
                case WorkAreaAction.AddAnchor:  this.actionAddAnchor.Checked = true;    break;
                case WorkAreaAction.AddExit:    this.actionAddExit.Checked = true;      break;
                case WorkAreaAction.Move:       this.actionMove.Checked = true;         break;
                case WorkAreaAction.Blocked:    this.actionBlock.Checked = true;        break;
            }

            // ----------------------------------------------------------------

            SetScale(was);

        }

        #endregion

        // Done!
        #region Helpers

        // Done!
        private void ForceParentUpdate(bool events = false)
        {
            var was = this.RealParent.WorkAreaState;

            was.IsDirty = true;
            was.IsSaved = false;
            was.ShowPath = false;
            if(events == true)
            {
                this.RealParent.InvokeStateChangedEvent();
                this.RealParent.InvokeNodeAddedEvent();
            }
            this.RealParent.ForceUpdate();
        }

        // Done!
        private static void ComputeSlide(KeyEventArgs e, out int dx, out int dy, out int dz)
        {
            dx = 0;
            dy = 0;
            dz = 0;

            switch (e.KeyCode)
            {
                case Keys.Left: { if (e.Shift) { dz = -1; } else { dx = -1; } break; }
                case Keys.Right: { if (e.Shift) { dz = 1; } else { dx = 1; } break; }
                case Keys.Up: { if (e.Shift) { dz = -1; } else { dy = -1; } break; }
                case Keys.Down: { if (e.Shift) { dz = 1; } else { dy = 1; } break; }
            }

            if (e.Control == true)
            {
                dx *= 10;
                dy *= 10;
                dz *= 10;
            }
        }

        #endregion

        // Done!
        #region Control Events

        // Done!
        private void SetShapeName_Click(object sender, EventArgs e)
        {
            SetShapeName();
        }

        // Done!
        private void SetShapeName()
        {
            var name = this.shapeName.Text;
            if (string.IsNullOrEmpty(name) == true)
            {
                return;
            }

            var shape = this.RealParent.WorkAreaState.LastSelectedShape;
            var node = this.RealParent.VisualMap.FindByName(name);
            if (node == shape)
            {
                return;
            }
            if (node != null)
            {
                MessageBox.Show(@"That name is already in use");
                return;
            }

            shape.ConcreteNode.Name = name;
            this.BindShapesToShapeSelector();
            this.ForceParentUpdate();
        }

        // Done!
        private void DeleteShapes_Click(object sender, EventArgs e)
        {
            var p = this.RealParent;
            var was = p.WorkAreaState;
            p.VisualMap.Remove(this.RealParent.WorkAreaState.LastSelectedShape, true);
            p.ClearSlopeEvents();
            p.ClearArtifactEvents();
            p.InvokeStateChangedEvent();
            p.InvokeNodeAddedEvent();
            this.groupBoxLocal.Enabled = false;
            was.LastSelectedShape = null;
            this.ForceParentUpdate();
        }

        // Done!
        private void ArtifactXCoord_ValueChanged(object sender, EventArgs e)
        {
            ArtifactCoord_OnEvent();
        }

        // Done!
        private void ArtifactYCoordValueChanged(object sender, EventArgs e)
        {
            ArtifactCoord_OnEvent();
        }

        // Done!
        private void ArtifactZCoordValueChanged(object sender, EventArgs e)
        {
            ArtifactCoord_OnEvent();
        }

        // Done!
        private void ArtifactCoord_OnEvent()
        {
            if(canFireArtifactEvent == false)
            {
                return;
            }

            var was = this.RealParent.WorkAreaState;

            switch (was.LastSelectedArtifact)
            {
                case Artifact.Anchor:
                {
                    var sa = was.LastSelectedAnchor;
                    sa.X = (float)this.artifactXCoord.Value;
                    sa.Y = (float)this.artifactYCoord.Value;
                    sa.Z = (float)this.artifactZCoord.Value;
                    this.RealParent.ClearSlopeEvents();
                    this.ForceParentUpdate();
                }
                break;
                case Artifact.Exit:
                {
                    var se = was.LastSelectedExit;
                    se.Location.X = (float)this.artifactXCoord.Value;
                    se.Location.Y = (float)this.artifactYCoord.Value;
                    se.Location.Z = (float)this.artifactZCoord.Value;
                    this.RealParent.ClearSlopeEvents();
                    this.ForceParentUpdate();
                }
                break;
            }
        }

        // Done!
        private void ArtifactCoord_KeyDown(object sender, KeyEventArgs e)
        {
            int dx;
            int dy;
            int dz;

            ComputeSlide(e, out dx, out dy, out dz);

            if(dx == 0 && dy == 0 && dz == 0)
            {
                return;
            }

            var was = this.RealParent.WorkAreaState;
            var shape = was.LastSelectedShape;

            switch (was.LastSelectedArtifact)
            {
                case Artifact.Anchor:
                {
                    var sa = was.LastSelectedAnchor;
                    sa.X += dx;
                    sa.Y += dy;
                    sa.Z += dz;
                    this.artifactXCoord.Value = (decimal)sa.X;
                    this.artifactYCoord.Value = (decimal)sa.Y;
                    this.artifactZCoord.Value = (decimal)sa.Z;
                    this.RealParent.ClearSlopeEvents();
                    shape.DetermineBounds();
                    this.ForceParentUpdate();
                }
                break;
                case Artifact.Exit:
                {
                    var se = was.LastSelectedExit;
                    se.Location.X += dx;
                    se.Location.Y += dy;
                    se.Location.Z += dz;
                    this.artifactXCoord.Value = (decimal)se.Location.X;
                    this.artifactYCoord.Value = (decimal)se.Location.Y;
                    this.artifactZCoord.Value = (decimal)se.Location.Z;
                    this.RealParent.ClearSlopeEvents();
                    shape.DetermineBounds();
                    this.ForceParentUpdate();
                }
                break;
                case Artifact.Geometry:
                {
                    was.LastSelectedShape.ConcreteNode.Translate(-dx, -dy, -dz, was.Scale);
                    this.RealParent.ClearSlopeEvents();
                    shape.DetermineBounds();
                    this.ForceParentUpdate();
                }
                break;
            }

        }

        // Done!
        private void LineWidth_ValueChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            var shape = was.LastSelectedShape;
            shape.Visuals.Geometry = new Pen(shape.Visuals.Geometry.Color, (float)this.lineWidth.Value);
            this.ForceParentUpdate();
        }

        // Done!
        private void ArtifactSize_ValueChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            var shape = was.LastSelectedShape;

            switch (was.LastSelectedArtifact)
            {
                case Artifact.Anchor:
                {
                    shape.Visuals.AnchorSize = (int)this.artifactSize.Value;
                    this.ForceParentUpdate();
                }
                break;
                case Artifact.Exit:
                {
                    shape.Visuals.ExitSize = (int)this.artifactSize.Value;
                    this.ForceParentUpdate();
                }
                break;
            }
        }

        // Done!
        private void LineColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            var shape = was.LastSelectedShape;
            shape.Visuals.Geometry = new Pen(Color.FromName(this.shapeLineColors.Items[this.shapeLineColors.SelectedIndex].ToString()), shape.Visuals.Geometry.Width);
            this.ForceParentUpdate();
        }

        // Done!
        private void FillColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            var shape = was.LastSelectedShape;
            shape.Visuals.GeometryFill = new SolidBrush(Color.FromName(this.shapeFillColors.Items[this.shapeFillColors.SelectedIndex].ToString()));
            this.ForceParentUpdate();
        }

        // Done!
        private void LockShape_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            var shape = was.LastSelectedShape;
            shape.IsLocked = this.lockShape.Checked;
            was.IsDirty = true;
            was.IsSaved = false;
            this.ForceParentUpdate();
        }

        // Done!
        private void VisibleShape_CheckedChanged(object sender, EventArgs e)
        {
            var shape = this.RealParent.WorkAreaState.LastSelectedShape;
            shape.IsVisible = this.visibleShape.Checked;
            this.ForceParentUpdate();
        }

        // Done!
        private void FillShape_CheckedChanged(object sender, EventArgs e)
        {
            var shape = this.RealParent.WorkAreaState.LastSelectedShape;
            shape.FillShape = this.fillShape.Checked;
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowAnchors_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowAnchors = this.showAnchors.Checked;
            if(was.ShowAnchors == false)
            {
                this.RealParent.ClearSlopeEvents();
                was.ShowAnchorLabels = this.showAnchorLabels.Checked = false;
                was.PersistAnchor = this.persistAnchors.Checked = false;
                was.ShowExtraDetails = this.showExtraDetails.Checked = false;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowAnchorLabels_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowAnchorLabels = this.showAnchorLabels.Checked;
            if (was.ShowAnchorLabels == true)
            {
                was.ShowAnchors = this.showAnchors.Checked = true;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowExits_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowExits = this.showExits.Checked;
            if (was.ShowExits == false)
            {
                was.ShowExitLabels = this.showExitLabels.Checked = false;
                was.ShowExitConnectors = this.showExitConnectors.Checked = false;
                was.ShowExitSources = this.showExitSources.Checked = false;
                was.PersistExit = this.persistExit.Checked = false;
                was.ShowConnectorColors = this.showConnectorColors.Checked = false;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowExitLabels_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowExitLabels = this.showExitLabels.Checked;
            if (was.ShowExitLabels == true)
            {
                was.ShowExits = this.showExits.Checked = true;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowExitConnectors_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowExitConnectors = this.showExitConnectors.Checked;
            if (was.ShowExitConnectors == true)
            {
                was.ShowExits = this.showExits.Checked = true;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowExitSources_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowExitSources = this.showExitSources.Checked;
            if (was.ShowExitSources == true)
            {
                was.ShowExits = this.showExits.Checked = true;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void PersistAnchors_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.PersistAnchor = this.persistAnchors.Checked;
            if (was.PersistAnchor == true)
            {
                was.ShowAnchors = this.showAnchors.Checked = true;
            }
            else
            {
                was.ShowExtraDetails = this.showExtraDetails.Checked = false;
                was.ShowAngles = this.showAngles.Checked = false;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void PersistExit_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.PersistExit = this.persistExit.Checked;
            if (was.PersistExit == true)
            {
                was.ShowExits = this.showExits.Checked = true;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowName_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowShapeName = this.showName.Checked;
            if (was.ShowShapeName == false)
            {
                was.ShowMST = this.showMST.Checked = false;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowMST_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowMST = this.showMST.Checked;
            if (was.ShowMST == true)
            {
                was.ShowShapeName = this.showName.Checked = true;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowExtraDetails_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowExtraDetails = this.showExtraDetails.Checked;
            if (was.ShowExtraDetails == false)
            {
                this.RealParent.ClearSlopeEvents();
                was.ShowAngles = this.showAngles.Checked = false;
            }
            else
            {
                was.ShowAnchors = this.showAnchors.Checked = true;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowAngles_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowAngles = this.showAngles.Checked;
            if (was.ShowAngles == true)
            {
                this.RealParent.ClearSlopeEvents();
                was.ShowAnchors = this.showAnchors.Checked = true;
                was.ShowExtraDetails = this.showExtraDetails.Checked = true;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowImageGuide_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowImageGuide = this.showImageGuide.Checked;
            this.RealParent.ComposeImageGuide();
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowZCoordinate_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowZCoordinate = this.showZCoordinate.Checked;
            this.ForceParentUpdate();

        }

        // Done!
        private void PersistShape_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.PersistShape = this.persistShape.Checked;
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowOnlyConnectors_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowOnlyConnectors = this.showOnlyConnectors.Checked;
            this.ForceParentUpdate();
        }

        // Done!
        private void NormalizeCoordinates_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.Normalize = this.normalizeCoordinates.Checked;
            this.ForceParentUpdate();
        }

        // Done!
        private void CloseGeometry_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.CloseGeometry = this.closeGeometry.Checked;
            this.ForceParentUpdate();
        }

        // Done!
        private void AnchorOnTop_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.IsAnchorOnTop = this.isAnchorOnTop.Checked;
            this.ForceParentUpdate();
        }

        // Done!
        private void ShapeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = this.shapeSelector.Items[this.shapeSelector.SelectedIndex].ToString();
            this.RealParent.FakeSelection(this.VisualMap.FindByName(name));
        }

        // Done!
        private void ActionAddAnchor_CheckedChanged(object sender, EventArgs e)
        {
            this.RealParent.WorkAreaState.Action = WorkAreaAction.AddAnchor;
        }

        // Done!
        private void ActionAddExit_CheckedChanged(object sender, EventArgs e)
        {
            this.RealParent.WorkAreaState.Action = WorkAreaAction.AddExit;
        }

        // Done!
        private void ActionMove_CheckedChanged(object sender, EventArgs e)
        {
            this.RealParent.WorkAreaState.Action = WorkAreaAction.Move;
        }

        // Done!
        private void ActionBlock_CheckedChanged(object sender, EventArgs e)
        {
            this.RealParent.WorkAreaState.Action = WorkAreaAction.Blocked;
        }

        // Done!
        private void ListOfPossibleExits_SelectedIndexChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            var se = was.LastSelectedExit;
            var name = this.listOfPossibleExits.Items[this.listOfPossibleExits.SelectedIndex].ToString();
            var ex = se.Exits;

            if(string.IsNullOrEmpty(name))
            {
                ex.Clear();
                this.ForceParentUpdate();
                return;
            }

            var node = this.RealParent.VisualMap.FindByName(name);

            if(ex.Count != 0)
            {
                ex.Clear();
            }

            ex.Add(node.ConcreteNode);
            this.ForceParentUpdate();
        }

        // Done!
        private void Scale_Scroll(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            var v = this.scale.Value;

            if (v == 0)
            {
                was.Scale = 1;
            }
            else if (v > 0)
            {
                was.Scale = (v + 1);
            }
            else if (v < 0)
            {
                was.Scale = -(1.0f / (v - 1));
            }

            this.RealParent.ComposeImageGuide();
            this.ForceParentUpdate();
        }

        // Done!
        private void ResetTransformations_Click(object sender, EventArgs e)
        {
            this.RealParent.ResetWorkSpace();
            this.scale.Value = 1;
            this.RealParent.ComposeImageGuide();
            this.ForceParentUpdate();
        }

        // Done!
        private void MemoryChanged(object sender, EventArgs e)
        {
            var shape = this.RealParent.WorkAreaState.LastSelectedShape;
            var mem = shape.ConcreteNode.Constraints.Memory;
            mem.Value = (float)this.memory.Value;
            this.ForceParentUpdate();
        }

        // Done!
        private void SpeedChanged(object sender, EventArgs e)
        {
            var shape = this.RealParent.WorkAreaState.LastSelectedShape;
            var mem = shape.ConcreteNode.Constraints.Speed;
            mem.Value = (float)this.speed.Value;
            this.ForceParentUpdate();
        }

        // Done!
        private void PassableChanged(object sender, EventArgs e)
        {
            var shape = this.RealParent.WorkAreaState.LastSelectedShape;
            var mem = shape.ConcreteNode.Constraints.Transitable;
            mem.Value = (float)this.passable.Value;
            this.ForceParentUpdate();
        }

        // Done!
        private void ShowConnectorColors(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowConnectorColors = this.showConnectorColors.Checked;
            if (was.ShowConnectorColors == true)
            {
                was.ShowExits = this.showExits.Checked = true;
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void ResetMemory_Click(object sender, EventArgs e)
        {
            this.VisualMap.ResetMemory();
            this.ForceParentUpdate();
        }

        // Done!
        private void ResetSpeed_Click(object sender, EventArgs e)
        {
            this.VisualMap.ResetSpeed();
            this.ForceParentUpdate();
        }

        // Done!
        private void ResetTransitable_Click(object sender, EventArgs e)
        {
            this.VisualMap.ResetTransitable();
            this.ForceParentUpdate();
        }

        // Done!
        private void ResetWorkbench_Click(object sender, EventArgs e)
        {
            this.VisualMap.Clear();
            this.RealParent.ClearImage();
            this.RealParent.ComposeImageGuide();
            this.ForceParentUpdate(true);
        }

        // Done!
        private void LoadImageGuide_Click(object sender, EventArgs e)
        {
            this.RealParent.LoadImageFromFile();
        }

        // Done!
        private void ClearImageGuide_Click(object sender, EventArgs e)
        {
            this.RealParent.ClearImage();
            this.RealParent.ComposeImageGuide();
            this.ForceParentUpdate();
        }

        // Done!
        private void BringShapeToFront_Click(object sender, EventArgs e)
        {
            var p = this.RealParent;
            var was = p.WorkAreaState;
            p.VisualMap.BringToFront(was.LastSelectedShape);
            this.ForceParentUpdate();
        }

        // Done!
        private void SendShapeToBack_Click(object sender, EventArgs e)
        {
            var p = this.RealParent;
            var was = p.WorkAreaState;
            p.VisualMap.SendToBack(was.LastSelectedShape);
            this.ForceParentUpdate();
        }

        // Done!
        private void ShapeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetShapeName();
            }
        }

        // Done!
        private void MainSelection_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowShapeSelection = this.shapeSelection.Checked;
            this.ForceParentUpdate();
        }

        // Done!
        private void TweakAgentView_CheckedChanged(object sender, EventArgs e)
        {
            var p = this.RealParent;
            var was = p.WorkAreaState;
            was.TweakAgentView = this.tweakAgentView.Checked;
            if (was.TweakAgentView == false)
            {
                p.ForceResizeEvent();
            }
            this.ForceParentUpdate();
        }

        // Done!
        private void MoveSelection_CheckedChanged(object sender, EventArgs e)
        {
            var was = this.RealParent.WorkAreaState;
            was.ShowMoveSelection = this.moveSelection.Checked;
            this.ForceParentUpdate();
        }

        #endregion

    }
}