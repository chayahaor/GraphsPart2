using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing.Drawing2D;

namespace GraphsClassProject
{
    public partial class Form1 : Form
    {
        public List<Label> LabelNodes { get; set; }
        public List<Point> NodeCircleLocations { get; set; }

        private List<Digraph> digraphs;
        private List<Graph> graphs;
        private List<WeightedDigraph> weightedDigraphs;
        private List<WeightedGraph> weightedGraphs;

        private Dictionary<String, String> graphNamesAndTypes;

        public List<Button> GraphNameButtons { get; set; } 

        private readonly Font SMALL_FONT = new Font("Arial", 8);  

        public readonly String SERVER;
        public readonly String DATABASE;

        private readonly int CENTER = 325;

        public Form1()
        {
            InitializeComponent();

            digraphs = new List<Digraph>();
            graphs = new List<Graph>();
            weightedDigraphs = new List<WeightedDigraph>();
            weightedGraphs = new List<WeightedGraph>();

            GraphNameButtons = new List<Button>();

            panelGraph.BackColor = Color.Gray;

            SERVER = ConfigurationManager.AppSettings["SERVER"];
            DATABASE = ConfigurationManager.AppSettings["DATABASE"];
            GetData getData = new GetData(SERVER, DATABASE);

            graphNamesAndTypes = getData.GraphTypes;

            int x = 0;
            int y = 0;
            int currNumber = 0;
            foreach (KeyValuePair<string, string> pair in graphNamesAndTypes)
            {
                Button button = new Button();
                button.Name = pair.Key;
                button.Text = pair.Key;
                button.Click += new EventHandler(btn_Click);
                button.Location = new Point(x, y);
                GraphNameButtons.Add(button);

                currNumber++;
                y += 100;

                panelGraphButtons.Controls.Add(button);

                switch (pair.Value)
                {
                    case "Weighted_Directed":
                        WeightedDigraph weightedDigraph = new WeightedDigraph(pair.Key);
                        weightedDigraph.LoadGraph(pair.Key, SERVER, DATABASE);
                        weightedDigraphs.Add(weightedDigraph);
                        break;
                    case "Unweighted_Directed":
                        Digraph digraph = new Digraph(pair.Key);
                        digraph.LoadGraph(pair.Key, SERVER, DATABASE);
                        digraphs.Add(digraph);
                        break;
                    case "Weighted_Undirected":
                        WeightedGraph weightedGraph = new WeightedGraph(pair.Key);
                        weightedGraph.LoadGraph(pair.Key, SERVER, DATABASE);
                        weightedGraphs.Add(weightedGraph);
                        break;
                    case "Unweighted_Undirected":
                        Graph graph = new Graph(pair.Key);
                        graph.LoadGraph(pair.Key, SERVER, DATABASE);
                        graphs.Add(graph);
                        break;
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            switch (graphNamesAndTypes[button.Name])
            {
                case "Weighted_Directed":
                    foreach (WeightedDigraph weightedDigraph in weightedDigraphs)
                    {
                        if (weightedDigraph.GraphName.Equals(button.Name))
                        {
                            FillPanel(weightedDigraph);
                            break;
                        }
                    }

                    break;
                case "Unweighted_Directed":
                    foreach (Digraph digraph in digraphs)
                    {
                        if (digraph.GraphName.Equals(button.Name))
                        {
                            FillPanel(digraph);
                            break;
                        }
                    }

                    break;
                case "Weighted_Undirected":
                    foreach (WeightedGraph weightedGraph in weightedGraphs)
                    {
                        if (weightedGraph.GraphName.Equals(button.Name))
                        {
                            FillPanel(weightedGraph);
                            break;
                        }
                    }

                    break;
                case "Unweighted_Undirected":
                    foreach (Graph graph in graphs)
                    {
                        if (graph.GraphName.Equals(button.Name))
                        {
                            FillPanel(graph);
                            break;
                        }
                    }

                    break;
            }
        }

        public Point GetLocation(int nodeNumber, int numNodes)
        {
            // MAX NUMBER OF NODES: 26 
            // MAX INNER NUMBER OF NODES: 10

            int xCoord;
            int yCoord;

            if (numNodes < 16 || nodeNumber < 16)
            {
                int DISTANCE_FROM_CENTER = 200;

                double angle = 2.0 * Math.PI / (numNodes) * nodeNumber;

                xCoord = (int)Math.Floor(CENTER + DISTANCE_FROM_CENTER * Math.Cos(angle));
                yCoord = (int)Math.Floor(CENTER - DISTANCE_FROM_CENTER * Math.Sin(angle));
            }
            else
            {
                int DISTANCE_FROM_CENTER = 100;

                double angle = 2.0 * Math.PI / numNodes - 17 * nodeNumber;

                xCoord = (int)Math.Floor(CENTER + DISTANCE_FROM_CENTER * Math.Cos(angle));
                yCoord = (int)Math.Floor(CENTER - DISTANCE_FROM_CENTER * Math.Sin(angle));
            }

            return new Point(xCoord, yCoord);
        }

        private Point GetNeighborLocation(Vertex neighbor)
        {
            for (int labelIndex = 0; labelIndex < LabelNodes.Count; labelIndex++)
            {
                if (LabelNodes[labelIndex].Text == neighbor.Name)
                {
                    return NodeCircleLocations[labelIndex];
                }
            }

            return new Point(200, 200);
        }

        private Point GetNewXAndY(Point location)
        {
            int xCoord;
            int yCoord;

            if (location.X >= 200)
                xCoord = location.X + 10;
            else
                xCoord = location.X - 15;
            if (location.Y >= 200)
                yCoord = location.Y + 15;
            else
                yCoord = location.Y - 15;
            return new Point(xCoord, yCoord);
        }

        private void FillPanel(ParentGraph graph)
        {
            LabelNodes = new List<Label>();
            NodeCircleLocations = new List<Point>();
            panelGraph.Controls.Clear();
            panelGraph.Refresh();

            for (int nodeNumber = 0; nodeNumber < graph.Vertices.Count; nodeNumber++)
            {
                Label label = new Label();
                label.Text = graph.Vertices[nodeNumber].Name;
                label.TextAlign = ContentAlignment.MiddleCenter;

                Graphics graphics = panelGraph.CreateGraphics();
                Pen pen = new Pen(Color.Black);
                Point location = GetLocation(nodeNumber, graph.Vertices.Count);
                graphics.DrawEllipse(pen, location.X, location.Y, 10, 10);
                NodeCircleLocations.Add(location);

                label.Location = GetNewXAndY(location);
                label.Font = SMALL_FONT;
                label.Size = new Size(20, 15);
                label.ForeColor = Color.White;
                label.SendToBack();
                label.Refresh();

                LabelNodes.Add(label);
            }

            foreach (Label label in LabelNodes)
            {
                panelGraph.Controls.Add(label);
                label.Refresh();
            }

            for (int nodeNumber = 0; nodeNumber < graph.Vertices.Count; nodeNumber++)
            {
                Vertex currNode = graph.Vertices[nodeNumber];
                foreach (Vertex neighbor in currNode.Neighbors)
                {
                    if (currNode.Neighbors.Contains(neighbor))
                    {
                        Graphics graphics = panelGraph.CreateGraphics();
                        Pen pen = new Pen(Color.Black);

                        AdjustableArrowCap adjustableArrowCap = new AdjustableArrowCap(3, 3);
                        pen.CustomEndCap = adjustableArrowCap;

                        int penWidth = graph.Vertices[nodeNumber].Neighbors.IndexOf(neighbor);

                        int width = graph.Vertices[nodeNumber].Weights[penWidth];

                        penWidth = width;

                        if (graph.maxWeight > 15)
                        {
                            penWidth /= 10;
                        }
                        
                        
                        pen.Width = penWidth;
                        pen.Color = Color.Black;

                        Point originalLocation = NodeCircleLocations[nodeNumber];
                        Point neighborLocation = GetNeighborLocation(neighbor);
                        graphics.DrawLine(pen, originalLocation, neighborLocation);
                    }
                }
            }
        }
    }
}