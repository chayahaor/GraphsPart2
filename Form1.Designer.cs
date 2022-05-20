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
            this.panelGraphButtons.Size = new System.Drawing.Size(253, 1708);
            this.panelGraphButtons.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(2562, 1972);
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
    }
}

