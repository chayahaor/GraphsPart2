using System;
using System.Drawing;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace GraphsClassProject
{
    public partial class WeightsChart : Form
    {
        private GraphNew GraphNew;
        public WeightsChart(GraphNew sentGraphNew)
        {
            InitializeComponent();
            GraphNew = sentGraphNew;
            LoopThrough();
        }

        private void LoopThrough()
        {
            String Output = "";

            foreach (Vertex CurrNode in GraphNew.Vertices)
            {
                foreach (Vertex Neighbor in CurrNode.Neighbors)
                {
                    if (CurrNode.Neighbors.Contains(Neighbor))
                    {
                        Output = Output + "\n\n" + CurrNode.Name + " " +
                                 Neighbor.Name + " has the weight " +
                                 GraphNew.GetEdgeWeight(CurrNode, Neighbor);
                    }
                }
            }
            Label Label = new Label();
            Label.Text = Output + "\n\n\n\n";
            Label.Font = new Font("Arial", 8);
            Label.AutoSize = true;
            Label.Location = new Point(5, 5);
            this.Controls.Add(Label);
            this.AutoScroll = true;
        }

    }
}