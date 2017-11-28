using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vlcr.Agent;
using Vlcr.CognitiveStateSearch;
using Vlcr.Core;
using Vlcr.Map;
using Vlcr.StateSearch;
using Vlcr.VisualMap;

namespace Vlcr.Creator.Controls
{
    internal sealed partial class FinderControl : UserControl
    {
        // Done!
        #region Automatic Properties

        internal ConcreteVisualMap    VisualMap       { get; set; }
        internal MainForm             RealParent      { get; private set; }
        
        #endregion

        // Done!
        #region Properties

        private readonly AgentCharacter agentCharacter = AgentCharacter.Default;

        // Done!
        private AgentCharacter AgentCharacter
        {
            get { return agentCharacter; }
        }
        
        #endregion

        // Done!
        #region .Ctor

        // Done!
        internal FinderControl() : this(null)
        {
        }

        // Done!
        internal FinderControl(MainForm parent)
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

            InitializeFuzzyControls();

            this.RealParent.NodeAdded += NodeAdded;
            this.VisibleChanged += InternalVisibleChanged;
        }

        // Done!
        private void InitializeFuzzyControls()
        {
            // Time!
            this.timeFS.Value = 0;
            this.timeFS.Label = "Time (Weeks)";

            // Memory!
            this.memoryFS.Value = this.AgentCharacter.Memory;
            this.memoryFS.Label = "Memory";
            this.memoryFS.SliderChange += FuzzySliderChanged;

            // Explore!
            this.exploreFS.Value = this.AgentCharacter.Explore;
            this.exploreFS.Label = "Explore";
            this.exploreFS.SliderChange += FuzzySliderChanged;

            // Greedy!
            this.greedyFS.Value = this.AgentCharacter.Greedy;
            this.greedyFS.Label = "Greedy";
            this.greedyFS.SliderChange += FuzzySliderChanged;

            // Bold!
            this.boldFS.Value = this.AgentCharacter.Bold;
            this.boldFS.Label = "Bold";
            this.boldFS.SliderChange += FuzzySliderChanged;

            // Temperamental!
            this.temperamentalFS.Value = this.AgentCharacter.Greedy;
            this.temperamentalFS.Label = "Temperamental";
            this.temperamentalFS.SliderChange += FuzzySliderChanged;
        }

        #endregion

        // Done!
        #region Helpers

        // Done!
        private void ClearPathSelection()
        {
            var p = this.RealParent;
            var was = p.WorkAreaState;
            was.ShowPath = false;
            was.Path = null;

            var vm = p.VisualMap;
            for (int i = 0; i < vm.Count; ++i)
            {
                vm[i].FillShapePath = false;
            }
        }

        // Done!
        private void ForceParentUpdate(IList<MapNode> path)
        {
            var p = this.RealParent;
            var was = p.WorkAreaState;
            was.ShowPath = (path == null) ? false : true;
            was.Path = path;
            was.IsDirty = true;
            p.ForceUpdate();
        }

        //// Done!
        //private static void FillGeometryPath(ConcreteVisualMap map, IEnumerable<MapNode> path)
        //{
        //    foreach (var item in path)
        //    {
        //        string name = string.Empty;
        //        switch (item.NodeType)
        //        {
        //            case NodeType.Exit:     name = item.Exits[0].Name;  break;
        //            case NodeType.Virtual:  name = item.Parent.Name;    break;
        //            case NodeType.Geometry: name = item.Name;           break;
        //            case NodeType.Goal:     name = item.Name;           break;
        //        }
        //        map.FindByName(name).FillShapePath = true;
        //    }
        //}

        #endregion

        // Done!
        #region Events

