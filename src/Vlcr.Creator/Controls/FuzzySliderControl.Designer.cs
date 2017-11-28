namespace Vlcr.Creator.Controls
{
    partial class FuzzySliderControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.slider = new System.Windows.Forms.TrackBar();
            this.name = new System.Windows.Forms.TextBox();
            this.scale = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.slider)).BeginInit();
            this.SuspendLayout();
            // 
            // slider
            // 
            this.slider.LargeChange = 2;
            this.slider.Location = new System.Drawing.Point(110, 0);
            this.slider.Maximum = 20;
            this.slider.Name = "slider";
            this.slider.Size = new System.Drawing.Size(176, 45);
            this.slider.TabIndex = 1;
            this.slider.Scroll += new System.EventHandler(this.SliderScroll);
            // 
            // name
            // 
            this.name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.name.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.name.Location = new System.Drawing.Point(4, 4);
            this.name.Multiline = true;
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Size = new System.Drawing.Size(82, 20);
            this.name.TabIndex = 2;
            // 
            // scale
            // 
            this.scale.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scale.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.scale.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scale.Location = new System.Drawing.Point(87, 4);
            this.scale.Multiline = true;
            this.scale.Name = "scale";
            this.scale.ReadOnly = true;
            this.scale.Size = new System.Drawing.Size(26, 20);
            this.scale.TabIndex = 3;
            // 
            // FuzzySliderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scale);
            this.Controls.Add(this.name);
            this.Controls.Add(this.slider);
            this.Name = "FuzzySliderControl";
            this.Size = new System.Drawing.Size(289, 32);
            ((System.ComponentModel.ISupportInitialize)(this.slider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar slider;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox scale;

    }
}
