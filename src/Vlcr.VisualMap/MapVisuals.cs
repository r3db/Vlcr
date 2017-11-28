using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using Vlcr.Core;

namespace Vlcr.VisualMap
{
    // Done!
    [Serializable]
    public sealed class MapVisuals : ISerializable
    {
        // Done!
        #region Automatic Properties

        public SolidBrush       AnchorFill                  { get; set; }
        public Pen              AnchorBorder                { get; set; }
        public Font             AnchorFont                  { get; set; }
        public int              AnchorSize                  { get; set; }
        public Pen              AnchorLabel                 { get; set; }

        public SolidBrush       ExitFill                    { get; set; }
        public Pen              ExitBorder                  { get; set; }
        public Pen              ExitConnectors              { get; set; }
        public Pen              ExitSourceConnectors        { get; set; }
        public Pen              ExitCross                   { get; set; }
        public int              ExitSize                    { get; set; }
        public Pen              ExitLabel                   { get; set; }

        public Pen              Geometry                    { get; set; }
        public Pen              ActiveGeometry              { get; set; }
        public SolidBrush       GeometryFill                { get; set; }
        public SolidBrush       GeometryPathFill            { get; set; }

        public Pen              SelectionMarginLine         { get; set; }
        public SolidBrush       SelectionMarginArea         { get; set; }

        public Pen              NameLabel                   { get; set; }
        public GraphicsPath     GeometryPath                { get; private set; }
        public GraphicsPath     GeometryNamePath            { get; private set; }
        public GraphicsPath     AnchorPath                  { get; private set; }
        public GraphicsPath     ExitPath                    { get; private set; }
        public GraphicsPath     ExitCrossPath               { get; private set; }
        public GraphicsPath     ExitConnectorPath           { get; private set; }
        public GraphicsPath     AnchorLabelPath             { get; private set; }
        public GraphicsPath     ExitLabelPath               { get; private set; }
        public GraphicsPath     ExitSourceConnectorsPath    { get; private set; }
        
        #endregion

        // Done!
        #region Automatic Static Properties

        public static Font ProgramFont { get; set; }

        #endregion

        // Done!
        #region .Ctor

        // Done!
        public MapVisuals()
        {
            this.AnchorFill = new SolidBrush(Color.White);
            this.AnchorBorder = Pens.Black;
            this.AnchorFont = new Font("Consolas", 9f);
            this.AnchorSize = 5;
            this.AnchorLabel = Pens.Black;

            this.ExitFill = new SolidBrush(Color.Green);
            this.ExitBorder = Pens.Blue;
            this.ExitConnectors = new Pen(Color.Green, 1f) { DashStyle = DashStyle.Solid };
            this.ExitSourceConnectors = new Pen(Color.Red, 1f) { DashStyle = DashStyle.Solid };
            this.ExitCross = Pens.Red;
            this.ExitSize = 5;
            this.ExitLabel = Pens.Red;

            this.Geometry = new Pen(Color.Blue, 1.0f);
            this.ActiveGeometry = new Pen(Color.Red, 5.0f);
            this.GeometryFill = new SolidBrush(Color.Green);
            this.GeometryPathFill = new SolidBrush(Color.Green);

            this.SelectionMarginLine = new Pen(Color.Green, 3f) { DashCap = DashCap.Flat, DashStyle = DashStyle.Dash };
            this.SelectionMarginArea = new SolidBrush(Color.Green);

            this.NameLabel = Pens.Black;

            InitializePathData();
        }

        // Done!
        private void InitializePathData()
        {
            this.GeometryPath = new GraphicsPath();
            this.GeometryNamePath = new GraphicsPath();
            this.AnchorPath = new GraphicsPath();
            this.ExitPath = new GraphicsPath();
            this.ExitCrossPath = new GraphicsPath();
            this.ExitConnectorPath = new GraphicsPath();
            this.AnchorLabelPath = new GraphicsPath();
            this.ExitLabelPath = new GraphicsPath();
            this.ExitSourceConnectorsPath = new GraphicsPath();
        }

        #endregion

        // Done!
        #region .CCtor

