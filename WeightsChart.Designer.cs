using System.ComponentModel;

namespace GraphsClassProject
{
    partial class WeightsChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.backPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // backPanel
            // 
            this.backPanel.AutoScroll = true;
            this.backPanel.AutoSize = true;
            this.backPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.backPanel.Location = new System.Drawing.Point(0, 0);
            this.backPanel.Name = "backPanel";
            this.backPanel.Size = new System.Drawing.Size(864, 0);
            this.backPanel.TabIndex = 0;
            // 
            // WeightsChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(864, 797);
            this.Controls.Add(this.backPanel);
            this.Name = "WeightsChart";
            this.Text = "WeightsChart";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel backPanel;

        #endregion
    }
}