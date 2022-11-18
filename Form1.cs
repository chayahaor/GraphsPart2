using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing.Drawing2D;

namespace GraphsClassProject
{
    public partial class Form1 : Form
    {
        //DB Connection
        private String Server;
        private String Database;

        // Graph Showing
        private Graph Graph;
        private GraphInfo AssociatedInfo;
        
        // contains all graph names (graph names must be unique in the database)
        private ArrayList InformationGraphs;
        
        // List of all the buttons containing graph names
        private List<Button> GraphNameButtons { get; }

        // the center of the panelGraph panel (size 600 by 600)
        private readonly int Center = 300;

        // list of all labels representing nodes specific to each selected graph
        private List<Label> LabelNodes { get; set; }

        // list of all locations of circles where graphics are pointing to for each graph
        private List<Point> NodeCircleLocations { get; set; }

        // stores the two vertices coming from panelNodeSelection
        private Vertex SelectedVertexA;
        private Vertex SelectedVertexB;

        public Form1()
        {
            InitializeComponent();
            GraphNameButtons = new List<Button>();
            panelGraph.BackColor = Color.Gray;
            Server = ConfigurationManager.AppSettings["SERVER"];
            Database = ConfigurationManager.AppSettings["DATABASE"];

            GetData GetData = new GetData(Server, Database);
            InformationGraphs = GetData.AssociatedInfo;

            SetUpGraphNameButtons();
        }

        private void SetUpGraphNameButtons()
        {
            int X = 30;
            int Y = 0;

            foreach (GraphInfo Info in InformationGraphs)
            {
                Button Button = new Button();
                Button.Name = Info.Name; // All button names are unique because in the SQL code, graph names are unique
                Button.Text = Info.Name;
                Button.Click += btn_Click;
                Button.Location = new Point(X, Y);
                Y += 100;
                panelGraphButtons.Controls.Add(Button);
            }

            Button ShowGraph = new Button();
            ShowGraph.Name = "btnShowGraph";
            ShowGraph.Text = "Show Weights";
            ShowGraph.Height += ShowGraph.Height;
            ShowGraph.Location = new Point(X, Y);
            ShowGraph.Click += ShowWeights;
            
            panelGraphButtons.Controls.Add(ShowGraph);
        }


        private void btn_Click(object sender, EventArgs e)
        {
            //When click a button
            Button Button = (Button)sender;
            //Load the graph
            Graph = new Graph(Button.Name, Server, Database);
            //Display the graph
            FillPanel();
        }

        private void FillPanel()
        {
            ResetPanels();
            CreateLabelType();
            CreateLabelNodes();
            CreateGraphics();
        }

        private void ResetPanels()
        {
            LabelNodes = new List<Label>();
            NodeCircleLocations = new List<Point>();
            panelGraph.Controls.Clear();
            panelGraph.Refresh();
            ResetNodeSelectionPanel();
        }

        private void ResetNodeSelectionPanel()
        {
            panelNodeSelection.Visible = false;
            originDropDown.SelectedIndex = -1;
            destDropDown.SelectedIndex = -1;
            originDropDown.Items.Clear();
            destDropDown.Items.Clear();
            originDropDown.ResetText();
            destDropDown.ResetText();
        }

        private void CreateLabelType()
        {
            Label LabelGraphType = new Label();
            LabelGraphType.Location = new Point(15, 20);
            Size Size = new Size(300, 20);
            LabelGraphType.Size = Size;
            String Type = "";

            foreach (GraphInfo Info in InformationGraphs)
            {
                if (Info.Name == Graph.GraphName)
                {
                    AssociatedInfo = Info;
                }
            }

            if (AssociatedInfo.Weight)
            {
                Type += "Weighted ";
            }
            else
            {
                Type += "Unweighted ";
            }

            if (AssociatedInfo.Direct)
            {
                Type += "Digraph";
            }
            else
            {
                Type += "Graph";
            }

            //TODO: confirm types done correctly
            switch (Type)
            {
                case "Weighted Digraph":
                    Dijkstra.Enabled = true;
                    Kruskal.Enabled = false;
                    Topological.Enabled = true;
                    Prim.Enabled = false;
                    break;
                case "Weighted Graph":
                    Dijkstra.Enabled = true;
                    Kruskal.Enabled = true;
                    Topological.Enabled = false;
                    Prim.Enabled = true;
                    break;
                case "Unweighted Digraph":
                    Dijkstra.Enabled = false;
                    Kruskal.Enabled = false;
                    Topological.Enabled = true;
                    Prim.Enabled = false;
                    break;
                case "Unweighted Graph":
                    Dijkstra.Enabled = false;
                    Kruskal.Enabled = false;
                    Topological.Enabled = false;
                    Prim.Enabled = false;
                    break;
            }
            
            LabelGraphType.Text = Type;
            LabelGraphType.Refresh();
            panelGraph.Controls.Add(LabelGraphType);
        }