        // Done!
        static MapVisuals()
        {
            ProgramFont = new Font("Consolas", 8f, FontStyle.Regular);
        }

        #endregion

        // Done!
        #region ISerializable Members

        // Done!
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            SerializationHelper.StoreSolidBrush(info, this.AnchorFill, "AnchorFill");
            SerializationHelper.StorePen(info, this.AnchorBorder, "AnchorBorder");
            SerializationHelper.StoreFont(info, this.AnchorFont, "AnchorFont");
            SerializationHelper.StoreInt(info, this.AnchorSize, "AnchorSize");
            SerializationHelper.StorePen(info, this.AnchorLabel, "AnchorLabel");

            SerializationHelper.StoreSolidBrush(info, this.AnchorFill, "ExitFill");
            SerializationHelper.StorePen(info, this.AnchorBorder, "ExitBorder");
            SerializationHelper.StorePen(info, this.ExitConnectors, "ExitConnectors");
            SerializationHelper.StorePen(info, this.ExitSourceConnectors, "ExitSourceConnectors");
            SerializationHelper.StorePen(info, this.ExitCross, "ExitCross");
            SerializationHelper.StoreInt(info, this.ExitSize, "ExitsSize");
            SerializationHelper.StorePen(info, this.ExitLabel, "ExitLabel");

            SerializationHelper.StorePen(info, this.Geometry, "Geometry");
            SerializationHelper.StorePen(info, this.ActiveGeometry, "ActiveGeometry");
            SerializationHelper.StoreSolidBrush(info, this.GeometryFill, "GeometryFill");
            SerializationHelper.StoreSolidBrush(info, this.GeometryPathFill, "GeometryPathFill");

            SerializationHelper.StorePen(info, this.SelectionMarginLine, "SelectionMarginLine");
            SerializationHelper.StoreSolidBrush(info, this.SelectionMarginArea, "SelectionMarginArea");

            SerializationHelper.StorePen(info, this.NameLabel, "NameLabel");
        }

        // Done!
        private MapVisuals(SerializationInfo info, StreamingContext context)
        {
            this.AnchorFill             = SerializationHelper.RetrieveSolidBrush(info, "AnchorFill");
            this.AnchorBorder           = SerializationHelper.RetrievePen(info, "AnchorBorder");
            this.AnchorFont             = SerializationHelper.RetrieveFont(info, "AnchorFont");
            this.AnchorSize             = SerializationHelper.RetrieveInt(info, "AnchorSize");
            this.AnchorLabel            = SerializationHelper.RetrievePen(info, "AnchorLabel");

            this.ExitFill               = SerializationHelper.RetrieveSolidBrush(info, "ExitFill");
            this.ExitBorder             = SerializationHelper.RetrievePen(info, "ExitBorder");
            this.ExitConnectors         = SerializationHelper.RetrievePen(info, "ExitConnectors");
            this.ExitSourceConnectors   = SerializationHelper.RetrievePen(info, "ExitSourceConnectors");
            this.ExitCross              = SerializationHelper.RetrievePen(info, "ExitCross");
            this.ExitSourceConnectors   = SerializationHelper.RetrievePen(info, "ExitSourceConnectors");
            this.ExitSize               = SerializationHelper.RetrieveInt(info, "ExitsSize");
            this.ExitLabel              = SerializationHelper.RetrievePen(info, "ExitLabel");

            this.Geometry               = SerializationHelper.RetrievePen(info, "Geometry");
            this.ActiveGeometry         = SerializationHelper.RetrievePen(info, "ActiveGeometry");
            this.GeometryFill           = SerializationHelper.RetrieveSolidBrush(info, "GeometryFill");
            this.GeometryPathFill       = SerializationHelper.RetrieveSolidBrush(info, "GeometryPathFill");

            this.SelectionMarginLine    = SerializationHelper.RetrievePen(info, "SelectionMarginLine");
            this.SelectionMarginArea    = SerializationHelper.RetrieveSolidBrush(info, "SelectionMarginArea");

            this.NameLabel              = SerializationHelper.RetrievePen(info, "NameLabel");
            
            this.InitializePathData();
        }

        #endregion

    }
}
