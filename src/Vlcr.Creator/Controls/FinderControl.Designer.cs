namespace Vlcr.Creator.Controls
{
    partial class FinderControl
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.reversePath = new System.Windows.Forms.Button();
            this.closeNode = new System.Windows.Forms.ComboBox();
            this.clearPath = new System.Windows.Forms.Button();
            this.computePath = new System.Windows.Forms.Button();
            this.startNode = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cognitive = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.buttonDemo1 = new System.Windows.Forms.Button();
            this.buttonDemo3 = new System.Windows.Forms.Button();
            this.buttonDemo2 = new System.Windows.Forms.Button();
            this.buttonDemo5 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonDemo4 = new System.Windows.Forms.Button();
            this.exploreFS = new Vlcr.Creator.Controls.FuzzySliderControl();
            this.timeFS = new Vlcr.Creator.Controls.FuzzySliderControl();
            this.boldFS = new Vlcr.Creator.Controls.FuzzySliderControl();
            this.temperamentalFS = new Vlcr.Creator.Controls.FuzzySliderControl();
            this.greedyFS = new Vlcr.Creator.Controls.FuzzySliderControl();
            this.memoryFS = new Vlcr.Creator.Controls.FuzzySliderControl();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.reversePath);
            this.groupBox3.Controls.Add(this.closeNode);
            this.groupBox3.Controls.Add(this.clearPath);
            this.groupBox3.Controls.Add(this.computePath);
            this.groupBox3.Controls.Add(this.startNode);
            this.groupBox3.Location = new System.Drawing.Point(5, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(283, 97);
            this.groupBox3.TabIndex = 50;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Finder";
            // 
            // reversePath
            // 
            this.reversePath.Location = new System.Drawing.Point(221, 65);
            this.reversePath.Name = "reversePath";
            this.reversePath.Size = new System.Drawing.Size(56, 26);
            this.reversePath.TabIndex = 52;
            this.reversePath.Text = "Reverse";
            this.reversePath.UseVisualStyleBackColor = true;
            this.reversePath.Click += new System.EventHandler(this.ReservePath_Click);
            // 
            // closeNode
            // 
            this.closeNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.closeNode.FormattingEnabled = true;
            this.closeNode.Location = new System.Drawing.Point(5, 41);
            this.closeNode.Name = "closeNode";
            this.closeNode.Size = new System.Drawing.Size(272, 21);
            this.closeNode.TabIndex = 42;
            // 
            // clearPath
            // 
            this.clearPath.Location = new System.Drawing.Point(62, 65);
            this.clearPath.Name = "clearPath";
            this.clearPath.Size = new System.Drawing.Size(56, 26);
            this.clearPath.TabIndex = 51;
            this.clearPath.Text = "Clear";
            this.clearPath.UseVisualStyleBackColor = true;
            this.clearPath.Click += new System.EventHandler(this.ClearPath_Click);
            // 
            // computePath
            // 
            this.computePath.Location = new System.Drawing.Point(5, 65);
            this.computePath.Name = "computePath";
            this.computePath.Size = new System.Drawing.Size(56, 26);
            this.computePath.TabIndex = 50;
            this.computePath.Text = "Path";
            this.computePath.UseVisualStyleBackColor = true;
            this.computePath.Click += new System.EventHandler(this.ComputePath_Click);
            // 
            // startNode
            // 
            this.startNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startNode.FormattingEnabled = true;
            this.startNode.Location = new System.Drawing.Point(5, 17);
            this.startNode.Name = "startNode";
            this.startNode.Size = new System.Drawing.Size(272, 21);
            this.startNode.TabIndex = 49;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cognitive);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(6, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 66);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show";
            // 
            // cognitive
            // 
            this.cognitive.AutoSize = true;
            this.cognitive.Checked = true;
            this.cognitive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cognitive.Location = new System.Drawing.Point(117, 13);
            this.cognitive.Name = "cognitive";
            this.cognitive.Size = new System.Drawing.Size(70, 17);
            this.cognitive.TabIndex = 55;
            this.cognitive.Text = "Cognitive";
            this.cognitive.UseVisualStyleBackColor = true;
            this.cognitive.CheckedChanged += new System.EventHandler(this.Cognitive_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(6, 45);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(80, 17);
            this.checkBox3.TabIndex = 54;
            this.checkBox3.Text = "Agent View";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(6, 29);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(67, 17);
            this.checkBox2.TabIndex = 53;
            this.checkBox2.Text = "Full Path";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(91, 17);
            this.checkBox1.TabIndex = 52;
            this.checkBox1.Text = "Natural Curve";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // buttonDemo1
            // 
            this.buttonDemo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDemo1.Location = new System.Drawing.Point(6, 353);
            this.buttonDemo1.Name = "buttonDemo1";
            this.buttonDemo1.Size = new System.Drawing.Size(138, 26);
            this.buttonDemo1.TabIndex = 53;
            this.buttonDemo1.Text = "Hall Section:V > 2.35";
            this.buttonDemo1.UseVisualStyleBackColor = true;
            this.buttonDemo1.Click += new System.EventHandler(this.Demo1_Click);
            // 
            // buttonDemo3
            // 
            this.buttonDemo3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDemo3.Location = new System.Drawing.Point(6, 405);
            this.buttonDemo3.Name = "buttonDemo3";
            this.buttonDemo3.Size = new System.Drawing.Size(138, 26);
            this.buttonDemo3.TabIndex = 56;
            this.buttonDemo3.Text = "2.53 > 2.44";
            this.buttonDemo3.UseVisualStyleBackColor = true;
            this.buttonDemo3.Click += new System.EventHandler(this.Demo3_Click);
            // 
            // buttonDemo2
            // 
            this.buttonDemo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDemo2.Location = new System.Drawing.Point(6, 379);
            this.buttonDemo2.Name = "buttonDemo2";
            this.buttonDemo2.Size = new System.Drawing.Size(138, 26);
            this.buttonDemo2.TabIndex = 57;
            this.buttonDemo2.Text = "2.35 > Hall Section:V";
            this.buttonDemo2.UseVisualStyleBackColor = true;
            this.buttonDemo2.Click += new System.EventHandler(this.Demo2_Click);
            // 
            // buttonDemo5
            // 
            this.buttonDemo5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDemo5.Location = new System.Drawing.Point(148, 379);
            this.buttonDemo5.Name = "buttonDemo5";
            this.buttonDemo5.Size = new System.Drawing.Size(138, 26);
            this.buttonDemo5.TabIndex = 60;
            this.buttonDemo5.Text = "Goal > Start";
            this.buttonDemo5.UseVisualStyleBackColor = true;
            this.buttonDemo5.Click += new System.EventHandler(this.Demo5_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(148, 405);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(138, 26);
            this.button5.TabIndex = 59;
            this.button5.Text = "2.53 > 2.44";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // buttonDemo4
            // 
            this.buttonDemo4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDemo4.Location = new System.Drawing.Point(148, 353);
            this.buttonDemo4.Name = "buttonDemo4";
            this.buttonDemo4.Size = new System.Drawing.Size(138, 26);
            this.buttonDemo4.TabIndex = 58;
            this.buttonDemo4.Text = "Start > Goal";
            this.buttonDemo4.UseVisualStyleBackColor = true;
            this.buttonDemo4.Click += new System.EventHandler(this.Demo4_Click);
            // 
            // exploreFS
            // 
            this.exploreFS.Label = "";
            this.exploreFS.Location = new System.Drawing.Point(7, 230);
            this.exploreFS.Name = "exploreFS";
            this.exploreFS.Size = new System.Drawing.Size(282, 32);
            this.exploreFS.TabIndex = 62;
            this.exploreFS.Value = 0F;
            // 
            // timeFS
            // 
            this.timeFS.Label = "";
            this.timeFS.Location = new System.Drawing.Point(6, 169);
            this.timeFS.Name = "timeFS";
            this.timeFS.Size = new System.Drawing.Size(282, 32);
            this.timeFS.TabIndex = 61;
            this.timeFS.Value = 0F;
            // 
            // boldFS
            // 
            this.boldFS.Label = "";
            this.boldFS.Location = new System.Drawing.Point(6, 318);
            this.boldFS.Name = "boldFS";
            this.boldFS.Size = new System.Drawing.Size(282, 32);
            this.boldFS.TabIndex = 55;
            this.boldFS.Value = 0F;
            // 
            // temperamentalFS
            // 
            this.temperamentalFS.Label = "";
            this.temperamentalFS.Location = new System.Drawing.Point(6, 288);
            this.temperamentalFS.Name = "temperamentalFS";
            this.temperamentalFS.Size = new System.Drawing.Size(282, 32);
            this.temperamentalFS.TabIndex = 54;
            this.temperamentalFS.Value = 0F;
            // 
            // greedyFS
            // 
            this.greedyFS.Label = "";
            this.greedyFS.Location = new System.Drawing.Point(6, 261);
            this.greedyFS.Name = "greedyFS";
            this.greedyFS.Size = new System.Drawing.Size(282, 32);
            this.greedyFS.TabIndex = 53;
            this.greedyFS.Value = 0F;
            // 
            // memoryFS
            // 
            this.memoryFS.Label = "";
            this.memoryFS.Location = new System.Drawing.Point(6, 199);
            this.memoryFS.Name = "memoryFS";
            this.memoryFS.Size = new System.Drawing.Size(282, 32);
            this.memoryFS.TabIndex = 52;
            this.memoryFS.Value = 0F;
            // 
            // FinderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.exploreFS);
            this.Controls.Add(this.timeFS);
            this.Controls.Add(this.buttonDemo5);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.buttonDemo4);
            this.Controls.Add(this.buttonDemo2);
            this.Controls.Add(this.buttonDemo3);
            this.Controls.Add(this.buttonDemo1);
            this.Controls.Add(this.boldFS);
            this.Controls.Add(this.temperamentalFS);
            this.Controls.Add(this.greedyFS);
            this.Controls.Add(this.memoryFS);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "FinderControl";
            this.Size = new System.Drawing.Size(291, 649);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox closeNode;
        private System.Windows.Forms.Button clearPath;
        private System.Windows.Forms.ComboBox startNode;
        private System.Windows.Forms.Button computePath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button reversePath;
        private FuzzySliderControl memoryFS;
        private FuzzySliderControl greedyFS;
        private FuzzySliderControl boldFS;
        private FuzzySliderControl temperamentalFS;
        private System.Windows.Forms.Button buttonDemo1;
        private System.Windows.Forms.Button buttonDemo3;
        private System.Windows.Forms.Button buttonDemo2;
        private System.Windows.Forms.CheckBox cognitive;
        private System.Windows.Forms.Button buttonDemo5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonDemo4;
        private FuzzySliderControl timeFS;
        private FuzzySliderControl exploreFS;
    }
}
