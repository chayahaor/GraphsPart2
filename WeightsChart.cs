using System;
using System.Drawing;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace GraphsClassProject
{
    public partial class WeightsChart : Form
    {
        private ParentGraph graph;
        public WeightsChart(ParentGraph sentGraph)
        {
            InitializeComponent();
            graph = sentGraph;
            LoopThrough();
        }

        private void LoopThrough()
        {
            String output = "";

            foreach (Vertex currNode in graph.Vertices)
            {
                foreach (Vertex neighbor in currNode.Neighbors)
                {
                    if (currNode.Neighbors.Contains(neighbor))
                    {
                        output = output + "\n\n" + currNode.Name + " " +
                                 neighbor.Name + " has the weight " +
                                 graph.GetWeight(currNode, neighbor);
                    }
                }
            }
            Label label = new Label();
            label.Text = output + "\n\n\n\n";
            label.Font = new Font("Arial", 8);
            label.AutoSize = true;
            label.Location = new Point(5, 5);
            this.Controls.Add(label);
            this.AutoScroll = true;
        }

    }
}