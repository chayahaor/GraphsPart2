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
            this.panelGraphButtons = new System.Windows.Forms.Panel();
            this.Kruskal = new System.Windows.Forms.Button();
            this.Dijkstra = new System.Windows.Forms.Button();
            this.Topological = new System.Windows.Forms.Button();
            this.Prim = new System.Windows.Forms.Button();
            this.panelNodeSelection = new System.Windows.Forms.Panel();
            this.btnNodeSelection = new System.Windows.Forms.Button();
            this.destDropDown = new System.Windows.Forms.ComboBox();
            this.anotherNode = new System.Windows.Forms.Label();
            this.originDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelGraph = new System.Windows.Forms.Panel();
            this.panelNodeSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGraphButtons
            // 
            this.panelGraphButtons.AutoScroll = true;
            this.panelGraphButtons.Location = new System.Drawing.Point(634, 12);
            this.panelGraphButtons.Name = "panelGraphButtons";
            this.panelGraphButtons.Size = new System.Drawing.Size(160, 330);
            this.panelGraphButtons.TabIndex = 0;
            // 
            // Kruskal
            // 
            this.Kruskal.Location = new System.Drawing.Point(191, 618);
            this.Kruskal.Name = "Kruskal";
            this.Kruskal.Size = new System.Drawing.Size(104, 35);
            this.Kruskal.TabIndex = 0;
            this.Kruskal.Text = "Kruskal\'s Algorithm";
            this.Kruskal.UseVisualStyleBackColor = true;
            this.Kruskal.Click += new System.EventHandler(this.Kruskal_Click);
            // 
            // Dijkstra
            // 
            this.Dijkstra.Location = new System.Drawing.Point(43, 618);
            this.Dijkstra.Name = "Dijkstra";
            this.Dijkstra.Size = new System.Drawing.Size(104, 35);
            this.Dijkstra.TabIndex = 1;
            this.Dijkstra.Text = "Dijkstra\'s Algorithm";
            this.Dijkstra.UseVisualStyleBackColor = true;
            this.Dijkstra.Click += new System.EventHandler(this.Dijkstra_Click);
            // 
            // Topological
            // 
            this.Topological.Location = new System.Drawing.Point(338, 618);
            this.Topological.Name = "Topological";
            this.Topological.Size = new System.Drawing.Size(104, 35);
            this.Topological.TabIndex = 1;
            this.Topological.Text = "Topological Sort";
            this.Topological.UseVisualStyleBackColor = true;
            this.Topological.Click += new System.EventHandler(this.Topological_Click);
            // 
            // Prim
            // 
            this.Prim.Location = new System.Drawing.Point(486, 618);
            this.Prim.Name = "Prim";
            this.Prim.Size = new System.Drawing.Size(104, 35);
            this.Prim.TabIndex = 2;
            this.Prim.Text = "Prim\'s Algorithm";
            this.Prim.UseVisualStyleBackColor = true;
            this.Prim.Click += new System.EventHandler(this.Prim_Click);
            // 
            // panelNodeSelection
            // 
            this.panelNodeSelection.Controls.Add(this.btnNodeSelection);
            this.panelNodeSelection.Controls.Add(this.destDropDown);
            this.panelNodeSelection.Controls.Add(this.anotherNode);
            this.panelNodeSelection.Controls.Add(this.originDropDown);
            this.panelNodeSelection.Controls.Add(this.label1);
            this.panelNodeSelection.Location = new System.Drawing.Point(634, 382);
            this.panelNodeSelection.Name = "panelNodeSelection";
            this.panelNodeSelection.Size = new System.Drawing.Size(165, 230);
            this.panelNodeSelection.TabIndex = 3;
            this.panelNodeSelection.Visible = false;
            // 
            // btnNodeSelection
            // 
            this.btnNodeSelection.Location = new System.Drawing.Point(44, 190);
            this.btnNodeSelection.Name = "btnNodeSelection";
            this.btnNodeSelection.Size = new System.Drawing.Size(76, 23);
            this.btnNodeSelection.TabIndex = 4;
            this.btnNodeSelection.Text = "Ready?";
            this.btnNodeSelection.UseVisualStyleBackColor = true;
            this.btnNodeSelection.Click += new System.EventHandler(this.readyNodes_Click);
            // 
            // destDropDown
            // 
            this.destDropDown.FormattingEnabled = true;
            this.destDropDown.Location = new System.Drawing.Point(22, 142);
            this.destDropDown.Name = "destDropDown";
            this.destDropDown.Size = new System.Drawing.Size(121, 21);
            this.destDropDown.TabIndex = 3;
            this.destDropDown.Visible = false;
            // 
            // anotherNode
            // 
            this.anotherNode.AutoSize = true;
            this.anotherNode.Location = new System.Drawing.Point(10, 109);
            this.anotherNode.Name = "anotherNode";
            this.anotherNode.Size = new System.Drawing.Size(145, 13);
            this.anotherNode.TabIndex = 2;
            this.anotherNode.Text = "Please choose another Node";
            this.anotherNode.Visible = false;
            // 
            // originDropDown
            // 
            this.originDropDown.FormattingEnabled = true;
            this.originDropDown.Location = new System.Drawing.Point(22, 50);
            this.originDropDown.Name = "originDropDown";
            this.originDropDown.Size = new System.Drawing.Size(121, 21);
            this.originDropDown.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please choose a Node";
            // 
            // panelGraph
            // 
            this.panelGraph.Location = new System.Drawing.Point(12, 12);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(600, 600);
            this.panelGraph.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(811, 660);
            this.Controls.Add(this.panelNodeSelection);
            this.Controls.Add(this.Prim);
            this.Controls.Add(this.Topological);
            this.Controls.Add(this.Dijkstra);
            this.Controls.Add(this.Kruskal);
            this.Controls.Add(this.panelGraphButtons);
            this.Controls.Add(this.panelGraph);
            this.Name = "Form1";
            this.Text = "Graphs";
            this.panelNodeSelection.ResumeLayout(false);
            this.panelNodeSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelGraphButtons;
        private System.Windows.Forms.Button Kruskal;
        private System.Windows.Forms.Button Dijkstra;
        private System.Windows.Forms.Button Topological;
        private System.Windows.Forms.Button Prim;
        private System.Windows.Forms.Panel panelNodeSelection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox originDropDown;
        private System.Windows.Forms.ComboBox destDropDown;
        private System.Windows.Forms.Label anotherNode;
        private System.Windows.Forms.Button btnNodeSelection;
        private System.Windows.Forms.Panel panelGraph;
    }
}