        private void CreateLabelNodes()
        {
            for (int NodeNumber = 0; NodeNumber < Graph.Vertices.Count; NodeNumber++)
            {
                Label Label = new Label();
                Label.Text = Graph.Vertices[NodeNumber].Name;
                Label.TextAlign = ContentAlignment.MiddleCenter;

                Graphics Graphics = panelGraph.CreateGraphics();
                Pen Pen = new Pen(Color.Black);
                Point Location = GetLocation(NodeNumber, Graph.Vertices.Count);
                Graphics.DrawEllipse(Pen, Location.X - 5, Location.Y - 5, 10, 10);

                NodeCircleLocations.Add(Location);

                Label.Location = GetNewXAndY(Location);
                Label.Font = new Font("Arial", 8);
                Label.Size = new Size(20, 15);
                Label.ForeColor = Color.White;
                Label.SendToBack();
                Label.Refresh();

                LabelNodes.Add(Label);
            }

            foreach (Label Label in LabelNodes)
            {
                panelGraph.Controls.Add(Label);
                Label.Refresh();
            }
        }

        private new void CreateGraphics()
        {
            SetUpGraphicsAndPen(out Graphics Graphics, out Pen Pen, Color.Black);

            for (int NodeNumber = 0; NodeNumber < Graph.Vertices.Count; NodeNumber++)
            {
                Vertex CurrNode = Graph.Vertices[NodeNumber];
                foreach (Vertex Neighbor in CurrNode.Neighbors)
                {
                    if (CurrNode.Neighbors.Contains(Neighbor))
                    {
                        Pen.Width = 2;
                        Pen.Color = Color.Black;

                        Point OriginalLocation = NodeCircleLocations[NodeNumber];
                        Point NeighborLocation = GetVertexLocation(Neighbor);
                        Graphics.DrawLine(Pen, OriginalLocation, NeighborLocation);
                    }
                }
            }
        }

        private void SetUpGraphicsAndPen(out Graphics graphics, out Pen pen, Color penColor)
        {
            graphics = panelGraph.CreateGraphics();
            pen = new Pen(penColor);
            if (AssociatedInfo.Direct)
            {
                AdjustableArrowCap AdjustableArrowCap = new AdjustableArrowCap(3, 3);
                pen.CustomEndCap = AdjustableArrowCap;
            }
            else
            {
                pen.EndCap = LineCap.Round;
            }
        }

        private Point GetLocation(int nodeNumber, int numNodes)
        {
            //TODO: Move this to a SP in DB
            // MAX NUMBER OF NODES: 26 
            // MAX INNER NUMBER OF NODES: 10

            int XCoord;
            int YCoord;
            if (numNodes < 16 || nodeNumber < 16)
            {
                int DistanceFromCenter = 200;

                double Angle = 2.0 * Math.PI / (numNodes) * nodeNumber;

                XCoord = (int)Math.Floor(Center + DistanceFromCenter * Math.Cos(Angle));
                YCoord = (int)Math.Floor(Center - DistanceFromCenter * Math.Sin(Angle));
            }
            else
            {
                int DistanceFromCenter = 100;

                double Angle = 2.0 * Math.PI / numNodes - 17 * nodeNumber;

                XCoord = (int)Math.Floor(Center + DistanceFromCenter * Math.Cos(Angle));
                YCoord = (int)Math.Floor(Center - DistanceFromCenter * Math.Sin(Angle));
            }

            return new Point(XCoord, YCoord);
        }

        private Point GetNewXAndY(Point location)
        {
            //TODO: is this needed?
            int XCoord;
            int YCoord;

            if (location.X >= 200)
                XCoord = location.X + 10;
            else
                XCoord = location.X - 15;
            if (location.Y >= 200)
                YCoord = location.Y + 15;
            else
                YCoord = location.Y - 15;
            return new Point(XCoord, YCoord);
        }

