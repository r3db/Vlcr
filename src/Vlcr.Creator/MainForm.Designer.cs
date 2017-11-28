using Vlcr.Creator.Controls;

namespace Vlcr.Creator
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.newShapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathFinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xnaWorkAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workArea = new System.Windows.Forms.PictureBox();
            this.contextMenuStripExit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripAnchor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripWorkspace = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.loadImageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.newShapeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.resetToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripShape = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllExitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workArea)).BeginInit();
            this.contextMenuStripExit.SuspendLayout();
            this.contextMenuStripAnchor.SuspendLayout();
            this.contextMenuStripWorkspace.SuspendLayout();
            this.contextMenuStripShape.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1061, 24);
            this.mainMenuStrip.TabIndex = 4;
            this.mainMenuStrip.Text = "mainMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenRawXmlFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveToolStripMenuItem.Text = "Save...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveRawXmlFile_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.loadImageToolStripMenuItem,
            this.clearImageToolStripMenuItem,
            this.toolStripSeparator4,
            this.newShapeToolStripMenuItem,
            this.toolStripSeparator5,
            this.resetToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Enabled = false;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.Z)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(171, 6);
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.loadImageToolStripMenuItem.Text = "Load Image...";
            // 
            // clearImageToolStripMenuItem
            // 
            this.clearImageToolStripMenuItem.Name = "clearImageToolStripMenuItem";
            this.clearImageToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.clearImageToolStripMenuItem.Text = "Clear Image";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(171, 6);
            // 
            // newShapeToolStripMenuItem
            // 
            this.newShapeToolStripMenuItem.Name = "newShapeToolStripMenuItem";
            this.newShapeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.newShapeToolStripMenuItem.Text = "New Shape";
            this.newShapeToolStripMenuItem.Click += new System.EventHandler(this.NewShapeMainMenu);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(171, 6);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.ResetWorkSpace_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asCodeToolStripMenuItem,
            this.asImageToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // asCodeToolStripMenuItem
            // 
            this.asCodeToolStripMenuItem.Enabled = false;
            this.asCodeToolStripMenuItem.Name = "asCodeToolStripMenuItem";
            this.asCodeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.asCodeToolStripMenuItem.Text = "As code...";
            // 
            // asImageToolStripMenuItem
            // 
            this.asImageToolStripMenuItem.Name = "asImageToolStripMenuItem";
            this.asImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.asImageToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.asImageToolStripMenuItem.Text = "As Image";
            this.asImageToolStripMenuItem.Click += new System.EventHandler(this.SaveAsImage_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pathFinderToolStripMenuItem,
            this.xnaWorkAreaToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // pathFinderToolStripMenuItem
            // 
            this.pathFinderToolStripMenuItem.Name = "pathFinderToolStripMenuItem";
            this.pathFinderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.pathFinderToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.pathFinderToolStripMenuItem.Text = "Path Finder";
            this.pathFinderToolStripMenuItem.Click += new System.EventHandler(this.ToogleFinder_Click);
            // 
            // xnaWorkAreaToolStripMenuItem
            // 
            this.xnaWorkAreaToolStripMenuItem.Name = "xnaWorkAreaToolStripMenuItem";
            this.xnaWorkAreaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.xnaWorkAreaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.xnaWorkAreaToolStripMenuItem.Text = "3D Visualizer";
            this.xnaWorkAreaToolStripMenuItem.Click += new System.EventHandler(this.XnaWorkAreaToolStripMenuItem_Click);
            // 
            // workArea
            // 
            this.workArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workArea.Location = new System.Drawing.Point(0, 24);
            this.workArea.Name = "workArea";
            this.workArea.Size = new System.Drawing.Size(1061, 754);
            this.workArea.TabIndex = 8;
            this.workArea.TabStop = false;
            // 
            // contextMenuStripExit
            // 
            this.contextMenuStripExit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStripExit.Name = "contextMenuStripAnchor";
            this.contextMenuStripExit.Size = new System.Drawing.Size(139, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.toolStripMenuItem1.Text = "Remove Exit";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.RemoveExit_Click);
            // 
            // contextMenuStripAnchor
            // 
            this.contextMenuStripAnchor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeNodeToolStripMenuItem});
            this.contextMenuStripAnchor.Name = "contextMenuStripAnchor";
            this.contextMenuStripAnchor.Size = new System.Drawing.Size(160, 26);
            // 
            // removeNodeToolStripMenuItem
            // 
            this.removeNodeToolStripMenuItem.Name = "removeNodeToolStripMenuItem";
            this.removeNodeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.removeNodeToolStripMenuItem.Text = "Remove Anchor";
            this.removeNodeToolStripMenuItem.Click += new System.EventHandler(this.RemoveAnchor_Click);
            // 
            // contextMenuStripWorkspace
            // 
            this.contextMenuStripWorkspace.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripSeparator6,
            this.loadImageToolStripMenuItem1,
            this.toolStripSeparator7,
            this.newShapeToolStripMenuItem1,
            this.toolStripSeparator8,
            this.resetToolStripMenuItem1});
            this.contextMenuStripWorkspace.Name = "contextMenuStripAnchor";
            this.contextMenuStripWorkspace.Size = new System.Drawing.Size(146, 132);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem3.Text = "Undo";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Enabled = false;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem4.Text = "Redo";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(142, 6);
            // 
            // loadImageToolStripMenuItem1
            // 
            this.loadImageToolStripMenuItem1.Name = "loadImageToolStripMenuItem1";
            this.loadImageToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.loadImageToolStripMenuItem1.Text = "Load Image...";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(142, 6);
            // 
            // newShapeToolStripMenuItem1
            // 
            this.newShapeToolStripMenuItem1.Name = "newShapeToolStripMenuItem1";
            this.newShapeToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.newShapeToolStripMenuItem1.Text = "New Shape";
            this.newShapeToolStripMenuItem1.Click += new System.EventHandler(this.NewShapeSubMenu);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(142, 6);
            // 
            // resetToolStripMenuItem1
            // 
            this.resetToolStripMenuItem1.Name = "resetToolStripMenuItem1";
            this.resetToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.resetToolStripMenuItem1.Text = "Reset";
            this.resetToolStripMenuItem1.Click += new System.EventHandler(this.ResetWorkSpaceSubMenu_Click);
            // 
            // contextMenuStripShape
            // 
            this.contextMenuStripShape.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.removeAllExitsToolStripMenuItem});
            this.contextMenuStripShape.Name = "contextMenuStripAnchor";
            this.contextMenuStripShape.Size = new System.Drawing.Size(161, 48);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem2.Text = "Remove Shape";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.RemoveShape_Click);
            // 
            // removeAllExitsToolStripMenuItem
            // 
            this.removeAllExitsToolStripMenuItem.Name = "removeAllExitsToolStripMenuItem";
            this.removeAllExitsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.removeAllExitsToolStripMenuItem.Text = "Remove All Exits";
            this.removeAllExitsToolStripMenuItem.Click += new System.EventHandler(this.RemoveAllExits_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 778);
            this.Controls.Add(this.workArea);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Opacity = 0.98D;
            this.Text = "MainForm";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workArea)).EndInit();
            this.contextMenuStripExit.ResumeLayout(false);
            this.contextMenuStripAnchor.ResumeLayout(false);
            this.contextMenuStripWorkspace.ResumeLayout(false);
            this.contextMenuStripShape.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem newShapeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pathFinderToolStripMenuItem;
        private System.Windows.Forms.PictureBox workArea;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAnchor;
        private System.Windows.Forms.ToolStripMenuItem removeNodeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripWorkspace;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem newShapeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripShape;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem removeAllExitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xnaWorkAreaToolStripMenuItem;
    }
}

