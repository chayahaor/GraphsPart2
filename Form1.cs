using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;

namespace GraphsClassProject
{
    public partial class Form1 : Form
    {
        // lists containing each graph in the database based on its type
        private List<Digraph> digraphs;
        private List<Graph> graphs;
        private List<WeightedDigraph> weightedDigraphs;
        private List<WeightedGraph> weightedGraphs;

        // contains all graph names and types (graph names must be unique in the database)
        private Dictionary<String, String> graphNamesAndTypes;

        // List of all the buttons containing graph names
        private List<Button> GraphNameButtons { get; set; }

        // the center of the panelGraph panel (size 600 by 600)
        private readonly int CENTER = 300;

        // the current graph on display in the panelGraph panel 
        private ParentGraph currentGraphShowing;

        // list of all labels representing nodes specific to each selected graph
        private List<Label> LabelNodes { get; set; }

        // list of all locations of circles where graphics are pointing to for each graph
        private List<Point> NodeCircleLocations { get; set; }

        // stores the two vertices coming from panelNodeSelection
        private Vertex selectedVertexA;
        private Vertex selectedVertexB;

        // the algorithm type of the current algorithm selected
        private AlgorithmType? algorithmType;

        public Form1()
        {
            InitializeComponent();

            digraphs = new List<Digraph>();
            graphs = new List<Graph>();
            weightedDigraphs = new List<WeightedDigraph>();
            weightedGraphs = new List<WeightedGraph>();

            GraphNameButtons = new List<Button>();

            panelGraph.BackColor = Color.Gray;

            var server = ConfigurationManager.AppSettings["SERVER"];
            var database = ConfigurationManager.AppSettings["DATABASE"];
            GetData getData = new GetData(server, database);

            graphNamesAndTypes = getData.GraphTypes;

            SetUpGraphNameButtons(server, database);
        }

        private void SetUpGraphNameButtons(string server, string database)
        {
            int x = 30;
            int y = 0;
            foreach (KeyValuePair<string, string> pair in graphNamesAndTypes)
            {
                Button button = new Button();
                button.Name =
                    pair.Key; // All button names are unique because in the SQL code, graph names are unique
                button.Text = pair.Key;
                button.Click += new EventHandler(btn_Click);
                button.Location = new Point(x, y);
                GraphNameButtons.Add(button);

                y += 100;

                panelGraphButtons.Controls.Add(button);
                LoadGraph(server, database, pair);
            }
        }

        private void LoadGraph(string server, string database, KeyValuePair<string, string> pair)
        {
            string errorMessage = "Something went wrong with loading the graph...";
            switch (pair.Value)
            {
                case "Weighted_Directed":
                    WeightedDigraph weightedDigraph = new WeightedDigraph(pair.Key);
                    if (!weightedDigraph.LoadGraph(pair.Key, server, database))
                    {
                        MessageBox.Show(errorMessage);
                    }

                    weightedDigraphs.Add(weightedDigraph);
                    break;
                case "Unweighted_Directed":
                    Digraph digraph = new Digraph(pair.Key);
                    if (!digraph.LoadGraph(pair.Key, server, database))
                    {
                        MessageBox.Show(errorMessage);
                    }

                    digraphs.Add(digraph);
                    break;
                case "Weighted_Undirected":
                    WeightedGraph weightedGraph = new WeightedGraph(pair.Key);
                    if (!weightedGraph.LoadGraph(pair.Key, server, database))
                    {
                        MessageBox.Show(errorMessage);
                    }

                    weightedGraphs.Add(weightedGraph);
                    break;
                case "Unweighted_Undirected":
                    Graph graph = new Graph(pair.Key);
                    if (!graph.LoadGraph(pair.Key, server, database))
                    {
                        MessageBox.Show(errorMessage);
                    }

                    graphs.Add(graph);
                    break;
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
                            currentGraphShowing = weightedDigraph;
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
                            currentGraphShowing = digraph;
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
                            currentGraphShowing = weightedGraph;
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
                            currentGraphShowing = graph;
                            break;
                        }
                    }

