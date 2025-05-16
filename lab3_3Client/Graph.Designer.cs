using ScottPlot;
using ScottPlot.WinForms;

namespace lab3_3Client
{
    partial class Graph
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.chart = new FormsPlot();
            this.lblIP = new System.Windows.Forms.Label();
            this.tBoxIPAddress = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btn_disconnect = new System.Windows.Forms.Button();
            this.ResetGraphs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.Location = new System.Drawing.Point(12, 12);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(905, 427);
            this.chart.TabIndex = 0;
            this.chart.Anchor = (System.Windows.Forms.AnchorStyles.Top |
                                       System.Windows.Forms.AnchorStyles.Left |
                                       System.Windows.Forms.AnchorStyles.Right);
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblIP.Location = new System.Drawing.Point(198, 468);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(60, 17);
            this.lblIP.TabIndex = 3;
            this.lblIP.Text = "IP-адрес:";
            // 
            // tBoxIPAddress
            // 
            this.tBoxIPAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tBoxIPAddress.Location = new System.Drawing.Point(258, 465);
            this.tBoxIPAddress.Name = "tBoxIPAddress";
            this.tBoxIPAddress.Size = new System.Drawing.Size(155, 25);
            this.tBoxIPAddress.TabIndex = 4;
            this.tBoxIPAddress.Text = "127.0.0.1";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnConnect.Location = new System.Drawing.Point(47, 461);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(145, 31);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Подключиться";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btn_disconnect.Location = new System.Drawing.Point(49, 509);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(143, 29);
            this.btn_disconnect.TabIndex = 2;
            this.btn_disconnect.Text = "Отключиться";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            this.btn_disconnect.Click += new System.EventHandler(this.btn_disconnect_Click);
            // 
            // ResetGraphs
            // 
            this.ResetGraphs.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ResetGraphs.Location = new System.Drawing.Point(258, 507);
            this.ResetGraphs.Name = "ResetGraphs";
            this.ResetGraphs.Size = new System.Drawing.Size(145, 31);
            this.ResetGraphs.TabIndex = 5;
            this.ResetGraphs.Text = "Сбросить график";
            this.ResetGraphs.UseVisualStyleBackColor = true;
            this.ResetGraphs.Click += new System.EventHandler(this.ResetGraphs_Click);
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 576);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.tBoxIPAddress);
            this.Controls.Add(this.btn_disconnect);
            this.Controls.Add(this.ResetGraphs);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.chart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Graph";
            this.Text = "Lab 3.3 – P/T-график";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private ScottPlot.FormsPlot chart;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox tBoxIPAddress;
        private System.Windows.Forms.Button btn_disconnect;
        private System.Windows.Forms.Button ResetGraphs;
        private System.Windows.Forms.Button btnConnect;
    }
}
