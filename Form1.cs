using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Threading;

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

        // list of all labels representing nodes specific to each selected graph
        private List<Label> LabelNodes { get; set; }

        // list of all locations of circles where graphics are pointing to for each graph
        private List<Point> NodeCircleLocations { get; set; }

        // stores the two vertices coming from panelNodeSelection
        private Vertex SelectedVertexA;
        private Vertex SelectedVertexB;

        private String AlgorithmSelected;

        public Form1()
        {
            InitializeComponent();
            panelGraph.BackColor = Color.Gray;
            Server = ConfigurationManager.AppSettings["SERVER"];
            Database = ConfigurationManager.AppSettings["DATABASE"];

            GetData GetData = new GetData(Server, Database);
            InformationGraphs = GetData.AssociatedInfo;

            SetUpGraphNameButtons();
        }

        private void SetUpGraphNameButtons()
        {
            const int X = 30;
            int Y = 0;

            Button ShowGraph = new Button();
            ShowGraph.Name = "btnShowGraph";
            ShowGraph.Text = "Show Weights";
            ShowGraph.Height += ShowGraph.Height; //Make it twice as tall as the other buttons
            ShowGraph.Location = new Point(X, Y);
            ShowGraph.Click += ShowWeights;
            Y += 100;
            panelGraphButtons.Controls.Add(ShowGraph);
            foreach (GraphInfo Info in InformationGraphs)
            {
                Button Button = new Button();
                Button.Name = Info.Name;
                Button.Text = Info.Name;
                Button.Click += btn_Click;
                Button.Location = new Point(X, Y);
                Y += 100;
                panelGraphButtons.Controls.Add(Button);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
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
            Size LblSize = new Size(300, 20);
            LabelGraphType.Size = LblSize;
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
                    Kruskal.Enabled = true;
                    Topological.Enabled = false;
                    Prim.Enabled = true;
                    break;
            }

            LabelGraphType.Text = Type;
            LabelGraphType.Refresh();
            panelGraph.Controls.Add(LabelGraphType);
        }

        private void CreateLabelNodes()
        {
            foreach (var CurrNode in Graph.Vertices)
            {
                Label Label = new Label();
                Label.Text = CurrNode.Name;
                Label.TextAlign = ContentAlignment.MiddleCenter;
                Graphics Graphics = panelGraph.CreateGraphics();
                Pen Pen = new Pen(Color.Black);
                Point LocationPont = GetLocation(CurrNode);
                Graphics.DrawEllipse(Pen, LocationPont.X - 5, LocationPont.Y - 5, 10, 10);

                NodeCircleLocations.Add(LocationPont);

                Label.Location = GetNewXAndY(LocationPont);
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
                        Point NeighborLocation = GetLocation(Neighbor);
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

        private Point GetLocation(Vertex vertex)
        {
            double PanelWidth = panelGraph.Width - 150;
            double PanelHeight = panelGraph.Height - 150;
            int XCoord = (int)(vertex.XCoord * PanelWidth) + 75;
            int YCoord = (int)(vertex.YCoord * PanelHeight) + 75;

            return new Point(XCoord, YCoord);
        }

        private Point GetNewXAndY(Point location)
        {
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


        //Algorithms
        private void Kruskal_Click(object sender, EventArgs e)
        {
            AlgorithmSelected = "Kruskal";
            CreateGraphics();
            panelNodeSelection.Visible = false;

            var Output = Graph.GetKruskalAlgorithm();
            DrawRedLines(Output);
        }

        private void Topological_Click(object sender, EventArgs e)
        {
            AlgorithmSelected = "Topological";
            CreateGraphics();
            panelNodeSelection.Visible = false;
            Vertex[] Output = Graph.GetTopologicalSort();
            if (Output == null)
            {
                MessageBox.Show("Graph contains a cycle.");
            }
            else
            {
                String Message = "";
                foreach (Vertex Element in Output)
                {
                    Message += Element + " ";
                }

                MessageBox.Show(Message);
            }
        }

        private void Prim_Click(object sender, EventArgs e)
        {
            AlgorithmSelected = "Prim";
            CreateGraphics();
            ShowPanelNodeSelection(false);
        }

        private void PrimCall()
        {
            Vertex[,] Output = Graph.PrimMinSpanningGraph(SelectedVertexA);
            // draw minimum spanning graph edges in red
            DrawRedLines(Output);

            ResetNodeSelectionPanel();
        }

        private void Dijkstra_Click(object sender, EventArgs e)
        {
            AlgorithmSelected = "Dijkstra";
            CreateGraphics();
            ShowPanelNodeSelection(true);
        }

        private void ShortestPathCall()
        {
            List<Vertex> Shortest = Graph.DijskstrasShortestPath(SelectedVertexA, SelectedVertexB);

            if (Shortest.Count == 1)
            {
                MessageBox.Show("Source and target are the same. Shortest distance: 0.0");
            }
            else
            {
                DrawRedLines(Shortest);
                MessageBox.Show("Shortest distance: " + Graph.ShortestDistance());
            }

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
            }
            else
            {
                SelectedVertexA = Graph.Vertices[originDropDown.SelectedIndex];
            }

            if (AlgorithmSelected.Equals("Dijkstra"))
            {
                if (destDropDown.SelectedIndex == -1)
                {
                    SelectedVertexB = Graph.Vertices[0];
                }
                else
                {
                    SelectedVertexB = Graph.Vertices[destDropDown.SelectedIndex];
                }

                ShortestPathCall();
            }
            else if (AlgorithmSelected.Equals("Prim"))
            {
                PrimCall();
            }
        }

        private void DrawRedLines(Vertex[,] input)
        {
            SetUpGraphicsAndPen(out Graphics Graphics, out Pen Pen, Color.Red);
            for (int Index = 0; Index < input.GetLength(0); Index++)
            {
                Vertex Beginning = input[Index, 0];
                Vertex Ending = input[Index, 1];

                Pen.Width = 2;

                Point BeginLocation = GetLocation(Beginning);
                Point NeighborLocation = GetLocation(Ending);
                Thread.Sleep(500);
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

                Point StartingPoint = GetLocation(StartingVertex);
                Point NeighborLocation = GetLocation(EndingVertex);
                Graphics.DrawLine(Pen, StartingPoint, NeighborLocation);

                Thread.Sleep(500);
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
    }
}