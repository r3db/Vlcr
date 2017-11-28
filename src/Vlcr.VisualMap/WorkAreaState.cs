using System;
using System.Collections.Generic;
using System.Drawing;
using Vlcr.Core;
using Vlcr.Map;

namespace Vlcr.VisualMap
{
    // Done!
    [Serializable]
    public sealed class WorkAreaState
    {
        // Done!
        #region Automatic Properties

        public int              Width                   { get; set; }
        public int              Height                  { get; set; }
        public bool             IsMouseDown             { get; set; }

        public Vector           SelectedAnchor          { get; set; }
        public Vector           LastSelectedAnchor      { get; set; }
        public VisualMapNode    SelectedShape           { get; set; }
        public VisualMapNode    LastSelectedShape       { get; set; }
        public MapNode          SelectedExit            { get; set; }
        public MapNode          LastSelectedExit        { get; set; }

        public Artifact         SelectedArtifact        { get; set; }
        public Artifact         LastSelectedArtifact    { get; set; }

        public Vector           LastMousePosition       { get; set; }

        public Vector           SlopeVector0            { get; set; }
        public Vector           SlopeVector1            { get; set; }

        public bool             PersistAnchor           { get; set; }
        public bool             PersistExit             { get; set; }
        public bool             PersistShape            { get; set; }
        public bool             PersistExtraInfo        { get; set; }
        public bool             ShowSlopes              { get; set; }
        
        public float            Scale                   { get; set; }
        public float            DeltaX                  { get; set; }
        public float            DeltaY                  { get; set; }
        public float            DeltaZ                  { get; set; }
        public bool             CloseGeometry           { get; set; }
        public bool             Normalize               { get; set; }
        public bool             ShowZCoordinate         { get; set; }
        public bool             ShowAnchorLabels        { get; set; }
        public bool             ShowAnchors             { get; set; }
        public bool             ShowExitLabels          { get; set; }
        public bool             ShowExits               { get; set; }
        public bool             ShowExitConnectors      { get; set; }
        public bool             ShowExitSources         { get; set; }
        public bool             ShowConnectorColors     { get; set; }
        public bool             ShowExtraDetails        { get; set; }
        public bool             ShowAngles              { get; set; }
        public bool             ShowImageGuide          { get; set; }
        public bool             ShowShapeName           { get; set; }
        public bool             ShowMST                 { get; set; }
        public Color            BackgroundColor         { get; set; }
        public bool             IsSaved                 { get; set; }
        public bool             ShowOnlyConnectors      { get; set; }
        public bool             IsAnchorOnTop           { get; set; }
        public WorkAreaAction   Action                  { get; set; }
        public bool             IsDirty                 { get; set; }
        public bool             HighQuality             { get; set; }
        public bool             ShowBounds              { get; set; }
        public bool             Redraw                  { get; set; }
        public WorkAreaState    Backup                  { get; set; }
        public IList<MapNode>   Path                    { get; set; }
        public bool             ShowPath                { get; set; }

        public bool             ShowShapeSelection      { get; set; }
        public bool             TweakAgentView          { get; set; }
        public bool             ShowMoveSelection       { get; set; }
 
        #endregion

        // Done!
        #region Static Properties

        public static WorkAreaState Default
        {
            get
            {
                return new WorkAreaState
                {
                    Width                   = 0,
                    Height                  = 0,
                    IsMouseDown             = false,
                    SelectedAnchor          = null,
                    LastSelectedAnchor      = null,
                    SelectedShape           = null,
                    LastSelectedShape       = null,
                    SelectedExit            = null,
                    LastSelectedExit        = null,
                    SelectedArtifact        = Artifact.None,
                    LastSelectedArtifact    = Artifact.None,
                    LastMousePosition       = null,
                    SlopeVector0            = null,
                    SlopeVector1            = null,
                    PersistAnchor           = true,
                    PersistExit             = true,
                    PersistShape            = true,
                    PersistExtraInfo        = true,
                    ShowSlopes              = true,
                    Scale                   = 1,
                    DeltaX                  = 0,
                    DeltaY                  = 0,
                    DeltaZ                  = 0,
                    CloseGeometry           = true,
                    Normalize               = false,
                    ShowZCoordinate         = false,
                    ShowAnchorLabels        = true,
                    ShowAnchors             = true,
                    ShowExitLabels          = true,
                    ShowExits               = true,
                    ShowExitConnectors      = true,
                    ShowExitSources         = true,
                    ShowExtraDetails        = true,
                    ShowAngles              = true,
                    ShowImageGuide          = true,
                    ShowShapeName           = true,
                    ShowMST                 = true,
                    BackgroundColor         = Color.LightYellow,
                    IsSaved                 = true,
                    ShowOnlyConnectors      = false,
                    IsAnchorOnTop           = true,
                    Action                  = WorkAreaAction.AddAnchor,
                    IsDirty                 = true,
                    HighQuality             = true,
                    ShowBounds              = true,
                    Redraw                  = false,
                    Backup                  = null,
                    ShowShapeSelection      = true,
                    TweakAgentView          = false,
                    ShowMoveSelection       = true,
                };
            }
        }

        #endregion

        // Done!
        #region Static Methods

        public static WorkAreaState LowQuality(WorkAreaState was)
        {
            return new WorkAreaState
            {
                Width                   = was.Width,
                Height                  = was.Height,
                IsMouseDown             = was.IsMouseDown,
                SelectedAnchor          = was.SelectedAnchor,
                LastSelectedAnchor      = was.LastSelectedAnchor,
                SelectedShape           = was.SelectedShape,
                LastSelectedShape       = was.LastSelectedShape,
                SelectedExit            = was.SelectedExit,
                LastSelectedExit        = was.LastSelectedExit,
                SelectedArtifact        = was.SelectedArtifact,
                LastSelectedArtifact    = was.LastSelectedArtifact,
                LastMousePosition       = was.LastMousePosition,
                SlopeVector0            = was.SlopeVector0,
                SlopeVector1            = was.SlopeVector1,
                PersistAnchor           = false,
                PersistExit             = false,
                PersistShape            = false,
                PersistExtraInfo        = false,
                ShowSlopes              = false,
                Scale                   = was.Scale,
                DeltaX                  = was.DeltaX,
                DeltaY                  = was.DeltaY,
                DeltaZ                  = was.DeltaZ,
                CloseGeometry           = true,
                Normalize               = false,
                ShowZCoordinate         = false,
                ShowAnchorLabels        = false,
                ShowAnchors             = false,
                ShowExitLabels          = false,
                ShowExits               = false,
                ShowExitConnectors      = false,
                ShowExitSources         = false,
                ShowExtraDetails        = false,
                ShowAngles              = false,
                ShowImageGuide          = false,
                ShowShapeName           = false,
                ShowMST                 = false,
                BackgroundColor         = was.BackgroundColor,
                IsSaved                 = was.IsSaved,
                ShowOnlyConnectors      = was.ShowOnlyConnectors,
                IsAnchorOnTop           = was.IsAnchorOnTop,
                Action                  = was.Action,
                IsDirty                 = was.IsDirty,
                HighQuality             = false,
                ShowBounds              = false,
                Redraw                  = true,
                Backup                  = was,
                ShowShapeSelection      = was.ShowShapeSelection,
                TweakAgentView          = was.TweakAgentView,
                ShowMoveSelection       = was.ShowMoveSelection,
            };
        }

        #endregion
    }
}