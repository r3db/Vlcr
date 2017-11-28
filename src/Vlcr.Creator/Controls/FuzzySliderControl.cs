using System;
using System.Windows.Forms;

namespace Vlcr.Creator.Controls
{
    public sealed partial class FuzzySliderControl : UserControl
    {
        // Done!
        #region Events

        public event EventHandler SliderChange;
        
        #endregion

        // Done!
        #region Internal Static Data

        private const float ScaleFactor = 1.0f;

        #endregion

        // Done!
        #region Internal Instance Data

        private string label;

        #endregion

        // Done!
        #region Properties

        public string Label
        {
            get { return this.label; }
            set
            {
                this.label = value;
                SetLabelName();
            }
        }

        public float Value
        {
            get { return ScaleFactor * this.slider.Value; }
            set { this.slider.Value = (int)(value / ScaleFactor); }
        }
        
        #endregion

        // Done!
        #region .Ctor

        // Done!
        public FuzzySliderControl()
        {
            InitializeComponent();
        }

        #endregion

        // Done!
        #region Events

        // Done!
        private void SliderScroll(object sender, EventArgs e)
        {
            SetLabelName();
            if(SliderChange != null)
            {
                SliderChange.Invoke(this, e);
            }
        }

        // Done!
        private void SetLabelName()
        {
            this.name.Text = this.label;
            this.scale.Text = string.Format(" ({0}x)", this.Value);
        }

        #endregion

    }
}