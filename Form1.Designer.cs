namespace GraphsClassProject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelGraph = new System.Windows.Forms.Panel();
            this.panelGraphButtons = new System.Windows.Forms.Panel();
            this.Kruskal = new System.Windows.Forms.Button();
            this.Dijkstra = new System.Windows.Forms.Button();
            this.Topological = new System.Windows.Forms.Button();
            this.Prim = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelGraph
            // 
            this.panelGraph.Location = new System.Drawing.Point(38, 34);
            this.panelGraph.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(1900, 1708);
            this.panelGraph.TabIndex = 0;
            // 
            // panelGraphButtons
            // 
            this.panelGraphButtons.Location = new System.Drawing.Point(2147, 34);
            this.panelGraphButtons.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.panelGraphButtons.Name = "panelGraphButtons";
            this.panelGraphButtons.Size = new System.Drawing.Size(253, 965);
            this.panelGraphButtons.TabIndex = 0;
            // 
            // Kruskal
            // 
            this.Kruskal.Location = new System.Drawing.Point(605, 1759);
            this.Kruskal.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.Kruskal.Name = "Kruskal";
            this.Kruskal.Size = new System.Drawing.Size(329, 100);
            this.Kruskal.TabIndex = 0;
            this.Kruskal.Text = "Kruskal\'s Algorithm";
            this.Kruskal.UseVisualStyleBackColor = true;
            this.Kruskal.Click += new System.EventHandler(this.Kruskal_Click);
            // 
            // Dijkstra
            // 
            this.Dijkstra.Location = new System.Drawing.Point(136, 1759);
            this.Dijkstra.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.Dijkstra.Name = "Dijkstra";
            this.Dijkstra.Size = new System.Drawing.Size(329, 100);
            this.Dijkstra.TabIndex = 1;
            this.Dijkstra.Text = "Dijkstra\'s Algorithm";
            this.Dijkstra.UseVisualStyleBackColor = true;
            this.Dijkstra.Click += new System.EventHandler(this.Dijkstra_Click);
            // 
            // Topological
            // 
            this.Topological.Location = new System.Drawing.Point(1070, 1759);
            this.Topological.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.Topological.Name = "Topological";
            this.Topological.Size = new System.Drawing.Size(329, 100);
            this.Topological.TabIndex = 1;
            this.Topological.Text = "Topological Sort";
            this.Topological.UseVisualStyleBackColor = true;
            this.Topological.Click += new System.EventHandler(this.Topological_Click);
            // 
            // Prim
            // 
            this.Prim.Location = new System.Drawing.Point(1539, 1759);
            this.Prim.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.Prim.Name = "Prim";
            this.Prim.Size = new System.Drawing.Size(329, 100);
            this.Prim.TabIndex = 2;
            this.Prim.Text = "Prim\'s Algorithm";
            this.Prim.UseVisualStyleBackColor = true;
            this.Prim.Click += new System.EventHandler(this.Prim_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(2537, 1870);
            this.Controls.Add(this.Prim);
            this.Controls.Add(this.Topological);
            this.Controls.Add(this.Dijkstra);
            this.Controls.Add(this.Kruskal);
            this.Controls.Add(this.panelGraphButtons);
            this.Controls.Add(this.panelGraph);
            this.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.Name = "Form1";
            this.Text = "Graphs";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelGraph;
        private System.Windows.Forms.Panel panelGraphButtons;
        private System.Windows.Forms.Button Kruskal;
        private System.Windows.Forms.Button Dijkstra;
        private System.Windows.Forms.Button Topological;
        private System.Windows.Forms.Button Prim;
    }
}

