namespace CrystalReportPOC
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
            this.salesReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.btnGenRep = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // salesReportViewer
            // 
            this.salesReportViewer.ActiveViewIndex = -1;
            this.salesReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.salesReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.salesReportViewer.Location = new System.Drawing.Point(0, 44);
            this.salesReportViewer.Name = "salesReportViewer";
            this.salesReportViewer.Size = new System.Drawing.Size(1232, 517);
            this.salesReportViewer.TabIndex = 0;
            this.salesReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // btnGenRep
            // 
            this.btnGenRep.Location = new System.Drawing.Point(12, 12);
            this.btnGenRep.Name = "btnGenRep";
            this.btnGenRep.Size = new System.Drawing.Size(139, 26);
            this.btnGenRep.TabIndex = 1;
            this.btnGenRep.Text = "Generate Report";
            this.btnGenRep.UseVisualStyleBackColor = true;
            this.btnGenRep.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(174, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "Pick Slip UK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 561);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnGenRep);
            this.Controls.Add(this.salesReportViewer);
            this.Name = "Form1";
            this.Text = "frmShowReport";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer salesReportViewer;
        private System.Windows.Forms.Button btnGenRep;
        private System.Windows.Forms.Button button1;
    }
}

