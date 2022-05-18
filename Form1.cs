using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing.Drawing2D;

namespace GraphsClassProject
{
    public partial class Form1 : Form
    {
        private List<Label> labelNodes = new List<Label>();

        private Digraph digraph;

        private Font SmallFont = new Font("Arial", 8);

        private readonly int CENTER = 200;

        public Form1()
        {
            InitializeComponent();

            panelGraph.BackColor = Color.Gray;

            digraph = new Digraph();

            digraph.LoadVertices(ConfigurationManager.AppSettings["TXT"]);
        }

        public void FillPanel()
        {
            for (int nodeNumber = 0; nodeNumber < digraph.Nodes.Count; nodeNumber++) // every node is on its own line
            {
                Label label = new Label();
                label.Text = digraph.Nodes[nodeNumber].Name;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Location = GetLocation(nodeNumber, digraph.Nodes.Count);
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

                        if (nodeNumber > 16)
                        {
                            graphics.DrawLine(pen, labelNodes[nodeNumber].Location, GetNeighborLocation(neighbor, labelNodes[nodeNumber].Location));
                        }
                        else
                        {
                            graphics.DrawLine(pen, GetNewXAndY(labelNodes[nodeNumber]), GetNeighborLocation(neighbor, labelNodes[nodeNumber].Location));
                        }
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

                    return GetNewXAndY(label);
                }
            }
            //return point;
            return new Point(200, 200);
        }

        private Point GetNewXAndY(Label label)
        {
            int xCoord;
            int yCoord;

            if (label.Location.X >= 200)
                xCoord = label.Location.X - 20;
            else
                xCoord = label.Location.X + 20;
            if (label.Location.Y >= 200)
                yCoord = label.Location.Y - 15;
            else
                yCoord = label.Location.Y + 15;
            return new Point(xCoord, yCoord);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillPanel();
        }

    }
}