        // Done!
        private void InternalVisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                return;
            }

            this.GenerateNodes();
        }

        // Done!
        private void GenerateNodes()
        {
            List<string> temp = new List<string>();
            ConcreteVisualMap vm = RealParent.VisualMap;
            for (int i = 0; i < vm.Count; ++i)
            {
                temp.Add(vm[i].ConcreteNode.Name);
            }
            temp.Sort();

            this.startNode.Items.Clear();
            this.closeNode.Items.Clear();
            this.startNode.Items.Add(string.Empty);
            this.closeNode.Items.Add(string.Empty);
            for (int i = 0; i < temp.Count; ++i)
            {
                this.startNode.Items.Add(temp[i]);
                this.closeNode.Items.Add(temp[i]);
            }
        }

        // Done!
        private void NodeAdded(object sender, EventArgs e)
        {
            if(this.Visible == false)
            {
                return;
            }

            this.GenerateNodes();

        }

        #endregion

        // Done!
        #region Control Events

        // Done!
        private void ComputePath_Click(object sender, EventArgs e)
        {
            var index1 = this.startNode.SelectedIndex;
            var index2 = this.closeNode.SelectedIndex;

            if (index1 == -1)
            {
                MessageBox.Show("Invalid Start MapNode");
                return;
            }

            if (index2 == -1)
            {
                MessageBox.Show("Invalid Goal MapNode");
                return;
            }

            string nStart = this.startNode.Items[index1].ToString();
            string nClose = this.startNode.Items[index2].ToString();

            var map = this.RealParent.VisualMap;
            VisualMapNode vStart = map.FindByName(nStart);
            VisualMapNode vClose = map.FindByName(nClose);

            FindPath(vStart, vClose);
        }

        // Done!
        private void FindPath(VisualMapNode vStart, VisualMapNode vClose)
        {
            if(vStart == null)
            {
                MessageBox.Show("Invalid Start MapNode");
                return;
            }

            if (vClose == null)
            {
                MessageBox.Show("Invalid Goal MapNode");
                return;
            }

            this.ClearPathSelection();

            var start = vStart.ConcreteNode;
            var close = vClose.ConcreteNode;

            var startStub = MapNode.ToVirtual(start, Vector.Average(start.Geometry));               // Change to Exact Location
            var closeStub = ConcreteMap.AdaptNodeToGoal(close, Vector.Average(close.Geometry));     // Change to Exact Location

            if(this.cognitive.Checked == true)
            {
                ICognitiveStateSearchable<MapNode> iss = new CognitiveAStar<MapNode>(startStub, closeStub, this.agentCharacter);
                iss.FindPath();

                var path = iss.Path;
                path.Add(closeStub.Clone());
                //FillGeometryPath(map, path);
                this.ForceParentUpdate(path);
            }
            else
            {
                IStateSearchable<MapNode> iss = new AStar<MapNode>(startStub, closeStub);
                iss.FindPath();

                var path = iss.Path;
                path.Add(closeStub.Clone());
                //FillGeometryPath(map, path);
                this.ForceParentUpdate(path);
            }

            

            ConcreteMap.RestoreNodeFromGoal(close);
        }

        //private static bool memorize = true;

        //private static void Memorize(ConcreteVisualMap map, string node, int count)
        //{
        //    if(memorize == false)
        //    {
        //        return;
        //    }

        //    VisualMapNode mem = map.FindByName(node);
        //    for (int i = 0; i < count; ++i)
        //    {
        //        Console.WriteLine("Memory: " + mem.ConcreteNode.Constraints.Memory.Value);
        //        mem.ConcreteNode.Constraints.Memory.Increment();
        //    }
        //    memorize = false;
        //}

        // Done!
        private void ClearPath_Click(object sender, EventArgs e)
        {
            this.ClearPathSelection();
            this.ForceParentUpdate(null);
        }

        // Done!
        private void ReservePath_Click(object sender, EventArgs e)
        {
            var index1 = this.startNode.SelectedIndex;
            var index2 = this.closeNode.SelectedIndex;

            this.startNode.SelectedIndex = index2;
            this.closeNode.SelectedIndex = index1;
        }

        #endregion

        // Done!
        #region Fuzzy Events

        // Done!
        private void FuzzySliderChanged(object sender, EventArgs e)
        {
            this.AgentCharacter.Memory          = this.memoryFS.Value;
            this.AgentCharacter.Explore         = this.exploreFS.Value;
            this.AgentCharacter.Greedy          = this.greedyFS.Value;
            this.AgentCharacter.Bold            = this.boldFS.Value;
            this.AgentCharacter.Temperamental   = this.temperamentalFS.Value;
        }

        // Done!
        private void Cognitive_CheckedChanged(object sender, EventArgs e)
        {
            var v = this.cognitive.Checked;
            this.timeFS.Enabled = v;
            this.memoryFS.Enabled = v;
            this.exploreFS.Enabled = v;
            this.greedyFS.Enabled = v;
            this.boldFS.Enabled = v;
            this.temperamentalFS.Enabled = v;
        }

        #endregion

        // Done!
        #region Demos!

        // Done!
        private void GeneralDemo(string nStart, string nClose)
        {
            var map = this.RealParent.VisualMap;
            VisualMapNode vStart = map.FindByName(nStart);
            VisualMapNode vClose = map.FindByName(nClose);

            FindPath(vStart, vClose);
        }

        // Done!
        private void Demo1_Click(object sender, EventArgs e)
        {
            const string nStart = "Hall Section:V";
            const string nClose = "2.35";
            GeneralDemo(nStart, nClose);
        }

        // Done!
        private void Demo2_Click(object sender, EventArgs e)
        {
            const string nStart = "2.35";
            const string nClose = "Hall Section:V";
            GeneralDemo(nStart, nClose);
        }

        // Done!
        private void Demo3_Click(object sender, EventArgs e)
        {
            const string nStart = "2.53";
            const string nClose = "2.44";
            GeneralDemo(nStart, nClose);
        }

        // Done!
        private void Demo4_Click(object sender, EventArgs e)
        {
            const string nStart = "Start";
            const string nClose = "Goal";
            GeneralDemo(nStart, nClose);
        }

        // Done!
        private void Demo5_Click(object sender, EventArgs e)
        {
            const string nStart = "Goal";
            const string nClose = "Start";
            GeneralDemo(nStart, nClose);
        }

        #endregion

    }
}