        private Point GetVertexLocation(Vertex neighbor)
        {
            // default location points to the center of the panel
            Point VertexLocation = new Point(Center, Center);
            for (int LabelIndex = 0; LabelIndex < LabelNodes.Count; LabelIndex++)
            {
                if (LabelNodes[LabelIndex].Text == neighbor.Name)
                {
                    VertexLocation = NodeCircleLocations[LabelIndex];
                }
            }

            return VertexLocation;
        }

        //Algorithms
        private void Kruskal_Click(object sender, EventArgs e)
        {
            CreateGraphics();
            panelNodeSelection.Visible = false;

            var Output = Graph.KruskalAlgorithm();
            DrawRedLines(Output);
        }

        private void Topological_Click(object sender, EventArgs e)
        {
            CreateGraphics();
            panelNodeSelection.Visible = false;
            Graph.DoTopological();
        }

        
        private void Prim_Click(object sender, EventArgs e)
        {
            CreateGraphics();
            ShowPanelNodeSelection(false);
            Vertex[,] Output = Graph.PrimAlgorithm(SelectedVertexA);

            // draw minimum spanning graph edges in red
            DrawRedLines(Output);

            ResetNodeSelectionPanel();
        }


        private void Dijkstra_Click(object sender, EventArgs e)
        {
            CreateGraphics();
            ShowPanelNodeSelection(true);
            List<Vertex> Output = new List<Vertex>();
            double ShortestDist = 0.0;

            Graph.DijkstraAlgorithm();
            DrawRedLines(Output);
            MessageBox.Show("Shortest distance: " + ShortestDist);
            ResetNodeSelectionPanel();
        }

     
        private void ShowPanelNodeSelection(bool isDestDropDownEnabled)
        {
            panelNodeSelection.Visible = true;
            destDropDown.Visible = isDestDropDownEnabled;
            destDropDown.Enabled = isDestDropDownEnabled;
            anotherNode.Visible = false;
            destDropDown.Refresh();
            anotherNode.Refresh();
            panelNodeSelection.Refresh();
            GetInput();
        }

        private void GetInput()
        {
            foreach (Vertex Vertex in Graph.Vertices)
            {
                originDropDown.Items.Add(Vertex.Name);
            }

            foreach (Vertex Vertex in Graph.Vertices)
            {
                destDropDown.Items.Add(Vertex.Name);
            }
        }

        private void readyNodes_Click(object sender, EventArgs e)
        {
            if (originDropDown.SelectedIndex == -1)
            {
                SelectedVertexA = Graph.Vertices[0];
                MessageBox.Show("Default vertex selected");
            }
            else
            {
                SelectedVertexA = Graph.Vertices[originDropDown.SelectedIndex];
                MessageBox.Show("You selected " + SelectedVertexA.Name);
            }

            if (destDropDown.SelectedIndex == -1)
            {
                SelectedVertexB = Graph.Vertices[0];
                /*if (algorithmType != null && algorithmType.Equals(AlgorithmType.DIJKSTRA))
                {
                    MessageBox.Show("Default vertex selected");
                }*/
            }
            else
            {
                SelectedVertexB = Graph.Vertices[destDropDown.SelectedIndex];
                MessageBox.Show("You selected " + SelectedVertexB.Name);
            }

            /*
            if (algorithmType != null && algorithmType.Equals(AlgorithmType.PRIM))
            {
                DoPrim();
            }
            else if (algorithmType != null && algorithmType.Equals(AlgorithmType.DIJKSTRA))
            {
                DoDijkstra();
            }*/
        }

        private void DrawRedLines(Vertex[,] input)
        {
            SetUpGraphicsAndPen(out Graphics Graphics, out Pen Pen, Color.Red);

            /*Vertex startingVertex = new Vertex("start");
            Vertex endingVertex = new Vertex("end");
            */

            for (int Index = 0; Index < input.GetLength(0); Index++)
            {
                Vertex Beginning = input[Index, 0];
                Vertex Ending = input[Index, 1];

                /*foreach (var vertex in newGraph.Vertices)
                {
                    if (vertex.Name.Equals(beginning.Name))
                    {
                        startingVertex = vertex;
                    }

                    if (vertex.Name.Equals(ending.Name))
                    {
                        endingVertex = vertex;
                    }
                }
                */

                Pen.Width = 2;

                Point BeginLocation = GetVertexLocation(Beginning);
                Point NeighborLocation = GetVertexLocation(Ending);
                Graphics.DrawLine(Pen, BeginLocation, NeighborLocation);
            }
        }

