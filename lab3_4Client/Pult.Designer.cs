﻿namespace lab3_4Client
{
    partial class Pult
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxIPAddress = new TextBox();
            buttonConnect = new Button();
            buttonDisconnect = new Button();
            labelIP = new Label();
            splitContainer1 = new SplitContainer();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxIPAddress
            // 
            textBoxIPAddress.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxIPAddress.Location = new Point(171, 11);
            textBoxIPAddress.Margin = new Padding(7, 6, 7, 6);
            textBoxIPAddress.Name = "textBoxIPAddress";
            textBoxIPAddress.Size = new Size(214, 39);
            textBoxIPAddress.TabIndex = 8;
            textBoxIPAddress.Text = "127.0.0.1";
            // 
            // buttonConnect
            // 
            buttonConnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonConnect.Location = new Point(32, 111);
            buttonConnect.Margin = new Padding(7, 6, 7, 6);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(186, 66);
            buttonConnect.TabIndex = 9;
            buttonConnect.Text = "Соединиться";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // buttonDisconnect
            // 
            buttonDisconnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonDisconnect.Location = new Point(232, 111);
            buttonDisconnect.Margin = new Padding(7, 6, 7, 6);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(186, 66);
            buttonDisconnect.TabIndex = 10;
            buttonDisconnect.Text = "Отключиться";
            buttonDisconnect.UseVisualStyleBackColor = true;
            buttonDisconnect.Click += buttonDisconnect_Click;
            // 
            // labelIP
            // 
            labelIP.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelIP.AutoSize = true;
            labelIP.Font = new Font("Tahoma", 10.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelIP.ForeColor = SystemColors.ButtonHighlight;
            labelIP.Location = new Point(32, 11);
            labelIP.Margin = new Padding(7, 0, 7, 0);
            labelIP.Name = "labelIP";
            labelIP.Size = new Size(128, 35);
            labelIP.TabIndex = 7;
            labelIP.Text = "IP-адрес";
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(22, 26);
            splitContainer1.Margin = new Padding(6);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AutoScroll = true;
            splitContainer1.Panel1MinSize = 400;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(labelIP);
            splitContainer1.Panel2.Controls.Add(buttonDisconnect);
            splitContainer1.Panel2.Controls.Add(textBoxIPAddress);
            splitContainer1.Panel2.Controls.Add(buttonConnect);
            splitContainer1.Size = new Size(2089, 1167);
            splitContainer1.SplitterDistance = 960;
            splitContainer1.SplitterWidth = 2;
            splitContainer1.TabIndex = 11;
            // 
            // Pult
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Navy;
            ClientSize = new Size(2134, 1218);
            Controls.Add(splitContainer1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(6);
            Name = "Pult";
            Text = "Контроллер";
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBoxIPAddress;
        private Button buttonConnect;
        private Button buttonDisconnect;
        private Label labelIP;
        private SplitContainer splitContainer1;
    }
}
