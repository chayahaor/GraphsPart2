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
            this.panelGraphButtons.Location = new System.Drawing.Point(2008, 34);
            this.panelGraphButtons.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.panelGraphButtons.Name = "panelGraphButtons";
            this.panelGraphButtons.Size = new System.Drawing.Size(507, 1035);
            this.panelGraphButtons.TabIndex = 0;
            // 
            // Kruskal
            // 
            this.Kruskal.Enabled = false;
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
            this.Dijkstra.Enabled = false;
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
            this.Topological.Enabled = false;
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
            this.Prim.Enabled = false;
            this.Prim.Location = new System.Drawing.Point(1539, 1759);
            this.Prim.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.Prim.Name = "Prim";
            this.Prim.Size = new System.Drawing.Size(329, 100);
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
            this.panelNodeSelection.Location = new System.Drawing.Point(2008, 1087);
            this.panelNodeSelection.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.panelNodeSelection.Name = "panelNodeSelection";
            this.panelNodeSelection.Size = new System.Drawing.Size(523, 655);
            this.panelNodeSelection.TabIndex = 3;
            this.panelNodeSelection.Visible = false;
            // 
            // btnNodeSelection
            // 
            this.btnNodeSelection.Location = new System.Drawing.Point(139, 541);
            this.btnNodeSelection.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.btnNodeSelection.Name = "btnNodeSelection";
            this.btnNodeSelection.Size = new System.Drawing.Size(241, 65);
            this.btnNodeSelection.TabIndex = 4;
            this.btnNodeSelection.Text = "Ready?";
            this.btnNodeSelection.UseVisualStyleBackColor = true;
            this.btnNodeSelection.Click += new System.EventHandler(this.readyNodes_Click);
            // 
            // destDropDown
            // 
            this.destDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.destDropDown.FormattingEnabled = true;
            this.destDropDown.Location = new System.Drawing.Point(70, 404);
            this.destDropDown.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.destDropDown.Name = "destDropDown";
            this.destDropDown.Size = new System.Drawing.Size(375, 45);
            this.destDropDown.TabIndex = 3;
            this.destDropDown.Visible = false;
            // 
            // anotherNode
            // 
            this.anotherNode.AutoSize = true;
            this.anotherNode.Location = new System.Drawing.Point(32, 310);
            this.anotherNode.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.anotherNode.Name = "anotherNode";
            this.anotherNode.Size = new System.Drawing.Size(429, 37);
            this.anotherNode.TabIndex = 2;
            this.anotherNode.Text = "Please choose another Node";
            this.anotherNode.Visible = false;
            // 
            // originDropDown
            // 
            this.originDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.originDropDown.FormattingEnabled = true;
            this.originDropDown.Location = new System.Drawing.Point(70, 142);
            this.originDropDown.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.originDropDown.Name = "originDropDown";
            this.originDropDown.Size = new System.Drawing.Size(375, 45);
            this.originDropDown.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please choose a Node";
            // 
            // panelGraph
            // 
            this.panelGraph.Location = new System.Drawing.Point(38, 34);
            this.panelGraph.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(1900, 1708);
            this.panelGraph.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(2568, 1878);
            this.Controls.Add(this.panelNodeSelection);
            this.Controls.Add(this.Prim);
            this.Controls.Add(this.Topological);
            this.Controls.Add(this.Dijkstra);
            this.Controls.Add(this.Kruskal);
            this.Controls.Add(this.panelGraphButtons);
            this.Controls.Add(this.panelGraph);
            this.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
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

