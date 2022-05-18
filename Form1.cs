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
        private List<Label> labelNodes = new List<Label>();
        private List<Point> nodeCircleLocations = new List<Point>();

        private Digraph digraph;

        private Font SmallFont = new Font("Arial", 8);

        private readonly int CENTER = 325;

        public Form1()
        {
            InitializeComponent();

            panelGraph.BackColor = Color.Gray;

            digraph = new Digraph();

            digraph.LoadVertices(ConfigurationManager.AppSettings["TXT"]);
        }

        public void FillPanel()
        {
            //int x = 0; int y = 20;

            // int x = 300; int y = 20;
            for (int nodeNumber = 0; nodeNumber < digraph.Nodes.Count; nodeNumber++) // every node is on its own line
            {
                Label label = new Label();
                label.Text = digraph.Nodes[nodeNumber].Name;
                label.TextAlign = ContentAlignment.MiddleCenter;
                // label.Location = new Point(x, y);

                /*
                y += 50;

                if (y > 600)
                {
                    x += 200;
                    y = 20;
                }
                */

                /*
                if (x > 700)
                {
                    x = 0; y += 100;
                }
                else
                {
                    x += 100;
                }
                */


                Graphics graphics = panelGraph.CreateGraphics();
                Pen pen = new Pen(Color.Black);

                Point location = GetLocation(nodeNumber, digraph.Nodes.Count);
                graphics.DrawEllipse(pen, location.X, location.Y, 10, 10);
                nodeCircleLocations.Add(location);


                //label.Location = new Point(location.X, location.Y - 20);
                label.Location = GetNewXAndY(location);
                label.Font = SmallFont;
                label.Size = new Size(20, 15);
                label.ForeColor = Color.White;
                label.SendToBack();
                label.Refresh();

                labelNodes.Add(label);

            }

            foreach (Label label in labelNodes)
            {
                panelGraph.Controls.Add(label);
                label.Refresh();
            }

            for (int nodeNumber = 0; nodeNumber < digraph.Nodes.Count; nodeNumber++)
            {
                Vertex currNode = digraph.Nodes[nodeNumber];
                foreach (Vertex neighbor in currNode.Neighbors)
                {
                    if (currNode.Neighbors.Contains(neighbor))
                    {
                        Graphics graphics = panelGraph.CreateGraphics();
                        Pen pen = new Pen(Color.Black);

                        AdjustableArrowCap adjustableArrowCap = new AdjustableArrowCap(3, 3);
                        pen.CustomEndCap = adjustableArrowCap;

                        pen.Width = 3;
                        pen.Color = Color.Black;

                        /*
                        if (nodeNumber > 16)
                        {
                            graphics.DrawLine(pen, labelNodes[nodeNumber].Location, GetNeighborLocation(neighbor, labelNodes[nodeNumber].Location));
                        }
                        else
                        {
                            graphics.DrawLine(pen, GetNewXAndY(labelNodes[nodeNumber]), GetNeighborLocation(neighbor, labelNodes[nodeNumber].Location));
                        }
                        */


                        graphics.DrawLine(pen, nodeCircleLocations[nodeNumber], GetNeighborLocation(neighbor, labelNodes[nodeNumber].Location));
                    }
                }
            }
        }

        /// <summary>
        /// nodeNumber is the ID from table, for txt file, it's the line number
        /// </summary>
        /// <param name="nodeNumber"></param>
        /// <param name="numNodes"></param>
        /// <returns></returns>
        public Point GetLocation(int nodeNumber, int numNodes)
        {
            // MAX NUMBER OF NODES: 26 
            // MAX INNER NUMBER OF NODES: 10

            int xCoord;
            int yCoord;

            if (numNodes < 16 || nodeNumber < 16)
            {


                int DISTANCE_FROM_CENTER = 200;

                int num = (numNodes < 16) ? numNodes : (numNodes % 16);
                double angle = 2.0 * Math.PI / (num) * nodeNumber;

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

        private Point GetNeighborLocation(Vertex neighbor, Point point)
        {
            for (int labelIndex = 0; labelIndex < labelNodes.Count; labelIndex++)
            {
                if (labelNodes[labelIndex].Text == neighbor.Name)
                {
                    Label label = labelNodes[labelIndex];

                    return nodeCircleLocations[labelIndex];
                    // return new Point(label.Location.X, label.Location.Y + 20);

                    //return GetNewXAndY(label.Location);
                }
            }
            //return point;
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


        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillPanel();
        }
    }
}