                    break;
            }
        }

        private void FillPanel(ParentGraph graph)
        {
            ResetPanels();

            CreateLabelType(graph);

            CreateLabelNodes(graph);

            CreateGraphics(graph);
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
            myBox.SelectedIndex = -1;
            destDropDown.SelectedIndex = -1;
            myBox.Items.Clear();
            destDropDown.Items.Clear();
            myBox.ResetText();
            destDropDown.ResetText();
        }

        private void CreateLabelType(ParentGraph graph)
        {
            Label labelGraphType = new Label();
            labelGraphType.Location = new Point(15, 20);

            String type = "";
            switch (graph.Type)
            {
                case GraphType.WEIGHTED_DIGRAPH:
                    type = "Weighted Digraph";
                    break;
                case GraphType.DIGRAPH:
                    type = "Digraph";
                    break;
                case GraphType.WEIGHTED_GRAPH:
                    type = "Weighted Graph";
                    break;
                case GraphType.GRAPH:
                    type = "Graph";
                    break;
            }

            labelGraphType.Text = type;
            labelGraphType.Refresh();
            panelGraph.Controls.Add(labelGraphType);
        }

        private void CreateLabelNodes(ParentGraph graph)
        {
            for (int nodeNumber = 0; nodeNumber < graph.Vertices.Count; nodeNumber++)
            {
                Label label = new Label();
                label.Text = graph.Vertices[nodeNumber].Name;
                label.TextAlign = ContentAlignment.MiddleCenter;

                Graphics graphics = panelGraph.CreateGraphics();
                Pen pen = new Pen(Color.Black);
                Point location = GetLocation(nodeNumber, graph.Vertices.Count);
                graphics.DrawEllipse(pen, location.X - 5, location.Y - 5, 10, 10);
                NodeCircleLocations.Add(location);

                label.Location = GetNewXAndY(location);
                label.Font = new Font("Arial", 8);
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
        }

        private void CreateGraphics(ParentGraph graph)
        {
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

                        int penWidth = graph.GetWeight(graph.Vertices[nodeNumber], neighbor);
                        if (graph.MaxWeight > 15)
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

        private Point GetLocation(int nodeNumber, int numNodes)
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

        private Point GetNeighborLocation(Vertex neighbor)
        {
            Point neighborLocation = new Point(CENTER, CENTER); // default location points to the center of the panel
            for (int labelIndex = 0; labelIndex < LabelNodes.Count; labelIndex++)
            {
                if (LabelNodes[labelIndex].Text == neighbor.Name)
                {
                    neighborLocation = NodeCircleLocations[labelIndex];
                }
            }

            return neighborLocation;
        }

        private void Kruskal_Click(object sender, EventArgs e)
        {
            if (currentGraphShowing == null)
            {
                MessageBox.Show("There is no graph showing yet.");
            }
            else if (currentGraphShowing.Type != GraphType.WEIGHTED_GRAPH)
            {
                MessageBox.Show("Kruskal's Algorithm is not available for selected graph.");
            }
            else
            {
                CreateGraphics(currentGraphShowing);

                algorithmType = AlgorithmType.KRUSKAL;

                panelNodeSelection.Visible = false;

                foreach (WeightedGraph weightedGraph in weightedGraphs)
                {
                    if (weightedGraph.GraphName.Equals(currentGraphShowing.GraphName))
                    {
                        Vertex[,] output = weightedGraph.DoKruskalAlgorithm();

                        // draw minimum spanning graph edges in red,
                        // gives users time to admire the red lines
                        DrawRedLines(currentGraphShowing, output);

                        break;
                    }
                }
            }
        }

        private void Topological_Click(object sender, EventArgs e)
        {
            if (currentGraphShowing == null)
            {
                MessageBox.Show("There is no graph showing yet.");
            }
            else if (currentGraphShowing.Type == GraphType.WEIGHTED_GRAPH ||
                     currentGraphShowing.Type == GraphType.GRAPH)
            {
                MessageBox.Show("Topological Sort is not available for selected graph.");
            }
            else
            {
                CreateGraphics(currentGraphShowing);

                algorithmType = AlgorithmType.TOPOLOGICAL;

                panelNodeSelection.Visible = false;
                DoTopological();
            }
        }

        private void DoTopological()
        {
            string showingOutput = "";
            try
            {
                Vertex[] output = new Vertex[0];
                if (currentGraphShowing.Type == GraphType.WEIGHTED_DIGRAPH)
                {
                    foreach (WeightedDigraph weightedDigraph in weightedDigraphs)
                    {
                        if (weightedDigraph.GraphName == currentGraphShowing.GraphName)
                        {
                            output = weightedDigraph.DoTopologicalSort();
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
                            output = digraph.DoTopologicalSort();
                            break;
                        }
                    }
                }

                System.Threading.Thread.Sleep(1000);


                foreach (Vertex vertex in output)
                {
                    showingOutput += vertex.Name + " ";
                }

                MessageBox.Show("Topological sort of " + currentGraphShowing.GraphName + ":\n\n" + showingOutput);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Prim_Click(object sender, EventArgs e)
        {
            if (currentGraphShowing == null)
            {
                MessageBox.Show("There is no graph showing yet.");
            }
            else if (currentGraphShowing.Type != GraphType.WEIGHTED_GRAPH)
            {
                MessageBox.Show("Prim's Algorithm is not available for selected graph.");
            }
            else
            {
                CreateGraphics(currentGraphShowing);

                algorithmType = AlgorithmType.PRIM;

                ShowPanelNodeSelection(false);
            }
        }

        private void DoPrim()
        {
            foreach (WeightedGraph weightedGraph in weightedGraphs)
            {
                if (weightedGraph.GraphName.Equals(currentGraphShowing.GraphName))
                {
                    Vertex[,] output = weightedGraph.DoPrimAlgorithm(selectedVertexA);
                    StringBuilder showingOutput = new StringBuilder();
                    for (int i = 0; i < output.GetLength(0); i++)
                    {
                        for (int j = 0; j < output.GetLength(1); j++)
                        {
                            showingOutput.Append(output[i, j].Name).Append("\n");
                        }
                    }

                    MessageBox.Show(showingOutput.ToString());
                    break;
                }
            }

            ResetNodeSelectionPanel();
        }

        private void Dijkstra_Click(object sender, EventArgs e)
        {
            if (currentGraphShowing == null)
            {
                MessageBox.Show("There is no graph showing yet.");
            }
            else if (currentGraphShowing.Type == GraphType.GRAPH || currentGraphShowing.Type == GraphType.DIGRAPH)
            {
                MessageBox.Show("Dijkstra's Algorithm is not available for selected graph.");
            }
            else
            {
                CreateGraphics(currentGraphShowing);

                algorithmType = AlgorithmType.DIJKSTRA;

                ShowPanelNodeSelection(true);
            }
        }

        private void DoDijkstra()
        {
            List<Vertex> output = new List<Vertex>();

            StringBuilder showingOutput = new StringBuilder();

            try
            {
                if (currentGraphShowing.Type == GraphType.WEIGHTED_GRAPH)
                {
                    foreach (WeightedGraph weightedGraph in weightedGraphs)
                    {
                        if (weightedGraph.GraphName.Equals(currentGraphShowing.GraphName))
                        {
                            output = weightedGraph.DoDijkstraAlgorithm(selectedVertexA, selectedVertexB);

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

                            break;
                        }
                    }
                }


                foreach (Vertex vertex in output)
                {
                    showingOutput.Append(vertex.Name).Append(" ");
                }

                MessageBox.Show(showingOutput.ToString());
                DrawRedLines(currentGraphShowing, output, selectedVertexB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            GetInput(currentGraphShowing);
        }

        private void GetInput(ParentGraph parentGraph)
        {
            foreach (Vertex vertex in parentGraph.Vertices)
            {
                myBox.Items.Add(vertex.Name);
            }

            foreach (Vertex vertex in parentGraph.Vertices)
            {
                destDropDown.Items.Add(vertex.Name);
            }
        }

        private void readyNodes_Click(object sender, EventArgs e)
        {
            if (myBox.SelectedIndex == -1)
            {
                selectedVertexA = currentGraphShowing.Vertices[0];
                MessageBox.Show("Default vertex selected");
            }
            else
            {
                selectedVertexA = currentGraphShowing.Vertices[myBox.SelectedIndex];
                MessageBox.Show("You selected " + selectedVertexA.Name);
            }

            if (destDropDown.SelectedIndex == -1)
            {
                selectedVertexB = currentGraphShowing.Vertices[0];
                if (algorithmType != null && algorithmType.Equals(AlgorithmType.DIJKSTRA))
                {
                    MessageBox.Show("Default vertex selected");
                }
            }
            else
            {
                selectedVertexB = currentGraphShowing.Vertices[destDropDown.SelectedIndex];
                MessageBox.Show("You selected " + selectedVertexB.Name);
            }

            if (algorithmType != null && algorithmType.Equals(AlgorithmType.PRIM))
            {
                DoPrim();
            }
            else if (algorithmType != null && algorithmType.Equals(AlgorithmType.DIJKSTRA))
            {
                DoDijkstra();
            }
        }

        private void DrawRedLines(ParentGraph graph, Vertex[,] input)
        {
            Graphics graphics = panelGraph.CreateGraphics();
            Pen pen = new Pen(Color.Red);
            Vertex startingVertex = new Vertex("start");
            Vertex endingVertex = new Vertex("end");

            for (int index = 0; index < input.GetLength(0); index++)
            {
                Vertex beginning = input[index, 0];

                Vertex ending = input[index, 1];

                AdjustableArrowCap adjustableArrowCap = new AdjustableArrowCap(3, 3);
                pen.CustomEndCap = adjustableArrowCap;

                Point beginPoint = new Point(0, 0);
                for (int i = 0; i < LabelNodes.Count; i++)
                {
                    if (LabelNodes[i].Text.Equals(beginning.Name))
                    {
                        beginPoint = NodeCircleLocations[i];
                    }
                }

                
                foreach (var vertex in graph.Vertices)
                {
                    if (vertex.Name.Equals(input[index, 0].Name))
                    {
                        startingVertex = vertex;
                    }

                    if (vertex.Name.Equals(input[index, 1].Name))
                    {
                        endingVertex = vertex;
                    }
                }

                int penWidth = 13;
                penWidth = graph.GetWeight(startingVertex, endingVertex);
                if (graph.MaxWeight > 15)
                {
                    penWidth /= 10;
                }

                pen.Width = penWidth;

                Point neighborLocation = GetNeighborLocation(ending);
                graphics.DrawLine(pen, beginPoint, neighborLocation);
            }
        }

        private void DrawRedLines(ParentGraph graph, List<Vertex> input, Vertex selectedVertexB)
        {
            Graphics graphics = panelGraph.CreateGraphics();
            Pen pen = new Pen(Color.Red);
            Vertex startingVertex;
            Vertex endingVertex;
            int penWidth;

            for (int i = 0; i < input.Count - 1; i++)
            {
                startingVertex = input[i];
                endingVertex = input[i + 1];
                penWidth = graph.GetWeight(startingVertex, endingVertex);
                if (graph.MaxWeight > 15)
                {
                    penWidth /= 10;
                }

                pen.Width = penWidth;

                Point startingPoint = GetNeighborLocation(startingVertex);
                Point neighborLocation = GetNeighborLocation(endingVertex);
                graphics.DrawLine(pen, startingPoint, neighborLocation);
                System.Threading.Thread.Sleep(500);
            }

            Point secondToLast = GetNeighborLocation(input[input.Count - 1]);
            Point last = GetNeighborLocation(selectedVertexB);
            graphics.DrawLine(pen, secondToLast, last);
        }
    }
}