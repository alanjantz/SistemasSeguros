namespace FormApp
{
    partial class ScramblerForm
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
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblChave = new System.Windows.Forms.Label();
            this.lblDestino = new System.Windows.Forms.Label();
            this.btnEncryptedFile = new System.Windows.Forms.Button();
            this.txtEncryptedFile = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenFile
            // 
            this.OpenFile.FileName = "OpenFile";
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(15, 25);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(331, 20);
            this.txtFileName.TabIndex = 0;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(352, 24);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 1;
            this.btnOpenFile.Text = "Selecionar";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtKey
            // 
            this.txtKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKey.Location = new System.Drawing.Point(15, 75);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(331, 20);
            this.txtKey.TabIndex = 2;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 9);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(77, 13);
            this.lblFile.TabIndex = 3;
            this.lblFile.Text = "Arquivo origem";
            // 
            // lblChave
            // 
            this.lblChave.AutoSize = true;
            this.lblChave.Location = new System.Drawing.Point(12, 59);
            this.lblChave.Name = "lblChave";
            this.lblChave.Size = new System.Drawing.Size(38, 13);
            this.lblChave.TabIndex = 4;
            this.lblChave.Text = "Chave";
            // 
            // lblDestino
            // 
            this.lblDestino.AutoSize = true;
            this.lblDestino.Location = new System.Drawing.Point(12, 108);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(80, 13);
            this.lblDestino.TabIndex = 7;
            this.lblDestino.Text = "Arquivo destino";
            // 
            // btnEncryptedFile
            // 
            this.btnEncryptedFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEncryptedFile.Location = new System.Drawing.Point(352, 123);
            this.btnEncryptedFile.Name = "btnEncryptedFile";
            this.btnEncryptedFile.Size = new System.Drawing.Size(75, 23);
            this.btnEncryptedFile.TabIndex = 4;
            this.btnEncryptedFile.Text = "Selecionar";
            this.btnEncryptedFile.UseVisualStyleBackColor = true;
            this.btnEncryptedFile.Click += new System.EventHandler(this.btnEncryptedFile_Click);
            // 
            // txtEncryptedFile
            // 
            this.txtEncryptedFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEncryptedFile.Location = new System.Drawing.Point(15, 124);
            this.txtEncryptedFile.Name = "txtEncryptedFile";
            this.txtEncryptedFile.Size = new System.Drawing.Size(331, 20);
            this.txtEncryptedFile.TabIndex = 3;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEncrypt.Location = new System.Drawing.Point(14, 163);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 5;
            this.btnEncrypt.Text = "Cifrar";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // ScramblerForm
            // 
            this.AcceptButton = this.btnEncrypt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 200);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.lblDestino);
            this.Controls.Add(this.btnEncryptedFile);
            this.Controls.Add(this.txtEncryptedFile);
            this.Controls.Add(this.lblChave);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.txtFileName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ScramblerForm";
            this.Text = "Scrambler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OpenFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lblChave;
        private System.Windows.Forms.Label lblDestino;
        private System.Windows.Forms.Button btnEncryptedFile;
        private System.Windows.Forms.TextBox txtEncryptedFile;
        private System.Windows.Forms.Button btnEncrypt;
    }
}