        private void DrawRedLines(List<Vertex> input)
        {
            SetUpGraphicsAndPen(out Graphics Graphics, out Pen Pen, Color.Red);

            for (int I = 0; I < input.Count - 1; I++)
            {
                var StartingVertex = input[I];
                var EndingVertex = input[I + 1];

                Pen.Width = 2;

                Point StartingPoint = GetVertexLocation(StartingVertex);
                Point NeighborLocation = GetVertexLocation(EndingVertex);
                Graphics.DrawLine(Pen, StartingPoint, NeighborLocation);

                System.Threading.Thread.Sleep(500);
            }
        }

        private void ShowWeights(Object o, EventArgs e)
        {
            if (Graph != null)
            {
                WeightsChart Chart = new WeightsChart(Graph);
                Chart.Show();
            }
        }
        
        //TODO: delete/move to GraphNew
        private void DoPrim()
        {
            /*foreach (WeightedGraph weightedGraph in weightedGraphs)
            {
                if (weightedGraph.GraphName.Equals(currentGraphShowing.GraphName))
                {
                    Vertex[,] output = weightedGraph.DoPrimAlgorithm(selectedVertexA);

                    // draw minimum spanning graph edges in red
                    DrawRedLines(currentGraphShowing, output);

                    break;
                }
            }

            ResetNodeSelectionPanel();*/
        }
        
        //TODO: delete/move to GraphNew
        private void DoDijkstra()
        {
            /*List<Vertex> output = new List<Vertex>();
            double shortestDist = 0.0;

            try
            {
                if (currentGraphShowing.Type == GraphType.WEIGHTED_GRAPH)
                {
                    foreach (WeightedGraph weightedGraph in weightedGraphs)
                    {
                        if (weightedGraph.GraphName.Equals(currentGraphShowing.GraphName))
                        {
                            output = weightedGraph.DoDijkstraAlgorithm(selectedVertexA, selectedVertexB);

                            shortestDist = weightedGraph.GetDijkstraShortestDistance();

                            break;
                        }
                    }
                }
                else
                {
                    foreach (WeightedDigraph weightedDigraph in weightedDigraphs)
                    {
                        if (weightedDigraph.GraphName.Equals(currentGraphShowing.GraphName))
                        {
                            output = weightedDigraph.DoDijkstraAlgorithm(selectedVertexA, selectedVertexB);

                            shortestDist = weightedDigraph.GetDijkstraShortestDistance();

                            break;
                        }
                    }
                }

                // Draw path one by one using red lines
                DrawRedLines(currentGraphShowing, output);

                MessageBox.Show("Shortest distance: " + shortestDist);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ResetNodeSelectionPanel();*/
        }

        
        //TODO: delete/move to GraphNew
        private void DoTopological()
        {
            /*string topologicalOutput = "";
            try
            {
                Vertex[] output = Array.Empty<Vertex>();
                if (currentGraphShowing.Type == GraphType.WEIGHTED_DIGRAPH)
                {
                    foreach (WeightedDigraph weightedDigraph in weightedDigraphs)
                    {
                        if (weightedDigraph.GraphName == currentGraphShowing.GraphName)
                        {
                            if (weightedDigraph.topologicalOutput == null)
                            {
                                output = weightedDigraph.DoTopologicalSort();
                            }
                            else
                            {
                                output = weightedDigraph.topologicalOutput;
                            }

                            break;
                        }
                    }
                }
                else
                {
                    foreach (Digraph digraph in digraphs)
                    {
                        if (digraph.GraphName == currentGraphShowing.GraphName)
                        {
                            if (digraph.topologicalOutput == null)
                            {
                                output = digraph.DoTopologicalSort();
                            }
                            else
                            {
                                output = digraph.topologicalOutput;
                            }

                            break;
                        }
                    }
                }

                foreach (Vertex vertex in output)
                {
                    topologicalOutput += vertex.Name + " ";
                }

                MessageBox.Show("Topological sort of " + currentGraphShowing.GraphName + ":\n\n" +
                                topologicalOutput);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }*/
        }

    }
}