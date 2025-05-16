namespace lab3Client
{
    partial class Translator
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
            comboBoxSearch = new ComboBox();
            listBoxSearch = new ListBox();
            labelIP = new Label();
            textBoxIPAddress = new TextBox();
            buttonConnect = new Button();
            buttonDisconnect = new Button();
            buttonExit = new Button();
            buttonRequestDir = new Button();
            buttonRequestFile = new Button();
            labelClient = new Label();
            richTextClient = new RichTextBox();
            fileContent = new RichTextBox();
            labelServer = new Label();
            richTextServer = new RichTextBox();
            SuspendLayout();
            // 
            // comboBoxSearch
            // 
            comboBoxSearch.FormattingEnabled = true;
            comboBoxSearch.Location = new Point(26, 30);
            comboBoxSearch.Name = "comboBoxSearch";
            comboBoxSearch.Size = new Size(600, 40);
            comboBoxSearch.TabIndex = 0;
            comboBoxSearch.TextChanged += comboBoxSearch_TextChanged;
            // 
            // listBoxSearch
            // 
            listBoxSearch.FormattingEnabled = true;
            listBoxSearch.Location = new Point(26, 96);
            listBoxSearch.Name = "listBoxSearch";
            listBoxSearch.Size = new Size(600, 740);
            listBoxSearch.TabIndex = 1;
            listBoxSearch.DoubleClick += SendToServer_DoubleClick;
            // 
            // labelIP
            // 
            labelIP.AutoSize = true;
            labelIP.Location = new Point(26, 866);
            labelIP.Name = "labelIP";
            labelIP.Size = new Size(106, 32);
            labelIP.TabIndex = 2;
            labelIP.Text = "IP-адрес";
            // 
            // textBoxIPAddress
            // 
            textBoxIPAddress.Location = new Point(149, 860);
            textBoxIPAddress.Name = "textBoxIPAddress";
            textBoxIPAddress.Size = new Size(214, 39);
            textBoxIPAddress.TabIndex = 3;
            textBoxIPAddress.Text = "127.0.0.1";
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(26, 932);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(186, 66);
            buttonConnect.TabIndex = 4;
            buttonConnect.Text = "Соединиться";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // buttonDisconnect
            // 
            buttonDisconnect.Location = new Point(245, 932);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(186, 66);
            buttonDisconnect.TabIndex = 5;
            buttonDisconnect.Text = "Отключиться";
            buttonDisconnect.UseVisualStyleBackColor = true;
            buttonDisconnect.Click += buttonDisconnect_Click;
            // 
            // buttonExit
            // 
            buttonExit.Location = new Point(468, 932);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(158, 66);
            buttonExit.TabIndex = 6;
            buttonExit.Text = "Выход";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // buttonRequestDir
            // 
            buttonRequestDir.Location = new Point(26, 1024);
            buttonRequestDir.Name = "buttonRequestDir";
            buttonRequestDir.Size = new Size(283, 66);
            buttonRequestDir.TabIndex = 7;
            buttonRequestDir.Text = "Передать серверу";
            buttonRequestDir.UseVisualStyleBackColor = true;
            buttonRequestDir.Click += buttonRequestDir_Click;
            // 
            // buttonRequestFile
            // 
            buttonRequestFile.Location = new Point(347, 1024);
            buttonRequestFile.Name = "buttonRequestFile";
            buttonRequestFile.Size = new Size(283, 66);
            buttonRequestFile.TabIndex = 8;
            buttonRequestFile.Text = "Передать клиенту";
            buttonRequestFile.UseVisualStyleBackColor = true;
            buttonRequestFile.Click += buttonRequestFile_Click;
            // 
            // labelClient
            // 
            labelClient.AutoSize = true;
            labelClient.Location = new Point(667, 30);
            labelClient.Name = "labelClient";
            labelClient.Size = new Size(235, 32);
            labelClient.TabIndex = 9;
            labelClient.Text = "Клиентская сторона";
            // 
            // richTextClient
            // 
            richTextClient.Location = new Point(667, 96);
            richTextClient.Name = "richTextClient";
            richTextClient.ReadOnly = true;
            richTextClient.Size = new Size(498, 350);
            richTextClient.TabIndex = 10;
            richTextClient.Text = "";
            // 
            // fileContent
            // 
            fileContent.Location = new Point(667, 462);
            fileContent.Name = "fileContent";
            fileContent.ReadOnly = true;
            fileContent.Size = new Size(498, 479);
            fileContent.TabIndex = 11;
            fileContent.Text = "";
            // 
            // labelServer
            // 
            labelServer.AutoSize = true;
            labelServer.Location = new Point(1189, 30);
            labelServer.Name = "labelServer";
            labelServer.Size = new Size(230, 32);
            labelServer.TabIndex = 12;
            labelServer.Text = "Серверная сторона";
            // 
            // richTextServer
            // 
            richTextServer.Location = new Point(1189, 96);
            richTextServer.Name = "richTextServer";
            richTextServer.ReadOnly = true;
            richTextServer.Size = new Size(487, 845);
            richTextServer.TabIndex = 13;
            richTextServer.Text = "";
            // 
            // Translator
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.OldLace;
            ClientSize = new Size(1701, 1131);
            Controls.Add(richTextServer);
            Controls.Add(labelServer);
            Controls.Add(fileContent);
            Controls.Add(richTextClient);
            Controls.Add(labelClient);
            Controls.Add(buttonRequestFile);
            Controls.Add(buttonRequestDir);
            Controls.Add(buttonExit);
            Controls.Add(buttonDisconnect);
            Controls.Add(buttonConnect);
            Controls.Add(textBoxIPAddress);
            Controls.Add(labelIP);
            Controls.Add(listBoxSearch);
            Controls.Add(comboBoxSearch);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Translator";
            Text = "Программа для обмена данными между компьютерами";
            FormClosing += Translator_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSearch;
        private System.Windows.Forms.ListBox listBoxSearch;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.TextBox textBoxIPAddress;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonRequestDir;
        private System.Windows.Forms.Button buttonRequestFile;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.RichTextBox richTextClient;
        private System.Windows.Forms.RichTextBox fileContent;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.RichTextBox richTextServer;
    }
}