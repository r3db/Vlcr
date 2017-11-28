using System;

namespace Vlcr.Core
{
    public static class Constants
    {
        public const string ConditionalDebug    = "DEBUG";
        public const string ApplicationTitle    = "Cognitive Map [Creator, Visualizer, Path Finder]";
        public const string PictureFilter       = "(Any Picture File)|*.bmp;*.jpg;*.gif*.jpeg;*.png;*.tif;";

        public static readonly string RawBinaryMapExtension     = "rbmf";
        public static readonly string RawXmlMapExtension        = "rxmf";
        public static readonly string VisualBinaryMapExtension  = "vbmf";
        public static readonly string BinaryRawMapFilter        = "(Raw Binary Map File)|*." + RawBinaryMapExtension;
        public static readonly string XmlRawMapFilter           = "(Raw Xml Map File)|*." + RawXmlMapExtension;
        public static readonly string BinaryVisualMapFilter     = "(Visual Binary Map File)|*." + VisualBinaryMapExtension;
    }
}
