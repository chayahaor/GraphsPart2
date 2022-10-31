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
        private String server;
        private String database;

        // One graph that contains all of its information
        private GraphNew newGraph;

        // contains all graph names (graph names must be unique in the database)
        //private ArrayList graphNames;
        private ArrayList informationGraphs;

        private GraphInfo associatedInfo;

        // List of all the buttons containing graph names
        private List<Button> GraphNameButtons { get; }

        // the center of the panelGraph panel (size 600 by 600)
        private readonly int CENTER = 300;

        // list of all labels representing nodes specific to each selected graph
        private List<Label> LabelNodes { get; set; }

        // list of all locations of circles where graphics are pointing to for each graph
        private List<Point> NodeCircleLocations { get; set; }

        // stores the two vertices coming from panelNodeSelection
        private Vertex selectedVertexA;
        private Vertex selectedVertexB;

        // the algorithm type of the current algorithm selected
        private AlgorithmType? algorithmType = AlgorithmType.DIJKSTRA;

        public Form1()
        {
            InitializeComponent();
            GraphNameButtons = new List<Button>();
            panelGraph.BackColor = Color.Gray;
            server = ConfigurationManager.AppSettings["SERVER"];
            database = ConfigurationManager.AppSettings["DATABASE"];

            GetData getData = new GetData(server, database);
            informationGraphs = getData.GraphInfos;

            SetUpGraphNameButtons();
        }

        private void SetUpGraphNameButtons()
        {
            int x = 30;
            int y = 0;

            foreach (GraphInfo info in informationGraphs)
            {
                Button button = new Button();
                button.Name = info.name; // All button names are unique because in the SQL code, graph names are unique
                button.Text = info.name;
                button.Click += btn_Click;
                button.Location = new Point(x, y);
                y += 100;
                panelGraphButtons.Controls.Add(button);
            }

            Button showGraph = new Button();
            showGraph.Name = "btnShowGraph";
            showGraph.Text = "Show Weights";
            showGraph.Height += showGraph.Height;
            showGraph.Location = new Point(x, y);
            showGraph.Click += ShowWeights;

            y += 100;
            panelGraphButtons.Controls.Add(showGraph);
        }


        private void btn_Click(object sender, EventArgs e)
        {
            //When click a button
            Button button = (Button)sender;
            //Load the graph
            newGraph = new GraphNew(button.Name, server, database);
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
            Label labelGraphType = new Label();
            labelGraphType.Location = new Point(15, 20);

            String type = "";

            //TODO: Replace with based on bool values of graph name and enable/disable buttons accordingly
            //TODO: Why does Graph A not show label correctly?
            foreach (GraphInfo info in informationGraphs)
            {
                if (info.name == newGraph.GraphName)
                {
                    associatedInfo = info;
                }
            }

            if (associatedInfo.weight)
            {
                type += "Weighted ";
            }
            else
            {
                type += "Unweighted ";
            }

            if (associatedInfo.direct)
            {
                type += "Digraph";
            }
            else
            {
                type += "Graph";
            }

            //TODO: confirm types done correctly
            switch (type)
            {
                case "Weighted Digraph":
                    Topological.Enabled = true;
                    Dijkstra.Enabled = true;
                    Prim.Enabled = false;
                    Kruskal.Enabled = false;
                    break;
                case "Weighted Graph":
                    Dijkstra.Enabled = true;
                    Prim.Enabled = true;
                    Kruskal.Enabled = true;
                    Topological.Enabled = false;
                    break;
                case "Unweighted Digraph":
                    Topological.Enabled = true;
                    Dijkstra.Enabled = false;
                    Prim.Enabled = false;
                    Kruskal.Enabled = false;
                    break;
                case "Unweighted Graph":
                    Topological.Enabled = false;
                    Dijkstra.Enabled = false;
                    Prim.Enabled = false;
                    Kruskal.Enabled = false;
                    break;
            }


            labelGraphType.Text = type;
            labelGraphType.Refresh();
            panelGraph.Controls.Add(labelGraphType);
        }

        private void CreateLabelNodes()
        {
            for (int nodeNumber = 0; nodeNumber < newGraph.Vertices.Count; nodeNumber++)
            {
                Label label = new Label();
                label.Text = newGraph.Vertices[nodeNumber].Name;
                label.TextAlign = ContentAlignment.MiddleCenter;

                Graphics graphics = panelGraph.CreateGraphics();
                Pen pen = new Pen(Color.Black);
                Point location = GetLocation(nodeNumber, newGraph.Vertices.Count);
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

        private new void CreateGraphics()
        {
            SetUpGraphicsAndPen(out Graphics graphics, out Pen pen, Color.Black);

            for (int nodeNumber = 0; nodeNumber < newGraph.Vertices.Count; nodeNumber++)
            {
                Vertex currNode = newGraph.Vertices[nodeNumber];
                foreach (Vertex neighbor in currNode.Neighbors)
                {
                    if (currNode.Neighbors.Contains(neighbor))
                    {
                        pen.Width = 2;
                        pen.Color = Color.Black;

                        Point originalLocation = NodeCircleLocations[nodeNumber];
                        Point neighborLocation = GetVertexLocation(neighbor);
                        graphics.DrawLine(pen, originalLocation, neighborLocation);
                    }
                }
            }
        }

        private void SetUpGraphicsAndPen(out Graphics graphics, out Pen pen, Color penColor)
        {
            graphics = panelGraph.CreateGraphics();
            pen = new Pen(penColor);
            if (associatedInfo.direct)
            {
                AdjustableArrowCap adjustableArrowCap = new AdjustableArrowCap(3, 3);
                pen.CustomEndCap = adjustableArrowCap;
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
            //TODO: is this needed?
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

        private Point GetVertexLocation(Vertex neighbor)
        {
            // default location points to the center of the panel
            Point vertexLocation = new Point(CENTER, CENTER);
            for (int labelIndex = 0; labelIndex < LabelNodes.Count; labelIndex++)
            {
                if (LabelNodes[labelIndex].Text == neighbor.Name)
                {
                    vertexLocation = NodeCircleLocations[labelIndex];
                }
            }

            return vertexLocation;
        }

        //Algorithms
        private void Kruskal_Click(object sender, EventArgs e)
        {
            CreateGraphics();
            panelNodeSelection.Visible = false;

            var output = newGraph.KruskalAlgorithm();
            DrawRedLines(output);
        }

        private void Topological_Click(object sender, EventArgs e)
        {
            CreateGraphics();
            panelNodeSelection.Visible = false;
            newGraph.DoTopological();
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

        private void Prim_Click(object sender, EventArgs e)
        {
            CreateGraphics();
            ShowPanelNodeSelection(false);
            Vertex[,] output = newGraph.PrimAlgorithm(selectedVertexA);

            // draw minimum spanning graph edges in red
            DrawRedLines(output);

            ResetNodeSelectionPanel();
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

        private void Dijkstra_Click(object sender, EventArgs e)
        {
            CreateGraphics();
            ShowPanelNodeSelection(true);
            List<Vertex> output = new List<Vertex>();
            double shortestDist = 0.0;

            newGraph.DijkstraAlgorithm();
            DrawRedLines(output);
            MessageBox.Show("Shortest distance: " + shortestDist);
            ResetNodeSelectionPanel();
        }

        //delete/move to GraphNew
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
            foreach (Vertex vertex in newGraph.Vertices)
            {
                originDropDown.Items.Add(vertex.Name);
            }

            foreach (Vertex vertex in newGraph.Vertices)
            {
                destDropDown.Items.Add(vertex.Name);
            }
        }

        private void readyNodes_Click(object sender, EventArgs e)
        {
            if (originDropDown.SelectedIndex == -1)
            {
                selectedVertexA = newGraph.Vertices[0];
                MessageBox.Show("Default vertex selected");
            }
            else
            {
                selectedVertexA = newGraph.Vertices[originDropDown.SelectedIndex];
                MessageBox.Show("You selected " + selectedVertexA.Name);
            }

            if (destDropDown.SelectedIndex == -1)
            {
                selectedVertexB = newGraph.Vertices[0];
                if (algorithmType != null && algorithmType.Equals(AlgorithmType.DIJKSTRA))
                {
                    MessageBox.Show("Default vertex selected");
                }
            }
            else
            {
                selectedVertexB = newGraph.Vertices[destDropDown.SelectedIndex];
                MessageBox.Show("You selected " + selectedVertexB.Name);
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
            SetUpGraphicsAndPen(out Graphics graphics, out Pen pen, Color.Red);

            Vertex startingVertex = new Vertex("start");
            Vertex endingVertex = new Vertex("end");

            for (int index = 0; index < input.GetLength(0); index++)
            {
                Vertex beginning = input[index, 0];

                Vertex ending = input[index, 1];

                foreach (var vertex in newGraph.Vertices)
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

                pen.Width = 2;

                Point beginLocation = GetVertexLocation(beginning);
                Point neighborLocation = GetVertexLocation(ending);
                graphics.DrawLine(pen, beginLocation, neighborLocation);
            }
        }

        private void DrawRedLines(List<Vertex> input)
        {
            SetUpGraphicsAndPen(out Graphics graphics, out Pen pen, Color.Red);

            Vertex startingVertex;
            Vertex endingVertex;

            for (int i = 0; i < input.Count - 1; i++)
            {
                startingVertex = input[i];
                endingVertex = input[i + 1];

                pen.Width = 2;

                Point startingPoint = GetVertexLocation(startingVertex);
                Point neighborLocation = GetVertexLocation(endingVertex);
                graphics.DrawLine(pen, startingPoint, neighborLocation);

                System.Threading.Thread.Sleep(500);
            }
        }

        private void ShowWeights(Object o, EventArgs e)
        {
            if (newGraph != null)
            {
                WeightsChart chart = new WeightsChart(newGraph);
                chart.Show();
            }
        }

        public void PopUpWeights()
        {
            //TODO: create window with table of weights
        }
    }
}